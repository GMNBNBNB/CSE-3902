using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Sprint0;
using System.Collections.Generic;

public class KeyboardController : IController
{
    private Game1 game;
    private Texture2D texture;
    private Texture2D enemyAttack;
    private Vector2 position;
    private Vector2 EnemyPosition;
    private Rectangle screenBounds;
    private int currentEnemyIndex = 0;
    private List<ISprite> enemies = new List<ISprite>();
    private KeyboardState previousKeyboardState;

    public KeyboardController(Game1 game, Texture2D texture, Texture2D enemyAttack, Vector2 position, Vector2 EnemyPosition)
    {
        this.game = game;
        this.texture = texture;
        this.enemyAttack = enemyAttack;
        this.position = position;
        this.EnemyPosition = EnemyPosition;
        this.screenBounds = game.GetScreenBounds();

        enemies.Add(new Enemy1(texture, EnemyPosition));
        enemies.Add(new Enemy2(texture, EnemyPosition, screenBounds));
        enemies.Add(new Enemy3(enemyAttack, EnemyPosition, screenBounds));

    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.N))
        {
            currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
            game.ChangeSprite(enemies[currentEnemyIndex]);
            System.Threading.Thread.Sleep(50);
        }
        else if (state.IsKeyDown(Keys.M))
        {
            currentEnemyIndex = (currentEnemyIndex - 1 + enemies.Count) % enemies.Count;
            game.ChangeSprite(enemies[currentEnemyIndex]);
            System.Threading.Thread.Sleep(50);
        }

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

