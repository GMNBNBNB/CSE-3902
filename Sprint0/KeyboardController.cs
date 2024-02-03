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
    private Vector2 EnemyPosition;
    private Rectangle screenBounds;

    public KeyboardController(Game1 game, Texture2D texture, Vector2 position,Vector2 EnemyPosition)
    {
        this.game = game;
        this.texture = texture;
        this.position = position;
        this.EnemyPosition = EnemyPosition;
        this.screenBounds = game.GetScreenBounds();
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.D0))
            {
                Environment.Exit(0);
            }
            if (state.IsKeyDown(Keys.N))
            {
                game.ChangeSprite(new Enemy1(texture, EnemyPosition));
            }
            if (state.IsKeyDown(Keys.M))
            {
                game.ChangeSprite(new Enemy2(texture, EnemyPosition, screenBounds));
            }
    }
}

