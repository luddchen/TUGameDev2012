using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies.View.MenuPages
{
    class InfoMenu : LevelMainMenu
    {
        private HUDString info;
        private HUDString info2;
        private HUDString chris;
        private HUDString eeva;
        private HUDString ludwig;
        private HUDString thomas;
        private HUDString xi;

        public HUDMenuPage quitPage { get; set; }
        public HUDMenuPage optionPage { get; set; }

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (info != null) { info.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.1f); }
            if (info2 != null) { info2.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.2f); }
            if (eeva != null) { eeva.Position = new Vector2(this.Viewport.Width * 0.3f, this.Viewport.Height * 0.45f); }
            if (xi != null) { xi.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.45f); }
            if (chris != null) { chris.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.65f); }
            if (ludwig != null) { ludwig.Position = new Vector2(this.Viewport.Width * 0.3f, this.Viewport.Height * 0.85f); }
            if (thomas != null) { thomas.Position = new Vector2(this.Viewport.Width * 0.7f, this.Viewport.Height * 0.85f); }
        }

        public InfoMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            info = new HUDString("2012 TU Berlin", null, null, Color.SkyBlue, null, 0.5f, 0, content);
            info2 = new HUDString("development team", null, null, Color.SkyBlue, null, 0.5f, 0, content);
            chris = new HUDString("Christopher Sierigk", null, null, Color.CadetBlue, null, 0.7f, -0.0f, content);
            eeva = new HUDString("Eeva Erkko", null, null, Color.Aqua, null, 0.7f, 0.1f, content);
            ludwig = new HUDString("Ludwig Ringler", null, null, Color.Aquamarine, null, 0.7f, -0.1f, content);
            thomas = new HUDString("Thomas Weber", null, null, Color.MediumAquamarine, null, 0.7f, 0.1f, content);
            xi = new HUDString("Xi Wang", null, null, Color.MediumOrchid, null, 0.7f, -0.1f, content);
            this.AllElements.Add(info);
            this.AllElements.Add(info2);
            this.AllElements.Add(chris);
            this.AllElements.Add(eeva);
            this.AllElements.Add(ludwig);
            this.AllElements.Add(thomas);
            this.AllElements.Add(xi);
            OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            //if (ButtonPressed(ControlButton.enter))
            //{
            //    if (this.ActiveElement != null)
            //    {
            //    }
            //}

            //// Key.down -----------------------------------------------------------------------------
            //if (ButtonIsDown(ControlButton.down))
            //{
            //    if (info.Position.Y > viewport.Height - info.Height / 2)
            //    {
            //        info.Position = new Vector2(info.Position.X, info.Position.Y - 2);
            //    }
            //} // ------------------------------------------------------------------------------------

            //// Key.Up -------------------------------------------------------------------------------
            //if (ButtonIsDown(ControlButton.up))
            //{
            //    if (info.Position.Y < info.Height / 2)
            //    {
            //        info.Position = new Vector2(info.Position.X, info.Position.Y + 2);
            //    }
            //} // -----------------------------------------------------------------------------------

        }

        public override void OnEnter()
        {
            base.OnEnter();
            chooseActiveElement(1, 3);
            this.Menu.makeTransparent(false);
        }

        public override void OnExit()
        {
            base.OnExit();
            this.Menu.makeTransparent(true);
        }
    }
}
