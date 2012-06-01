using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Objects
{
    public class Switch : PhysicObject
    {
        public Switch(Vector2 pos, Vector2 size, Color color, Texture2D texture, World world)
            : base(world)
        {
            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.Color = color;
            this.Texture = texture;
            this.BodyType = BodyType.Static;
            this.Friction = 10f; //not know exactly

//            Scale = 0.03f;
//           _player = player;
//            _player.Budi.Activate += Activate;
//            _player.BudBudi.Activate += Activate;
//            _switchable = switchable;
//            _isRevertable = false;
//            _isActivated = false;
        }
    }
}
