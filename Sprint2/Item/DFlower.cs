using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0;

public class DFlower : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;
    private Game1 game;


    public DFlower(Game1 game,Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        this.game = game;
        frames = new Rectangle[4];
        frames[0] = new Rectangle(122, 61, 19, 20);
        frames[1] = new Rectangle(152, 61, 19, 20);
        frames[2] = new Rectangle(182, 61, 19, 20);
        frames[3] = new Rectangle(212, 61, 19, 20);
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
        if (CollisionDetector.DetectCollision(Bounds, p.Bounds))
        {
            p.ChangeCurrentState();
            game.music.playCoin();
            game.DestroyItem(this);
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
