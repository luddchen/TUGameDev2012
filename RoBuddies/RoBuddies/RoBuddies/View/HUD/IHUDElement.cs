﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.View.HUD
{

    /// <summary>
    /// interface for Head Up Display Elements
    /// </summary>
    public interface IHUDElement
    { 
        /// <summary>
        /// name of this element
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// local position of this element 
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// color of this element
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// scale of this element
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// width of this element
        /// </summary>
        float Width { get; set; }

        /// <summary>
        /// height of this element
        /// </summary>
        float Height { get; set; }

        /// <summary>
        /// rotation of this element
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// if this element visible or not
        /// </summary>
        bool isVisible { get; set; }

        /// <summary>
        /// for update of values and effects 
        /// </summary>
        /// <param name="gameTime">the gametime</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// draw this element
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// testing intersection with point
        /// </summary>
        /// <param name="point">the test point</param>
        /// <returns>true if there is an intersetion</returns>
        bool Intersects(Vector2 point);
    }
}
