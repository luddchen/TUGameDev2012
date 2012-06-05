
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RoBuddies.View.HUD;
using RoBuddies.Model;
using RoBuddies.Model.Objects;
using FarseerPhysics.Dynamics;
using RoBuddies.Control.Editor;
using RoBuddies.Model.Serializer;
 
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
            this.background = this.Game.Content.Load<Texture2D>("Sprites//Square");

            level = new HUDString("Game", null, null, null, null, 0.7f, null, content);
            this.AllElements.Add(level);
            this.ChoiceList.Add(level);

            testLevel = new HUDString("Test Level", null, null, null, null, 0.7f, null, content);
            this.AllElements.Add(testLevel);
            this.ChoiceList.Add(testLevel);

            this.ActiveElement = level;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Key.Enter -----------------------------------------------------------------------------
            if (this.Menu.newKeyboardState.IsKeyDown(Keys.Enter) && this.Menu.oldKeyboardState.IsKeyUp(Keys.Enter))
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
                        (new LevelWriter(this.Game.EditorView.Level)).writeLevel(".\\", "editor_temp.json");
                        this.activateObjects(this.Game.EditorView.Level);
                        this.Game.LevelView.Level = this.Game.EditorView.Level;

                        ((LevelView)this.Game.LevelView).SnapShot.CreateBodyList(this.Game.LevelView.Level);
                        ((LevelView)this.Game.LevelView).SnapShot.MakeSnapshot();

                        this.Game.SwitchToViewMode(RoBuddies.ViewMode.Level);
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
            foreach (Layer layer in level.AllLayers)
            {
                // this is a bit dirty, because the robot model isn't ready yet
                foreach (PhysicObject phyObj  in layer.AllObjects)
                {
                    if (phyObj is Wall || phyObj is Ladder || phyObj is Pipe || phyObj is Switch || phyObj is Door)
                    {
                        phyObj.BodyType = BodyType.Static;
                    } 
                    else
                    {
                        phyObj.BodyType = BodyType.Dynamic;
                    }
                }
            }
        }

    }
}
