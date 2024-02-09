using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Missile : IProjectiles
{
    private Vector2 Position;
    private Vector2 Velocity;
    private bool IsActive;
    private Rectangle screenBounds;
    private Rectangle SourceRectangle;
    private Texture2D texture;

    public Missile(Texture2D texture, Vector2 position, Vector2 velocity, Rectangle screenBounds)
    {
        Position = position;
        Velocity = velocity;
        IsActive = true;
        this.screenBounds = screenBounds;
        this.texture = texture;
    }

    public void Update(GameTime gameTime)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Position.X < screenBounds.Left || Position.X > screenBounds.Right - 40)
        {
            IsActive = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Velocity.X < 0)
        {
            SourceRectangle = new Rectangle(227, 332, 20, 18);
        }
        else
        {
            SourceRectangle = new Rectangle(266, 332, 19, 18);
        }

        if (IsActive)
        {
            spriteBatch.Draw(texture, Position, SourceRectangle, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }
    }
}