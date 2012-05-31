using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using RoBuddies.View;
using RoBuddies.View.HUD;

namespace RoBuddies.View.MenuPages
{
    class OptionMenu : HUDMenuPage
    {
        private HUDString sound;
        private HUDString soundValue;

        private HUDString music;
        private HUDString musicValue;

        public HUDMenuPage quitPage { get; set; }

        public override void OnViewPortResize()
        {
            if (sound != null) { sound.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.2f); }
            if (soundValue != null) { soundValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.2f); }
            if (music != null) { music.Position = new Vector2(this.Viewport.Width * 0.33f, this.Viewport.Height * 0.4f); }
            if (musicValue != null) { musicValue.Position = new Vector2(this.Viewport.Width * 0.66f, this.Viewport.Height * 0.4f); }
        }

        public OptionMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");

            sound = new HUDString("Sound", content);
            sound.Scale = 0.7f;
            this.AllElements.Add(sound);
            this.ChoiceList.Add(sound);

            soundValue = new HUDString("on", content);
            soundValue.Scale = 0.7f;
            this.AllElements.Add(soundValue);

            music = new HUDString("Music", content);
            music.Scale = 0.7f;
            this.AllElements.Add(music);
            this.ChoiceList.Add(music);

            musicValue = new HUDString("on", content);
            musicValue.Scale = 0.7f;
            this.AllElements.Add(musicValue);

            this.ActiveElement = sound;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
            {
                if (this.ActiveElement != null)
                {
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
