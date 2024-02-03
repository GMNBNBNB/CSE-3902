using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class MovingSprite : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    float velocity;
    private Rectangle screenBounds;
    public MovingSprite(Texture2D texture, Vector2 position, Rectangle screenBounds)
    {
        this.texture = texture;
        this.position = position;
        this.velocity = 300f;
        this.screenBounds = screenBounds;
    }

    public void Update(GameTime gameTime)
    {
        float nextY = position.Y + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (nextY < screenBounds.Top)
        {
            velocity = -velocity;
            nextY = screenBounds.Top;
        }
        else if (nextY > screenBounds.Bottom - 40)
        {
            velocity = -velocity;
            nextY = screenBounds.Bottom - 40;
        }

        position.Y = nextY;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(12, 45, 16, 16);
        spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
    }
}
