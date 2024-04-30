using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Sprint0;

public class TortoiseEnemy : ISprite
{
    private Texture2D enemyAttack;
    private Vector2 position;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;
    private Rectangle[] leftFrames;
    private Rectangle[] rightFrames;
    float velocity;
    private Rectangle screenBounds;
    private Vector2 FireSpeed;
    private List<object> projectiles;
    private Game1 game;

    List<IBlock> block;
    private float moveRangeStart;
    private float moveRangeEnd;
    CollisionHelper.CollisionDirection collisionDirection;

    public TortoiseEnemy(Game1 game, Texture2D enemyAttack, Vector2 position, Rectangle screenBounds, List<IBlock> block, List<object> projectiles)
    {
        this.game = game;
        this.enemyAttack = enemyAttack;
        this.position = position;
        this.projectiles = projectiles;

        leftFrames = new Rectangle[4];
        rightFrames = new Rectangle[4];
        leftFrames[0] = new Rectangle(120, 210, 34, 33);
        leftFrames[1] = new Rectangle(81, 210, 34, 33);
        leftFrames[2] = new Rectangle(40, 210, 34, 33);
        leftFrames[3] = new Rectangle(1, 210, 34, 33);
        rightFrames[0] = new Rectangle(160, 210, 34, 33);
        rightFrames[1] = new Rectangle(200, 210, 34, 33);
        rightFrames[2] = new Rectangle(240, 210, 34, 33);
        rightFrames[3] = new Rectangle(280, 210, 34, 33);
        frames = rightFrames;
        currentFrame = 0;
        timeSinceLastFrame = 0;
        this.velocity = 50;
        this.screenBounds = screenBounds;

        this.block = block;
        moveRangeStart = position.X - 100;
        moveRangeEnd = position.X + 100;
    }



    public TortoiseEnemy(Texture2D enemyAttack, Vector2 enemyPosition, Rectangle rectangle)
    {
        EnemyAttack = enemyAttack;
        EnemyPosition = enemyPosition;
        Rectangle = rectangle;
    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        float nextX = position.X + velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        timeSinceLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;


        bool collisionOccurred = false;

        foreach (IBlock block in block)
        {
            Rectangle nextPosition = new Rectangle(
                (int)nextX,
                (int)position.Y,
                frames[currentFrame].Width * 2,
                frames[currentFrame].Height * 2);

            if (nextX < moveRangeStart || CollisionDetector.DetectCollision(nextPosition, block.Bounds))
            {
                collisionOccurred = true;
                velocity = -velocity;
                //frames = rightFrames;
                FireSpeed = new Vector2(-300, 0);
                EnemyFireball newEnemyFireball = new EnemyFireball(game, enemyAttack, position, FireSpeed, screenBounds);
                projectiles.Add(newEnemyFireball);
            }
            else if (nextX > moveRangeEnd || CollisionDetector.DetectCollision(nextPosition, block.Bounds))
            {
                collisionOccurred = true;
                velocity = -velocity;
                nextX = screenBounds.Right - 100;
                //frames = leftFrames;
                FireSpeed = new Vector2(300, 0);
                EnemyFireball newEnemyFireball = new EnemyFireball(game, enemyAttack, position, FireSpeed, screenBounds);
                projectiles.Add(newEnemyFireball);
            }
            if (velocity > 0)
            {
                frames = rightFrames;
            }
            else
            {
                frames = leftFrames;
            }
            break;
        }
        if (!collisionOccurred)
        {
            position.X = nextX;
        }

        if (CollisionDetector.DetectCollision(Bounds, p.Bounds) && p.GetMarioState() != MarioState.Dead)
        {
            collisionDirection = CollisionHelper.DetermineCollisionDirection(Bounds, p.Bounds);
            if (collisionDirection == CollisionHelper.CollisionDirection.Bottom)
            {
                p.CheckCollisionWithEnemy(true);
                p.jump();
                game.DestroyEnemy(this);
                game.music.playStomp();
            }
        }

        if (timeSinceLastFrame >= 200)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            timeSinceLastFrame = 0;
        }
        // above new code

        // old code below
        //if (nextX < screenBounds.Left + 300)
        //{
        //    velocity = -velocity;
        //    frames = rightFrames;
        //    FireSpeed = new Vector2(-300, 0);
        //    EnemyFireball newEnemyFireball = new EnemyFireball(game, enemyAttack, position, FireSpeed, screenBounds);
        //    projectiles.Add(newEnemyFireball);
        //}
        //else if (nextX > screenBounds.Right - 200)
        //{
        //    velocity = -velocity;
        //    nextX = screenBounds.Right - 200;
        //    frames = leftFrames;
        //    FireSpeed = new Vector2(300, 0);
        //    EnemyFireball newEnemyFireball = new EnemyFireball(game, enemyAttack, position, FireSpeed, screenBounds);
        //    projectiles.Add(newEnemyFireball);
        //}
        //if (timeSinceLastFrame >= 200.0)
        //{
        //    currentFrame++;
        //    if (currentFrame >= frames.Length)
        //        currentFrame = 0;

        //    timeSinceLastFrame = 0;
        //}
        //position.X = nextX;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(enemyAttack, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
    }
    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                 (int)position.X,
                 (int)position.Y,
                 frames[currentFrame].Width * 2,
                 frames[currentFrame].Height * 2
             );

            return bounds;
        }
    }

    public Texture2D EnemyAttack { get; }
    public Vector2 EnemyPosition { get; }
    public Rectangle Rectangle { get; }

}