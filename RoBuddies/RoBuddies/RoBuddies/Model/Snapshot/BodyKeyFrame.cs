
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RoBuddies.Model.Objects;

namespace RoBuddies.Model.Snapshot
{
    class BodyKeyFrame
    {
        public PhysicObject Body;
        public Vector2 Position;
        public float Rotation;
        public SpriteEffects Effect;
        public Vector2 Velocity;
        public bool IsVisible;

        public BodyKeyFrame(PhysicObject body)
        {
            this.Body = body;
            this.Position = body.Position;
            this.Rotation = body.Rotation;
            this.Effect = body.Effect;
            this.IsVisible = body.IsVisible;
            if (this.Body is Crate)
            {
                // don't keep the velocity of creates, because this causes bugs
                this.Velocity = Vector2.Zero;
            }
            else
            {
                this.Velocity = body.LinearVelocity;
            }
        }

        public virtual void Restore()
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
