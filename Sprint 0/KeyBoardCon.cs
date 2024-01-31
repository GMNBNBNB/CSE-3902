using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class KeyBoradCon : IController
    {
        private ISprite StaticSprite;
        private ISprite AnimatedSprite;
        private ISprite MoveUpDownSprite;
        private ISprite MoveRightLeft;
        private Game game;
        bool StartMove = false;
        bool StartUp = false;
        bool Startleft = false;
        bool PressBut = false;
        int Kcheck = 1;
        public KeyBoradCon(Game _game, GraphicsDeviceManager _graphics)
        {
            game = _game;
            StaticSprite = new StaticSprite();
            AnimatedSprite = new AnimatedSprite();
            MoveRightLeft = new MoveRightLeft(_graphics);
            MoveUpDownSprite = new MoveUpDownSprite(_graphics);
        }
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D0))
            {
                game.Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                StartMove = false;
                StartUp = false;
                Startleft = false;
                Kcheck = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2) || StartMove)
            {
                StartUp = false;
                AnimatedSprite.Update(gameTime);
                Kcheck = 2;
                StartMove = true;
                Startleft = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3) || StartUp)
            {
                StartMove = false;
                StartUp = true;
                Startleft = false;
                MoveUpDownSprite.Update(gameTime);
                Kcheck = 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4) || Startleft)
            {
                StartMove = false;
                StartUp = false;
                Startleft = true;
                MoveRightLeft.Update(gameTime);
                Kcheck = 4;
            }
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                PressBut = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1) || Keyboard.GetState().IsKeyDown(Keys.D2) || Keyboard.GetState().IsKeyDown(Keys.D3) || Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                PressBut = false;
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
            if (!PressBut)
            {
                if (Kcheck == 1)
                {
                    StaticSprite.Draw(spriteBatch);
                }
                if (Kcheck == 2)
                {
                    AnimatedSprite.Draw(spriteBatch);
                }
                if (Kcheck == 3)
                {
                    MoveUpDownSprite.Draw(spriteBatch);
                }
                if (Kcheck == 4)
                {
                    MoveRightLeft.Draw(spriteBatch);
                }
            }

        }
    }

}
