using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Player;
using Sprint0;
using System;
using System.Collections.Generic;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Sprint2.Icon
{
    // draw the mario sprite symbol + the number of lifes
    public class CoinCount
    {
        //var
        private Game1 game;

        private Texture2D textureCoin; // coin texture
        private Vector2 coin_position;
        private float scale_coin; // Scale factor for coin icon drawing
        private Rectangle sourceRectangle;

        private SpriteFont font;
        private Vector2 font_position;
        private int count; // coin count 
        private string count_string; // convert from int to string for drawing purpose
        float scale_font; // scale coin count font

        // constructor
        public CoinCount(Texture2D texture_coin, SpriteFont font, Game1 game)
        {
            this.game = game;

            this.textureCoin = texture_coin;
            coin_position = new Vector2(120, 20);
            sourceRectangle = new Rectangle(125, 92, 12, 18);
            scale_coin = 2f;

            count = 0;
            this.font = font;
            scale_font = 1.5f;
            count_string = count.ToString();
            font_position = new Vector2(160, 20);
        }
        private void UpdateCoinString()
        {
            count_string = count.ToString();
        }
        public void increaseCoin()
        {
            count++;
        }

        public void Update(GameTime gameTime, IPlayer player_mario, ISprite coin) // need to add EnemyFireball enemyAttack later
        { 
            if (CollisionDetector.DetectCollision(coin.Bounds, player_mario.Bounds) )
            {
                count++;
                //player_mario.damaged(gameTime);
                UpdateCoinString();
            }

        }
        public void Draw(SpriteBatch sb) //, Texture2D texture
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity); // Draw Begin

            sb.Draw(textureCoin, coin_position, sourceRectangle, Color.White, 0f, Vector2.Zero, scale_coin, SpriteEffects.None, 0f);
            sb.DrawString(font, count_string, font_position, Color.Black, 0f, Vector2.Zero, scale_font, SpriteEffects.None, 0f);
            //spriteBatch.Draw(spriteTexture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            sb.End(); // Draw End
        }

    }
}
