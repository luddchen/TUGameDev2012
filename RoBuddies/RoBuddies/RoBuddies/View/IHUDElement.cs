using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.View
{
    interface IHUDElement
    { 
        String Name { get; set; }

        Vector2 Position { get; set; }

        Color Color { get; set; }

        float Width { get; set; }

        float Height { get; set; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}
