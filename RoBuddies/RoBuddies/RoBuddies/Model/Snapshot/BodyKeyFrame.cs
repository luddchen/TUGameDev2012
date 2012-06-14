
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model.Snapshot
{
    class BodyKeyFrame
    {
        private PhysicObject Body;
        private Vector2 Position;
        private float Rotation;
        private SpriteEffects Effect;
        private Vector2 Velocity;
        private bool IsVisible;

        public BodyKeyFrame(PhysicObject body)
        {
            this.Body = body;
            this.Position = body.Position;
            this.Rotation = body.Rotation;
            this.Effect = body.Effect;
            this.IsVisible = body.IsVisible;
            this.Velocity = body.LinearVelocity;
        }

        public void Restore()
        {
            this.Body.Position = this.Position;
            this.Body.Rotation = this.Rotation;
            this.Body.Effect = this.Effect;
            this.Body.LinearVelocity = this.Velocity;
            this.Body.IsVisible = this.IsVisible;
        }

        public void Release()
        {
            this.Body = null;
        }

    }
}
