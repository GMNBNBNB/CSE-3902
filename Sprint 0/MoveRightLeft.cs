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
    public class MoveRightLeft : ISprite
    {
        private Texture2D Character;
        private Vector2 imagePosition;
        private GraphicsDeviceManager _graphics;
        List<Texture2D> arrowTexture2DList;
        int currentFrame = 0;
        int timeLastFrame = 0;
        int timePerFame = 100;
        int Speed = 100;
        bool MoveLeft = true;

        public MoveRightLeft(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }
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
            if (MoveLeft)
            {
                imagePosition.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                imagePosition.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (imagePosition.X > _graphics.PreferredBackBufferWidth - Character.Width / 2)
            {
                MoveLeft = false;
            }
            else if (imagePosition.X < Character.Width / 2)
            {
                MoveLeft = true;
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(arrowTexture2DList[currentFrame], imagePosition, Color.White);
            
        }
    }
}
