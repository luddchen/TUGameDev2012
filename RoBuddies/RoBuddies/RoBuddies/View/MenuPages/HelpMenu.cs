using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies.View.MenuPages
{
    class HelpMenu : LevelMainMenu
    {
        private HUDString helpHeadline;
        private HUDString jump;
        private HUDString move;
        private HUDString seperate;
        private HUDString switchPart;
        private HUDString use;
        private HUDString rewind;

        private HUDTexture jumpKey;
        private HUDTexture moveKey;
        private HUDTexture seperateKey;
        private HUDTexture switchKey;
        private HUDTexture useKey;
        private HUDTexture rewindKey;

        private HUDTexture jumpButton;
        private HUDTexture moveButton;
        private HUDTexture seperateButton;
        private HUDTexture switchButton;
        private HUDTexture useButton;
        private HUDTexture rewindButton; 

        private HUDString help;

        private float helpHeight;

        private String helpText = "\n\n\n\n remember some rules\n"
                                + "----------------------------------\n"
                                + "upper part can't walk without legs\n"
                                + "\n"
                                + "lower part can't climb armless \n"
                                + "\n"
                                + "lower part can jump higher than the combined robot\n"
                                + "\n"
                                + "lower part can't pull crates only push\n"
                                + "\n"
                                + "combined robot can pull crates\n"
                                + "and push bigger crates than lower part\n"
                                + "\n"
                                + "only the combined robot can pass the door to next level";
                              

        public HUDMenuPage quitPage { get; set; }
        public HUDMenuPage optionPage { get; set; }

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (help != null) {
                float textX = this.Viewport.Width * 0.3f;
                float keyX = this.Viewport.Width * 0.6f;
                float buttonX = this.Viewport.Width * 0.8f;
                float pos = helpHeadline.Height / 2;
                helpHeadline.Position = new Vector2(this.Viewport.Width / 2, pos); 

                pos += jump.Height / 2;
                jump.Position = new Vector2(textX, pos);
                jumpKey.Position = new Vector2(keyX, pos);
                jumpButton.Position = new Vector2(buttonX, pos);

                pos += move.Height / 2;
                move.Position = new Vector2(textX, pos);
                moveKey.Position = new Vector2(keyX, pos);
                moveButton.Position = new Vector2(buttonX, pos);

                pos += seperate.Height / 2;
                seperate.Position = new Vector2(textX, pos);
                seperateKey.Position = new Vector2(keyX, pos);
                seperateButton.Position = new Vector2(buttonX, pos);

                pos += switchPart.Height / 2;
                switchPart.Position = new Vector2(textX, pos);
                switchKey.Position = new Vector2(keyX, pos);
                switchButton.Position = new Vector2(buttonX, pos);

                pos += use.Height / 2;
                use.Position = new Vector2(textX, pos);
                useKey.Position = new Vector2(keyX, pos);
                useButton.Position = new Vector2(buttonX, pos);

                pos += rewind.Height / 2;
                rewind.Position = new Vector2(textX, pos);
                rewindKey.Position = new Vector2(keyX, pos);
                rewindButton.Position = new Vector2(buttonX, pos);

                pos += help.Height / 2;
                help.Position = new Vector2(this.Viewport.Width / 2, pos);
            }
        }

        public HelpMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            helpHeadline = new HUDString("Welcome to RoBuddies first aid\n", null, null, textColor, null, 0.65f, 0, content);
            this.AllElements.Add(helpHeadline);

            jump = new HUDString("\n\njump\n\n", null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(jump);
            jumpKey = new HUDTexture(content.Load<Texture2D>("Sprites//Keyboard//Space"), null, 50, 50, textColor, 1.7f, 0, content);
            this.AllElements.Add(jumpKey);
            jumpButton = new HUDTexture(content.Load<Texture2D>("Sprites//Xbox//Xbox_A"), null, 60, 40, textColor, 1, 0, content);
            this.AllElements.Add(jumpButton);

            move = new HUDString("\n\nmove\n\n", null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(move);            
            moveKey = new HUDTexture(content.Load<Texture2D>("Sprites//Keyboard//Arrows"), null, 75, 50, textColor, 1.2f, 0, content);
            this.AllElements.Add(moveKey);
            moveButton = new HUDTexture(content.Load<Texture2D>("Sprites//Xbox//Xbox_analog"), null, 60, 40, textColor, 1, 0, content);
            this.AllElements.Add(moveButton);

            seperate = new HUDString("\n\nseperate\n\n", null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(seperate); 
            seperateKey = new HUDTexture(content.Load<Texture2D>("Sprites//Keyboard//X"), null, 50, 50, textColor, 0.7f, 0, content);
            this.AllElements.Add(seperateKey);
            seperateButton = new HUDTexture(content.Load<Texture2D>("Sprites//Xbox//Xbox_Y"), null, 60, 40, textColor, 1, 0, content);
            this.AllElements.Add(seperateButton);

            switchPart = new HUDString("\n\nswitch\n\n", null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(switchPart);
            switchKey = new HUDTexture(content.Load<Texture2D>("Sprites//Keyboard//S"), null, 50, 50, textColor, 0.7f, 0, content);
            this.AllElements.Add(switchKey);
            switchButton = new HUDTexture(content.Load<Texture2D>("Sprites//Xbox//Xbox_switchPart"), null, 60, 40, textColor, 1, 0, content);
            this.AllElements.Add(switchButton);

            use = new HUDString("\n\nuse\n\n", null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(use);
            useKey = new HUDTexture(content.Load<Texture2D>("Sprites//Keyboard//A"), null, 50, 50, textColor, 0.7f, 0, content);
            this.AllElements.Add(useKey);
            useButton = new HUDTexture(content.Load<Texture2D>("Sprites//Xbox//Xbox_X"), null, 60, 40, textColor, 1, 0, content);
            this.AllElements.Add(useButton);

            rewind = new HUDString("\n\nrewind\n\n", null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(rewind);
            rewindKey = new HUDTexture(content.Load<Texture2D>("Sprites//Keyboard//R"), null, 50, 50, textColor, 0.7f, 0, content);
            this.AllElements.Add(rewindKey);
            rewindButton = new HUDTexture(content.Load<Texture2D>("Sprites//Xbox//Xbox_rewind"), null, 60, 40, textColor, 1, 0, content);
            this.AllElements.Add(rewindButton);

            help = new HUDString(helpText, null, null, textColor, null, 0.5f, 0, content);
            this.AllElements.Add(help);

            helpHeight = helpHeadline.Height + jump.Height + move.Height + seperate.Height +
                switchPart.Height + use.Height + help.Height;
            OnEnter();
        }

        public override void Update(GameTime gameTime)
        {
            if (ButtonIsDown(ControlButton.left) || ButtonIsDown(ControlButton.right))
            {
                base.Update(gameTime);
            }
            else
            {
                animate(gameTime);
            }

            // Key.down -----------------------------------------------------------------------------
            if (ButtonIsDown(ControlButton.down))
            {
                if (help.Position.Y > viewport.Height - help.Height / 2)
                {
                    helpHeadline.Position = new Vector2(helpHeadline.Position.X, helpHeadline.Position.Y - 2);
                    jump.Position = new Vector2(jump.Position.X, jump.Position.Y - 2);
                    jumpKey.Position = new Vector2(jumpKey.Position.X, jumpKey.Position.Y - 2);
                    jumpButton.Position = new Vector2(jumpButton.Position.X, jumpButton.Position.Y - 2);

                    move.Position = new Vector2(move.Position.X, move.Position.Y - 2);
                    moveKey.Position = new Vector2(moveKey.Position.X, moveKey.Position.Y - 2);
                    moveButton.Position = new Vector2(moveButton.Position.X, moveButton.Position.Y - 2);

                    seperate.Position = new Vector2(seperate.Position.X, seperate.Position.Y - 2);
                    seperateKey.Position = new Vector2(seperateKey.Position.X, seperateKey.Position.Y - 2);
                    seperateButton.Position = new Vector2(seperateButton.Position.X, seperateButton.Position.Y - 2);

                    switchPart.Position = new Vector2(switchPart.Position.X, switchPart.Position.Y - 2);
                    switchKey.Position = new Vector2(switchKey.Position.X, switchKey.Position.Y - 2);
                    switchButton.Position = new Vector2(switchButton.Position.X, switchButton.Position.Y - 2);

                    use.Position = new Vector2(use.Position.X, use.Position.Y - 2);
                    useKey.Position = new Vector2(useKey.Position.X, useKey.Position.Y - 2);
                    useButton.Position = new Vector2(useButton.Position.X, useButton.Position.Y - 2);

                    rewind.Position = new Vector2(rewind.Position.X, rewind.Position.Y - 2);
                    rewindKey.Position = new Vector2(rewindKey.Position.X, rewindKey.Position.Y - 2);
                    rewindButton.Position = new Vector2(rewindButton.Position.X, rewindButton.Position.Y - 2);

                    help.Position = new Vector2(help.Position.X, help.Position.Y - 2);
                }
            } // ------------------------------------------------------------------------------------

            // Key.Up -------------------------------------------------------------------------------
            if (ButtonIsDown(ControlButton.up))
            {
                if (helpHeadline.Position.Y < helpHeadline.Height / 2)
                {
                    helpHeadline.Position = new Vector2(helpHeadline.Position.X, helpHeadline.Position.Y + 2);
                    jump.Position = new Vector2(jump.Position.X, jump.Position.Y + 2);
                    jumpKey.Position = new Vector2(jumpKey.Position.X, jumpKey.Position.Y + 2);
                    jumpButton.Position = new Vector2(jumpButton.Position.X, jumpButton.Position.Y + 2);

                    move.Position = new Vector2(move.Position.X, move.Position.Y + 2);
                    moveKey.Position = new Vector2(moveKey.Position.X, moveKey.Position.Y + 2);
                    moveButton.Position = new Vector2(moveButton.Position.X, moveButton.Position.Y + 2);

                    seperate.Position = new Vector2(seperate.Position.X, seperate.Position.Y + 2);
                    seperateKey.Position = new Vector2(seperateKey.Position.X, seperateKey.Position.Y + 2);
                    seperateButton.Position = new Vector2(seperateButton.Position.X, seperateButton.Position.Y + 2);

                    switchPart.Position = new Vector2(switchPart.Position.X, switchPart.Position.Y + 2);
                    switchKey.Position = new Vector2(switchKey.Position.X, switchKey.Position.Y + 2);
                    switchButton.Position = new Vector2(switchButton.Position.X, switchButton.Position.Y + 2);

                    use.Position = new Vector2(use.Position.X, use.Position.Y + 2);
                    useKey.Position = new Vector2(useKey.Position.X, useKey.Position.Y + 2);
                    useButton.Position = new Vector2(useButton.Position.X, useButton.Position.Y + 2);

                    rewind.Position = new Vector2(rewind.Position.X, rewind.Position.Y + 2);
                    rewindKey.Position = new Vector2(rewindKey.Position.X, rewindKey.Position.Y + 2);
                    rewindButton.Position = new Vector2(rewindButton.Position.X, rewindButton.Position.Y + 2);

                    help.Position = new Vector2(help.Position.X, help.Position.Y + 2);
                }
            } // -----------------------------------------------------------------------------------

        }

        public override void OnEnter()
        {
            base.OnEnter();
            chooseActiveElement(1, 2);
            this.Menu.makeTransparent(false);
        }

        public override void OnExit()
        {
            base.OnExit();
            this.Menu.makeTransparent(true);
        }
    }
}
