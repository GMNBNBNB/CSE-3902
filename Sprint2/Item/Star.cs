using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0;


public class Star : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;
    private Game1 game;
    private bool IsActive;

    public Star(Game1 game, Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.game = game;
        frames = new Rectangle[4];
        frames[0] = new Rectangle(2, 92, 18, 19);
        frames[1] = new Rectangle(32, 92, 18, 19);
        frames[2] = new Rectangle(62, 92, 18, 19);
        frames[3] = new Rectangle(92, 92, 18, 19);
        currentFrame = 0;
        timeSinceLastFrame = 0;
        IsActive = true;
    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        if (!IsActive) return;
         
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
        if (timeSinceLastFrame >= 200)
        {
            currentFrame++;
            if (currentFrame >= frames.Length)
                currentFrame = 0;

            timeSinceLastFrame = 0;
        }
        if (CollisionDetector.DetectCollision(Bounds, p.Bounds))
        {
            game.mario_health.SetInvincible();
            IsActive = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;

        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
    }
    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                frames[currentFrame].Width,
                frames[currentFrame].Height
            );

            return bounds;
        }
    }
}
