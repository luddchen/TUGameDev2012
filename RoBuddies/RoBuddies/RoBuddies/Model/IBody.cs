using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;

namespace RoBuddies.Model
{

    /// <summary>
    /// should be implemented by all GameObjects
    /// </summary>
    public interface IBody
    {

        /// <summary>
        /// set or get the visibility of this object
        /// </summary>
        bool IsVisible { get; set; }

        Vector2 Position { get; set; }

        float Rotation { get; set; }

        SpriteEffects Effect { get; set; }

        Texture2D Texture { get; set; }

        float Width { get; set; }

        float Height { get; set; }

        Color Color { get; set; }

        Vector2 Origin { get; set; }

        Layer Layer { get; set; }

    }
}
