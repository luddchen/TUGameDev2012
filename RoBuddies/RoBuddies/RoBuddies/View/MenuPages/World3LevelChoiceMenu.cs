using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using RoBuddies.Utilities;

namespace RoBuddies.View.MenuPages
{
    class World3LevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;
        private HUDString world3_1;
        private HUDString world3_2;
        private HUDString world3_3;
        private HUDString world3_4;
        private HUDString world3_5;
        private HUDString world3_6;
        private HUDString world3_7;
        private HUDString world3_8;
        private HUDString world3_9;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null)
            {
                levelChoose.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.15f);

                world3_1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f);
                world3_2.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.35f);
                world3_3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.35f);

                world3_4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.60f);
                world3_5.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.60f);
                world3_6.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.60f);

                world3_7.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.85f);
                world3_8.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.85f);
                world3_9.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.85f);
            }
        }

        public World3LevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a World 3 level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            world3_1 = new HUDString("-- 1 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_1, true);

            world3_2 = new HUDString("-- 2 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_2, true);

            world3_3 = new HUDString("-- 3 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_3, true);


            addChoiceLine();

            world3_4 = new HUDString("-- 4 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_4, true);

            world3_5 = new HUDString("-- 5 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_5, true);

            world3_6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_6, true);


            addChoiceLine();

            world3_7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_7, true);

            world3_8 = new HUDString("-- 8 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_8, true);

            world3_9 = new HUDString("-- 9 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world3_9, true);


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
                if (this.ActiveElement == world3_1 && loadedLevelIndex >= 25)
                {

                }
                if (this.ActiveElement == world3_2 && loadedLevelIndex >= 26)
                {

                }
                if (this.ActiveElement == world3_3 && loadedLevelIndex >= 27)
                {

                }
                if (this.ActiveElement == world3_4 && loadedLevelIndex >= 28)
                {

                }
                if (this.ActiveElement == world3_5 && loadedLevelIndex >= 29)
                {

                }
                if (this.ActiveElement == world3_6 && loadedLevelIndex >= 30)
                {

                }
                if (this.ActiveElement == world3_7 && loadedLevelIndex >= 31)
                {

                }
                if (this.ActiveElement == world3_8 && loadedLevelIndex >= 32)
                {

                }
                if (this.ActiveElement == world3_9 && loadedLevelIndex >= 33)
                {

                }
            }

        }

        private void UpdateLevelProgress()
        {
            if (loadedLevelIndex >= 25)
            {
                world3_1.Color = textColor;
            }
            if (loadedLevelIndex >= 26)
            {
                world3_2.Color = textColor;
            }
            if (loadedLevelIndex >= 27)
            {
                world3_3.Color = textColor;
            }
            if (loadedLevelIndex >= 28)
            {
                world3_4.Color = textColor;
            }
            if (loadedLevelIndex >= 29)
            {
                world3_5.Color = textColor;
            }
            if (loadedLevelIndex >= 30)
            {
                world3_6.Color = textColor;
            }
            if (loadedLevelIndex >= 31)
            {
                world3_7.Color = textColor;
            }
            if (loadedLevelIndex >= 32)
            {
                world3_8.Color = textColor;
            }
            if (loadedLevelIndex >= 33)
            {
                world3_9.Color = textColor;
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            chooseActiveElement(2, 0);
            this.Menu.makeTransparent(false);
        }

        public override void OnExit()
        {
            base.OnExit();
            this.Menu.makeTransparent(true);
        }

    }
}
