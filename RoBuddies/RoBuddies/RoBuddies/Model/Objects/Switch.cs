using FarseerPhysics.Dynamics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Robuddies.Interfaces;

namespace RoBuddies.Model.Objects
{
    class Switch : PhysicObject
    {
        private bool isActivated = false;
        private ISwitchable switchObject;
        private Robot player;
        private Texture2D switcherOnTex;
        private Texture2D switcherOffTex;
        private Color realColor;

        public Switch(Vector2 pos, Vector2 size, Color color, Level level, Game game, ISwitchable switchPair, Robot robot)
            : base(level)
        {
            switcherOnTex = game.Content.Load<Texture2D>("Sprites//switcher_l");
            switcherOffTex = game.Content.Load<Texture2D>("Sprites//switcher_r");

            this.realColor = color;
            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            this.switchObject = switchPair;
            this.player = robot;
            
            this.BodyType = BodyType.Static;
            this.Friction = 1f;

            this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;

            IsActivated = false;
        }

        public void Activate()
        {
            if (!isActivated) //&& Vector2.Distance(Position, player.ActivePart.Position + new Vector2(player.ActivePart.Height / 20, player.ActivePart.Width / 20)) < 20)
            {
                //this.Effect = SpriteEffects.FlipVertically;
                
                IsActivated = true;
                this.Color = new Color(160, 160, 160, 160);
                switchObject.switchOn();
            }
        }

        // need this for rewind feature
        public bool IsActivated
        {
            get { return isActivated; }
            set
            {
                isActivated = value;

                if (value)
                {
                    this.Color = this.realColor;
                    defineTextures(switcherOnTex, switcherOnTex, switcherOnTex);
                }
                if (!value)
                {
                    this.Color = new Color(this.realColor.R / 2, this.realColor.G / 2, this.realColor.B / 2, 255);
                    defineTextures(switcherOffTex, switcherOffTex, switcherOffTex);
                }
            }
        }

    }
}
