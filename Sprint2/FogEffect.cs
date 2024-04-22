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
        public FogEffect(GraphicsDevice _GraphicsDevice) 
        {

            fogTexture = new Texture2D(_GraphicsDevice, 1, 1);
            fogData = new Color[] { new Color(0, 0, 0, 230) };
            fogTexture.SetData(fogData);
            tileSize = 10;
            mapHeight = 300;
            mapWidth = 3000;

            fogOfWarVisible = new bool[mapWidth, mapHeight];
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    fogOfWarVisible[x, y] = false;
                }
            }
        }

         public void Update(Vector2 playerPosition)
         {
            int playerX = (int)playerPosition.X / tileSize;
            int playerY = (int)playerPosition.Y / tileSize;

            int fogRadius = 3; // 3 tiles radius of clear fog
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
        public void Draw(SpriteBatch spriteBatch)
        {

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
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
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    fogOfWarVisible[x, y] = false;
                }
            }
        }
    }
}
