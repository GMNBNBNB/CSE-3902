﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2.Block
{
    public class BrownBlock3 : IBlock
    {
        private Rectangle currentBlockRect;
        private Texture2D texture2;
        private Vector2 position;
        public BrownBlock3(Texture2D texture, Vector2 position)
        {
            texture2 = texture;
            this.position = position;
        }
        public void Update(GameTime gameTime, IPlayer player)
        {
            currentBlockRect = new Rectangle(320, 112, 16, 16);
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
