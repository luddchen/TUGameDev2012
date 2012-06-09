using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    class EditorHUD : HUD.HUD
    {

        private int HUD_height = 30;
        private int HUD_width = 100;

        private EditorView editorView;
        private HUDString xMouse;
        private HUDString yMouse;
        private HUDString seperator;

        public override void OnViewPortResize()
        {
            this.viewport.Y = -1 + this.viewport.Height - this.HUD_height;
            this.viewport.X = this.viewport.Width - (this.HUD_width + 1);
            this.viewport.Height = this.HUD_height;
            this.viewport.Width = this.HUD_width;
            this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);

            if (this.seperator != null) { this.seperator.Position = new Vector2(this.viewport.Width / 2, this.viewport.Height / 2); }
            if (this.xMouse != null) { this.xMouse.Position = new Vector2(this.viewport.Width * 0.3f, this.viewport.Height / 2); }
            if (this.yMouse != null) { this.yMouse.Position = new Vector2(this.viewport.Width * 0.7f, this.viewport.Height / 2); }
        
        }

        public EditorHUD(EditorView editorView)
            : base(editorView.Game)
        {
            SpriteFont font = editorView.Game.Content.Load<SpriteFont>("Fonts\\Font");
            this.editorView = editorView;
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");
            this.backgroundColor = new Color(0, 0, 0, 160);

            seperator = new HUDString(":", font, null, null, null, 0.5f, null, this.Game.Content);
            this.AllElements.Add(seperator);
            xMouse = new HUDString("", font, null, null, null, 0.5f, null, this.Game.Content);
            this.AllElements.Add(xMouse);
            yMouse = new HUDString("", font, null, null, null, 0.5f, null, this.Game.Content);
            this.AllElements.Add(yMouse);
        }

        public void Update(GameTime gameTime, Vector2 mousePos)
        {
            base.Update(gameTime);
            this.xMouse.String = (int)Math.Round(mousePos.X) + "";
            this.yMouse.String = (int)Math.Round(mousePos.Y) + "";
        }

    }
}