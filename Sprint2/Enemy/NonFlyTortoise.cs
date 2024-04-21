using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using Sprint2.Block;
using System.Collections.Generic;

public class NonFlyTortoise : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;
    private Rectangle[] leftFrames;
    private Rectangle[] rightFrames;
    float velocity;
    private Rectangle screenBounds;
    List<IBlock> block;

    private float moveRangeStart;
    private float moveRangeEnd;
    public NonFlyTortoise(Texture2D texture, Vector2 position, Rectangle screenBounds, Game1 game, List<IBlock> block)
    {
        this.texture = texture;
        this.position = position;
        this.block = block;
        leftFrames = new Rectangle[3];
        rightFrames = new Rectangle[3];
        leftFrames[0] = new Rectangle(86, 89, 21, 24);
        leftFrames[1] = new Rectangle(117, 89, 21, 24);
        leftFrames[2] = new Rectangle(148, 89, 21, 24);
        rightFrames[0] = new Rectangle(177, 89, 21, 25);
        rightFrames[1] = new Rectangle(208, 89, 21, 25);
        rightFrames[2] = new Rectangle(238, 89, 21, 25);

        frames = rightFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 250f;
        this.screenBounds = screenBounds;

        moveRangeStart = position.X - 300;
        moveRangeEnd = position.X + 300;
    }

    public NonFlyTortoise(Texture2D enemyAttack, Vector2 enemyPosition, Rectangle rectangle)
    {
        EnemyAttack = enemyAttack;
        EnemyPosition = enemyPosition;
        Rectangle = rectangle;
    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;

        foreach (IBlock block in block)
        {
            if (nextX < moveRangeStart || CollisionDetector.DetectCollision(new Rectangle(
                    (int)nextX,
                    (int)position.Y,
                    frames[currentFrame].Width * 2,
                    frames[currentFrame].Height * 2), block.Bounds))
            {
                velocity = -velocity;
                frames = rightFrames;
            }
            else if (nextX > moveRangeEnd || CollisionDetector.DetectCollision(new Rectangle(
                    (int)nextX,
                    (int)position.Y,
                    frames[currentFrame].Width * 2,
                    frames[currentFrame].Height * 2), block.Bounds))
            {
                velocity = -velocity;
                frames = leftFrames;
            }
            if (timeSinceLastFrame >= 50.0)
            {
                currentFrame++;
                if (currentFrame >= frames.Length)
                    currentFrame = 0;

                timeSinceLastFrame = 0;
            }
        }

        position.X = nextX;
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