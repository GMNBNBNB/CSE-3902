using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class AnimatedSprite : ISprite
    {
        private Texture2D Character;
        private Vector2 imagePosition;
        List<Texture2D> arrowTexture2DList;
        int currentFrame = 0;
        int timeLastFrame = 0;
        int timePerFame = 100;
        public void LoadContent(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            Character = Content.Load<Texture2D>("characters0");
            imagePosition = new Vector2(
           (graphicsDevice.Viewport.Width - Character.Width) / 2,
           (graphicsDevice.Viewport.Height - Character.Height) / 2);

            arrowTexture2DList = new List<Texture2D>();
            for (int i = 0; i < 3; i++)
            {
                arrowTexture2DList.Add(Content.Load<Texture2D>("characters" + i));
            }
        }
        public void Update(GameTime gameTime)
        {
            timeLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeLastFrame > timePerFame)
            {
                timeLastFrame = gameTime.ElapsedGameTime.Milliseconds;
                if (currentFrame >= arrowTexture2DList.Count - 1)
                {
                    currentFrame = 0;
                }
                else
                {
                    currentFrame++;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(arrowTexture2DList[currentFrame], imagePosition, Color.White);
           
        }
    }
}
