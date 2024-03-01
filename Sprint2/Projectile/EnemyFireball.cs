using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Sprint0;

public class EnemyFireball : IProjectiles
{
    private Vector2 Position;
    private Vector2 Velocity;
    private bool IsActive;
    private Rectangle screenBounds;
    private Rectangle[] frames;
    private Rectangle[] leftFrames;
    private Rectangle[] rightFrames;
    private Texture2D texture;
    private int currentFrame;
    private double timeSinceLastFrame;
    private EnemyFireballCollision enemyFireballCollision;

    public EnemyFireball(Texture2D texture, Vector2 position, Vector2 velocity, Rectangle screenBounds)
    {
        Position = position;
        Velocity = velocity;
        IsActive = true;
        this.screenBounds = screenBounds;
        leftFrames = new Rectangle[2];
        rightFrames = new Rectangle[2];
        leftFrames[0] = new Rectangle(100, 252, 24, 9);
        leftFrames[1] = new Rectangle(130, 252, 25, 8);
        rightFrames[0] = new Rectangle(160, 252, 26, 10);
        rightFrames[1] = new Rectangle(190, 252, 25, 8);
        currentFrame = 0;
        if (Velocity.X<0) {
            frames = leftFrames;
        }
        else
        {
            frames = rightFrames;
        }
        this.texture = texture;
        enemyFireballCollision = new EnemyFireballCollision(this);
    }

    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                frames[currentFrame].Width,
                frames[currentFrame].Height
            );

            return bounds;
        }
    }



    public void Update(GameTime gameTime, Queue<ISprite> enemies, IPlayer player)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (enemyFireballCollision.EnemyFireballHitMario(player))
        {
            player.damaged(gameTime);
        }

        if (Position.X < screenBounds.Left || Position.X > screenBounds.Right - 40)
        {
            IsActive = false;
        }

        if (timeSinceLastFrame >= 100.0)
        {
            currentFrame++;
            if (currentFrame >= frames.Length)
                currentFrame = 0;

            timeSinceLastFrame = 0;
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            spriteBatch.Draw(texture, Position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }
    }
}