using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Sprint0
{
    public class Game1 : Game
    {
        Texture2D texture;
        Vector2 position;
        Vector2 textPosition;

        ISprite sprite;
        ISprite textSprite;
        List<object> controllerList;

        SpriteFont spriteFont;

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
            textPosition = new Vector2(_graphics.PreferredBackBufferWidth /9, _graphics.PreferredBackBufferHeight / 4*3);
            controllerList = new List<object>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("sheet");
            spriteFont = Content.Load<SpriteFont>("myfont"); 

            sprite = new StaticSprite(texture, position);
            textSprite = new TextSprite(textPosition,spriteFont);

            controllerList.Add(new GamepadController(this, texture, position));
            controllerList.Add(new KeyboardController(this, texture, position));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IController controller in controllerList)
            {
                controller.Update(gameTime);
            }

            sprite.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            textSprite.Draw(_spriteBatch);
            sprite.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
