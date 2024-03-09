using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    public Goomba(Texture2D texture, Vector2 position, Rectangle screenBounds)
    {
        this.texture = texture;
        this.position = position;

        frames = new Rectangle[2];
        frames[0] = new Rectangle(0, 0, 18, 17);
        frames[1] = new Rectangle(23, 0, 24, 18);
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

        if (nextX < moveRangeStart || nextX > moveRangeEnd)
        {
            velocity = -velocity;
            nextX = MathHelper.Clamp(nextX, moveRangeStart, moveRangeEnd);
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