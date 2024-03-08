using Microsoft.Xna.Framework;
using Sprint0;


namespace Player
{
    public partial class PlayerSprite : IPlayer
    {
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            float nextX = position.X + (facingRight ? velocity * (float)gameTime.ElapsedGameTime.TotalSeconds : -velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            var lastPositionX = position.X;

            if (isInvincible && (gameTime.TotalGameTime - lastDamagedTime > invincibleDuration))
            {
                isInvincible = false;
            }

            if (currentState == MarioState.Dead)
            {
                damagedAnimationTime -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (damagedAnimationTime <= 0)
                {
                    currentFrame = 0;
                    currentState = MarioState.Big;
                    facingRight = true;
                    frames = facingRight ? bigRightFrames : bigLeftFrames;
                    isInvincible = true;
                    lastDamagedTime = gameTime.TotalGameTime;
                    damagedAnimationTime = 0;
                    position = game.initialPosition();
                    map.resetPosition();
                }
                else
                {
                    frames = leftFrames;
                    currentFrame = 1;
                }
            }

            if (frames == leftJumpFrames || frames == rightJumpFrames)
            {
                switch (currentFrame)
                {
                    case < 2 when timeSinceLastFrame > 300:
                        currentFrame++;
                        timeSinceLastFrame = 0;
                        break;
                    case 2:
                        currentFrame = 0;
                        frames = frames == leftJumpFrames ? leftFrames : rightFrames;
                        break;
                }
            }

            if (frames == bigLeftJumpFrames || frames == bigRightJumpFrames)
            {
                switch (currentFrame)
                {
                    case < 1 when timeSinceLastFrame > 300:
                        currentFrame++;
                        timeSinceLastFrame = 0;
                        break;
                    case 1:
                        currentFrame = 0;
                        frames = frames == bigLeftJumpFrames ? bigLeftFrames : bigRightFrames;
                        break;
                }
            }

            if (frames == bigLeftCrouchFrames || frames == bigRightCrouchFrames)
            {
                if (timeSinceLastFrame >= 100.0)
                {
                    currentFrame++;
                    if (currentFrame >= frames.Length)
                        currentFrame = 0;

                    timeSinceLastFrame = 0;
                }
            }

            if (frames == leftWalkFrames || frames == rightWalkFrames)
            {
                if (nextX < screenBounds.Left)
                {
                    nextX = screenBounds.Left;
                }
                //else if (nextX > screenBounds.Right - 60)
                //{
                //    nextX = screenBounds.Right - 60;                   
                //}
                position.X = nextX;
                if (timeSinceLastFrame >= 100.0)
                {
                    currentFrame++;
                    if (currentFrame >= frames.Length)
                        currentFrame = 0;

                    timeSinceLastFrame = 0;
                }

            }

            if (frames == bigLeftWalkFrames || frames == bigRightWalkFrames)
            {
                if (nextX < screenBounds.Left)
                {
                    nextX = screenBounds.Left;
                }
                //else if (nextX > screenBounds.Right - 60)
                //{
                //    nextX = screenBounds.Right - 60;
                //}
                position.X = nextX;
                if (timeSinceLastFrame >= 100.0)
                {
                    currentFrame++;
                    if (currentFrame >= frames.Length)
                        currentFrame = 0;

                    timeSinceLastFrame = 0;
                }
            }

            //if (game.Level() != 2)
            //{
            //    if (lastPositionX - position.X != 0)
            //    {
            //         Console.WriteLine("lastPositionX: " + lastPositionX + " position.X: " + position.X);
            //        if (map.Update(gameTime, facingRight, position, new Vector2(velocity, 0)))
            //            position.X = lastPositionX;
            //    }
            //}

            if (jumpSpeed != 0 || position.Y <= screenBounds.Height - this.Bounds.Height - 60)
            {
                jumpSpeed += gravity;
                position.Y += jumpSpeed;

                if (position.Y >= screenBounds.Height - this.Bounds.Height - 60)
                {
                    position.Y = screenBounds.Height - this.Bounds.Height - 60;
                    jumpSpeed = 0;
                }
            }
            
        }
    }
}
