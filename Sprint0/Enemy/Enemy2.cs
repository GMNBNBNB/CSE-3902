using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Enemy2 : ISprite
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
    public Enemy2(Texture2D texture, Vector2 position, Rectangle screenBounds)
    {
        this.texture = texture;
        this.position = position;
        leftFrames = new Rectangle[4];
        rightFrames = new Rectangle[4];
        leftFrames[0] = new Rectangle(237, 205, 18, 25);
        leftFrames[1] = new Rectangle(218, 205, 18, 25);
        leftFrames[2] = new Rectangle(198, 205, 18, 25);
        leftFrames[3] = new Rectangle(179, 205, 18, 25);
        rightFrames[0] = new Rectangle(256, 205, 18, 25);
        rightFrames[1] = new Rectangle(276, 205, 18, 25);
        rightFrames[2] = new Rectangle(296, 205, 18, 25);
        rightFrames[3] = new Rectangle(314, 205, 18, 25);
        frames = rightFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 200f;
        this.screenBounds = screenBounds;
    }

    public void Update(GameTime gameTime)
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