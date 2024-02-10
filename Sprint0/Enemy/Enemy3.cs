using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Enemy3 : ISprite
{
    private Texture2D enemyAttack;
    private Vector2 position;
    private Vector2 fireBallPosition;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] fireBall;
    private Rectangle[] frames;
    private Rectangle Fireframes;
    private Rectangle[] leftFrames;
    private Rectangle[] rightFrames;
    float velocity;
    private Rectangle screenBounds;
    private bool IsActive;
    private float FireSpeed = 300f;
    public Enemy3(Texture2D enemyAttack, Vector2 position, Rectangle screenBounds)
    {
        this.enemyAttack = enemyAttack;
        this.position = position;
        fireBall = new Rectangle[2];
        fireBall[0] = new Rectangle(130, 252, 25, 8);
        fireBall[1] = new Rectangle(190, 252, 25, 8);

        leftFrames = new Rectangle[4];
        rightFrames = new Rectangle[4];
        leftFrames[0] = new Rectangle(120, 210, 34, 33);
        leftFrames[1] = new Rectangle(81, 210, 34, 33);
        leftFrames[2] = new Rectangle(40, 210, 34, 33);
        leftFrames[3] = new Rectangle(1, 210, 34, 33);
        rightFrames[0] = new Rectangle(160, 210, 34, 33);
        rightFrames[1] = new Rectangle(200, 210, 34, 33);
        rightFrames[2] = new Rectangle(240, 210, 34, 33);
        rightFrames[3] = new Rectangle(280, 210, 34, 33);
        frames = rightFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 100f;
        this.screenBounds = screenBounds;
        fireBallPosition = this.position;
    }

    public void Update(GameTime gameTime)
    {
        float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        fireBallPosition.X = fireBallPosition.X - FireSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;

        if (fireBallPosition.X < screenBounds.Left || fireBallPosition.X > screenBounds.Right - 40)
        {
            IsActive = false;
        }

        if (nextX < screenBounds.Left + 300)
        {
            velocity = -velocity;
            frames = rightFrames;
            IsActive = true;
            FireSpeed = -FireSpeed;
            Fireframes = fireBall[0];
            fireBallPosition = position;
        }
        else if (nextX > screenBounds.Right - 200)
        {
            velocity = -velocity;
            nextX = screenBounds.Right - 200;
            frames = leftFrames;
            IsActive = true;
            FireSpeed = -FireSpeed;
            Fireframes = fireBall[1];
            fireBallPosition = position;
        }
        if (timeSinceLastFrame >= 200.0)
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
        spriteBatch.Draw(enemyAttack, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        if (IsActive)
        {
            spriteBatch.Draw(enemyAttack, fireBallPosition, Fireframes, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }
    }
}