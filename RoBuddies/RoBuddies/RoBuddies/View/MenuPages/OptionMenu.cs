
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.View.HUD;
using RoBuddies.Control;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.View.MenuPages
{
    class OptionMenu : LevelMainMenu
    {
        private HUDString fullscreen;
        private HUDString fullscreenValue;

        private HUDString sound;
        private HUDString soundValue;

        private HUDString music;
        private HUDString musicValue;

        public HUDMenuPage quitPage { get; set; }

        public override void OnViewPortResize()
        {
            base.OnViewPortResize();

            if (fullscreen != null) { fullscreen.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.25f); }
            if (fullscreenValue != null) { fullscreenValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.25f); }

            if (sound != null) { sound.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.5f); }
            if (soundValue != null) { soundValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.5f); }

            if (music != null) { music.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.75f); }
            if (musicValue != null) { musicValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.75f); }
        }

        public OptionMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            addChoiceLine();

            fullscreen = new HUDString("Fullscreen", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(fullscreen, true);

            fullscreenValue = new HUDString("on", null, null, textColor, null, 0.7f, null, content);
            this.AllElements.Add(fullscreenValue);

            addChoiceLine();

            sound = new HUDString("Sound", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(sound, true);

            soundValue = new HUDString("on", null, null, textColor, null, 0.7f, null, content);
            this.AllElements.Add(soundValue);

            addChoiceLine();

            music = new HUDString("Music", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(music, true);

            musicValue = new HUDString("on", null, null, textColor, null, 0.7f, null, content);
            this.AllElements.Add(musicValue);

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
                    if (this.ActiveElement == fullscreen)
                    {
                        if (fullscreenValue.String == "on")
                        {
                            fullscreenValue.String = "off";
                            this.Menu.Game.graphics.PreferredBackBufferWidth = 800;
                            this.Menu.Game.graphics.PreferredBackBufferHeight = 480;
                            this.Menu.Game.graphics.IsFullScreen = false;
                            this.Menu.Game.graphics.ApplyChanges();
                        }
                        else
                        {
                            fullscreenValue.String = "on";
                            this.Menu.Game.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                            this.Menu.Game.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                            this.Menu.Game.graphics.IsFullScreen = true;
                            this.Menu.Game.graphics.ApplyChanges();
                        }
                    }

                    if (this.ActiveElement == sound)
                    {
                        if (soundValue.String == "on")
                        {
                            soundValue.String = "off";
                            this.Menu.Game.audioEngine.GetCategory("SFX").SetVolume(0f);
                        }
                        else
                        {
                            soundValue.String = "on";
                            this.Menu.Game.audioEngine.GetCategory("SFX").SetVolume(3.5f);
                        }
                    }

                    if (this.ActiveElement == music)
                    {
                        if (musicValue.String == "on")
                        {
                            musicValue.String = "off";
                            this.Menu.Game.audioEngine.GetCategory("Music").SetVolume(0f);
                        }
                        else
                        {
                            musicValue.String = "on";
                            this.Menu.Game.audioEngine.GetCategory("Music").SetVolume(1f);
                        }
                    }
                }
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            chooseActiveElement(1, 4);
            this.Menu.makeTransparent(false);
        }

        public override void OnExit()
        {
            base.OnExit();
            this.Menu.makeTransparent(true);
        }

    }
}
