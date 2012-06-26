using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;

namespace RoBuddies.View.MenuPages
{
    class LabLevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;
        private HUDString lab1;
        private HUDString lab2;
        private HUDString lab3;
        private HUDString lab4;
        private HUDString lab5;
        private HUDString lab6;
        private HUDString lab7;
        private HUDString lab8;
        private HUDString lab9;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null)
            {
                levelChoose.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.15f);

                lab1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f);
                lab2.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.35f);
                lab3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.35f);

                lab4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.60f);
                lab5.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.60f);
                lab6.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.60f);

                lab7.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.85f);
                lab8.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.85f);
                lab9.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.85f);
            }
        }

        public LabLevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a Laboratory level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            lab1 = new HUDString("-- 1 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab1, true);

            lab2 = new HUDString("-- 2 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab2, true);

            lab3 = new HUDString("-- 3 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab3, true);


            addChoiceLine();

            lab4 = new HUDString("-- 4 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab4, true);

            lab5 = new HUDString("-- 5 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab5, true);

            lab6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab6, true);


            addChoiceLine();

            lab7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab7, true);

            lab8 = new HUDString("-- 8 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab8, true);

            lab9 = new HUDString("-- 9 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(lab9, true);


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
                    //if (this.ActiveElement == lab1)
                    //{
                    //    ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_1(this.Menu.Game).Level, gameTime);
                    //    this.Menu.IsVisible = false;
                    //}
                    //if (this.ActiveElement == lab2)
                    //{
                    //    ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_3(this.Menu.Game).Level, gameTime);
                    //    this.Menu.IsVisible = false;
                    //}
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
