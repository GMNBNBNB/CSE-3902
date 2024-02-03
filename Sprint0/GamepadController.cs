using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Sprint0;

public class GamepadController: IController
{
    private Game1 game;
    private Texture2D texture;
    private Vector2 position;
    private Rectangle screenBounds;

    public GamepadController(Game1 game,Texture2D texture, Vector2 position)
	{
        this.game = game;
        this.texture = texture;
        this.position = position;
        this.screenBounds = game.GetScreenBounds();
}

    public void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();

        int screenWidth = game.GraphicsDevice.Viewport.Width;
        int screenHeight = game.GraphicsDevice.Viewport.Height;
 
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (mouseState.X < screenWidth / 2 && mouseState.Y < screenHeight / 2)
            {
                game.ChangeSprite(new StaticSprite(texture,position));
            }
            else if (mouseState.X >= screenWidth / 2 && mouseState.Y < screenHeight / 2)
            {
                game.ChangeSprite(new AnimatedSprite(texture, position));
            }
            else if (mouseState.X < screenWidth / 2 && mouseState.Y >= screenHeight / 2)
            {
                game.ChangeSprite(new MovingSprite(texture, position, screenBounds));
            }
            else if (mouseState.X >= screenWidth / 2 && mouseState.Y >= screenHeight / 2)
            {
                game.ChangeSprite(new AnimatedMovingSprite(texture, position, screenBounds));
            }
        }

        if (mouseState.RightButton == ButtonState.Pressed)
        {
            Environment.Exit(0);
        }
    }
}
