using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Robuddies.Interfaces;

namespace Robuddies.Objects
{
    class Switch : GameObject
    {
        private ISwitchable _switchable;
        private bool _isRevertable;
        private bool _isActivated;

        public Switch(ISwitchable switchable, Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            _switchable = switchable;
            _isRevertable = false;
            _isActivated = false;
        }

        public Switch(ISwitchable switchable, Texture2D texture, Vector2 pos, bool isRevertable)
            : base (texture, pos)
        {
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

        public void Activate()
        {
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
