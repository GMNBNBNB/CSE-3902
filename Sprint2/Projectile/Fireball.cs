using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Sprint0;
using Sprint2;

public class Fireball : IProjectiles
{
    private Vector2 Position;
    private Vector2 Velocity;
    private bool IsActive;
    private Rectangle screenBounds;
    private Rectangle[] frames;
    private Texture2D texture;
    private int currentFrame;
    private double timeSinceLastFrame;
    private FireballCollision fireballCollision;
    private Game1 game;
    private float moveRangeL;
    private float moveRangeR;

    public Fireball(Game1 game, Texture2D texture, Vector2 position, Vector2 velocity, Rectangle screenBounds)
    {
        this.game = game;
        Position = position;
        Velocity = velocity;
        IsActive = true;
        this.screenBounds = screenBounds;
        frames = new Rectangle[4];
        frames[0] = new Rectangle(25, 149, 10, 10);
        frames[1] = new Rectangle(40, 149, 10, 10);
        frames[2] = new Rectangle(25, 163, 10, 10);
        frames[3] = new Rectangle(40, 164, 10, 10);
        currentFrame = 0;
        this.texture = texture;
        fireballCollision = new FireballCollision(this);
        moveRangeL = position.X - 300;
        moveRangeR = position.X + 300;
    }

    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                frames[currentFrame].Width * 3,
                frames[currentFrame].Height * 3
            );

            return bounds;
        }
    }



    public void Update(GameTime gameTime, List<ISprite> enemies, IPlayer player, List<IBlock> blocks)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
        if (enemies.Count > 0)
        {

            if (!IsActive)
            {
                return;
            }
            ISprite toRemove = null;
            foreach (ISprite e in enemies)
            {
                if (fireballCollision.FireballHitEnemy(e))
                {
                    game.music.playFireworks();
                    IsActive = false;
                    toRemove = e;
                    break;
                }
            }
            foreach (IBlock b in blocks)
            {
                if (fireballCollision.FireballHitBlock(b))
                {
                    game.music.playFireworks();
                    IsActive = false;
                    break;
                }
            }
            if (toRemove != null)
            {
                enemies.Remove(toRemove);
            }
        }

        if (Position.X < moveRangeL || Position.X > moveRangeR)
        {
            IsActive = false;
        }

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
        if (IsActive)
        {
            spriteBatch.Draw(texture, Position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }
    }
}