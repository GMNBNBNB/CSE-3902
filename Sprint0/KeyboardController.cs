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
    private int enemy = 0;

    public KeyboardController(Game1 game, Texture2D texture, Vector2 position, Vector2 EnemyPosition)
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

        if (state.IsKeyDown(Keys.N))
        {
            enemy++;
        }
        else if (state.IsKeyDown(Keys.M))
        {
            enemy--;
        }
        switch (enemy)
        {
            case 1:
                game.ChangeSprite(new Enemy1(texture, EnemyPosition));
                break;

            case 2:
                game.ChangeSprite(new Enemy2(texture, EnemyPosition, screenBounds));
                break;
        }
    }
}


