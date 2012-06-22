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
        private Texture2D switcherOnTex;
        private Texture2D switcherOffTex;
        private Color realColor;

        public Switch(Vector2 pos, Vector2 size, Color color, Level level, Game game, ISwitchable switchPair, Robot robot)
            : base(pos, size, color, level)
        {
            switcherOnTex = game.Content.Load<Texture2D>("Sprites//switcher_l");
            switcherOffTex = game.Content.Load<Texture2D>("Sprites//switcher_r");

            this.realColor = color;
            this.switchObject = switchPair;

            IsActivated = false;
        }

        public void Activate()
        {
            if (!isActivated) //&& Vector2.Distance(Position, player.ActivePart.Position + new Vector2(player.ActivePart.Height / 20, player.ActivePart.Width / 20)) < 20)
            {
                IsActivated = true;
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
