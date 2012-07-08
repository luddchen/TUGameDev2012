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
    class World1LevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;
        private HUDString world1_1;
        private HUDString world1_2;
        private HUDString world1_3;
        private HUDString world1_4;
        private HUDString world1_5;
        private HUDString world1_6;
        private HUDString world1_7;
        private HUDString world1_8;
        private HUDString world1_9;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null)
            {
                levelChoose.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.15f);

                world1_1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f);
                world1_2.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.35f);
                world1_3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.35f);

                world1_4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.60f);
                world1_5.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.60f);
                world1_6.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.60f);

                world1_7.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.85f);
                world1_8.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.85f);
                world1_9.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.85f);
            }
        }

        public World1LevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a World 1 level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            world1_1 = new HUDString("-- 1 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_1, true);

            world1_2 = new HUDString("-- 2 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_2, true);

            world1_3 = new HUDString("-- 3 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_3, true);


            addChoiceLine();

            world1_4 = new HUDString("-- 4 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_4, true);

            world1_5 = new HUDString("-- 5 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_5, true);

            world1_6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_6, true);


            addChoiceLine();

            world1_7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_7, true);

            world1_8 = new HUDString("-- 8 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_8, true);

            world1_9 = new HUDString("-- 9 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(world1_9, true);


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
                    if (this.ActiveElement == world1_1 && loadedLevelIndex >= 7)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_1(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_2 && loadedLevelIndex >= 8)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_2(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_3 && loadedLevelIndex >= 9)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_3(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_4 && loadedLevelIndex >= 10)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_4(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_5 && loadedLevelIndex >= 11)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_5(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_6 && loadedLevelIndex >= 12)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_6(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_7 && loadedLevelIndex >= 13)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_7(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_8 && loadedLevelIndex >= 14)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_8(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == world1_9 && loadedLevelIndex >= 15)
                    {

                    }
                }
            }

        }

        private void UpdateLevelProgress()
        {
            if (loadedLevelIndex >= 7)
            {
                world1_1.Color = textColor;
            }
            if (loadedLevelIndex >= 8)
            {
                world1_2.Color = textColor;
            }
            if (loadedLevelIndex >= 9)
            {
                world1_3.Color = textColor;
            }
            if (loadedLevelIndex >= 10)
            {
                world1_4.Color = textColor;
            }
            if (loadedLevelIndex >= 11)
            {
                world1_5.Color = textColor;
            }
            if (loadedLevelIndex >= 12)
            {
                world1_6.Color = textColor;
            }
            if (loadedLevelIndex >= 13)
            {
                world1_7.Color = textColor;
            }
            if (loadedLevelIndex >= 14)
            {
                world1_8.Color = textColor;
            }
            if (loadedLevelIndex >= 15)
            {
                world1_9.Color = textColor;
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
