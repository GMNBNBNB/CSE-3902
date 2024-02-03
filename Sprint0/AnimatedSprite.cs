using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class AnimatedSprite: ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;


    public AnimatedSprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        frames = new Rectangle[2];
        frames[0] = new Rectangle(194, 43, 13, 18);
        frames[1] = new Rectangle(141, 43, 18, 18);
        currentFrame = 0;
        timeSinceLastFrame = 0;
    }

    public void Update(GameTime gameTime) {
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
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
        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
    }
}
