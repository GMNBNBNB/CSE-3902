using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using Sprint0;

public class KeyboardController : IController
{
    private Game1 game;
    private Texture2D texture;
    private Vector2 position;
    private Rectangle screenBounds;
    private Keys[] previousKeyboard;

    private Texture2D enemyAttack;
    private Vector2 EnemyPosition;
    private int currentEnemyIndex = 0;
    private List<ISprite> enemies = new List<ISprite>();


    private Dictionary<Keys, ICommand> controllerMappings;

    public KeyboardController(Game1 game, Texture2D texture, Texture2D enemyAttack, Vector2 position, Vector2 EnemyPosition)
    {
        controllerMappings = new Dictionary<Keys, ICommand>();
        controllerMappings.Add(Keys.E, new DamagedCommand(game));
        controllerMappings.Add(Keys.D1, new FireBallCommand(game));
        controllerMappings.Add(Keys.D2, new MissileCommand(game));



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
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

        foreach (Keys key in pressedKeys)
        {
            if (controllerMappings.ContainsKey(key) && (Array.IndexOf(previousKeyboard, key)==-1))
            {
                controllerMappings[key].Execute();
            }
        }

        previousKeyboard = pressedKeys;

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
    }



}

