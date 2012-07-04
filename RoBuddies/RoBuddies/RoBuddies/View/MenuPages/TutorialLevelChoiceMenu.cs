using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.Tutorial;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using RoBuddies.Model.Worlds;
using RoBuddies.Utilities;
using System;

namespace RoBuddies.View.MenuPages
{
    class TutorialLevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;
        private HUDString tutorial1;
        private HUDString tutorial2;
        private HUDString tutorial3;
        private HUDString tutorial4;
        private HUDString tutorial5;
        private HUDString tutorial6;
        private HUDString tutorial7;

        Worlds Worlds
        {
            get { return ((LevelView)this.Menu.Game.LevelView).worlds; }
        }

        public override void OnViewPortResize()
        {
            base.OnViewPortResize(); 

            if (levelChoose != null) { 
                levelChoose.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.15f);

                tutorial1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f);
                tutorial2.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.35f);
                tutorial3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.35f);

                tutorial4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.60f);
                tutorial5.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.60f);
                tutorial6.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.60f);

                tutorial7.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.85f);
            }
        }

        public TutorialLevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a Tutorial level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            tutorial1 = new HUDString("-- 1 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial1, true);

            tutorial2 = new HUDString("-- 2 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial2, true);

            tutorial3 = new HUDString("-- 3 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial3, true);


            addChoiceLine();

            tutorial4 = new HUDString("-- 4 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial4, true);

            tutorial5 = new HUDString("-- 5 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial5, true);

            tutorial6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial6, true);


            addChoiceLine();

            tutorial7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial7, true);


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
                    if (this.ActiveElement == tutorial1)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_1(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial2 && loadedLevelIndex >= 1)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_2(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial3 && loadedLevelIndex >= 2)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_3(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial4 && loadedLevelIndex >= 3)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_4(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial5 && loadedLevelIndex >= 4)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_5(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial6 && loadedLevelIndex >= 5)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_6(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial7 && loadedLevelIndex >= 6)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_7(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                }
            }

        }

        private void UpdateLevelProgress()
        {
            if (loadedLevelIndex >= 1)
            {
                tutorial2.Color = textColor;
            }
            if (loadedLevelIndex >= 2)
            {
                tutorial3.Color = textColor;
            }
            if (loadedLevelIndex >= 3)
            {
                tutorial4.Color = textColor;
            }
            if (loadedLevelIndex >= 4)
            {
                tutorial5.Color = textColor;
            }
            if (loadedLevelIndex >= 5)
            {
                tutorial6.Color = textColor;
            }
            if (loadedLevelIndex >= 6)
            {
                tutorial7.Color = textColor;
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
