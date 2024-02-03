using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class AnimatedMovingSprite : ISprite
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
    public AnimatedMovingSprite(Texture2D texture, Vector2 position, Rectangle screenBounds)
    {
        this.texture = texture;
        this.position = position;
        leftFrames = new Rectangle[2];
        rightFrames = new Rectangle[2];
        leftFrames[0] = new Rectangle(194, 43, 13, 18);
        leftFrames[1] = new Rectangle(141, 43, 18, 18);
        rightFrames[0] = new Rectangle(305, 43, 14, 18);
        rightFrames[1] = new Rectangle(354, 43, 18, 18);
        frames = rightFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 600f;
        this.screenBounds = screenBounds;
    }

    public void Update(GameTime gameTime)
    {
        float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
        

        if (nextX < screenBounds.Left)
        {
            velocity = -velocity;
            nextX = screenBounds.Top;
            frames = rightFrames;
        }
        else if (nextX > screenBounds.Right - 40)
        {
            velocity = -velocity;
            nextX = screenBounds.Right - 40;
            frames = leftFrames;
        }
        if (timeSinceLastFrame >= 100.0)
        {
                currentFrame++;
                if (currentFrame >= frames.Length)
                    currentFrame = 0;

                timeSinceLastFrame = 0;
        }

        position.X = nextX;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
    }
}