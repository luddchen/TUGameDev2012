﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.Model;
using RoBuddies.Model.Serializer;
using RoBuddies.View.HUD;

namespace RoBuddies.View.MenuPages
{
    class GameMenu : HUDMenuPage
    {
        private HUDTexture editor;
        private HUDTexture rewind;
        private HUDTexture forward;

        private int rewindTimer = 8;
        private int rewindCounter = 0;

        public override void OnViewPortResize()
        {
            if (editor != null) { editor.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.3f); }
            if (rewind != null) { rewind.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.6f); }
            if (forward != null) { forward.Position = new Vector2(this.Viewport.Width * 0.5f, this.Viewport.Height * 0.8f); }
        }

        public GameMenu(LevelMenu menu, ContentManager content)
            : base(menu, content)
        {
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");

            editor = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//Stationery"), null, 64, 64, null, null, null, content);
            this.AllElements.Add(editor);
            this.ChoiceList.Add(editor);

            rewind = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//rewind_64"), null, 64, 64, null, null, null, content);
            rewind.Scale = 0.7f;
            this.AllElements.Add(rewind);
            this.ChoiceList.Add(rewind);

            forward = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Menu//forward_64"), null, 64, 64, null, null, null, content);
            forward.Scale = 0.7f;
            this.AllElements.Add(forward);
            this.ChoiceList.Add(forward);

            this.ActiveElement = editor;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ((LevelView)this.Game.LevelView).CameraUpdate();
            ((LevelView)this.Game.LevelView).Camera.Update(gameTime);

            // Key.Enter pressed once -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
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
                }
            }

            // Key.Enter hold down -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter))
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

        public override void OnExit()
        {
            if (((LevelView)this.Game.LevelView).SnapShot != null)
            {
                ((LevelView)this.Game.LevelView).SnapShot.PlayOn();
            }
        }

    }
}
