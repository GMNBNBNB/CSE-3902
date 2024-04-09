using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;
using System;

namespace Sprint2.Block
{
    public class PipeEn : IBlock
    {
        private Rectangle pipeRect;
        private Texture2D texture2;
        private Vector2 position;
        CollisionHelper.CollisionDirection collisionDirection;
        public PipeEn(Texture2D texture, Vector2 position)
        {
            texture2 = texture;
            this.position = position;
        }
        public void Update(GameTime gameTime, IPlayer player)
        {
            pipeRect = new Rectangle(0, 0, 32,64);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, position, pipeRect, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }
        public Rectangle Bounds
        {
            get
            {
                Rectangle bounds = new Rectangle(
                     (int)position.X,
                     (int)position.Y,
                     pipeRect.Width * 2,
                     pipeRect.Height * 2);
                return bounds;
            }
        }
    }
}
