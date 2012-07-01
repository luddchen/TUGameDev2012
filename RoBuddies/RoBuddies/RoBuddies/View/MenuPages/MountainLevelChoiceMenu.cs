using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using RoBuddies.Model.Worlds.MountainLevel;

namespace RoBuddies.View.MenuPages
{
    class MountainLevelChoiceMenu : LevelMainMenu
    {
        private HUDString levelChoose;
        private HUDString mountain1;
        private HUDString mountain2;
        private HUDString mountain3;
        private HUDString mountain4;
        private HUDString mountain5;
        private HUDString mountain6;
        private HUDString mountain7;
        private HUDString mountain8;
        private HUDString mountain9;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null)
            {
                levelChoose.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.15f);

                mountain1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f);
                mountain2.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.35f);
                mountain3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.35f);

                mountain4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.60f);
                mountain5.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.60f);
                mountain6.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.60f);

                mountain7.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.85f);
                mountain8.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.85f);
                mountain9.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.85f);
            }
        }

        public MountainLevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a Mountain level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            mountain1 = new HUDString("-- 1 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain1, true);

            mountain2 = new HUDString("-- 2 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain2, true);

            mountain3 = new HUDString("-- 3 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain3, true);


            addChoiceLine();

            mountain4 = new HUDString("-- 4 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain4, true);

            mountain5 = new HUDString("-- 5 --", null, null, textColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain5, true);

            mountain6 = new HUDString("-- 6 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain6, true);


            addChoiceLine();

            mountain7 = new HUDString("-- 7 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain7, true);

            mountain8 = new HUDString("-- 8 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain8, true);

            mountain9 = new HUDString("-- 9 --", null, null, notUsableColor, choiceBackgroundColor, 0.7f, null, content);
            addChoiceElement(mountain9, true);


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
                    if (this.ActiveElement == mountain1)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_1(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain2)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_2(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain3)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Mountain_3(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain4)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level2_1(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain5)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level2_5(this.Menu.Game).Level, gameTime);
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
