using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Leaf : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;


    public Leaf(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        frames = new Rectangle[2];
        frames[0] = new Rectangle(186, 5, 12, 12);
        frames[1] = new Rectangle(213, 1, 17, 20);
        currentFrame = 0;
        timeSinceLastFrame = 0;
    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
        if (timeSinceLastFrame >= 200)
        {
            currentFrame++;
            if (currentFrame >= frames.Length)
                currentFrame = 0;

            timeSinceLastFrame = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
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
