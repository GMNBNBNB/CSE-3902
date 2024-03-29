﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Sprint0
{
    public class Game1 : Game
    {
        Texture2D texture;
        Texture2D enemyAttack;
        Vector2 position;
        Vector2 EnemyPosition;
        
        ISprite sprite;
        List<object> controllerList;
        IPlayer player;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void ChangeSprite(ISprite newSprite)
        {
            sprite = newSprite;
        }
        public Rectangle GetScreenBounds()
        {
            return new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            EnemyPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight / 2 + 100);
            controllerList = new List<object>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("mario");
            enemyAttack = Content.Load<Texture2D>("EnemyAttack");
            sprite = new Enemy1(texture, EnemyPosition);
            player = new PlayerSprite(texture, position, GetScreenBounds());
            controllerList.Add(new KeyboardController(this, texture, enemyAttack, position, EnemyPosition));

        }

        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IController controller in controllerList)
            {
                controller.Update(gameTime);
            }

            player.Update(gameTime);
            sprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _spriteBatch.Begin();
            player.Draw(_spriteBatch);
            sprite.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void takeDamage()
        {
            player.damaged();
        }

        public void shotFireBall()
        {
            player.fireball();
        }

        public void shotMissile()
        {
            player.missile();
        }
    }
}
