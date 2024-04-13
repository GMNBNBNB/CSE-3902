using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2.Block
{
    public class CoinBlock : IBlock
    {
        private int currentFrame = 0;
        private double timeSinceLastFrame;
        private Rectangle[] frames;
        private Texture2D texture2;
        private Texture2D textureI;
        private Vector2 position;
        private Vector2 CoinPosition;
        private Rectangle currentItemRect;
        CollisionHelper.CollisionDirection collisionDirection;
        private double cooldownTimer;
        private const double CooldownPeriod = 0.2;
        private bool GetCoin = false;
        private int hitCount = 0;
        private Game1 game;
        public CoinBlock(Texture2D texture, Vector2 position, Texture2D textureItem, Game1 game)
        {
            this.game = game;
            texture2 = texture;
            textureI = textureItem;
            this.position = position;
            CoinPosition = new Vector2(position.X, position.Y - 40);
            frames = new Rectangle[4];
            frames[0] = new Rectangle(80, 111, 16, 16);
            frames[1] = new Rectangle(96, 111, 16, 16);
            frames[2] = new Rectangle(112, 111, 16, 16);
            frames[3] = new Rectangle(128, 111, 16, 16);
            currentItemRect = new Rectangle(125, 94, 12, 18);
        }
        public void Update(GameTime gameTime, IPlayer player)
        {

            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (cooldownTimer <= 0)
            {
                if (CollisionDetector.DetectCollision(Bounds, player.Bounds))
                {
                    if (hitCount < 3)
                    {
                        collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, player.Bounds);
                        if (collisionDirection == CollisionHelper.CollisionDirection.Top)
                        {
                            game.music.playCoin();
                            hitCount++;
                            currentFrame++;
                            if (currentFrame >= frames.Length)
                            {
                                currentFrame = 0;
                            }
                            GetCoin = true;
                            cooldownTimer = CooldownPeriod;
                        }
                    }
                }
            }
            if (GetCoin)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeSinceLastFrame >= 200)
                {
                    GetCoin = false;
                    timeSinceLastFrame = 0;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            if (GetCoin)
            {
                spriteBatch.Draw(textureI, CoinPosition, currentItemRect, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
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
