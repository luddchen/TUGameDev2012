using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RoBuddies.Utilities
{

    /// <summary>
    /// texture helper to connect many textures to one;
    /// remember that texture.width and texture.height
    /// of the resulting texture have to be smaller than 2048 
    /// </summary>
    static class TextureConverter
    {

        /// <summary>
        /// connect two textures to one new, left to right
        /// </summary>
        /// <param name="device"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Texture2D connectLeftRight(GraphicsDevice device, Texture2D left, Texture2D right)
        {
            Texture2D result;
            int newWidth = left.Width + right.Width;
            int newHeight = Math.Min( left.Height, right.Height);

            result = new Texture2D(device, newWidth, newHeight);

            Color[] inLeft = new Color[left.Width * left.Height];
            Color[] inRight = new Color[right.Width * right.Height];
            left.GetData<Color>(inLeft);
            right.GetData<Color>(inRight);

            Color[] outResult = new Color[newWidth*newHeight];

            for (int y = 0; y < newHeight; y++)
            {

                for (int x = 0; x < left.Width; x++)
                {
                    outResult[y * newWidth + x] = inLeft[y * left.Width + x];
                }

                for (int x = 0; x < right.Width; x++)
                {
                    outResult[y * newWidth + x + left.Width] = inRight[y * right.Width + x];
                }

            }

            result.SetData<Color>(outResult);

            return result;
        }

        /// <summary>
        /// connect two textures to one new, top to down
        /// </summary>
        /// <param name="device"></param>
        /// <param name="top"></param>
        /// <param name="down"></param>
        /// <returns></returns>
        public static Texture2D connectTopDown(GraphicsDevice device, Texture2D top, Texture2D down)
        {
            Texture2D result;
            int newWidth = Math.Min( top.Width, down.Width);
            int newHeight = top.Height + down.Height;

            result = new Texture2D(device, newWidth, newHeight);

            Color[] inTop = new Color[top.Width * top.Height];
            Color[] inDown = new Color[down.Width * down.Height];
            top.GetData<Color>(inTop);
            down.GetData<Color>(inDown);

            Color[] outResult = new Color[newWidth * newHeight];

            for (int x = 0; x < newWidth; x++)
            {

                for (int y = 0; y < top.Height; y++)
                {
                    outResult[y * newWidth + x] = inTop[y * top.Width + x];
                }

                for (int y = 0; y < down.Height; y++)
                {
                    outResult[(y + top.Height) * newWidth + x] = inDown[y * down.Width + x];
                }

            }

            result.SetData<Color>(outResult);

            return result;
        }

        /// <summary>
        /// connect 3 textures to one new, left to right;
        /// center texture can be repeated
        /// </summary>
        /// <param name="device"></param>
        /// <param name="left"></param>
        /// <param name="center"></param>
        /// <param name="right"></param>
        /// <param name="centerRepeat"></param>
        /// <returns></returns>
        public static Texture2D connectLCR(GraphicsDevice device, Texture2D left, Texture2D center, Texture2D right, int centerRepeat)
        {
            Texture2D result = left;

            for (int i = 0; i < centerRepeat; i++)
            {
                result = connectLeftRight(device, result, center);
            }

            result = connectLeftRight(device, result, right);

            return result;
        }

        /// <summary>
        /// connect 3 textures to one new, top to down; 
        /// center texture can be repeated
        /// </summary>
        /// <param name="device"></param>
        /// <param name="top"></param>
        /// <param name="center"></param>
        /// <param name="down"></param>
        /// <param name="centerRepeat"></param>
        /// <returns></returns>
        public static Texture2D connectTCD(GraphicsDevice device, Texture2D top, Texture2D center, Texture2D down, int centerRepeat) 
        {
            Texture2D result = top;

            for (int i = 0; i < centerRepeat; i++)
            {
                result = connectTopDown(device, result, center);
            }

            result = connectTopDown(device, result, down);

            return result;
        }

    }
}
