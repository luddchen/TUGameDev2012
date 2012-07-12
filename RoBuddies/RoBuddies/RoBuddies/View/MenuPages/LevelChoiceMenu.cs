using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using RoBuddies.Utilities;

namespace RoBuddies.View.MenuPages
{
    class LevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;

        private HUDString tutorial;
        private LevelMainMenu tutorialPage;

        private HUDString world1;
        private LevelMainMenu labPage;

        private HUDString world2;
        private LevelMainMenu mountainPage;

        private HUDString world3;
        private LevelMainMenu hospitalPage;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null) { levelChoose.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.15f); }

            if (tutorial != null) { tutorial.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.3f); }
            if (world1 != null) { world1.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.5f); }
            if (world2 != null) { world2.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.7f); }
            if (world3 != null) { world3.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.9f); }
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

            world1 = new HUDString("Begin Journey", null, null, notUsableColor, null, 0.7f, null, content);
            addChoiceElement(world1, true);

            addChoiceLine();

            //world2 = new HUDString("Head World", null, null, notUsableColor, null, 0.7f, null, content);
            //addChoiceElement(world2, true);

            //addChoiceLine();

            //world3 = new HUDString("Advanced World", null, null, notUsableColor, null, 0.7f, null, content);
            //addChoiceElement(world3, true);


            this.tutorialPage = new TutorialLevelChoiceMenu(menu, content);
            this.labPage = new World1LevelChoiceMenu(menu, content);
            this.mountainPage = new World2LevelChoiceMenu(menu, content);
            this.hospitalPage = new World3LevelChoiceMenu(menu, content);

            OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            loadedLevelIndex = SaveGameUtility.loadGame();
            UpdateLevelProgress();

            // Key.Enter -----------------------------------------------------------------------------
            if (ButtonPressed(ControlButton.enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == tutorial)
                    {
                        this.Menu.ActivePage = this.tutorialPage;
                    }

                    if (this.ActiveElement == world1 && loadedLevelIndex >= 7)
                    {
                        this.Menu.ActivePage = this.labPage;
                    }

                    //if (this.ActiveElement == world2 && loadedLevelIndex >= 16)
                    //{
                    //    this.Menu.ActivePage = this.mountainPage;
                    //}

                    //if (this.ActiveElement == world3 && loadedLevelIndex >= 25)
                    //{
                    //    this.Menu.ActivePage = this.hospitalPage;
                    //}
                }
            }

        }

        private void UpdateLevelProgress()
        {
            if (loadedLevelIndex >= 7)
            {
                world1.Color = textColor;
            }
            if (loadedLevelIndex >= 16)
            {
                world2.Color = textColor;
            }
            if (loadedLevelIndex >= 25)
            {
                world3.Color = textColor;
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
