﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Player;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
// using Microsoft.Xna.Framework;

namespace Sprint2.Icon
{
    public class Score
    {


        private Game1 game;
        float scale_font;
        private SpriteFont font;
        

        private string score_sprite;
        private Vector2 sprite_font_position;

        private int score_point;
        private string score_point_string;
        private Vector2 point_font_position;

        CollisionHelper.CollisionDirection collisionDirection;
        
        private int coin_point = 100;
        private int enemy_point = 1000;


        public Score(SpriteFont font, Game1 game)
        {
            // common variables
            this.game = game;
            scale_font = 1.3f;
            this.font = font;

            // word "Score" drawing
            score_sprite = "Score";
            sprite_font_position = new Vector2(230, 20);

            // scroe number drawing variables
            int score_point = 0;
            score_point_string = score_point.ToString();
            point_font_position = new Vector2(320, 20);

        }
        private void UpdateScoreString()
        {
            score_point_string = score_point.ToString();
        }
        public void increaseCoinScore()
        {
            score_point += 100;
            UpdateScoreString();
        }
        public void increaseItemScore()
        {
            score_point += 500;
            UpdateScoreString();
        }
        public void increaseEnemyScore()
        {
            score_point += 1000;
            UpdateScoreString();
        }

        public void UpdateEnemyScore(GameTime gameTime, IPlayer player_mario, ISprite enemies)
        {
            // stamp on the enemy, increases the score
            if (CollisionDetector.DetectCollision(enemies.Bounds, player_mario.Bounds)) // player_mario.GetMarioState() != MarioState.Dead
            {
                //player_mario.setPosition(new_position);
                collisionDirection = CollisionHelper.DetermineCollisionDirection(enemies.Bounds, player_mario.Bounds);
                if (collisionDirection == CollisionHelper.CollisionDirection.Bottom && player_mario.GetMarioState() != MarioState.Dead && enemies is not FireEmemy && enemies is not FlowerEnemy) 
                {
                    score_point = score_point + enemy_point;
                }
                UpdateScoreString();
            }
        }

        public void Draw(SpriteBatch sb) //, Texture2D texture
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity); // Draw Begin

            sb.DrawString(font, score_sprite, sprite_font_position, Color.White, 0f, Vector2.Zero, scale_font, SpriteEffects.None, 0f);
            sb.DrawString(font, score_point_string, point_font_position, Color.White, 0f, Vector2.Zero, scale_font, SpriteEffects.None, 0f);

            sb.End(); // Draw End
        }

    }
}
