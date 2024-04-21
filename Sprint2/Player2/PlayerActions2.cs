using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static CollisionHelper;

namespace Player2
{
    public partial class PlayerSprite2 : IPlayer
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
            if (FireBallIsActive)
            {
                Vector2 fireballVelocity;
                if (facingRight)
                {
                    fireballVelocity = new Vector2(350, 0);
                }
                else
                {
                    fireballVelocity = new Vector2(-350, 0);
                }
                game.music.playFireball();
                Fireball newFireball = new Fireball(game, texturePro, position, fireballVelocity, GetScreenBounds);
                return newFireball;
            }
            else
            {
                return null;
            }

        }

        public void jump()
        {
            if (currentState != MarioState.Dead && (jumpSpeed == 0 || hasCollidedWithEnemy))
            {
                jumpSpeed = -JumpSpeed;
                hasCollidedWithEnemy = false;
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
                    position.Y = position.Y - this.Bounds.Height / 16;
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
            position.Y = position.Y - this.Bounds.Height / 4;
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
                    game.mario_health.decreaseHealth();
                    game.music.stopMusic();
                    game.music.playDie();
                    break;
                case MarioState.Dead:
                    break;
            }
        }
        public void reset()
        {
            FireBallIsActive = false;
            currentState = MarioState.Small;
            position = game.initialPosition();
            frames = rightFrames;
            currentFrame = 0;
            timeSinceLastFrame = 0;
            damagedAnimationTime = 0;
            isInvincible = true;
            facingRight = true;
        }

        public Vector2 getPosition()
        {
            return position;
        }
        public void setPosition(Vector2 positionS)
        {
            position = positionS;
        }
        public void setSpeed(float speed)
        {
            velocity = speed;
        }
        public void setJumpSpeed(float speed)
        {
            JumpSpeed = speed;
        }
        public void inCave(Boolean incaveS)
        {
            incave = incaveS;
        }
        public MarioState GetMarioState() // used for Mario Life
        {
            return currentState;
        }

        public void setMarioState(MarioState state)
        {
            currentState = state;
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
            float buffer = 2.5f;
            if (Math.Abs(collisionDepthY) > buffer)
            {
                jumpSpeed = 0;
                position.Y -= (collisionDepthY - buffer * Math.Sign(collisionDepthY));
            }
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

        public void ChangeCurrentState()
        {
            currentState = MarioState.Big;
            position.Y = position.Y - this.Bounds.Height;
        }

        public void CheckCollisionWithEnemy(bool IfJump)
        {
            if (IfJump)
            {
                hasCollidedWithEnemy = true;
            }
        }
        public void SetFireBall()
        {
            FireBallIsActive = true;
        }
    }
}
