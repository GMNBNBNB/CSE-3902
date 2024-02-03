using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Sprint0;

public class KeyboardController : IController
{
    private Game1 game;
    private Texture2D texture;
    private Vector2 position;
    private Rectangle screenBounds;

    public KeyboardController(Game1 game, Texture2D texture, Vector2 position)
    {
        this.game = game;
        this.texture = texture;
        this.position = position;
        this.screenBounds = game.GetScreenBounds();
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.D0))
            {
                Environment.Exit(0);
            }

            if (state.IsKeyDown(Keys.D1))
            {
                game.ChangeSprite(new StaticSprite(texture, position));
            }

            if (state.IsKeyDown(Keys.D2))
            {
                game.ChangeSprite(new AnimatedSprite(texture, position));
            }

            if (state.IsKeyDown(Keys.D3))
            {
                game.ChangeSprite(new MovingSprite(texture, position, screenBounds));
            }

            if (state.IsKeyDown(Keys.D4))
            {
                game.ChangeSprite(new AnimatedMovingSprite(texture, position, screenBounds));
            }
    }
}

