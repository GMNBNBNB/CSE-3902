using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2.Block
{
    public class TimeBlock
    {
        private Rectangle currentBlockRect;
        private Texture2D texture2;
        private Vector2 position, p2;
        private SpriteFont font;
        private int time_left;
        private double timeSinceLastFrame;

        float scale_font;

        public TimeBlock(Texture2D texture, SpriteFont font)
        {
            currentBlockRect = new Rectangle(271, 112, 16, 16);
            texture2 = texture;
            position = new Vector2(400, 20);
            p2 = position + new Vector2(130, 0);
            time_left = 120;
            this.font = font;

            scale_font = 1.3f;
        }
        public void Update(GameTime gameTime,Game1 game)
        {

            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastFrame >= 1000)
            {
                time_left -= 1;
                timeSinceLastFrame = 0;
            }
            if(time_left <= 0)
            {
                game.reStart();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            spriteBatch.DrawString(font, "Time left:", position, Color.White, 0f, Vector2.Zero, scale_font, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, time_left.ToString(), p2, Color.White, 0f, Vector2.Zero, scale_font, SpriteEffects.None, 0f);
            //sb.DrawString(font, score_point_string, point_font_position, Color.White, 0f, Vector2.Zero, scale_font, SpriteEffects.None, 0f);
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