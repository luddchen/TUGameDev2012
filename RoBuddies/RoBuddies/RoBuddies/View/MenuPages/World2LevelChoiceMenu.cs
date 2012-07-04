using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using RoBuddies.Model.Worlds.MountainLevel;
using RoBuddies.Utilities;

namespace RoBuddies.View.MenuPages
{
    class World2LevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;
        private HUDString world2_1;
        private HUDString world2_2;
        private HUDString world2_3;
        private HUDString world2_4;
        private HUDString world2_5;
        private HUDString world2_6;
        private HUDString world2_7;
        private HUDString world2_8;
        private HUDString world2_9;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null)
            {
                levelChoose.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.15f);

                world2_1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f);
                world2_2.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.35f);
                world2_3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.35f);

                world2_4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.60f);
                world2_5.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.60f);
                world2_6.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.60f);

                world2_7.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.85f);
                world2_8.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.85f);
                world2_9.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.85f);
            }
        }

        public World2LevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a World 2 level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            world2_1 = new HUDString("-- 1 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_1, true);

            world2_2 = new HUDString("-- 2 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_2, true);

            world2_3 = new HUDString("-- 3 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_3, true);


            addChoiceLine();

            world2_4 = new HUDString("-- 4 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_4, true);

            world2_5 = new HUDString("-- 5 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_5, true);

            world2_6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_6, true);


            addChoiceLine();

            world2_7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_7, true);

            world2_8 = new HUDString("-- 8 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_8, true);

            world2_9 = new HUDString("-- 9 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world2_9, true);


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
                if (this.ActiveElement == world2_1 && loadedLevelIndex >= 16)
                {

                }
                if (this.ActiveElement == world2_2 && loadedLevelIndex >= 17)
                {

                }
                if (this.ActiveElement == world2_3 && loadedLevelIndex >= 18)
                {

                }
                if (this.ActiveElement == world2_4 && loadedLevelIndex >= 19)
                {

                }
                if (this.ActiveElement == world2_5 && loadedLevelIndex >= 20)
                {

                }
                if (this.ActiveElement == world2_6 && loadedLevelIndex >= 21)
                {

                }
                if (this.ActiveElement == world2_7 && loadedLevelIndex >= 22)
                {

                }
                if (this.ActiveElement == world2_8 && loadedLevelIndex >= 23)
                {

                }
                if (this.ActiveElement == world2_9 && loadedLevelIndex >= 24)
                {

                }
            }

        }

        private void UpdateLevelProgress()
        {
            if (loadedLevelIndex >= 16)
            {
                world2_1.Color = textColor;
            }
            if (loadedLevelIndex >= 17)
            {
                world2_2.Color = textColor;
            }
            if (loadedLevelIndex >= 18)
            {
                world2_3.Color = textColor;
            }
            if (loadedLevelIndex >= 19)
            {
                world2_4.Color = textColor;
            }
            if (loadedLevelIndex >= 20)
            {
                world2_5.Color = textColor;
            }
            if (loadedLevelIndex >= 21)
            {
                world2_6.Color = textColor;
            }
            if (loadedLevelIndex >= 22)
            {
                world2_7.Color = textColor;
            }
            if (loadedLevelIndex >= 23)
            {
                world2_8.Color = textColor;
            }
            if (loadedLevelIndex >= 24)
            {
                world2_9.Color = textColor;
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
