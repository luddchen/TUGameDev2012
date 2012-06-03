
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing objects with physical behavior
    /// </summary>
    public class PhysicObject : FarseerPhysics.Dynamics.Body , IBody
    {
        private Texture2D texture;


        public bool IsVisible
        {
            get;
            set;
        }

        public SpriteEffects Effect
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set {
                texture = value;
                Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            }
        }

        public float Width
        {
            get;
            set;
        }

        public float Height
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public Vector2 Origin
        {
            get;
            set;
        }

        public Layer Layer
        {
            get;
            set;
        }

        public PhysicObject(World world)
            : base(world)
        {
            //this.Effect = SpriteEffects.None;
        }

    }
}
