using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Player
{
    public partial class PlayerSprite : IPlayer
    {
        public void moveLeft()
        {
            if (currentState != MarioState.Dead)
            {
                position.X -= (float)moveVelocity;
                if (currentState == MarioState.Big)
                {
                    frames = bigLeftWalkFrames;
                }
                else if (currentState == MarioState.Small)
                {
                    frames = leftWalkFrames;
                }
                facingRight = false;
            }
        }

        public void moveLeftStop()
        {
            if (currentState == MarioState.Big)
            {
                frames = bigLeftFrames;
            }
            else if (currentState == MarioState.Small)
            {
                frames = leftFrames;
            }
            facingRight = false;
            currentFrame = 0;
        }
        public void moveRight()
        {
            if (currentState != MarioState.Dead)
            {
                position.X += (float)moveVelocity;
                if (currentState == MarioState.Big)
                {
                    frames = bigRightWalkFrames;
                }
                else if (currentState == MarioState.Small)
                {
                    frames = rightWalkFrames;
                }
                facingRight = true;
            }
        }
        public void moveRightStop()
        {
            if (currentState == MarioState.Big)
            {
                frames = bigRightFrames;
            }
            else if (currentState == MarioState.Small)
            {
                frames = rightFrames;
            }
            facingRight = true;
            currentFrame = 0;
        }
        public Fireball fireball()
        {

            Vector2 fireballVelocity;
            if (facingRight)
            {
                fireballVelocity = new Vector2(300, 0);
            }
            else
            {
                fireballVelocity = new Vector2(-300, 0);
            }
            Fireball newFireball = new Fireball(texturePro, position, fireballVelocity, screenBounds);
            return newFireball;

        }

        public void jump()
        {
            if (currentState != MarioState.Dead && jumpSpeed == 0)
            {
                jumpSpeed = -15;
                if (frames == leftFrames)
                {
                    frames = leftJumpFrames;
                }
                else if (frames == rightFrames)
                {
                    frames = rightJumpFrames;
                }
                else if (frames == bigLeftFrames)
                {
                    frames = bigLeftJumpFrames;
                }
                else if (frames == bigRightFrames)
                {
                    frames = bigRightJumpFrames;
                }
                else return;
                currentFrame = 0;
                timeSinceLastFrame = 0;
            }
        }

        public void crouch()
        {
            if (currentState != MarioState.Dead)
            {
                if (currentState == MarioState.Big)
                {
                    if (frames == bigLeftFrames)
                    {
                        frames = bigLeftCrouchFrames;
                    }
                    else if (frames == bigRightFrames)
                    {
                        frames = bigRightCrouchFrames;
                    }
                    else return;
                    currentFrame = 0;
                    timeSinceLastFrame = 0;
                }
            }
        }

        public void crouchStop()
        {
            if (frames == bigLeftCrouchFrames)
            {
                frames = bigLeftFrames;
            }
            else if (frames == bigRightCrouchFrames)
            {
                frames = bigRightFrames;
            }
            else return;
            currentFrame = 0;
            timeSinceLastFrame = 0;
        }

        public void damaged(GameTime gameTime)
        {
            if (isInvincible) return;

            if (gameTime.TotalGameTime - lastDamagedTime < invincibleDuration)
            {
                return;
            }
            lastDamagedTime = gameTime.TotalGameTime;

            switch (currentState)
            {
                case MarioState.Big:
                    currentState = MarioState.Small;
                    frames = facingRight ? rightFrames : leftFrames;
                    isInvincible = true;
                    break;
                case MarioState.Small:
                    currentState = MarioState.Dead;
                    damagedAnimationTime = 500;
                    break;
                case MarioState.Dead:
                    break;
            }
        }
        public void reset()
        {
            currentState = MarioState.Big;
            position = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2);
            frames = bigLeftFrames;
            currentFrame = 0;
            timeSinceLastFrame = 0;
            damagedAnimationTime = 0;
            facingRight = false;
        }

        public void IfCollision()
        {
            Vector2 LastPosition = position;
        }
    }
}
