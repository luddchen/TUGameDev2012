﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Objects;
using Robuddies.Interfaces;

namespace Robuddies.Objects
{
    class Switch : GameObject
    {
        private ISwitchable _switchable;
        private bool _isRevertable;
        private bool _isActivated;
        private Robot _player;

        public Switch(ISwitchable switchable, Robot player, Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            Color = Color.Red;
            Scale = 0.03f;
            _player = player;
            _player.Budi.Activate += Activate;
            _player.BudBudi.Activate += Activate;
            _switchable = switchable;
            _isRevertable = false;
            _isActivated = false;
        }

        public Switch(ISwitchable switchable, Robot player, Texture2D texture, Vector2 pos, bool isRevertable)
            : this(switchable, player, texture, pos)
        {
            _isRevertable = isRevertable;
        }

        private void init()
        {
        }

        public ISwitchable Switchable
        {
            get { return _switchable; }
            set { _switchable = value; }
        }

        public bool IsRevertable
        {
            get { return _isRevertable; }
            set { _isRevertable = value; }
        }

        public bool IsActivated
        {
            get { return _isActivated; }
            set { _isActivated = value; }
        }

        private void Activate(object sender, EventArgs e)
        {
            Console.WriteLine(Vector2.Distance(Position, _player.ActivePart.Physics.Body.Position) + "");
            Console.WriteLine("Pos Player: " + _player.ActivePart.Physics.Body.Position);
            Console.WriteLine("Pos Switch: " + Position);

            if (Vector2.Distance(Position, _player.ActivePart.Physics.Body.Position + new Vector2(_player.ActivePart.Physics.Height / 20, _player.ActivePart.Physics.Width / 20)) < 20)
            {
                Console.WriteLine("Switch activate");

                if (_isActivated && !_isRevertable)
                {
                    return;
                }

                if (!_isActivated)
                {
                    this.Color = Color.Green;
                    _switchable.activate();
                    _isActivated = true;
                }
            }
        }
    }
}
