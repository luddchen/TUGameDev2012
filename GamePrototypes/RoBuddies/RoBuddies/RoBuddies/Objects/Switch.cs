using System;
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
            _player.ActivePart.Activate += Activate;
            _switchable = switchable;
            _isRevertable = false;
            _isActivated = false;
        }

        public Switch(ISwitchable switchable, Robot player, Texture2D texture, Vector2 pos, bool isRevertable)
            : base (texture, pos)
        {
            _player = player;
            _player.ActivePart.Activate += Activate;
            _switchable = switchable;
            _isRevertable = isRevertable;
            _isActivated = false;
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

            if (Vector2.Distance(Position, _player.ActivePart.Physics.Body.Position) < 15)
            {
                Console.WriteLine("Switch activate");

                if (_isActivated && !_isRevertable)
                {
                    return;
                }

                if (!_isActivated)
                {
                    _switchable.activate();
                    _isActivated = true;
                }
            }
        }
    }
}
