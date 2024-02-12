using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Coin : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;


    public Coin(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        frames = new Rectangle[4];
        frames[0] = new Rectangle(125, 92, 12, 18);
        frames[1] = new Rectangle(157, 92, 9, 18);
        frames[2] = new Rectangle(189, 93, 4, 18);
        frames[3] = new Rectangle(218, 93, 7, 18);
        currentFrame = 0;
        timeSinceLastFrame = 0;
    }

    public void Update(GameTime gameTime)
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
        Rectangle destinationRectangle = new Rectangle(600, 150, 20, 38);
        spriteBatch.Draw(texture, destinationRectangle, frames[currentFrame], Color.White);
    }
}
