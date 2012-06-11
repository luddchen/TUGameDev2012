
using Microsoft.Xna.Framework;

namespace RoBuddies.Model.Snapshot
{
    class BodyKeyFrame
    {
        private IBody Body;
        private Vector2 Position;
        private float Rotation;

        public BodyKeyFrame(IBody body)
        {
            this.Body = body;
            this.Position = body.Position;//new Vector2(body.Position.X, body.Position.Y);
            this.Rotation = body.Rotation;
        }

        public void Restore()
        {
            this.Body.Position = this.Position;
            this.Body.Rotation = this.Rotation;
        }

        public void Release()
        {
            this.Body = null;
        }

    }
}
