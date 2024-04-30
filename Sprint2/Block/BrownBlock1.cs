using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0;

namespace Sprint2.Block
{
    public class BrownBlock1 : IBlock
    {
        private enum BlockState
        {
            Intact,
            Cracked,
            Disappeared
        }
        private Rectangle currentBlockRect;
        private Texture2D texture2;
        private Vector2 position;
        private BlockState state;
        private Game1 game;
        CollisionHelper.CollisionDirection collisionDirection;
        private double cooldownTimer;
        private MarioState currentState;
        private const double CooldownPeriod = 0.2;
        private float originalY;
        private bool isBumping = false;
        private double bumpAnimationTimer = 0;

        public BrownBlock1(Texture2D texture, Vector2 position,Game1 game)
        {
            this.game = game;
            texture2 = texture;
            this.position = position;
            currentBlockRect = new Rectangle(271,112, 16, 16);
            originalY = position.Y;
        }
        public void Update(GameTime gameTime, IPlayer player)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (isBumping)
            {
                bumpAnimationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (bumpAnimationTimer >= 30)
                {
                    position.Y = originalY;
                    isBumping = false;
                    bumpAnimationTimer = 0;
                }
            }

            if (CollisionDetector.DetectCollision(Bounds, player.Bounds))
            {
                collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, player.Bounds);
                if (collisionDirection == CollisionHelper.CollisionDirection.Top)
                {
                    originalY = position.Y;
                    position.Y -= 10;
                    isBumping = true;
                    bumpAnimationTimer = 0;

                    if (player.GetMarioState() == MarioState.Big)
                    {
                        if (cooldownTimer <= 0)
                        {
                            if (state == BlockState.Intact)
                            {
                                currentBlockRect = new Rectangle(304, 111, 16, 16);
                                state = BlockState.Cracked;
                                cooldownTimer = CooldownPeriod;
                                game.music.playKick();
                            }
                            else if (state == BlockState.Cracked)
                            {
                                state = BlockState.Disappeared;
                                cooldownTimer = CooldownPeriod;
                                game.music.playKick();
                            }
                        }
                    }
                    else
                    {
                        game.music.playKick();
                    }
                }
            }

            if (state == BlockState.Disappeared)
            {
                game.DestroyBlocks(this, false);
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
