﻿using Microsoft.Xna.Framework;
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
        private Vector2 MusPosition;
        private Rectangle currentItemRect;
        CollisionHelper.CollisionDirection collisionDirection;
        private bool GetCoin = false;
        private Game1 game;
        private ISprite Coin;
        private bool activeSound; private float originalY;
        private bool isBumping = false;
        private double bumpAnimationTimer = 0;

        public CoinBlock(Texture2D texture, Vector2 position, Texture2D textureItem, Game1 game)
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
            Coin = new Coin(game, textureItem, MusPosition);
            originalY = position.Y;

        }
        public void Update(GameTime gameTime, IPlayer player)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastFrame >= 600 && currentFrame != 3)
            {
                currentFrame++;
                if (currentFrame >= 2)
                    currentFrame = 0;
                timeSinceLastFrame = 0;
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
                    currentFrame = 3;
                    GetCoin = true;
                    if (activeSound)
                    {
                        game.music.playPower();
                        activeSound = false;
                    }
                    else
                    {
                        game.music.playKick();
                    }

                    originalY = position.Y;
                    position.Y -= 10;
                    isBumping = true;
                    bumpAnimationTimer = 0;
                }
            }
            if (GetCoin)
            {
                Coin.Update(gameTime, player);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            if (GetCoin)
            {
                Coin.Draw(spriteBatch);
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
