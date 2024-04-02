using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2.Block
{
    public class BrownBlock1 : IBlock
    {
        private Rectangle currentBlockRect;
        private Texture2D texture2;
        private Vector2 position;
        CollisionHelper.CollisionDirection collisionDirection;
        public BrownBlock1(Texture2D texture, Vector2 position)
        {
            texture2 = texture;
            this.position = position;
            currentBlockRect = new Rectangle(271,112, 16, 16);
        }
        public void Update(GameTime gameTime, IPlayer player)
        {
            if (CollisionDetector.DetectCollision(Bounds, player.Bounds))
            {
                collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, player.Bounds);
                if (collisionDirection == CollisionHelper.CollisionDirection.Top)
                {
                    currentBlockRect = new Rectangle(304, 111, 16, 16);
                }


            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, position, currentBlockRect, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
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
