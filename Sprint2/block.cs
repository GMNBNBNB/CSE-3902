using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2
{
    public class block
    {
        private int currentBlockIndex = 0;
        private Rectangle currentBlockRect;
        private Texture2D texture2;
        private Vector2 position;
        private Rectangle bounds;
        public void changeBlock(int index)
        {
            currentBlockIndex = index;
        }
        public block(Texture2D texture,Vector2 position)
        {
            this.texture2 = texture;
            this.position = position;
        }
        public void Update(GameTime gameTime, IPlayer player)
        {
            currentBlockRect = new Rectangle(currentBlockIndex * 16, 0, 16, 16);
            bounds = new Rectangle(
                     (int)position.X,
                     (int)position.Y,
                     currentBlockRect.Width*2,
                     currentBlockRect.Height*2
                 );
            if (CollisionDetector.DetectCollision(player.Bounds, bounds))
            {
                currentBlockIndex++;
                player.IfCollision();
            }

            }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, position, currentBlockRect, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }
    }
}
