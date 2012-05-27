using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model
{

    /// <summary>
    /// should be implemented by all GameObjects
    /// </summary>
    interface IBody
    {

        bool IsVisible { get; set; }

        Vector2 Position { get; set; }

        SpriteEffects Effect { get; set; }

        Texture2D Texture { get; set; }

        float Width { get; set; }

        float Height { get; set; }

        Color Color { get; set; }

        Vector2 Origin { get; set; }

        Layer Layer { get; set; }

        void Update(GameTime gameTime);
    }
}
