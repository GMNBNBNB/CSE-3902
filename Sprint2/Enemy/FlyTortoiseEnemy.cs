using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using Sprint2.Block;
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
    private float velocity;
    private Rectangle screenBounds;

    private float moveRangeStart;
    private float moveRangeEnd;
    CollisionHelper.CollisionDirection collisionDirection;
    List<IBlock> block;
    private Game1 game;
    public FlyTortoiseEnemy(Texture2D texture, Vector2 position, Rectangle screenBounds, Game1 game, List<IBlock> block)
    {
        this.texture = texture;
        this.position = position;
        this.screenBounds = screenBounds;
        this.game = game;
        this.block = block;
        leftFrames = new Rectangle[4];
        rightFrames = new Rectangle[4];
        leftFrames[0] = new Rectangle(178, 0, 18, 25);
        leftFrames[1] = new Rectangle(147, 0, 18, 25);
        leftFrames[2] = new Rectangle(116, 0, 18, 25);
        leftFrames[3] = new Rectangle(88, 0, 18, 25);
        rightFrames[0] = new Rectangle(208, 0, 18, 25);
        rightFrames[1] = new Rectangle(238, 0, 18, 25);
        rightFrames[2] = new Rectangle(267, 0, 18, 25);
        rightFrames[3] = new Rectangle(298, 0, 18, 25);
        frames = rightFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 200f;
        this.screenBounds = screenBounds;
        moveRangeStart = position.X - 300;
        moveRangeEnd = position.X + 300;
    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;

        bool collisionOccurred = false;

        foreach (IBlock block in block)
        {
            Rectangle nextPosition = new Rectangle(
                (int)nextX,
                (int)position.Y,
                frames[currentFrame].Width * 2,
                frames[currentFrame].Height * 2);

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
        if (!collisionOccurred)
        {
            position.X = nextX;
        }

        if (CollisionDetector.DetectCollision(Bounds, p.Bounds))
        {
            collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, p.Bounds);
            if (collisionDirection == CollisionHelper.CollisionDirection.Bottom)
            {
                p.CheckCollisionWithEnemy(true);
                p.jump();
                game.DestroyEnemy(this);
                game.music.playStomp();
            }
        }

        if (timeSinceLastFrame >= 200)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            timeSinceLastFrame = 0;
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