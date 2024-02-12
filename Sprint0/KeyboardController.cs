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

    private Texture2D textureI;
    private Vector2 positionI;
    private int currentItemIndex = 0;
    private List<ISprite> items = new List<ISprite>();

    private Texture2D textureB;
    private int currentBlockIndex = 0;

    public KeyboardController(Game1 game, Texture2D texture, Texture2D enemyAttack, Vector2 position, Vector2 EnemyPosition, Texture2D textureI, Vector2 positionI, Texture2D textureB)
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

        this.textureI = textureI;
        this.positionI = positionI;
        items.Add(new Item1(textureI, positionI));
        items.Add(new Item2(textureI, positionI));
        items.Add(new Item3(textureI, positionI));
        items.Add(new Item4(textureI, positionI));
        items.Add(new Item5(textureI, positionI));
        items.Add(new Item6(textureI, positionI));
        items.Add(new Item7(textureI, positionI));
        items.Add(new Item8(textureI, positionI));
        items.Add(new Item9(textureI, positionI));
        items.Add(new Item10(textureI, positionI));

        this.textureB = textureB;
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

        if (state.IsKeyDown(Keys.N) && !previousKeyboardState.IsKeyDown(Keys.N))
        {
            currentEnemyIndex = (currentEnemyIndex + 1) % enemies.Count;
            game.ChangeSprite(enemies[currentEnemyIndex]);
        }
        else if (state.IsKeyDown(Keys.M) && !previousKeyboardState.IsKeyDown(Keys.M))
        {
            currentEnemyIndex = (currentEnemyIndex - 1 + enemies.Count) % enemies.Count;
            game.ChangeSprite(enemies[currentEnemyIndex]);
        }

        if (state.IsKeyDown(Keys.I) && !previousKeyboardState.IsKeyDown(Keys.I))
        {
            currentItemIndex++;
            if (currentItemIndex == items.Count)
            {
                currentItemIndex = 0;
            }
            game.ChangeItem(items[currentItemIndex]);
        }
        else if (state.IsKeyDown(Keys.U) && !previousKeyboardState.IsKeyDown(Keys.U))
        {
            currentItemIndex--;
            if (currentItemIndex < 0)
            {
                currentItemIndex = items.Count - 1;
            }
            game.ChangeItem(items[currentItemIndex]);
        }
        if (state.IsKeyDown(Keys.T) && !previousKeyboardState.IsKeyDown(Keys.T))
        {
            currentBlockIndex = (currentBlockIndex - 1 + 20) % 20;
            game.changeBlock(currentBlockIndex);
        }
        else if (state.IsKeyDown(Keys.Y) && !previousKeyboardState.IsKeyDown(Keys.Y))
        {
            currentBlockIndex = (currentBlockIndex + 1) % 20;
            game.changeBlock(currentBlockIndex);
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

