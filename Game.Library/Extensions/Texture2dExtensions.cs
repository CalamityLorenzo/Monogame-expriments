using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace GameLibrary.Extensions
{
    public static class Texture2d
    {
        public static Texture2D FromFileName(this GraphicsDevice graphicsDevice, string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            var newTexture = Texture2D.FromStream(graphicsDevice, fileStream);
            fileStream.Dispose();
            return newTexture;
        }
    }


}