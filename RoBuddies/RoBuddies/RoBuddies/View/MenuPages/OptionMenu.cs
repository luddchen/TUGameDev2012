
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.View.HUD;
using RoBuddies.Control;

namespace RoBuddies.View.MenuPages
{
    class OptionMenu : HUDMenuPage
    {
        private HUDString fullscreen;
        private HUDString fullscreenValue;

        private HUDString cameraMode;
        private HUDString cameraModeValue;

        private HUDString sound;
        private HUDString soundValue;

        private HUDString music;
        private HUDString musicValue;

        public HUDMenuPage quitPage { get; set; }

        public override void OnViewPortResize()
        {
            if (fullscreen != null) { fullscreen.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.2f); }
            if (fullscreenValue != null) { fullscreenValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.2f); }

            if (cameraMode != null) { cameraMode.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.4f); }
            if (cameraModeValue != null) { cameraModeValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.4f); }

            if (sound != null) { sound.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.6f); }
            if (soundValue != null) { soundValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.6f); }

            if (music != null) { music.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.8f); }
            if (musicValue != null) { musicValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.8f); }
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

            cameraMode = new HUDString("Camera Mode", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(cameraMode, true);

            cameraModeValue = new HUDString("smooth", null, null, textColor, null, 0.7f, null, content);
            this.AllElements.Add(cameraModeValue);

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
                    if (this.ActiveElement == fullscreen)
                    {
                        if (fullscreenValue.String == "on")
                        {
                            fullscreenValue.String = "off";
                        }
                        else
                        {
                            fullscreenValue.String = "on";
                        }
                    }

                    if (this.ActiveElement == cameraMode)
                    {
                        if (cameraModeValue.String == "smooth")
                        {
                            cameraModeValue.String = "normal";
                            this.Menu.Game.LevelView.Camera.SmoothMove = false;
                        }
                        else
                        {
                            cameraModeValue.String = "smooth";
                            this.Menu.Game.LevelView.Camera.SmoothMove = true;
                        }
                    }

                    if (this.ActiveElement == sound)
                    {
                        if (soundValue.String == "on")
                        {
                            soundValue.String = "off";
                        }
                        else
                        {
                            soundValue.String = "on";
                        }
                    }

                    if (this.ActiveElement == music)
                    {
                        if (musicValue.String == "on")
                        {
                            musicValue.String = "off";
                        }
                        else
                        {
                            musicValue.String = "on";
                        }
                    }
                }
            }
        }

    }
}
