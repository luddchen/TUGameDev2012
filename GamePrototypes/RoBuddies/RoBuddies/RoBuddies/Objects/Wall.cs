using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Factories;

namespace Robuddies.Objects
{
    class Wall : PhysicObject
    {
        private Vector2 size;           // = (width, height)
        private Vector2 subSize;
        private Vector2 spriteOrigin;   // sprite center
        private Rectangle dest;
        private float rot;
        private Matrix rMat;
        private Color backCol;
        private Color color;
        private float density;

        public Wall(Vector2 pos, Vector2 size, Vector2 subSize, float rot, Texture2D texture, World world)
            : base(texture, pos, world)
        {
            this.size = size;
            this.subSize = subSize;
            this.rot = rot;
            this.texture = texture;
            this.color = Color.Green;
            this.spriteOrigin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.backCol = new Color(color.R / 2, color.G / 2, color.B / 2);

            rMat = Matrix.CreateRotationZ(rot);

            if (Settings.MaxPolygonVertices < 24) Settings.MaxPolygonVertices = 24;

            Vector2 leftTop = Vector2.Transform(new Vector2(-size.X / 2, -size.Y / 2), rMat);
            Vector2 rightTop = Vector2.Transform(new Vector2(size.X / 2, -size.Y / 2), rMat);
            Vector2 leftBottom = Vector2.Transform(new Vector2(-size.X / 2, size.Y / 2), rMat);
            Vector2 rightBottom = Vector2.Transform(new Vector2(size.X / 2, size.Y / 2), rMat);

            Vertices verts = new Vertices();
            verts.Add(leftTop); verts.Add(rightTop); verts.Add(rightBottom); verts.Add(leftBottom);
            FixtureFactory.AttachPolygon(verts, density, Body);

            //this.Width = max(max(leftTop.X, rightTop.X), max(leftBottom.X, rightBottom.X)) - min(min(leftTop.X, rightTop.X), min(leftBottom.X, rightBottom.X));
            //this.Height = max(max(leftTop.Y, rightTop.Y), max(leftBottom.Y, rightBottom.Y)) - min(min(leftTop.Y, rightTop.Y), min(leftBottom.Y, rightBottom.Y));
        }

        public Wall(Vector2 pos, Vector2 size, Vector2 subSize, float rot, Color color, Color backColor, Texture2D texture, World world)
            : this(pos, size, subSize, rot, texture, world)
        {
            this.color = color;
            this.backCol = backColor;
        }

        public Wall(Vector2 pos, Vector2 size, Vector2 subSize, float rot, float density, Color color, Color backColor, Texture2D texture, World world)
            : this(pos, size, subSize, rot, color, backColor, texture, world)
        {
            this.density = density;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            float scale = 1;
            Vector2 offset = new Vector2(0, 0);
            this.rMat = Matrix.CreateRotationZ(rot + this.Body.Rotation);
            int drawSizeX = (int)(size.X * scale);
            if (drawSizeX < 1) drawSizeX = 1;

            int drawSizeY = (int)(size.Y * scale);
            if (drawSizeY < 1) drawSizeY = 1;

            dest = new Rectangle((int)(this.Body.Position.X * scale + offset.X), (int)(this.Body.Position.Y * scale + offset.Y), drawSizeX, drawSizeY);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, dest, null, this.backCol, rot + this.Body.Rotation, spriteOrigin, SpriteEffects.None, 0f);
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

                    dest = new Rectangle((int)(this.Body.Position.X * scale + pos.X + offset.X), (int)(this.Body.Position.Y * scale + pos.Y + offset.Y), drawSizeX, drawSizeY);
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, dest, null, this.color, rot + this.Body.Rotation, spriteOrigin, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }

                if (subY % 2 == 1)
                {
                    xPos = (0.25f - xCount / 2) * subSize.X * scale;
                    yPos = (0.5f + subY - yCount / 2) * subSize.Y * scale;
                    pos = Vector2.Transform(new Vector2(xPos, yPos), rMat);

                    dest = new Rectangle((int)(this.Body.Position.X * scale + pos.X + offset.X), (int)(this.Body.Position.Y * scale + pos.Y + offset.Y), drawSizeX / 2, drawSizeY);
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, dest, null, this.color, rot + this.Body.Rotation, spriteOrigin, SpriteEffects.None, 0f);
                    spriteBatch.End();

                    xPos = (-0.25f + xCount / 2) * subSize.X * scale;
                    yPos = (0.5f + subY - yCount / 2) * subSize.Y * scale;
                    pos = Vector2.Transform(new Vector2(xPos, yPos), rMat);

                    dest = new Rectangle((int)(this.Body.Position.X * scale + pos.X + offset.X), (int)(this.Body.Position.Y * scale + pos.Y + offset.Y), drawSizeX / 2, drawSizeY);
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, dest, null, this.color, rot + this.Body.Rotation, spriteOrigin, SpriteEffects.None, 0f);
                    spriteBatch.End();
                }
            }
        }

        public float min(float a, float b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }

        public float max(float a, float b)
        {
            if (a < b)
            {
                return b;
            }
            return a;
        }
    }
}
