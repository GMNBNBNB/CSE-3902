using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;

namespace Sprint_0
{
    public class Game1 : Game
    {
        private IController KeyBoradCon;
        private IController MouseCon;
        private Text Text;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            KeyBoradCon = new KeyBoradCon(this, _graphics);
            MouseCon = new MouseCon(this, _graphics);
            Text = new Text();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            KeyBoradCon.LoadContent(Content, GraphicsDevice);
            MouseCon.LoadContent(Content, GraphicsDevice);
            Text.LoadContent(Content, GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseCon.Update(gameTime);
            KeyBoradCon.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            Text.Draw(_spriteBatch);
            KeyBoradCon.Draw(gameTime, _spriteBatch,GraphicsDevice);
            MouseCon.Draw(gameTime,_spriteBatch,GraphicsDevice);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
