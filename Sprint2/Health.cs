using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Player;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
// using Microsoft.Xna.Framework;

namespace Sprint2
{
    // draw the mario sprite symbol + the number of lifes
    public class Health
    {
        //var
        private int health;

        private string health_string;

        private Texture2D texture; // mario texture
        private Vector2 mario_position;
        private Rectangle sourceRectangle;

        private SpriteFont font;
        private Vector2 font_position;
        private Game1 game;
        // constructor
        public Health(Texture2D texture, SpriteFont font, Game1 game)
        {

            this.texture = texture;
            this.game = game;
            mario_position = new Vector2(20, 20);
            sourceRectangle = new Rectangle(224, 44, 12, 16);

            health = 1;
            this.font = font;
            health_string = health.ToString();
            font_position = new Vector2(40, 20);
        }
        private void UpdateHealthString()
        {
            health_string = health.ToString();
        }
        public void IncreaeHealth()
        {
            health++;
        }

        public void Update(GameTime gameTime, IPlayer player_mario, ISprite enemies) // need to add EnemyFireball enemyAttack later
        { //PlayerSprite player_mario

            if (CollisionDetector.DetectCollision(enemies.Bounds, player_mario.Bounds))
            {
                health--;
                player_mario.damaged(gameTime);
                UpdateHealthString();
            }
            if (health == 0)
            {
                //game.reStart(); 
            }


        }
        public void Draw(SpriteBatch sb) //, Texture2D texture
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            sb.DrawString(font, health_string, font_position, Color.Black);
            sb.Draw(texture, mario_position, sourceRectangle, Color.White);
            sb.End();

        }

    }
}
