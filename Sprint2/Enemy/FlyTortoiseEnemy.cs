﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using Sprint2.Block;
using System;
using System.Collections.Generic;
using static CollisionHelper;


public class FlyTortoiseEnemy : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;
    private Rectangle[] leftFrames;
    private Rectangle[] rightFrames;
    private Rectangle[] DideFrames;
    private float velocity;
    private Rectangle screenBounds;

    List<IBlock> block;
    private float moveRangeStart;
    private float moveRangeEnd;
    CollisionHelper.CollisionDirection collisionDirection;
    private Game1 game;

    private float verticalVelocity;
    private float verticalMoveRangeStart;
    private float verticalMoveRangeEnd;
    private enum State
    {
        Walking,
        Dying
    }

    private State currentState;

    private double deathAnimationDuration = 1000;
    private double deathAnimationTime;

    public FlyTortoiseEnemy(Texture2D texture, Vector2 position, Rectangle screenBounds, Game1 game, List<IBlock> block)
    {
        this.texture = texture;
        this.position = position;

        leftFrames = new Rectangle[2];
        rightFrames = new Rectangle[2];

        DideFrames = new Rectangle[2];

        DideFrames[0] = new Rectangle(329, 63, 16, 16);
        DideFrames[1] = new Rectangle(359, 64, 16, 16);


        leftFrames[0] = new Rectangle(88, 59, 18, 25);
        leftFrames[1] = new Rectangle(118, 59, 18, 25);

        rightFrames[0] = new Rectangle(298, 59, 18, 25);
        rightFrames[1] = new Rectangle(267, 59, 18, 25); // width, height

        frames = rightFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 50f;
        this.verticalVelocity = 30f;
        this.screenBounds = screenBounds;

        this.block = block;
        this.game = game;
        moveRangeStart = position.X - 150;
        moveRangeEnd = position.X + 150;
      
        verticalMoveRangeEnd = position.Y;
    }


    public FlyTortoiseEnemy(Texture2D enemyAttack, Vector2 enemyPosition, Rectangle rectangle)
    {
        EnemyAttack = enemyAttack;
        EnemyPosition = enemyPosition;
        Rectangle = rectangle;
    }
    public void Update(GameTime gameTime, IPlayer p)
    {
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
        if (currentState == State.Dying)
        {
            if (timeSinceLastFrame >= 300)
            {
                currentFrame = (currentFrame + 1) % frames.Length;
                timeSinceLastFrame = 0;
            }
            deathAnimationTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            currentFrame = (currentFrame + 1) % frames.Length;
            if (deathAnimationTime >= deathAnimationDuration)
            {
                game.DestroyEnemy(this);
                return;
            }
        }
        else
        {
            Random rand = new Random();
            float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            float nextY = position.Y + verticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool collisionOccurred = false;

            foreach (IBlock block in block)
            {
                Rectangle nextPosition = new Rectangle((int)nextX, (int)position.Y, frames[currentFrame].Width * 2, frames[currentFrame].Height * 2); //

                if (nextX < moveRangeStart || CollisionDetector.DetectCollision(nextPosition, block.Bounds))
                {
                    collisionOccurred = true;
                    velocity = -velocity;
                }
                else if (nextX > moveRangeEnd || CollisionDetector.DetectCollision(nextPosition, block.Bounds))
                {
                    collisionOccurred = true;
                    velocity = -velocity;
                }
                if (velocity > 0)
                {
                    frames = rightFrames;
                }
                else
                {
                    frames = leftFrames;
                }
                break;
            }

            if (nextY < verticalMoveRangeStart || nextY > verticalMoveRangeEnd)
            {
                verticalVelocity = -verticalVelocity;
            }

            if (!collisionOccurred)
            {
                position.X = nextX;
                position.Y = nextY;
            }          
            if (timeSinceLastFrame >= 300)
            {
                verticalMoveRangeStart = rand.Next(100, 450);
                currentFrame = (currentFrame + 1) % frames.Length;
                timeSinceLastFrame = 0;
            }

            if (CollisionDetector.DetectCollision(Bounds, p.Bounds) && p.GetMarioState() != MarioState.Dead)
            {
                collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, p.Bounds);
                if (collisionDirection == CollisionHelper.CollisionDirection.Bottom)
                {
                    frames = DideFrames;
                    currentState = State.Dying;
                    game.score_point.increaseEnemyScore();
                    p.CheckCollisionWithEnemy(true);
                    p.jump();
                    game.music.playStomp();
                }
            }
        }
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
    }
    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                 (int)position.X,
                 (int)position.Y,
                 frames[currentFrame].Width * 2,
                 frames[currentFrame].Height * 2
             );

            return bounds;
        }
    }

    public Texture2D EnemyAttack { get; }
    public Vector2 EnemyPosition { get; }
    public Rectangle Rectangle { get; }

}