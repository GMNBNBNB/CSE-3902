using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    public partial class PlayerSprite : IPlayer
    {
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentState == MarioState.Dead)
            {
                currentFrame = 1;
                damagedAnimationTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
                if (damagedAnimationTime <= 0)
                {
                    currentFrame = 0;
                    currentState = MarioState.Big;
                    frames = bigLeftFrames;
                    damagedAnimationTime = 0;
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
                    position.X = nextX;
                }
                else if (nextX > screenBounds.Right - 60)
                {
                    nextX = screenBounds.Right - 60;
                    position.X = nextX;
                }
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
                    position.X = nextX;
                }
                else if (nextX > screenBounds.Right - 60)
                {
                    nextX = screenBounds.Right - 60;
                    position.X = nextX;
                }
                if (timeSinceLastFrame >= 100.0)
                {
                    currentFrame++;
                    if (currentFrame >= frames.Length)
                        currentFrame = 0;

                    timeSinceLastFrame = 0;
                }
            }
            if (jumpSpeed != 0 || position.Y < screenBounds.Height - 100)
            {
                jumpSpeed += gravity;
                position.Y += jumpSpeed;

                if (position.Y >= screenBounds.Height - 100)
                {
                    position.Y = screenBounds.Height - 100;
                    jumpSpeed = 0;
                }
            }

            foreach (IProjectiles pro in projectiles)
            {
                pro.Update(gameTime);
            }

        }
    }
    }
