
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.View.HUD;

namespace RoBuddies.View.MenuPages
{
    class GameMenu : HUDMenuPage
    {
        private HUDString editor;
        private HUDString makeSnapshot;
        private HUDString restoreSnapshot;

        public override void OnViewPortResize()
        {
            if (editor != null) { editor.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.3f); }
            if (makeSnapshot != null) { makeSnapshot.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.6f); }
            if (restoreSnapshot != null) { restoreSnapshot.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.8f); }
        }

        public GameMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");

            editor = new HUDString("Editor", content);
            editor.Scale = 0.7f;
            this.AllElements.Add(editor);
            this.ChoiceList.Add(editor);

            makeSnapshot = new HUDString("do a Snapshot", content);
            makeSnapshot.Scale = 0.7f;
            this.AllElements.Add(makeSnapshot);
            this.ChoiceList.Add(makeSnapshot);

            restoreSnapshot = new HUDString("restore Snapshot", content);
            restoreSnapshot.Scale = 0.7f;
            this.AllElements.Add(restoreSnapshot);
            this.ChoiceList.Add(restoreSnapshot);

            this.ActiveElement = editor;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == editor)
                    {
                        this.Game.SwitchToViewMode(RoBuddies.ViewMode.Editor);
                    }

                    if (this.ActiveElement == makeSnapshot)
                    {
                        ((LevelView)((RoBuddies)this.Game).View).Level.ClearForces();
                        ((LevelView)((RoBuddies)this.Game).View).SnapShot.MakeSnapshot();
                        ((LevelView)((RoBuddies)this.Game).View).Level.ClearForces();
                        this.Menu.IsVisible = false;
                   }

                    if (this.ActiveElement == restoreSnapshot)
                    {
                        ((LevelView)((RoBuddies)this.Game).View).Level.ClearForces();
                        ((LevelView)((RoBuddies)this.Game).View).SnapShot.Rewind(1);
                        ((LevelView)((RoBuddies)this.Game).View).Level.ClearForces();
                        this.Menu.IsVisible = false;
                    }
                }
            }
        }

    }
}
