using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Sprint0;
using System.Collections.Generic;
using Sprint2;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;


public class KeyboardController : IController
{
    private Game1 game;
    private Texture2D texture;
    private Texture2D enemyAttack;
    private Vector2 position;
    private Rectangle screenBounds;
    private List<ISprite> enemies;
    private ISprite sprite;
    private KeyboardState previousKeyboardState;

    private Texture2D textureI;
    private Vector2 positionI;
    private int currentItemIndex = 0;
    private List<ISprite> items = new List<ISprite>();

    private Texture2D textureB;
    private int currentBlockIndex = 0;
    private bool mute = false;

    CheatCodeManager CheatCodeManager;

    public KeyboardController(Game1 game)
    {
        this.game = game;
        this.texture = game.texture;
        this.enemyAttack = game.enemyAttack;
        this.enemies = game.enemies;
        this.position = game.position;
        this.screenBounds = game.GetScreenBounds();
        this.CheatCodeManager = game.CheatCodeManager;

        this.textureI = game.textureI;
        this.positionI = game.positionI;
        items.Add(new Spring(textureI, positionI));
        items.Add(new Coin(game, textureI, positionI));
        items.Add(new LFlower(textureI, positionI));
        items.Add(new DFlower(game,textureI, positionI));
        items.Add(new Box(textureI, positionI));
        items.Add(new Leaf(textureI, positionI));
        items.Add(new Platform(textureI, positionI));
        items.Add(new WPlatform(textureI, positionI));
        items.Add(new Mushroom(game,textureI, positionI));
        items.Add(new Star(textureI, positionI));

        this.textureB = game.textureB;
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.T) && !CheatCodeManager.IsActive)
        {
            CheatCodeManager.Activate();
        }
        CheatCodeManager.Update();

        if (!CheatCodeManager.IsActive)
        {
            if (state.IsKeyDown(Keys.R))
            {
                currentItemIndex = 0;
                currentBlockIndex = 0;
                game.ChangeItem(items[currentItemIndex]);
                game.changeBlock(currentBlockIndex);
                game.reset();
                game.music.startMusic();
            }

            if (state.IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P))
            {
                if (enemies.Count > 0)
                {
                    sprite = enemies[0];
                    enemies.RemoveAt(0);
                    enemies.Add(sprite);
                }
            }
            else if (state.IsKeyDown(Keys.O) && !previousKeyboardState.IsKeyDown(Keys.O))
            {
                if (enemies.Count > 0)
                {
                    sprite = enemies[enemies.Count - 1];
                    enemies.RemoveAt(enemies.Count - 1);
                    enemies.Insert(0, sprite);
                }
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
                game.takeDamage(gameTime);
            }
            if (state.IsKeyDown(Keys.D1) && !previousKeyboardState.IsKeyDown(Keys.D1))
            {
                game.shotFireBall();
            }

            // W for jump
            if ((state.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W)) || (state.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up)))
            {
                game.jump();
            }

            if ((state.IsKeyDown(Keys.M) && !previousKeyboardState.IsKeyDown(Keys.M)))
            {
                if (mute)
                {
                    mute = false;
                    game.music.startMusic();
                }
                else
                {
                    mute = true;
                    game.music.stopMusic();
                }
            }

            // S for crouch
            if (previousKeyboardState.IsKeyDown(Keys.S) || previousKeyboardState.IsKeyDown(Keys.Down))
            {
                if (state.IsKeyUp(Keys.S) && state.IsKeyUp(Keys.Down))
                {
                    game.crouchStop();
                }
                else
                {
                    game.crouch();
                }
            }

            if (previousKeyboardState.IsKeyDown(Keys.Left) || previousKeyboardState.IsKeyDown(Keys.A))
            {
                if (state.IsKeyUp(Keys.Left) && state.IsKeyUp(Keys.A))
                {
                    game.leftStop();
                }
                else
                {
                    game.moveLeft();
                }
            }
            //-> for move right
            if (previousKeyboardState.IsKeyDown(Keys.Right) || previousKeyboardState.IsKeyDown(Keys.D))
            {
                if (state.IsKeyUp(Keys.Right) && state.IsKeyUp(Keys.D))
                {
                    game.rightStop();
                }
                else
                {
                    game.moveRight();
                }
            }           
        }
        previousKeyboardState = state;
    }
}

