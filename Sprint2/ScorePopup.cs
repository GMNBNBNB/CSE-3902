using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    public class ScorePopup
    {
        Vector2 positionN;
        private int score = 1000;
        private float timer = 1.0f;
        Boolean active;
        Game game;
        public ScorePopup(Game game)
        {
            active = false;
            this.game = game;
        }

        public void Update(GameTime gameTime, Boolean dead)
        {
            if (dead)
                active = true;
            timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer < 0)
            {
                active = false;
                timer = 1.0f;
            }
        }

        public void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            positionN = position;
            positionN.Y -= 20;
            if (active)
            {
                spriteBatch.DrawString(game.Content.Load<SpriteFont>("File"), score.ToString(), positionN, Color.White);
            }
        }
    }
}
