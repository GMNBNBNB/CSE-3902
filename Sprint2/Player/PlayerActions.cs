using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static CollisionHelper;

namespace Player
{
    public partial class PlayerSprite : IPlayer
    {
        public void moveLeft()
        {
            if (currentState != MarioState.Dead)
            {
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
            game.music.playFireball();
            Fireball newFireball = new Fireball(game, texturePro, position, fireballVelocity,GetScreenBounds);
            return newFireball;

        }

        public void jump()
        {
            if (currentState != MarioState.Dead && jumpSpeed == 0)
            {
                jumpSpeed = -18;
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
                    damagedAnimationTime = 2000;
                    game.music.stopMusic();
                    game.music.playDie();
                    break;
                case MarioState.Dead:
                    break;
            }
        }
        public void reset()
        {
            currentState = MarioState.Small;
            position = game.initialPosition();
            frames = rightFrames;
            currentFrame = 0;
            timeSinceLastFrame = 0;
            damagedAnimationTime = 0;
            facingRight = true;
        }


        public void IfCollisionTop(Rectangle rectA, Rectangle rectB)
        {

            float collisionDepthX = CollisionHelper.getX(rectA, rectB);
            float collisionDepthY = CollisionHelper.getY(rectA, rectB);
            jumpSpeed = 0;
            jumpSpeed += gravity;
            position.Y += Math.Min(jumpSpeed, collisionDepthY);
        }

        public void IfCollisionBot(Rectangle rectA, Rectangle rectB)
        {

            float collisionDepthX = CollisionHelper.getX(rectA, rectB);
            float collisionDepthY = CollisionHelper.getY(rectA, rectB);
            jumpSpeed = 0;
            position.Y -= collisionDepthY;
        }

        public void IfCollisionRSide(Rectangle rectA, Rectangle rectB)
        {

            float collisionDepthX = CollisionHelper.getX(rectA, rectB); 
            float collisionDepthY = CollisionHelper.getY(rectA, rectB); 
            position.X -= collisionDepthX;
        }

        public void IfCollisionLSide(Rectangle rectA, Rectangle rectB)
        {

            float collisionDepthX = CollisionHelper.getX(rectA, rectB); 
            float collisionDepthY = CollisionHelper.getY(rectA, rectB); 
            position.X += collisionDepthX;
        }
    }
}
