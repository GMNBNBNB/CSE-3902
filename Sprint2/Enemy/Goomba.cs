using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using Vector2 = Microsoft.Xna.Framework.Vector2;

public class Goomba : ISprite
{
    private Texture2D texture;
    private Vector2 position;

    private int currentFrame;
    private double timeSinceLastFrame;

    private Rectangle[] frames;
    
    float velocity;
    private Rectangle screenBounds;

    private float moveRangeStart;
    private float moveRangeEnd;
    private Game1 game;
    CollisionHelper.CollisionDirection collisionDirection;
    private List<IBlock> block;
    private float FixHight;
    private double deathAnimationDuration = 1000;
    private double deathAnimationTime;
    private enum State
    {
        Walking,
        Dying
    }

    private State currentState;

    public Goomba(Texture2D texture, Vector2 position, Rectangle screenBounds,Game1 game, List<IBlock> block)
    {
        this.texture = texture;
        this.position = position;
        this.game = game;
        this.block = block;

        frames = new Rectangle[3];
        frames[0] = new Rectangle(0, 2, 18, 18);
        frames[1] = new Rectangle(28, 2, 18, 18);
        frames[2] = new Rectangle(59, 7, 16, 9);
        currentFrame = 0;

        timeSinceLastFrame = 0;
        this.velocity = 150;
        this.screenBounds = screenBounds;

        moveRangeStart = position.X - 300;
        moveRangeEnd = position.X + 300;

        FixHight = frames[0].Height * 3 - frames[2].Height * 3;
        currentState = State.Walking;
    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        
        if (currentState == State.Dying)
        {
            deathAnimationTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (deathAnimationTime >= deathAnimationDuration)
            {
                game.DestroyEnemy(this);
                return;
            }
        }
        else
        {
            float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (IBlock block in block)
            {
                if (nextX < moveRangeStart || nextX > moveRangeEnd || CollisionDetector.DetectCollision(new Rectangle(
                    (int)nextX,
                    (int)position.Y,
                    frames[currentFrame].Width * 3,
                    frames[currentFrame].Height * 3), block.Bounds))
                {
                    velocity = -velocity;
                    nextX = MathHelper.Clamp(nextX, moveRangeStart, moveRangeEnd);
                }
            }
            position.X = nextX;
            if (timeSinceLastFrame >= 200)
            {
                currentFrame++;
                if (currentFrame >= frames.Length - 1)
                {
                    currentFrame = 0;
                }
                timeSinceLastFrame = 0;
            }
        }     
        
        if (CollisionDetector.DetectCollision(Bounds, p.Bounds))
        {
            collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, p.Bounds);
            if (collisionDirection == CollisionHelper.CollisionDirection.Bottom)
            {
                currentFrame = 2;
                currentState = State.Dying;
                p.CheckCollisionWithEnemy(true);
                p.jump();
                game.music.playStomp();
                position.Y += FixHight;
            }
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

    }
    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                frames[currentFrame].Width * 3,
                frames[currentFrame].Height * 3
            );

            return bounds;
        }
    }
}