using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

namespace Fall_Ball
{
    class SquareStack : GameObject
    {
        private Vector2 size;           // = (width, height)
        private Vector2 subSize;
        private Vector2 spriteOrigin;   // sprite center
        private Rectangle dest;
        private float rot;
        Matrix rMat;
        private Color backCol;

        public SquareStack(Vector2 pos, Vector2 size, Vector2 subSize, float rot, SpriteBatch batch, Texture2D texture, World world)
            : base(pos, batch, texture, world)
        {
            this.size = size;
            this.subSize = subSize;
            this.rot = rot;
            this.texture = texture;
            this.color = Color.Green;
            this.spriteOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.backCol = new Color(color.R / 2, color.G / 2, color.B / 2);

            rMat = Matrix.CreateRotationZ(rot);

            Vector2 leftTop = Vector2.Transform(new Vector2(-size.X / 2, -size.Y / 2), rMat);
            Vector2 rightTop = Vector2.Transform(new Vector2(size.X / 2, -size.Y / 2), rMat);
            Vector2 leftBottom = Vector2.Transform(new Vector2(-size.X / 2, size.Y / 2), rMat);
            Vector2 rightBottom = Vector2.Transform(new Vector2(size.X / 2, size.Y / 2), rMat);
            FixtureFactory.AttachEdge(leftTop, rightTop, body);
            FixtureFactory.AttachEdge(rightTop, rightBottom, body);
            FixtureFactory.AttachEdge(rightBottom, leftBottom, body);
            FixtureFactory.AttachEdge(leftBottom, leftTop, body);
            this.width = max(max(leftTop.X, rightTop.X), max(leftBottom.X, rightBottom.X)) - min(min(leftTop.X, rightTop.X), min(leftBottom.X, rightBottom.X));
            this.height = max(max(leftTop.Y, rightTop.Y), max(leftBottom.Y, rightBottom.Y)) - min(min(leftTop.Y, rightTop.Y), min(leftBottom.Y, rightBottom.Y));
        }

        public SquareStack(Vector2 pos, Vector2 size, Vector2 subSize, float rot, Color color, Color backColor, SpriteBatch batch, Texture2D texture, World world)
            : this(pos, size, subSize, rot, batch, texture, world)
        {
            this.color = color;
            this.backCol = backColor;
        }

        public override void draw(Vector2 offset, float scale)
        {
            int drawSizeX = (int)(size.X * scale);
            if (drawSizeX < 1) drawSizeX = 1;

            int drawSizeY = (int)(size.Y * scale);
            if (drawSizeY < 1) drawSizeY = 1;

            dest = new Rectangle((int)(this.body.Position.X * scale + offset.X), (int)(this.body.Position.Y * scale + offset.Y), drawSizeX, drawSizeY);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, this.backCol, rot, spriteOrigin, SpriteEffects.None, 0f);
            spriteBatch.End();

            Vector2 pos;
            float xCount = (size.X / subSize.X);
            float yCount = (size.Y / subSize.Y);

            drawSizeX = (int)((subSize.X - 1) * scale);
            if (drawSizeX < 1) drawSizeX = 1;

            drawSizeY = (int)((subSize.Y - 1) * scale);
            if (drawSizeY < 1) drawSizeY = 1;

            float xPos;
            float yPos;

            for (int subY = 0; subY < yCount; subY++)
            {
                for (int subX = 0; subX < xCount - (subY % 2); subX++)
                {
                    xPos = ( (subY % 2) * 0.5f + 0.5f + subX - xCount/2) * subSize.X * scale;
                    yPos = (0.5f + subY - yCount / 2) * subSize.Y * scale;
                    pos = Vector2.Transform(new Vector2( xPos, yPos), rMat);

                    dest = new Rectangle((int)(this.body.Position.X * scale + pos.X + offset.X), (int)(this.body.Position.Y * scale + pos.Y + offset.Y), drawSizeX, drawSizeY);
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, dest, null, this.color, rot, spriteOrigin, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }

                if (subY % 2 == 1)
                {
                    xPos = (0.25f - xCount / 2) * subSize.X * scale;
                    yPos = (0.5f + subY - yCount / 2) * subSize.Y * scale;
                    pos = Vector2.Transform(new Vector2(xPos, yPos), rMat);

                    dest = new Rectangle((int)(this.body.Position.X * scale + pos.X + offset.X), (int)(this.body.Position.Y * scale + pos.Y + offset.Y), drawSizeX / 2, drawSizeY);
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, dest, null, this.color, rot, spriteOrigin, SpriteEffects.None, 0f);
                    spriteBatch.End();

                    xPos = (-0.25f + xCount / 2) * subSize.X * scale;
                    yPos = (0.5f + subY - yCount / 2) * subSize.Y * scale;
                    pos = Vector2.Transform(new Vector2(xPos, yPos), rMat);

                    dest = new Rectangle((int)(this.body.Position.X * scale + pos.X + offset.X), (int)(this.body.Position.Y * scale + pos.Y + offset.Y), drawSizeX / 2, drawSizeY);
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, dest, null, this.color, rot, spriteOrigin, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }
            }
        }

        public override void drawMap(Vector2 offset, float scale)
        {
            int drawSizeX = (int)(size.X * scale);
            if (drawSizeX < 1) drawSizeX = 1;

            int drawSizeY = (int)(size.Y * scale);
            if (drawSizeY < 1) drawSizeY = 1;

            dest = new Rectangle((int)(this.body.Position.X * scale + offset.X), (int)(this.body.Position.Y * scale + offset.Y), drawSizeX, drawSizeY);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, this.color, rot, spriteOrigin, SpriteEffects.None, 0f);
            spriteBatch.End();

        }

    }
}