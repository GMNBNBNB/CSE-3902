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
    private KeyboardState previousKeyboardState;

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

        if (state.IsKeyDown(Keys.E) && !previousKeyboardState.IsKeyDown(Keys.E))
        {
            game.takeDamage();
        }
        if (state.IsKeyDown(Keys.D1) && !previousKeyboardState.IsKeyDown(Keys.D1))
        {
            game.shotFireBall();
        }
        if (state.IsKeyDown(Keys.D2) && !previousKeyboardState.IsKeyDown(Keys.D2))
        {
            game.shotMissile();
        }

        previousKeyboardState = state;
    }
}

