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
    class LevelMainMenu : HUDMenuPage 
    {
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

        protected Color notUsableColor = new Color(92, 92, 92, 92);

        public override void OnViewPortResize()
        {
            if (editor != null) { editor.Position   = new Vector2(this.Viewport.Width * 0.25f, this.Viewport.Height * 0.1f); }
            if (rewind != null) { rewind.Position   = new Vector2(this.Viewport.Width * 0.35f, this.Viewport.Height * 0.1f); }
            if (forward != null){ forward.Position  = new Vector2(this.Viewport.Width * 0.45f, this.Viewport.Height * 0.1f); }
            if (help != null)   { help.Position     = new Vector2(this.Viewport.Width * 0.55f, this.Viewport.Height * 0.1f); }
            if (options != null){ options.Position  = new Vector2(this.Viewport.Width * 0.65f, this.Viewport.Height * 0.1f); }
            if (quit != null)   { quit.Position     = new Vector2(this.Viewport.Width * 0.75f, this.Viewport.Height * 0.1f); }
        }

        public LevelMainMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            addChoiceLine();

            editor = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Stationery"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(editor);
            addChoiceElement(editor);

            rewind = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//rewind_64"), null, 50, 50, null, null, null, content);
            rewind.Scale = 0.7f;
            this.AllElements.Add(rewind);
            addChoiceElement(rewind);

            forward = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//forward_64"), null, 50, 50, null, null, null, content);
            forward.Scale = 0.7f;
            this.AllElements.Add(forward);
            addChoiceElement(forward);

            help = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Info"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(help);
            addChoiceElement(help);

            options = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Tools"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(options);
            addChoiceElement(options);

            quit = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Close"), null, 40, 40, null, null, null, content);
            this.AllElements.Add(quit);
            addChoiceElement(quit);

            chooseActiveElement(0, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (ButtonPressed(ControlButton.enter))
            {
                if (this.ActiveElement != null)
                {
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
                this.rewind.Color = notUsableColor;
                this.forward.Color = notUsableColor;
            }
            else
            {
                if (((LevelView)this.Game.LevelView).SnapShot.isOnStart(3))
                {
                    this.rewind.Color = notUsableColor;
                } 
                else
                {
                    this.rewind.Color = Color.White;
                }

                if (((LevelView)this.Game.LevelView).SnapShot.isOnEnd(3))
                {
                    this.forward.Color = notUsableColor;
                }
                else
                {
                    this.forward.Color = Color.White;
                }

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
