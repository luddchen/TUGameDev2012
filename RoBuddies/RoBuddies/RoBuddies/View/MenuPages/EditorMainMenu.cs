using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.Model;
using RoBuddies.Model.Objects;
using RoBuddies.Model.Serializer;
using RoBuddies.View.HUD;
using RoBuddies.Control;
 
namespace RoBuddies.View.MenuPages
{
    class EditorMainMenu : HUDMenuPage
    {
        private HUDString level;

        private HUDString testLevel;

        public override void OnViewPortResize()
        {
            if (level != null) { level.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.2f); }
            if (testLevel != null) { testLevel.Position = new Vector2(this.Viewport.Width / 2, this.Viewport.Height * 0.4f); }
        }

        public EditorMainMenu(HUDMenu menu, ContentManager content)
            : base(menu, content)
        {
            addChoiceLine();

            level = new HUDString("Game", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(level, true);

            addChoiceLine();

            testLevel = new HUDString("Test Level", null, null, textColor, null, 0.7f, null, content);
            addChoiceElement(testLevel, true);

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
                    if (this.ActiveElement == level)
                    {
                        this.Game.LevelView = new LevelView(this.Game);
                        this.Game.SwitchToViewMode(RoBuddies.ViewMode.Level);
                    }

                    if (this.ActiveElement == testLevel)
                    {
                        if (this.Game.EditorView.Level.Robot != null)
                        {
                            (new LevelWriter(this.Game.EditorView.Level)).writeLevel(".\\", "editor_temp.json");
                            this.Game.EditorView.Level.LevelName = "Test Level";
                            this.activateObjects(this.Game.EditorView.Level);
                            ((LevelView)this.Game.LevelView).viewNextLevel(this.Game.EditorView.Level, gameTime);
                            this.Game.SwitchToViewMode(RoBuddies.ViewMode.Level);
                        }
                        else
                        {
                            Console.Out.WriteLine("your level need at least a robot to play ...");
                        }
                    }

                }
            }
        }

        /// <summary>
        /// activates all objects in the level.  E.g. makes crates and the robot to dynamic bodies.
        /// It's not the best place for this method. An EditorController would be better.
        /// Maybe I will refactor it later. (thomas)
        /// </summary>
        /// <param name="level"></param>
        private void activateObjects(Level level)
        {
            if (level.Robot != null) 
            { 
                level.Robot.ActivePart.BodyType = BodyType.Dynamic; 
            }

            foreach (Layer layer in level.AllLayers)
            {
                foreach (IBody body in layer.AllObjects)
                {
                    if (body is Crate)
                    {
                        Crate crate = (Crate)body;
                        crate.BodyType = BodyType.Dynamic;
                    }
                }
            }
        }

    }
}
