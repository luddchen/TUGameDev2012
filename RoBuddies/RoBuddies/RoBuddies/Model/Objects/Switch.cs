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

        public Switch(Vector2 pos, Vector2 size, Color color, Level level, Game game, ISwitchable switchPair, Robot robot)
            : base(level)
        {
            Texture2D switcherTex = game.Content.Load<Texture2D>("Sprites//switcher_r");
            defineTextures(switcherTex, switcherTex, switcherTex);
                
            switcherOnTex = game.Content.Load<Texture2D>("Sprites//switcher_l");

            this.Position = pos;
            this.Width = size.X;
            this.Height = size.Y;
            color.R /= 2; color.G /= 2; color.B /= 2;
            this.Color = color;
            this.switchObject = switchPair;
            this.player = robot;
            
            this.BodyType = BodyType.Static;
            this.Friction = 1f;

            this.CollisionCategories = Category.Cat1;
            this.CollidesWith = Category.None;
        }

        public void Activate()
        {
            if (!isActivated) //&& Vector2.Distance(Position, player.ActivePart.Position + new Vector2(player.ActivePart.Height / 20, player.ActivePart.Width / 20)) < 20)
            {
                this.Effect = SpriteEffects.FlipVertically;
                
                Color temp = this.Color;
                temp.R *= 2; temp.G *= 2; temp.B *= 2;
                this.Color = temp;
                
                isActivated = true;

                switchObject.switchOn();
            }
        }

    }
}
