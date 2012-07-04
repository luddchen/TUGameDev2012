using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;

namespace RoBuddies.View.MenuPages
{
    class LevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;

        private HUDString tutorial;
        private LevelMainMenu tutorialPage;

        private HUDString lab;
        private LevelMainMenu labPage;

        private HUDString mountain;
        private LevelMainMenu mountainPage;

        private HUDString hospital;
        private LevelMainMenu hospitalPage;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null) { levelChoose.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.15f); }

            if (tutorial != null) { tutorial.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.3f); }
            if (lab != null) { lab.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.5f); }
            if (mountain != null) { mountain.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.7f); }
            if (hospital != null) { hospital.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.9f); }
        }

        public LevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a level :", null, null, Color.SkyBlue, null, 0.6f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            tutorial = new HUDString("Tutorials", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(tutorial, true);

            addChoiceLine();

            lab = new HUDString("Easy World", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(lab, true);

            addChoiceLine();

            mountain = new HUDString("Head World", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(mountain, true);

            addChoiceLine();

            hospital = new HUDString("Advanced World", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(hospital, true);


            this.tutorialPage = new TutorialLevelChoiceMenu(menu, content);
            this.labPage = new World1LevelChoiceMenu(menu, content);
            this.mountainPage = new World2LevelChoiceMenu(menu, content);
            this.hospitalPage = new World3LevelChoiceMenu(menu, content);

            OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (ButtonPressed(ControlButton.enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == tutorial)
                    {
                        this.Menu.ActivePage = this.tutorialPage;
                    }

                    if (this.ActiveElement == lab)
                    {
                        this.Menu.ActivePage = this.labPage;
                    }

                    if (this.ActiveElement == mountain)
                    {
                        this.Menu.ActivePage = this.mountainPage;
                    }

                    if (this.ActiveElement == hospital)
                    {
                        this.Menu.ActivePage = this.hospitalPage;
                    }
                }
            }

        }

        public override void OnEnter()
        {
            base.OnEnter();
            chooseActiveElement(1, 1);
            this.Menu.makeTransparent(false);
        }

        public override void OnExit()
        {
            base.OnExit();
            this.Menu.makeTransparent(true);
        }

    }
}
