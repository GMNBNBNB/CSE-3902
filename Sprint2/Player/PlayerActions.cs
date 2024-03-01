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
        public void fireball()
        {
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
                Fireball newFireball = new Fireball(texture, position, fireballVelocity, screenBounds);
                projectiles.Add(newFireball);
            }
        }
        public void missile()
        {
            {
                Vector2 missileVelocity;
                if (facingRight)
                {
                    missileVelocity = new Vector2(600, 0);
                }
                else
                {
                    missileVelocity = new Vector2(-600, 0);
                }
                Missile newMissile = new Missile(texture, position, missileVelocity, screenBounds);
                projectiles.Add(newMissile);
            }
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

        public void damaged()
        {
            switch (currentState)
            {
                case MarioState.Big:
                    currentState = MarioState.Small;
                    frames = facingRight ? rightFrames : leftFrames;
                    break;
                case MarioState.Small:
                    currentState = MarioState.Dead;
                    damagedAnimationTime = 500;
                    break;
                case MarioState.Dead:
                    frames = facingRight ? bigRightFrames : bigLeftFrames;
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
            projectiles = new List<object>();
        }

    }
}
