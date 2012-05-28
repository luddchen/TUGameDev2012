using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RoBuddies.View
{
    class MenuPage
    {
        public List<IHUDElement> AllElements;

        public Viewport Viewport { get; set; }

        public MenuPage(ContentManager content)
        {
            this.AllElements = new List<IHUDElement>();
            this.AllElements.Add(new HUDString(content));
            this.AllElements.Add(new HUDTexture(content));
            this.AllElements.Add(new HUDTexture(content));
        }

        public void Update(GameTime gameTime)
        {
            this.AllElements[0].Position = new Vector2(this.Viewport.Width/2, 20);
            this.AllElements[1].Position = new Vector2(20, 20);
            this.AllElements[2].Position = new Vector2(this.Viewport.Width - 20, 20);
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
