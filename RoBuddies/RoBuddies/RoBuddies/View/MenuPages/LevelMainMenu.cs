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
        private HUDTexture play;
        private HUDTexture reload;
        private HUDTexture rewind;
        private HUDTexture forward;

        private int rewindTimer = 3;
        private int rewindCounter = 0;

        private HUDTexture chooser;
        private HUDTexture help;
        private HUDTexture info;
        private HUDTexture options;
        private HUDTexture quit;

        protected Color notUsableColor = new Color(96, 0, 0, 64);
        protected Color choiceBackgroundColor = new Color(0, 0, 0, 96);

        private IHUDElement oldActiveElement;

        public LevelMainMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {

            addChoiceLine(); 
            
                reload = menu.reload;
                addChoiceElement(reload, false);

                rewind = menu.rewind;
                addChoiceElement(rewind, false);

                forward = menu.forward;
                addChoiceElement(forward, false);

            addChoiceLine();

                play = menu.play;
                addChoiceElement(play, false);

                chooser = menu.chooser;
                addChoiceElement(chooser, false);

                help = menu.help;
                addChoiceElement(help, false);

                info = menu.info;
                addChoiceElement(info, false);

                options = menu.options;
                addChoiceElement(options, false);

                quit = menu.quit;
                addChoiceElement(quit, false);

            chooseActiveElement(1, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.activeElement != this.oldActiveElement)
            {
                if (this.activeElement.Color != notUsableColor)
                {
                    if (this.activeElement == reload)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).mainMenu;
                        ((LevelMenu)this.Menu).mainMenu.chooseActiveElement(0, 0);
                    }

                    if (this.activeElement == rewind && this.oldActiveElement != forward)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).mainMenu;
                        ((LevelMenu)this.Menu).mainMenu.chooseActiveElement(0, 1);
                        ((LevelMenu)this.Menu).mainMenu.oldActiveElement = ((LevelMenu)this.Menu).mainMenu.rewind;
                    }

                    if (this.activeElement == forward && this.oldActiveElement != rewind)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).mainMenu;
                        ((LevelMenu)this.Menu).mainMenu.chooseActiveElement(0, 2);
                        ((LevelMenu)this.Menu).mainMenu.oldActiveElement = ((LevelMenu)this.Menu).mainMenu.forward;
                    }

                    if (this.activeElement == play)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).mainMenu;
                        ((LevelMenu)this.Menu).mainMenu.chooseActiveElement(1, 0);
                    }

                    if (this.activeElement == chooser)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).chooserMenu;
                    }

                    if (this.activeElement == help)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).helpMenu;
                    }

                    if (this.activeElement == info)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).infoMenu;
                    }

                    if (this.activeElement == options)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).optionMenu;
                    }

                    if (this.activeElement == quit)
                    {
                        this.Menu.ActivePage = ((LevelMenu)this.Menu).quitMenu;
                    }

                }
                else
                {
                    if (this.activeElement == reload || this.activeElement == rewind || this.activeElement == forward)
                    {
                        this.ActiveElement = this.oldActiveElement;
                        if (this.activeElement == reload || this.activeElement == rewind || this.activeElement == forward)
                        {
                            this.ChoiceLine = 0;
                        }
                        else
                        {
                            this.ChoiceLine = 1;
                        }
                    }
                }

            }

            // Key.Enter pressed once-----------------------------------------------------------------------------
            if (ButtonPressed(ControlButton.enter))
            {
                if (this.ActiveElement != null)
                {
                    if (this.ActiveElement == play)
                    {
                        this.Menu.IsVisible = false;
                    }
                }
            }


            if (((LevelView)this.Game.LevelView).SnapShot == null)
            {
                this.rewind.Color = notUsableColor;
                this.forward.Color = notUsableColor;
                this.reload.Color = notUsableColor;
            }
            else
            {
                this.reload.Color = Color.White;

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


                // Key.Enter pressed once-----------------------------------------------------------------------------
                if (ButtonPressed(ControlButton.enter))
                {
                    if (this.ActiveElement != null)
                    {

                        if (this.ActiveElement == reload)
                        {
                            ((LevelView)this.Game.LevelView).SnapShot.RewindToStart();
                            this.Menu.IsVisible = false;
                        }

                    }
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

            this.oldActiveElement = this.activeElement;

        }

        public override void OnEnter()
        {
            ((LevelMenu)this.Menu).play.isVisible = true;
            ((LevelMenu)this.Menu).reload.isVisible = true;
            ((LevelMenu)this.Menu).rewind.isVisible = true;
            ((LevelMenu)this.Menu).forward.isVisible = true;

            ((LevelMenu)this.Menu).chooser.isVisible = true;
            ((LevelMenu)this.Menu).help.isVisible = true;
            ((LevelMenu)this.Menu).info.isVisible = true;
            ((LevelMenu)this.Menu).options.isVisible = true;
            ((LevelMenu)this.Menu).quit.isVisible = true;

            ((LevelMenu)this.Menu).play.Scale = 1;
            ((LevelMenu)this.Menu).reload.Scale = 0.7f;
            ((LevelMenu)this.Menu).rewind.Scale = 0.5f;
            ((LevelMenu)this.Menu).forward.Scale = 0.5f;

            ((LevelMenu)this.Menu).chooser.Scale = 1;
            ((LevelMenu)this.Menu).help.Scale = 1;
            ((LevelMenu)this.Menu).info.Scale = 1;
            ((LevelMenu)this.Menu).options.Scale = 1;
            ((LevelMenu)this.Menu).quit.Scale = 1;
        }

        public override void OnExit()
        {
            base.OnExit();
            if (((LevelView)this.Game.LevelView).SnapShot != null)
            {
                ((LevelView)this.Game.LevelView).SnapShot.PlayOn();
            }

            ((LevelMenu)this.Menu).play.isVisible = false;
            ((LevelMenu)this.Menu).reload.isVisible = false;
            ((LevelMenu)this.Menu).rewind.isVisible = false;
            ((LevelMenu)this.Menu).forward.isVisible = false;

            ((LevelMenu)this.Menu).chooser.isVisible = false;
            ((LevelMenu)this.Menu).help.isVisible = false;
            ((LevelMenu)this.Menu).info.isVisible = false;
            ((LevelMenu)this.Menu).options.isVisible = false;
            ((LevelMenu)this.Menu).quit.isVisible = false;
        }

    }
}
