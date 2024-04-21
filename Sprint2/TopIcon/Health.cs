using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Player;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
// using Microsoft.Xna.Framework;

namespace Sprint2.Icon
{
    // draw the mario sprite symbol + the number of lifes
    public class Health
    {
        //var
        private Game1 game;

        // mario icon drawing variables
        private Texture2D texture; // mario texture
        private Vector2 mario_position;
        private float scale_mario; // Scale factor for mario icon drawing
        private Rectangle sourceRectangle;

        // health number drawing variables
        private int health;
        private string health_string;
        private SpriteFont font;
        private Vector2 font_position;
        float scale_font;
        private bool isInvincible1;
        private bool isInvincible2;
        private double InvincibleTime1;
        private double InvincibleTime2;
        CollisionHelper.CollisionDirection collisionDirection;

        // constructor
        public Health(Texture2D texture, SpriteFont font, Game1 game)
        {
            this.game = game;

            this.texture = texture;
            mario_position = new Vector2(20, 20);
            sourceRectangle = new Rectangle(224, 44, 12, 16);
            scale_mario = 2f;

            health = 3;
            this.font = font;
            scale_font = 1.5f;
            health_string = health.ToString();
            font_position = new Vector2(60, 20);

            isInvincible1 = false;
            isInvincible2 = false;
            InvincibleTime1 = 0;
            InvincibleTime2 = 0;
        }
        private void UpdateHealthString()
        {
            health_string = health.ToString();
        }
        public void increaseHealth()
        {
            health++;
        }
        public void decreaseHealth()
        {
            health--;
        }
        public void SetInvincible(IPlayer player)
        {
            if (player.index() == 1)
            {
                isInvincible1 = true;
            }
            else if (player.index() == 2)
            {
                isInvincible2 = true;
            }
        }
        public void Update(GameTime gameTime, IPlayer player_mario1, IPlayer player_mario2, ISprite enemies) // need to add EnemyFireball enemyAttack later
        {   //PlayerSprite player_mario

            //Vector2 current_position = player_mario.getPosition();
            //Vector2 new_position = new Vector2(current_position.X, current_position.Y + 50);

            if (!isInvincible1)
            {
                if (CollisionDetector.DetectCollision(enemies.Bounds, player_mario1.Bounds) && health != 0) // player_mario.GetMarioState() != MarioState.Dead
                {
                    //player_mario.setPosition(new_position);
                    collisionDirection = CollisionHelper.DetermineCollisionDirection(enemies.Bounds, player_mario1.Bounds);
                    if (collisionDirection != CollisionHelper.CollisionDirection.Bottom)
                    {
                        player_mario1.damaged(gameTime);
                    }
                    UpdateHealthString();
                }
                if (health == 0)
                {
                    //game.reStart(); 
                }

            }
            else
            {
                if (player_mario1.GetMarioState() == MarioState.Small)
                {
                    player_mario1.setMarioState(MarioState.invincibleS);
                }
                else if(player_mario1.GetMarioState() == MarioState.Big)
                {
                    player_mario1.setMarioState(MarioState.invincibleB);
                }
                    
                if (CollisionDetector.DetectCollision(enemies.Bounds, player_mario1.Bounds))
                {
                    game.DestroyEnemy(enemies);
                }
                InvincibleTime1 += gameTime.ElapsedGameTime.TotalMilliseconds;
                if( InvincibleTime1 > 10000 )
                {
                    isInvincible1 = false;
                    InvincibleTime1 = 0;
                    if (player_mario1.GetMarioState() == MarioState.invincibleS)
                    {
                        player_mario1.setMarioState(MarioState.Small);
                    }
                    else if (player_mario1.GetMarioState() == MarioState.invincibleB)
                    {
                        player_mario1.setMarioState(MarioState.Big);
                    }
                }
            }

            if (!isInvincible2)
            {
                if (CollisionDetector.DetectCollision(enemies.Bounds, player_mario2.Bounds) && health != 0) // player_mario.GetMarioState() != MarioState.Dead
                {
                    //player_mario.setPosition(new_position);
                    collisionDirection = CollisionHelper.DetermineCollisionDirection(enemies.Bounds, player_mario2.Bounds);
                    if (collisionDirection != CollisionHelper.CollisionDirection.Bottom)
                    {
                        player_mario2.damaged(gameTime);
                    }
                    UpdateHealthString();
                }
                if (health == 0)
                {
                    //game.reStart(); 
                }

            }
            else
            {
                if (player_mario2.GetMarioState() == MarioState.Small)
                {
                    player_mario2.setMarioState(MarioState.invincibleS);
                }
                else if (player_mario2.GetMarioState() == MarioState.Big)
                {
                    player_mario2.setMarioState(MarioState.invincibleB);
                }

                if (CollisionDetector.DetectCollision(enemies.Bounds, player_mario2.Bounds))
                {
                    game.DestroyEnemy(enemies);
                }
                InvincibleTime2 += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (InvincibleTime2 > 10000)
                {
                    isInvincible2 = false;
                    InvincibleTime2 = 0;
                    if (player_mario2.GetMarioState() == MarioState.invincibleS)
                    {
                        player_mario2.setMarioState(MarioState.Small);
                    }
                    else if (player_mario2.GetMarioState() == MarioState.invincibleB)
                    {
                        player_mario2.setMarioState(MarioState.Big);
                    }
                }
            }

        }
        public void Draw(SpriteBatch sb) //, Texture2D texture
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity); // Draw Begin

            sb.Draw(texture, mario_position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale_mario, SpriteEffects.None, 0f);
            sb.DrawString(font, health_string, font_position, Color.White, 0f, Vector2.Zero, scale_font, SpriteEffects.None, 0f);
            //spriteBatch.Draw(spriteTexture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            sb.End(); // Draw End
        }

    }
}
