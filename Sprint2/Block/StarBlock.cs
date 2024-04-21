using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2.Block
{
    public class StarBlock : IBlock
    {
        private int currentFrame = 0;
        private double timeSinceLastFrame;
        private Rectangle[] frames;
        private Texture2D texture2;
        private Texture2D textureI;
        private Vector2 position;
        private Vector2 MusPosition;
        private Rectangle currentItemRect;
        CollisionHelper.CollisionDirection collisionDirection;
        private bool GetStar = false;
        private Game1 game;
        private ISprite star;
        private bool activeSound;

        public StarBlock(Texture2D texture, Vector2 position, Texture2D textureItem, Game1 game)
        {
            activeSound = true;
            this.game = game;
            texture2 = texture;
            textureI = textureItem;
            this.position = position;
            MusPosition = new Vector2(position.X, position.Y - 38);
            frames = new Rectangle[4];
            frames[0] = new Rectangle(80, 111, 16, 16);
            frames[1] = new Rectangle(96, 111, 16, 16);
            frames[2] = new Rectangle(112, 111, 16, 16);
            frames[3] = new Rectangle(128, 111, 16, 16);
            currentItemRect = new Rectangle(125, 94, 12, 18);
            star = new Star(game, textureItem, MusPosition);

        }
        public void Update(GameTime gameTime, IPlayer player)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastFrame >= 300 && currentFrame != 3)
            {
                currentFrame++;
                if (currentFrame >= 2)
                    currentFrame = 0;

                timeSinceLastFrame = 0;
            }

            if (CollisionDetector.DetectCollision(Bounds, player.Bounds))
            {
                collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, player.Bounds);
                if (collisionDirection == CollisionHelper.CollisionDirection.Top)
                {
                    currentFrame = 3;
                    GetStar = true;
                    if (activeSound)
                    {
                        game.music.playPower();
                        activeSound = false;
                    }
                    else
                    {
                        game.music.playKick();
                    }
                }
            }
            if (GetStar)
            {
                star.Update(gameTime, player);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            if (GetStar)
            {
                star.Draw(spriteBatch);
            }
        }
        public Rectangle Bounds
        {
            get
            {
                Rectangle bounds = new Rectangle(
                     (int)position.X,
                     (int)position.Y,
                     frames[currentFrame].Width * 2,
                     frames[currentFrame].Height * 2);
                return bounds;
            }
        }
    }
}
