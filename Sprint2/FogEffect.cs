using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    public class FogEffect
    {
        Texture2D fogTexture;
        Color[] fogData;
        bool[,] fogOfWarVisible;
        int tileSize;
        int mapHeight;
        int mapWidth;
        int screenWidth;
        int screenHeight;
        public FogEffect(GraphicsDevice _GraphicsDevice, Rectangle screenBounds) 
        {

            fogTexture = new Texture2D(_GraphicsDevice, 1, 1);
            fogData = new Color[] { new Color(0, 0, 0, 230) };
            fogTexture.SetData(fogData);
            tileSize = 10;
            mapHeight = 300;
            mapWidth = 3000;
            screenWidth = screenBounds.Width;
            screenHeight = screenBounds.Height;

            fogOfWarVisible = new bool[mapWidth, mapHeight];
            reset();
        }

         public void Update(Vector2 playerPosition)
         {
            int playerX = (int)playerPosition.X / tileSize;
            int playerY = (int)playerPosition.Y / tileSize;

            int fogRadius = 3; 
            for (int x = -fogRadius; x <= fogRadius; x++)
            {
                for (int y = -fogRadius; y <= fogRadius; y++)
                {
                    if (playerX + x >= 0 && playerX + x < mapWidth && playerY + y >= 0 && playerY + y < mapHeight)
                    {
                        fogOfWarVisible[playerX + x, playerY + y] = true;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 cameraPosition)
        {
            int startX = (int)cameraPosition.X / tileSize;
            int startY = (int)cameraPosition.Y / tileSize;
            int endX = Math.Min(startX + screenWidth / tileSize, mapWidth) + 50;
            int endY = Math.Min(startY + screenHeight / tileSize, mapHeight);

            for (int x = startX; x < endX; x++)
            {
                for (int y = startY; y < endY; y++)
                {
                    if (!fogOfWarVisible[x, y])
                    {
                        spriteBatch.Draw(fogTexture, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Color.White);
                    }
                }
            }
        }

        public void reset()
        {
            Array.Clear(fogOfWarVisible, 0, fogOfWarVisible.Length);
        }
    }
}
