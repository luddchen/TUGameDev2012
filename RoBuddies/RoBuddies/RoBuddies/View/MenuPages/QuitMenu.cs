
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies.View.MenuPages
{
    class QuitMenu : LevelMainMenu
    {
        private HUDString question; 
        private HUDString yes;
        private HUDString no;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (question != null) { question.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.3f); }
            if (yes != null) { yes.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.6f); }
            if (no != null) { no.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.8f); }
        }

        public QuitMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            question = new HUDString("realy quit ?", null, null, textColor, null, 0.7f, null, content);
            this.AllElements.Add(question);

            addChoiceLine();

            yes = new HUDString("YES, i need a short break", null, null, textColor, null, 0.5f, null, content);
            addChoiceElement(yes, true);

            addChoiceLine();

            no = new HUDString("NO, bring me back to RoBuddies world", null, null, textColor, null, 0.6f, null, content);
            addChoiceElement(no, true);

            chooseActiveElement(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (ButtonPressed(ControlButton.enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == yes)
                    {
                        this.Menu.Game.Exit();
                    }

                    if (this.ActiveElement == no)
                    {
                        this.Menu.IsVisible = false;
                    }
                }
            }
        }

    }
}
