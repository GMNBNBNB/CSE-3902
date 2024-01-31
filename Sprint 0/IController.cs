using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public interface IController
    {
        void Update(GameTime gameTime);
        void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice);
    }
}
