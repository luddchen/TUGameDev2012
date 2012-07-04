using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;

namespace RoBuddies.View.MenuPages
{
    class World3LevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;
        private HUDString hospital1;
        private HUDString hospital2;
        private HUDString hospital3;
        private HUDString hospital4;
        private HUDString hospital5;
        private HUDString hospital6;
        private HUDString hospital7;
        private HUDString hospital8;
        private HUDString hospital9;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null)
            {
                levelChoose.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.15f);

                hospital1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f);
                hospital2.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.35f);
                hospital3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.35f);

                hospital4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.60f);
                hospital5.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.60f);
                hospital6.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.60f);

                hospital7.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.85f);
                hospital8.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.85f);
                hospital9.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.85f);
            }
        }

        public World3LevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a World 3 level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            hospital1 = new HUDString("-- 1 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital1, true);

            hospital2 = new HUDString("-- 2 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital2, true);

            hospital3 = new HUDString("-- 3 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital3, true);


            addChoiceLine();

            hospital4 = new HUDString("-- 4 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital4, true);

            hospital5 = new HUDString("-- 5 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital5, true);

            hospital6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital6, true);


            addChoiceLine();

            hospital7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital7, true);

            hospital8 = new HUDString("-- 8 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital8, true);

            hospital9 = new HUDString("-- 9 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(hospital9, true);


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
                    //if (this.ActiveElement == hospital1)
                    //{
                    //    ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_1(this.Menu.Game).Level, gameTime);
                    //    this.Menu.IsVisible = false;
                    //}
                    //if (this.ActiveElement == hospital2)
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
