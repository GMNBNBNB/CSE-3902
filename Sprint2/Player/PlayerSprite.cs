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
    private Rectangle[] leftJumpFrames;
    private Rectangle[] rightJumpFrames;
    private Rectangle[] leftWalkFrames;
    private Rectangle[] rightWalkFrames;

    private Rectangle[] bigLeftFrames;
    private Rectangle[] bigRightFrames;
    private Rectangle[] bigLeftJumpFrames;
    private Rectangle[] bigRightJumpFrames;
    private Rectangle[] bigLeftCrouchFrames;
    private Rectangle[] bigRightCrouchFrames;
    private Rectangle[] bigLeftWalkFrames;
    private Rectangle[] bigRightWalkFrames;

    float velocity;
    private Rectangle screenBounds;
    private double damagedAnimationTime;
    private List<object> projectiles;
    private bool facingRight;

    private double moveVelocity = 10;
    float gravity = 0.8f;
    float jumpSpeed;
    private enum MarioState
    {
        Big,
        Small,
        Dead,
        Crouch
    }
    private MarioState currentState;
    public PlayerSprite(Texture2D texture, Vector2 position, Rectangle screenBounds)
    {
        this.texture = texture;
        this.position = position;
        leftFrames = new Rectangle[2];
        rightFrames = new Rectangle[2];
        leftJumpFrames = new Rectangle[3];
        rightJumpFrames = new Rectangle[3];
        leftWalkFrames = new Rectangle[3];
        rightWalkFrames = new Rectangle[3];

        currentState = MarioState.Big;

        leftFrames[0] = new Rectangle(194, 43, 13, 18);
        leftFrames[1] = new Rectangle(12, 45, 16, 16);
        rightFrames[0] = new Rectangle(305, 43, 16, 18);
        rightFrames[1] = new Rectangle(12, 45, 16, 16);

        leftJumpFrames[0] = new Rectangle(140, 43, 16, 16);
        leftJumpFrames[1] = new Rectangle(140, 43, 16, 16);
        leftJumpFrames[2] = new Rectangle(140, 43, 16, 16);

        rightJumpFrames[0] = new Rectangle(354, 43, 16, 16);
        rightJumpFrames[1] = new Rectangle(354, 43, 16, 16);
        rightJumpFrames[2] = new Rectangle(354, 43, 16, 16);

        leftWalkFrames[0] = new Rectangle(175, 43, 16, 16);
        leftWalkFrames[1] = new Rectangle(193, 43, 16, 16);
        leftWalkFrames[2] = new Rectangle(207, 43, 16, 16);

        rightWalkFrames[0] = new Rectangle(305, 43, 16, 16);
        rightWalkFrames[1] = new Rectangle(320, 43, 16, 16);
        rightWalkFrames[2] = new Rectangle(290, 43, 16, 16);

        bigLeftFrames = new Rectangle[2];
        bigRightFrames = new Rectangle[2];
        bigLeftJumpFrames = new Rectangle[2];
        bigRightJumpFrames = new Rectangle[2];
        bigLeftCrouchFrames = new Rectangle[2];
        bigRightCrouchFrames = new Rectangle[2];
        bigLeftWalkFrames = new Rectangle[3];
        bigRightWalkFrames = new Rectangle[3];

        bigLeftFrames[0] = new Rectangle(237, 0, 20, 34);
        bigRightFrames[0] = new Rectangle(258, 0, 17, 34);

        bigLeftJumpFrames[0] = new Rectangle(126, 0, 19, 34);
        bigLeftJumpFrames[1] = new Rectangle(126, 0, 19, 34);

        bigLeftCrouchFrames[0] = new Rectangle(219, 9, 18, 25);
        bigLeftCrouchFrames[1] = new Rectangle(219, 9, 18, 25);

        bigLeftWalkFrames[0] = new Rectangle(165, 0, 18, 33);
        bigLeftWalkFrames[1] = new Rectangle(183, 0, 16, 35);
        bigLeftWalkFrames[2] = new Rectangle(201, 0, 17, 35);

        bigRightJumpFrames[0] = new Rectangle(369, 0, 18, 36);
        bigRightJumpFrames[1] = new Rectangle(369, 0, 18, 36);

        bigRightCrouchFrames[0] = new Rectangle(276, 9, 18, 25);
        bigRightCrouchFrames[1] = new Rectangle(276, 9, 18, 25);

        bigRightWalkFrames[0] = new Rectangle(294, 0, 20, 34);
        bigRightWalkFrames[1] = new Rectangle(312, 0, 17, 34);
        bigRightWalkFrames[2] = new Rectangle(330, 0, 19, 35);

        frames = bigLeftFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 100f;
        this.screenBounds = screenBounds;
        damagedAnimationTime = 0;
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

    public void moveLeft()
    {
        if (currentState != MarioState.Dead)
        {
            position.X -= (float)moveVelocity;
            if (currentState == MarioState.Big)
            {
                frames = bigLeftWalkFrames;
            }
            else if (currentState == MarioState.Small)
            {
                frames = leftWalkFrames;
            }
            facingRight = false;
        }
    }

    public void moveLeftStop()
    {
        if (currentState == MarioState.Big)
        {
            frames = bigLeftFrames;
        }
        else if (currentState == MarioState.Small)
        {
            frames = leftFrames;
        }
        facingRight = false;
        currentFrame = 0;
    }

    public void moveRight()
    {
        if (currentState != MarioState.Dead)
        {
            position.X += (float)moveVelocity;
            if (currentState == MarioState.Big)
            {
                frames = bigRightWalkFrames;
            }
            else if (currentState == MarioState.Small)
            {
                frames = rightWalkFrames;
            }
            facingRight = true;
        }
    }

    public void moveRightStop()
    {
        if (currentState == MarioState.Big)
        {
            frames = bigRightFrames;
        }
        else if (currentState == MarioState.Small)
        {
            frames = rightFrames;
        }
        facingRight = true;
        currentFrame = 0;
    }

    public void jump()
    {
        if (currentState != MarioState.Dead && jumpSpeed == 0)
        {
            jumpSpeed = -15;
            if (frames == leftFrames)
            {
                frames = leftJumpFrames;
            }
            else if (frames == rightFrames)
            {
                frames = rightJumpFrames;
            }
            else if (frames == bigLeftFrames)
            {
                frames = bigLeftJumpFrames;
            }
            else if (frames == bigRightFrames)
            {
                frames = bigRightJumpFrames;
            }
            else return;
            currentFrame = 0;
            timeSinceLastFrame = 0;
        }
    }

    public void crouch()
    {
        if (currentState != MarioState.Dead)
        {
            if (currentState == MarioState.Big)
            {
                if (frames == bigLeftFrames)
                {
                    frames = bigLeftCrouchFrames;
                }
                else if (frames == bigRightFrames)
                {
                    frames = bigRightCrouchFrames;
                }
                else return;
                currentFrame = 0;
                timeSinceLastFrame = 0;
            }
        }
    }

    public void crouchStop()
    {
        if (frames == bigLeftCrouchFrames)
        {
            frames = bigLeftFrames;
        }
        else if (frames == bigRightCrouchFrames)
        {
            frames = bigRightFrames;
        }
        else return;
        currentFrame = 0;
        timeSinceLastFrame = 0;
    }

    public void damaged()
    {
        switch (currentState)
        {
            case MarioState.Big:
                currentState = MarioState.Small;
                frames = facingRight ? rightFrames : leftFrames;
                break;
            case MarioState.Small:
                currentState = MarioState.Dead;
                damagedAnimationTime = 500;
                break;
            case MarioState.Dead:
                frames = facingRight ? bigRightFrames : bigLeftFrames;
                break;
        }
    }

    public void reset()
    {
        currentState = MarioState.Big;
        position = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2);
        frames = bigLeftFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        damagedAnimationTime = 0;
        facingRight = false;
        projectiles = new List<object>();
    }

    public void Update(GameTime gameTime)
    {
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
        float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (currentState == MarioState.Dead)
        {
            currentFrame = 1;
            damagedAnimationTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (damagedAnimationTime <= 0)
            {
                currentFrame = 0;
                currentState = MarioState.Big;
                frames = bigLeftFrames;
                damagedAnimationTime = 0;
            }
        }

        if (frames == leftJumpFrames || frames == rightJumpFrames)
        {
            switch (currentFrame)
            {
                case < 2 when timeSinceLastFrame > 300:
                    currentFrame++;
                    timeSinceLastFrame = 0;
                    break;
                case 2:
                    currentFrame = 0;
                    frames = frames == leftJumpFrames ? leftFrames : rightFrames;
                    break;
            }
        }

        if (frames == bigLeftJumpFrames || frames == bigRightJumpFrames)
        {
            switch (currentFrame)
            {
                case < 1 when timeSinceLastFrame > 300:
                    currentFrame++;
                    timeSinceLastFrame = 0;
                    break;
                case 1:
                    currentFrame = 0;
                    frames = frames == bigLeftJumpFrames ? bigLeftFrames : bigRightFrames;
                    break;
            }
        }

        if (frames == bigLeftCrouchFrames || frames == bigRightCrouchFrames)
        {
            if (timeSinceLastFrame >= 100.0)
            {
                currentFrame++;
                if (currentFrame >= frames.Length)
                    currentFrame = 0;

                timeSinceLastFrame = 0;
            }
        }

        if (frames == leftWalkFrames || frames == rightWalkFrames)
        {
            if (nextX < screenBounds.Left)
            {
                nextX = screenBounds.Left;
                position.X = nextX;
            }
            else if (nextX > screenBounds.Right - 60)
            {
                nextX = screenBounds.Right - 60;
                position.X = nextX;
            }
            if (timeSinceLastFrame >= 100.0)
            {
                currentFrame++;
                if (currentFrame >= frames.Length)
                    currentFrame = 0;

                timeSinceLastFrame = 0;
            }

        }

        if (frames == bigLeftWalkFrames || frames == bigRightWalkFrames)
        {
            if (nextX < screenBounds.Left)
            {
                nextX = screenBounds.Left;
                position.X = nextX;
            }
            else if (nextX > screenBounds.Right - 60)
            {
                nextX = screenBounds.Right - 60;
                position.X = nextX;
            }
            if (timeSinceLastFrame >= 100.0)
            {
                currentFrame++;
                if (currentFrame >= frames.Length)
                    currentFrame = 0;

                timeSinceLastFrame = 0;
            }
        }
        if (jumpSpeed != 0 || position.Y < screenBounds.Height - 100)
        {
            jumpSpeed += gravity;
            position.Y += jumpSpeed;

            if (position.Y >= screenBounds.Height - 100)
            {
                position.Y = screenBounds.Height - 100;
                jumpSpeed = 0;
            }
        }

        foreach (IProjectiles pro in projectiles)
        {
            pro.Update(gameTime);
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        foreach (IProjectiles pro in projectiles)
        {
            pro.Draw(spriteBatch);
        }
    }
}