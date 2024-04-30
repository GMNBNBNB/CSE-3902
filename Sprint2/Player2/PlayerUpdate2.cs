using Microsoft.Xna.Framework;
using Sprint0;


namespace Player2
{
    public partial class PlayerSprite2 : IPlayer
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
                FireBallIsActive = false;
                damagedAnimationTime -= gameTime.ElapsedGameTime.TotalMilliseconds;

                if (damagedAnimationTime <= 0)
                {
                    lastDamagedTime = gameTime.TotalGameTime;
                    isDead = false;
                    game.back();
                    game.music.startMusic();
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

            if (frames == leftWalkFrames || frames == rightWalkFrames || frames == leftWalkFramesI || frames == rightWalkFramesI)
            {
                if (!incave)
                {
                    if (nextX < 0)
                    {
                        nextX = 0;
                    }
                    else if (nextX > MapTexture.Width - 50)
                    {
                        nextX = MapTexture.Width - 50;
                    }
                }
                else
                {
                    if (nextX < caveTexture.Width / 2)
                    {
                        nextX = caveTexture.Width / 2;
                    }
                    else if (nextX > (caveTexture.Width * (2.3f)))
                    {
                        nextX = caveTexture.Width * (2.3f);
                    }

                }
                position.X = nextX;
                if (timeSinceLastFrame >= 100.0)
                {
                    currentFrame++;
                    if (currentFrame >= frames.Length)
                        currentFrame = 0;

                    timeSinceLastFrame = 0;
                }

            }

            if (frames == bigLeftWalkFrames || frames == bigRightWalkFrames || frames == bigLeftWalkFramesI || frames == bigRightWalkFramesI)
            {
                if (!incave)
                {
                    if (nextX < 0)
                    {
                        nextX = 0;
                    }
                    else if (nextX > MapTexture.Width - 50)
                    {
                        nextX = MapTexture.Width - 50;
                    }
                }
                else
                {
                    if (nextX < caveTexture.Width / 2)
                    {
                        nextX = caveTexture.Width / 2;
                    }
                    else if (nextX > (caveTexture.Width * (2.3f)))
                    {
                        nextX = caveTexture.Width * (2.3f);
                    }
                }
                position.X = nextX;
                if (timeSinceLastFrame >= 100.0)
                {
                    currentFrame++;
                    if (currentFrame >= frames.Length)
                        currentFrame = 0;

                    timeSinceLastFrame = 0;
                }
            }
            jumpSpeed += gravity;
            position.Y += jumpSpeed;


            if ((position.Y > GetScreenBounds.Height - this.Bounds.Height) && !isDead)
            {
                gravity = 0;
                jumpSpeed = 0;
                position.Y = GetScreenBounds.Height - this.Bounds.Height;
                isDead = true;
                currentState = MarioState.Small;
                damaged(gameTime);
                game.mario_health.UpdateHealthString();
            }

        }
    }
}
