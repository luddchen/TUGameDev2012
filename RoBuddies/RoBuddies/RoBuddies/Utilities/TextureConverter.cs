using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        /// <param name="bottom"></param>
        /// <returns></returns>
        public static Texture2D connectTopBottom(GraphicsDevice device, Texture2D top, Texture2D bottom)
        {
            Texture2D result;
            int newWidth = Math.Min( top.Width, bottom.Width);
            int newHeight = top.Height + bottom.Height;

            result = new Texture2D(device, newWidth, newHeight);

            Color[] inTop = new Color[top.Width * top.Height];
            Color[] inBottom = new Color[bottom.Width * bottom.Height];
            top.GetData<Color>(inTop);
            bottom.GetData<Color>(inBottom);

            Color[] outResult = new Color[newWidth * newHeight];

            for (int x = 0; x < newWidth; x++)
            {

                for (int y = 0; y < top.Height; y++)
                {
                    outResult[y * newWidth + x] = inTop[y * top.Width + x];
                }

                for (int y = 0; y < bottom.Height; y++)
                {
                    outResult[(y + top.Height) * newWidth + x] = inBottom[y * bottom.Width + x];
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
        /// <param name="bottom"></param>
        /// <param name="centerRepeat"></param>
        /// <returns></returns>
        public static Texture2D connectTCB(GraphicsDevice device, Texture2D top, Texture2D center, Texture2D bottom, int centerRepeat) 
        {
            Texture2D result = top;

            for (int i = 0; i < centerRepeat; i++)
            {
                result = connectTopBottom(device, result, center);
            }

            result = connectTopBottom(device, result, bottom);

            return result;
        }

    }
}
