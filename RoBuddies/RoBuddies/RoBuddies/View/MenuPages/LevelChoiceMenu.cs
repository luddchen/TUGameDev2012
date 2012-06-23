using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model;
using RoBuddies.Model.Serializer;
using RoBuddies.Model.Worlds.World1;
using RoBuddies.Model.Worlds.World2;
using RoBuddies.Model.Worlds.World3;

namespace RoBuddies.View.MenuPages
{
    class LevelChoiceMenu : HUDMenuPage
    {
        private Color choiceBackground = new Color(32, 32, 32, 64);

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


        private HUDTexture editor;
        private HUDTexture rewind;
        private HUDTexture forward;

        private int rewindTimer = 8;
        private int rewindCounter = 0;

        private HUDTexture help;
        private HUDTexture options;
        private HUDTexture quit;

        public HUDMenuPage quitPage { get; set; }
        public HUDMenuPage optionPage { get; set; }
        public HUDMenuPage helpPage { get; set; }

        public override void OnViewPortResize()
        {
            if (levelChoose != null) { levelChoose.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.1f); }
            if (tutorial1 != null) { tutorial1.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.3f); }
            if (tutorial2 != null) { tutorial2.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.42f); }
            if (tutorial3 != null) { tutorial3.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.54f); }
            if (tutorial4 != null) { tutorial4.Position = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.66f); }

            if (lab1 != null) { lab1.Position = new Vector2(this.Viewport.Width * 0.4f, this.Viewport.Height * 0.3f); }
            if (lab2 != null) { lab2.Position = new Vector2(this.Viewport.Width * 0.4f, this.Viewport.Height * 0.42f); }
            if (lab3 != null) { lab3.Position = new Vector2(this.Viewport.Width * 0.4f, this.Viewport.Height * 0.54f); }
            if (lab4 != null) { lab4.Position = new Vector2(this.Viewport.Width * 0.4f, this.Viewport.Height * 0.66f); }

            if (mountain1 != null) { mountain1.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.3f); }
            if (mountain2 != null) { mountain2.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.42f); }
            if (mountain3 != null) { mountain3.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.54f); }
            if (mountain4 != null) { mountain4.Position = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.66f); }

            if (hospital1 != null) { hospital1.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.3f); }
            if (hospital2 != null) { hospital2.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.42f); }
            if (hospital3 != null) { hospital3.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.54f); }
            if (hospital4 != null) { hospital4.Position = new Vector2(this.Viewport.Width * 0.8f, this.Viewport.Height * 0.66f); }


            if (editor != null) { editor.Position   = new Vector2(this.Viewport.Width * 0.1f, this.Viewport.Height * 0.9f); }
            if (rewind != null) { rewind.Position   = new Vector2(this.Viewport.Width * 0.2f, this.Viewport.Height * 0.9f); }
            if (forward != null){ forward.Position  = new Vector2(this.Viewport.Width * 0.3f, this.Viewport.Height * 0.9f); }
            if (help != null)   { help.Position     = new Vector2(this.Viewport.Width * 0.4f, this.Viewport.Height * 0.9f); }
            if (options != null){ options.Position  = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.9f); }
            if (quit != null)   { quit.Position     = new Vector2(this.Viewport.Width * 0.6f, this.Viewport.Height * 0.9f); }
        }

        public LevelChoiceMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");
            this.leftRightStep = 5;

            levelChoose = new HUDString("choose a level", null, null, null, null, 0.7f, null, content);
            this.AllElements.Add(levelChoose);

            tutorial1 = new HUDString("Tutorial 1", null, null, null, null, 0.45f, null, content);
            this.AllElements.Add(tutorial1);
            this.ChoiceList.Add(tutorial1);

            tutorial2 = new HUDString("Tutorial 2", null, null, null, null, 0.45f, null, content);
            this.AllElements.Add(tutorial2);
            this.ChoiceList.Add(tutorial2);

            tutorial3 = new HUDString("Tutorial 3", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(tutorial3);
            this.ChoiceList.Add(tutorial3);

            tutorial4 = new HUDString("Tutorial 4", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(tutorial4);
            this.ChoiceList.Add(tutorial4);

            editor = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Stationery"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(editor);
            this.ChoiceList.Add(editor);

            lab1 = new HUDString("lab 1", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(lab1);
            this.ChoiceList.Add(lab1);

            lab2 = new HUDString("lab 2", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(lab2);
            this.ChoiceList.Add(lab2);

            lab3 = new HUDString("lab 3", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(lab3);
            this.ChoiceList.Add(lab3);

            lab4 = new HUDString("lab 4", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(lab4);
            this.ChoiceList.Add(lab4);

            rewind = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//rewind_64"), null, 50, 50, null, null, null, content);
            rewind.Scale = 0.7f;
            this.AllElements.Add(rewind);
            this.ChoiceList.Add(rewind);

            mountain1 = new HUDString("Mountain 1", null, null, null, null, 0.45f, null, content);
            this.AllElements.Add(mountain1);
            this.ChoiceList.Add(mountain1);

            mountain2 = new HUDString("Mountain 2", null, null, null, null, 0.45f, null, content);
            this.AllElements.Add(mountain2);
            this.ChoiceList.Add(mountain2);

            mountain3 = new HUDString("Mountain 3", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(mountain3);
            this.ChoiceList.Add(mountain3);

            mountain4 = new HUDString("Mountain 4", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(mountain4);
            this.ChoiceList.Add(mountain4);

            forward = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//forward_64"), null, 50, 50, null, null, null, content);
            forward.Scale = 0.7f;
            this.AllElements.Add(forward);
            this.ChoiceList.Add(forward);

            hospital1 = new HUDString("Hospital 1", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(hospital1);
            this.ChoiceList.Add(hospital1);

            hospital2 = new HUDString("Hospital 2", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(hospital2);
            this.ChoiceList.Add(hospital2);

            hospital3 = new HUDString("Hospital 3", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(hospital3);
            this.ChoiceList.Add(hospital3);

            hospital4 = new HUDString("Hospital 4", null, null, Color.Black, null, 0.45f, null, content);
            this.AllElements.Add(hospital4);
            this.ChoiceList.Add(hospital4);

            help = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Info"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(help);
            this.ChoiceList.Add(help);

            this.ChoiceList.Add(hospital1);     // not that good solution, but i will fix that later
            this.ChoiceList.Add(hospital2);
            this.ChoiceList.Add(hospital3);
            this.ChoiceList.Add(hospital4);

            options = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Tools"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(options);
            this.ChoiceList.Add(options);

            this.ChoiceList.Add(hospital1);     // not that good solution, but i will fix that later
            this.ChoiceList.Add(hospital2);
            this.ChoiceList.Add(hospital3);
            this.ChoiceList.Add(hospital4);

            quit = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Close"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(quit);
            this.ChoiceList.Add(quit);

            this.ChoiceList.Add(hospital1);     // not that good solution, but i will fix that later
            this.ChoiceList.Add(hospital2);
            this.ChoiceList.Add(hospital3);
            this.ChoiceList.Add(hospital4);

            this.ActiveElement = tutorial1;
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
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_1(this.Menu.Game).Level);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == tutorial2)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level1_3(this.Menu.Game).Level);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain1)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level2_5(this.Menu.Game).Level);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == mountain2)
                    {
                        ((LevelView)this.Menu.Game.LevelView).viewNextLevel(new Level2_1(this.Menu.Game).Level);
                        this.Menu.IsVisible = false;
                    }
                    if (this.ActiveElement == editor)
                    {
                        Level loadedLevel = (new LevelReader(this.Game)).readLevel(".\\", "editor_temp.json");
                        if (loadedLevel != null)
                        {
                            this.Game.EditorView.Level = loadedLevel;
                        }
                        this.Game.SwitchToViewMode(RoBuddies.ViewMode.Editor);
                    }
                    if (this.ActiveElement == quit)
                    {
                        this.Menu.ActivePage = this.quitPage;
                    }

                    if (this.ActiveElement == options)
                    {
                        this.Menu.ActivePage = this.optionPage;
                    }

                    if (this.ActiveElement == help)
                    {
                        this.Menu.ActivePage = this.helpPage;
                    }
                }
            }


            if (((LevelView)this.Game.LevelView).SnapShot == null)
            {
                this.rewind.Color = Color.Gray;
                this.forward.Color = Color.Gray;
            }
            else
            {

                this.rewind.Color = Color.White;
                this.forward.Color = Color.White;
                // Key.Enter hold down -----------------------------------------------------------------------------
                if (ButtonIsDown(ControlButton.enter))
                {
                    if (this.ActiveElement != null)
                    {

                        if (this.ActiveElement == rewind)
                        {
                            this.rewindCounter--;
                            if (this.rewindCounter < 0)
                            {
                                ((LevelView)this.Game.LevelView).SnapShot.Rewind();
                                this.rewindCounter = this.rewindTimer;
                            }
                        }

                        if (this.ActiveElement == forward)
                        {
                            this.rewindCounter--;
                            if (this.rewindCounter < 0)
                            {
                                ((LevelView)this.Game.LevelView).SnapShot.Forward();
                                this.rewindCounter = this.rewindTimer;
                            }
                        }

                    }

                }
            }

        }

        public override void OnExit()
        {
            if (((LevelView)this.Game.LevelView).SnapShot != null)
            {
                ((LevelView)this.Game.LevelView).SnapShot.PlayOn();
            }
        }

    }
}
