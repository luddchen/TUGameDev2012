using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using RoBuddies.Model;

namespace RoBuddies.Control.StateMachines
{
    class UpperPartStateMachine : StateMachine
    {
        private ContentManager contentManager;
        private Robot robot;

        public UpperPartStateMachine(IBody body, ContentManager contentManager, Robot robot)
            : base(body)
        {
            this.contentManager = contentManager;
            this.robot = robot;
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
