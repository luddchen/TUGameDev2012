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
        private HUDString tutorial1;
        private HUDString tutorial2;
        private HUDString tutorial3;
        private HUDString tutorial4;

        private HUDString lab1;
        private HUDString lab2;
        private HUDString lab3;
        private HUDString lab4;

        private HUDString mountain1;
        private HUDString mountain2;
        private HUDString mountain3;
        private HUDString mountain4;

        private HUDString hospital1;
        private HUDString hospital2;
        private HUDString hospital3;
        private HUDString hospital4;

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (levelChoose != null) { levelChoose.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.35f); }

            if (tutorial1 != null) { tutorial1.Position = new Vector2(this.Viewport.Width * 0.12f, this.Viewport.Height * 0.5f); }
            if (tutorial2 != null) { tutorial2.Position = new Vector2(this.Viewport.Width * 0.12f, this.Viewport.Height * 0.62f); }
            if (tutorial3 != null) { tutorial3.Position = new Vector2(this.Viewport.Width * 0.12f, this.Viewport.Height * 0.74f); }
            if (tutorial4 != null) { tutorial4.Position = new Vector2(this.Viewport.Width * 0.12f, this.Viewport.Height * 0.86f); }

            if (lab1 != null) { lab1.Position = new Vector2(this.Viewport.Width * 0.35f, this.Viewport.Height * 0.5f); }
            if (lab2 != null) { lab2.Position = new Vector2(this.Viewport.Width * 0.35f, this.Viewport.Height * 0.62f); }
            if (lab3 != null) { lab3.Position = new Vector2(this.Viewport.Width * 0.35f, this.Viewport.Height * 0.74f); }
            if (lab4 != null) { lab4.Position = new Vector2(this.Viewport.Width * 0.35f, this.Viewport.Height * 0.86f); }

            if (mountain1 != null) { mountain1.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.5f); }
            if (mountain2 != null) { mountain2.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.62f); }
            if (mountain3 != null) { mountain3.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.74f); }
            if (mountain4 != null) { mountain4.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.86f); }

            if (hospital1 != null) { hospital1.Position = new Vector2(this.Viewport.Width * 0.87f, this.Viewport.Height * 0.5f); }
            if (hospital2 != null) { hospital2.Position = new Vector2(this.Viewport.Width * 0.87f, this.Viewport.Height * 0.62f); }
            if (hospital3 != null) { hospital3.Position = new Vector2(this.Viewport.Width * 0.87f, this.Viewport.Height * 0.74f); }
            if (hospital4 != null) { hospital4.Position = new Vector2(this.Viewport.Width * 0.87f, this.Viewport.Height * 0.86f); }
        }

        public LevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            levelChoose = new HUDString("choose a level :", null, null, Color.SkyBlue, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            addChoiceLine();

            tutorial1 = new HUDString("Tutorial 1", null, null, textColor, null, 0.5f, null, content);
            addChoiceElement(tutorial1, true);

            lab1 = new HUDString("Lab 1", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(lab1, true);

            mountain1 = new HUDString("Mountain 1", null, null, textColor, null, 0.5f, null, content);
            addChoiceElement(mountain1, true);

            hospital1 = new HUDString("Hospital 1", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(hospital1, true);


            addChoiceLine();

            tutorial2 = new HUDString("Tutorial 2", null, null, textColor, null, 0.5f, null, content);
            addChoiceElement(tutorial2, true);

            lab2 = new HUDString("Lab 2", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(lab2, true);

            mountain2 = new HUDString("Mountain 2", null, null, textColor, null, 0.5f, null, content);
            addChoiceElement(mountain2, true);

            hospital2 = new HUDString("Hospital 2", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(hospital2, true);


            addChoiceLine();

            tutorial3 = new HUDString("Tutorial 3", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(tutorial3, true);

            lab3 = new HUDString("Lab 3", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(lab3, true);

            mountain3 = new HUDString("Mountain 3", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(mountain3, true);

            hospital3 = new HUDString("Hospital 3", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(hospital3, true);


            addChoiceLine();

            tutorial4 = new HUDString("Tutorial 4", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(tutorial4, true);

            lab4 = new HUDString("Lab 4", null, null, Color.Black, null, 0.45f, null, content);
            addChoiceElement(lab4, true);

            mountain4 = new HUDString("Mountain 4", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(mountain4, true);

            hospital4 = new HUDString("Hospital 4", null, null, Color.Black, null, 0.5f, null, content);
            addChoiceElement(hospital4, true);

            chooseActiveElement(1, 0);
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
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_1(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial2)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_3(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain1)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level2_5(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain2)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level2_1(this.Menu.Game).Level, gameTime);
                        this.Menu.IsVisible = false;
                    }
                }
            }

        }

    }
}
