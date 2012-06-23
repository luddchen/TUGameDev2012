
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies.View.MenuPages
{
    class QuitMenu : HUDMenuPage
    {
        private HUDString question; 
        private HUDString yes;
        private HUDString no;

        public override void OnViewPortResize()
        {
            if (question != null) { question.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.3f); }
            if (yes != null) { yes.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.6f); }
            if (no != null) { no.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.8f); }
        }

        public QuitMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");

            question = new HUDString("realy quit ?", content);
            question.Scale = 0.7f;
            this.AllElements.Add(question);

            yes = new HUDString("YES, i need a short break", content);
            yes.Scale = 0.5f;
            this.AllElements.Add(yes);
            this.ChoiceList.Add(yes);

            no= new HUDString("NO, bring me back to RoBuddies world", content);
            no.Scale = 0.7f;
            this.AllElements.Add(no);
            this.ChoiceList.Add(no);

            this.ActiveElement = no;
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
