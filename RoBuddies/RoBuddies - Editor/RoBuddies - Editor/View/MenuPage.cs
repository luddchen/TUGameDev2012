using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies___Editor.View
{
    public class MenuPage
    {
        public List<IHUDElement> AllElements;

        public Viewport Viewport { get; set; }

        public MenuPage(ContentManager content)
        {
            this.AllElements = new List<IHUDElement>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IHUDElement element in this.AllElements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IHUDElement element in this.AllElements)
            {
                element.Draw(spriteBatch);
            }
        }
    }
}
