using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model
{

    /// <summary>
    /// representing objects with physical behavior
    /// </summary>
    class PhysicObject : FarseerPhysics.Dynamics.Body , IBody
    {
        private Texture2D texture;
        protected Level level;

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

        /// <summary>
        /// Defines the textures for the graphic themes
        /// </summary>
        /// <param name="roboLabTex">the texture for the robo lab theme</param>
        /// <param name="mountainTex">the texture for the mountain theme</param>
        /// <param name="mentalHospitalTex">the texture for the hospital theme</param>
        protected void defineTextures(Texture2D roboLabTex, Texture2D mountainTex, Texture2D mentalHospitalTex)
        {
            if (level.theme == LevelTheme.ROBO_LAB)
            {
                Texture = roboLabTex;
            }
            else if (level.theme == LevelTheme.MOUNTAIN)
            {
                Texture = mountainTex;
            }
            else if (level.theme == LevelTheme.MENTAL_HOSPITAL)
            {
                Texture = mentalHospitalTex;
            }
        }

        public PhysicObject(Level level)
            : base(level)
        {
            this.level = level;
            this.IsVisible = true;
            this.Color = Color.White;
            this.BodyType = BodyType.Static;
        }

        public PhysicObject(Vector2 position, Vector2 size, Color color, Level level)
            : base(level)
        {
            this.level = level;
            this.IsVisible = true;
            this.Color = color;
            this.Position = position;
            this.Width = size.X;
            this.Height = size.Y;
            this.BodyType = BodyType.Static;
        }

        public void createRectangleFixture()
        {
            createRectangleFixture(1f);
        }

        public void createRectangleFixture(float density)
        {
            foreach (Fixture fixture in this.FixtureList)
            {
                this.DestroyFixture(fixture);
            }
            FixtureFactory.AttachRectangle(this.Width, this.Height, density, Vector2.Zero, this);
        }

        public void setUncollidable()
        {
            this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;
        }
    }
}
