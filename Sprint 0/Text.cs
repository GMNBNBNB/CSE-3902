using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class Text
    {
        private SpriteFont ScreenText;
        private int score = 0;
        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            ScreenText = Content.Load<SpriteFont>("ScreenText");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ScreenText, "Credits", new Vector2(300,300), Color.Black);
            spriteBatch.DrawString(ScreenText, "Program Made By: Meng Gao", new Vector2(300, 325), Color.Black);
            spriteBatch.DrawString(ScreenText, "Sprites from:https://www.mariouniverse.com/wp-content/img/sprites/nes/smb/characters.gif", new Vector2(0, 350), Color.Black);
        }
    }
    
}
