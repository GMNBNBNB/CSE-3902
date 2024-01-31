using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class MouseCon : IController
    {
        private ISprite StaticSprite;
        private ISprite AnimatedSprite;
        private ISprite MoveUpDownSprite;
        private ISprite MoveRightLeft;
        private Game game;
        bool StartMove = false;
        bool StartUp = false;
        bool Startleft = false;
        bool PressKey = false;
        int quarterWidth;
        int quarterHeight;
        int check = 1;
        public MouseCon(Game _game, GraphicsDeviceManager _graphics)
        {
            game = _game;
            StaticSprite = new StaticSprite();
            AnimatedSprite = new AnimatedSprite();
            MoveRightLeft = new MoveRightLeft(_graphics);
            MoveUpDownSprite = new MoveUpDownSprite(_graphics);
            quarterWidth = _graphics.PreferredBackBufferWidth / 2;
            quarterHeight = _graphics.PreferredBackBufferHeight / 2;

        }
        public void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            Point mousePosition = new Point(state.X, state.Y);

            if (state.RightButton == ButtonState.Pressed)
            {
                game.Exit();
            }

            if (mousePosition.X < quarterWidth && mousePosition.Y < quarterHeight && state.LeftButton == ButtonState.Pressed)
            {
                StartMove = false;
                StartUp = false;
                Startleft = false;
                check = 1;
            }
            if (mousePosition.X >= quarterWidth && mousePosition.Y < quarterHeight && state.LeftButton == ButtonState.Pressed || StartMove)
            {
                StartUp = false;
                AnimatedSprite.Update(gameTime);
                check = 2;
                StartMove = true;
                Startleft = false;
            }
            if (mousePosition.X < quarterWidth && mousePosition.Y >= quarterHeight && state.LeftButton == ButtonState.Pressed || StartUp)
            {
                StartMove = false;
                StartUp = true;
                Startleft = false;
                MoveUpDownSprite.Update(gameTime);
                check = 3;
            }
            if (mousePosition.X >= quarterWidth && mousePosition.Y >= quarterHeight && state.LeftButton == ButtonState.Pressed || Startleft)
            {
                StartMove = false;
                StartUp = false;
                Startleft = true;
                MoveRightLeft.Update(gameTime);
                check = 4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1)|| Keyboard.GetState().IsKeyDown(Keys.D2)|| Keyboard.GetState().IsKeyDown(Keys.D3)|| Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                PressKey = true;
            }
            if (state.LeftButton == ButtonState.Pressed)
            {
                PressKey = false;
            }

        }
        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice)
        {
            StaticSprite.LoadContent(Content, GraphicsDevice);
            AnimatedSprite.LoadContent(Content, GraphicsDevice);
            MoveUpDownSprite.LoadContent(Content, GraphicsDevice);
            MoveRightLeft.LoadContent(Content, GraphicsDevice);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDevice GraphicsDevice)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (!PressKey)
            {
                if (check == 1)
                {
                    StaticSprite.Draw(spriteBatch);
                }
                if (check == 2)
                {
                    AnimatedSprite.Draw(spriteBatch);
                }
                if (check == 3)
                {
                    MoveUpDownSprite.Draw(spriteBatch);
                }
                if (check == 4)
                {
                    MoveRightLeft.Draw(spriteBatch);
                }
            }
        }
    }
}
