using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Sprint0;
using Sprint2.Block;


namespace Player2
{
    public partial class PlayerSprite2 : IPlayer
    {
        private Texture2D texture;
        private Texture2D texturePro;
        private Vector2 position;
        private int currentFrame;
        private double timeSinceLastFrame;
        Rectangle GetScreenBounds;

        private Map map;
        private Cave cave;

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

        public float velocity;
        private double damagedAnimationTime;
        private bool facingRight;
        private bool hasCollidedWithEnemy = false;

        private float gravity = 0.8f;
        private float jumpSpeed;
        private float JumpSpeed;
        Texture2D MapTexture;
        Texture2D caveTexture;
        block block;

        Boolean incave;
        
        private MarioState currentState;
        private TimeSpan lastDamagedTime;
        private TimeSpan invincibleDuration;
        public bool isInvincible { get; private set; }
        private bool FireBallIsActive;

        private Game1 game;

        public PlayerSprite2(Game1 game, Rectangle GetScreenBounds)
        {
            this.game = game;
            this.texture = game.texture;
            this.texturePro = game.enemyAttack;
            this.position = game.position;
            this.map = game.map;
            this.block = game.block;
            this.GetScreenBounds = GetScreenBounds;
            this.cave = game.cave;
            this.caveTexture = game.caveTexture;

            leftFrames = new Rectangle[3];
            rightFrames = new Rectangle[3];
            leftJumpFrames = new Rectangle[3];
            rightJumpFrames = new Rectangle[3];
            leftWalkFrames = new Rectangle[3];
            rightWalkFrames = new Rectangle[3];

            currentState = MarioState.Small;

            leftFrames[0] = new Rectangle(194, 105, 13, 18);
            leftFrames[1] = new Rectangle(12, 105, 16, 19);
            leftFrames[2] = new Rectangle(12, 105, 16, 19);
            rightFrames[0] = new Rectangle(305, 105, 16, 18);
            rightFrames[1] = new Rectangle(12, 105, 16, 19);
            rightFrames[2] = new Rectangle(12, 105, 16, 19);

            leftJumpFrames[0] = new Rectangle(140, 105, 18, 18);
            leftJumpFrames[1] = new Rectangle(140, 105, 18, 18);
            leftJumpFrames[2] = new Rectangle(140, 105, 18, 18);

            rightJumpFrames[0] = new Rectangle(354, 105, 18, 18);
            rightJumpFrames[1] = new Rectangle(354, 105, 18, 18);
            rightJumpFrames[2] = new Rectangle(354, 105, 18, 18);

            leftWalkFrames[0] = new Rectangle(175, 105, 16, 16);
            leftWalkFrames[1] = new Rectangle(193, 105, 16, 16);
            leftWalkFrames[2] = new Rectangle(207, 105, 16, 16);

            rightWalkFrames[0] = new Rectangle(305, 105, 16, 16);
            rightWalkFrames[1] = new Rectangle(320, 105, 16, 16);
            rightWalkFrames[2] = new Rectangle(290, 105, 16, 16);

            bigLeftFrames = new Rectangle[3];
            bigRightFrames = new Rectangle[3];
            bigLeftJumpFrames = new Rectangle[3];
            bigRightJumpFrames = new Rectangle[3];
            bigLeftCrouchFrames = new Rectangle[3];
            bigRightCrouchFrames = new Rectangle[3];
            bigLeftWalkFrames = new Rectangle[3];
            bigRightWalkFrames = new Rectangle[3];

            bigLeftFrames[0] = new Rectangle(237, 62, 20, 34);
            bigLeftFrames[1] = new Rectangle(237, 62, 20, 34);
            bigLeftFrames[2] = new Rectangle(237, 62, 20, 34);
            bigRightFrames[0] = new Rectangle(258, 62, 17, 34);
            bigRightFrames[1] = new Rectangle(258, 62, 17, 34);
            bigRightFrames[2] = new Rectangle(258, 62, 17, 34);

            bigLeftJumpFrames[0] = new Rectangle(126, 62, 19, 34);
            bigLeftJumpFrames[1] = new Rectangle(126, 62, 19, 34);
            bigLeftJumpFrames[2] = new Rectangle(126, 62, 19, 34);

            bigLeftCrouchFrames[0] = new Rectangle(219, 71, 18, 25);
            bigLeftCrouchFrames[1] = new Rectangle(219, 71, 18, 25);
            bigLeftCrouchFrames[2] = new Rectangle(219, 71, 18, 25);

            bigLeftWalkFrames[0] = new Rectangle(201, 62, 18, 33);
            bigLeftWalkFrames[1] = new Rectangle(183, 62, 16, 35);
            bigLeftWalkFrames[2] = new Rectangle(165, 62, 17, 35);

            bigRightJumpFrames[0] = new Rectangle(369, 62, 18, 36);
            bigRightJumpFrames[1] = new Rectangle(369, 62, 18, 36);
            bigRightJumpFrames[2] = new Rectangle(369, 62, 18, 36);

            bigRightCrouchFrames[0] = new Rectangle(276, 71, 18, 25);
            bigRightCrouchFrames[1] = new Rectangle(276, 71, 18, 25);
            bigRightCrouchFrames[2] = new Rectangle(276, 71, 18, 25);

            bigRightWalkFrames[0] = new Rectangle(294, 62, 20, 34);
            bigRightWalkFrames[1] = new Rectangle(312, 62, 17, 34);
            bigRightWalkFrames[2] = new Rectangle(330, 62, 19, 35);

            facingRight = true;
            frames = rightFrames;
            currentFrame = 0;
            timeSinceLastFrame = 0;
            this.velocity = 300f;
            this.JumpSpeed = 18;
            this.MapTexture = game.mapTexture;
            damagedAnimationTime = 0;
            isInvincible = false;
            incave = false;
            invincibleDuration = TimeSpan.FromMilliseconds(1500);

            FireBallIsActive = false;
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
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }
    }
}
