using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using RoBuddies.Model;
using RoBuddies.Model.Serializer;

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
        private HUDTexture info;
        private HUDTexture options;
        private HUDTexture quit;

        public HUDMenuPage quitPage { get; set; }
        public HUDMenuPage optionPage { get; set; }
        public HUDMenuPage helpPage { get; set; }
        public HUDMenuPage infoPage { get; set; }

        protected Color notUsableColor = new Color(92, 92, 92, 92);

        protected LevelMenu menu;

        public LevelMainMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.menu = menu;

            addChoiceLine();

            editor = menu.editor;
            addChoiceElement(editor, false);

            rewind = menu.rewind;
            addChoiceElement(rewind, false);

            forward = menu.forward;
            addChoiceElement(forward, false);

            help = menu.help;
            addChoiceElement(help, false);

            info = menu.info;
            addChoiceElement(info, false);

            options = menu.options;
            addChoiceElement(options, false);

            quit = menu.quit;
            addChoiceElement(quit, false);

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

                    if (this.ActiveElement == info)
                    {
                        this.Menu.ActivePage = this.infoPage;
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

        public override void OnEnter()
        {
            menu.editor.isVisible = true;
            menu.rewind.isVisible = true;
            menu.forward.isVisible = true;
            menu.help.isVisible = true;
            menu.info.isVisible = true;
            menu.options.isVisible = true;
            menu.quit.isVisible = true;
        }

        public override void OnExit()
        {
            if (((LevelView)this.Game.LevelView).SnapShot != null)
            {
                ((LevelView)this.Game.LevelView).SnapShot.PlayOn();
            }

            menu.editor.isVisible = false;
            menu.rewind.isVisible = false;
            menu.forward.isVisible = false;
            menu.help.isVisible = false;
            menu.info.isVisible = false;
            menu.options.isVisible = false;
            menu.quit.isVisible = false;
        }

    }
}
