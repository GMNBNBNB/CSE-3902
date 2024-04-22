using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2.Block
{
    public class TimeBlock
    {
        private int currentBlockIndex = 0;
        private Rectangle currentBlockRect;
        private Texture2D texture2;
        private Vector2 position, p2;
        private SpriteFont font;
        private int time_left;
        private double timeSinceLastFrame;

        public TimeBlock(Texture2D texture,SpriteFont font)
        {
            texture2 = texture;
            position = new Vector2(400, 0);
            p2 = position + new Vector2(100, 0);
            time_left = 120;
            this.font = font;
        }
        public void Update(GameTime gameTime)
        {

            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastFrame >= 1000)
            {
                time_left -= 1;
                timeSinceLastFrame = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            spriteBatch.DrawString(font, "Time left:", position, Color.Black);
            spriteBatch.DrawString(font, time_left.ToString(), p2, Color.Black);
            spriteBatch.End();
        }
        public Rectangle Bounds
        {
            get
            {
                Rectangle bounds = new Rectangle(
                     (int)position.X,
                     (int)position.Y,
                     currentBlockRect.Width * 2,
                     currentBlockRect.Height * 2);
                return bounds;
            }
        }
    }
}