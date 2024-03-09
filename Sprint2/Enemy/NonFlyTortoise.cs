using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
    public NonFlyTortoise(Texture2D texture, Vector2 position, Rectangle screenBounds)
    {
        this.texture = texture;
        this.position = position;
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
        this.velocity = 200f;
        this.screenBounds = screenBounds;
    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;


        if (nextX < screenBounds.Left + 300)
        {
            velocity = -velocity;
            frames = rightFrames;
        }
        else if (nextX > screenBounds.Right - 200)
        {
            velocity = -velocity;
            nextX = screenBounds.Right - 200;
            frames = leftFrames;
        }
        if (timeSinceLastFrame >= 50.0)
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