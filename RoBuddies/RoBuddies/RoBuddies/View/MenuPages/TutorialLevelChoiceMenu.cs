using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.Tutorial;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;

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
        private HUDString tutorial8;
        private HUDString tutorial9;

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
                tutorial8.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.85f);
                tutorial9.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.85f);
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

            tutorial2 = new HUDString("-- 2 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial2, true);

            tutorial3 = new HUDString("-- 3 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial3, true);


            addChoiceLine();

            tutorial4 = new HUDString("-- 4 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial4, true);

            tutorial5 = new HUDString("-- 5 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial5, true);

            tutorial6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial6, true);


            addChoiceLine();

            tutorial7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial7, true);

            tutorial8 = new HUDString("-- 8 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial8, true);

            tutorial9 = new HUDString("-- 9 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(tutorial9, true);


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
                    if (this.ActiveElement == tutorial1)
                    {
                        //((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_1(this.Menu.Game).Level, gameTime);
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_1(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial2)
                    {
                        //((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_2(this.Menu.Game).Level, gameTime);
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_2(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial3)
                    {
                        //((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_3(this.Menu.Game).Level, gameTime);
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_3(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial4)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Tutorial_4(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                }
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
