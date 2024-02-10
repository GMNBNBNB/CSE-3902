using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public class PlayerSprite : IPlayer
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;
    private Rectangle[] leftFrames;
    private Rectangle[] rightFrames;
    float velocity;
    private Rectangle screenBounds;
    private double damagedAnimationTime;
    private bool isDamaged;
    private List<object> projectiles;
    private bool facingRight;

    public PlayerSprite(Texture2D texture, Vector2 position, Rectangle screenBounds)
    {
        this.texture = texture;
        this.position = position;
        leftFrames = new Rectangle[2];
        rightFrames = new Rectangle[2];
        leftFrames[0] = new Rectangle(194, 43, 13, 18);
        leftFrames[1] = new Rectangle(12, 45, 16, 16);
        rightFrames[0] = new Rectangle(305, 43, 14, 18);
        rightFrames[1] = new Rectangle(12, 45, 16, 16);
        frames = leftFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 600f;
        this.screenBounds = screenBounds;
        damagedAnimationTime = 0;
        isDamaged = false;
        facingRight = false;
        projectiles = new List<object>();
    }

    public void fireball()
    {
        {
            Vector2 fireballVelocity;
            if (facingRight)
            {
                fireballVelocity = new Vector2(300, 0);
            }
            else
            {
                fireballVelocity = new Vector2(-300, 0);
            }
            Fireball newFireball = new Fireball(texture, position, fireballVelocity, screenBounds);
            projectiles.Add(newFireball);
        }
    }

    public void missile()
    {
        {
            Vector2 missileVelocity;
            if (facingRight)
            {
                missileVelocity = new Vector2(600, 0);
            }
            else
            {
                missileVelocity = new Vector2(-600, 0);
            }
            Missile newMissile = new Missile(texture, position, missileVelocity, screenBounds);
            projectiles.Add(newMissile);
        }
    }

    public void damaged()
    {
        if (!isDamaged)
        {
            isDamaged = true;
            damagedAnimationTime = 500;
        }
    }

    public void Update(GameTime gameTime)
    {
        if (isDamaged)
        {
            currentFrame = 1;
            damagedAnimationTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (damagedAnimationTime <= 0)
            {
                currentFrame = 0;
                isDamaged = false;
                damagedAnimationTime = 0;
            }
        }

        foreach (IProjectiles pro in projectiles)
        {
            pro.Update(gameTime);
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        foreach (IProjectiles pro in projectiles)
        {
            pro.Draw(spriteBatch);
        }
    }
}