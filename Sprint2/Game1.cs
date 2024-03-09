using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Player;
using System;
using Sprint0.Controller;
using Sprint2;

namespace Sprint0
{
    public class Game1 : Game
    {
        Texture2D texture;
        Texture2D enemyAttack;
        Vector2 position;
        Vector2 EnemyPosition;
        Vector2 BlockPosition;

        ISprite sprite;
        List<object> controllerList;
        IPlayer player;
        List<object> projectiles;
        List<ISprite> enemies;
        public List<ISprite> enemies1;
        block block;

        ISprite spriteI;
        Texture2D textureI;
        Vector2 positionI;

        Texture2D textureB;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        static int health = 2;

        Texture2D mapTexture;
        Map map;

        public GameState currentState = GameState.MainMenu;
        public SpriteFont font;
        private MenuController menuController;

        public enum GameState
        {
            MainMenu,
            Playing,
            Paused
        }
        private int gameIndex = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeSprite(ISprite newSprite)
        {
            sprite = newSprite;
        }
        public void ChangeItem(ISprite newSprite)
        {
            spriteI = newSprite;
        }

        public void changeNextLevel()
        {
            gameIndex++;
            if (gameIndex > 2)
            {
                gameIndex = 0;
            }
        }

        public void changePreviousLevel()
        {
            gameIndex--;
            if (gameIndex < 0)
            {
                gameIndex = 2;
            }
        }

        public void focusLevel(int i)
        {
            gameIndex = i;
        }

        public int Level()
        {
            return gameIndex;
        }

        public void play()
        {
            currentState = GameState.Playing;
        }

        public Rectangle GetScreenBounds()
        {
            return new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, _graphics.PreferredBackBufferHeight / 2);
            EnemyPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight / 2 + 130);
            positionI = new Vector2(400, 300);
            BlockPosition = new Vector2(300, 300);
            controllerList = new List<object>();
            projectiles = new List<object>();
            enemies = new List<ISprite>();
            enemies1 = new List<ISprite>();

            menuController = new MenuController(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            mapTexture = Content.Load<Texture2D>("1-1");
            texture = Content.Load<Texture2D>("sheet");
            textureI = Content.Load<Texture2D>("items");
            textureB = Content.Load<Texture2D>("blocks");
            enemyAttack = Content.Load<Texture2D>("EnemyAttack");

            block = new block(textureB, BlockPosition);
            map = new Map(mapTexture,GetScreenBounds(), this);
            spriteI = new Spring(textureI, positionI);
            player = new PlayerSprite(this, texture, enemyAttack, position, GetScreenBounds(), map, block);
            enemies.Add(new FlowerEmeny(texture, EnemyPosition));
            enemies.Add(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds()));
            enemies.Add(new TortoiseEnemy(enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Add(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds()));
            enemies.Add(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds()));
            controllerList.Add(new KeyboardController(this, texture, enemyAttack, position, enemies, textureI, positionI, textureB));
            font = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime){

            if (currentState == GameState.MainMenu)
            {
                menuController.Update(gameTime);
                if (Keyboard.GetState().IsKeyDown(Keys.Q)) 
                    Exit();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    currentState = GameState.Playing;
                }
            }
            else if (currentState == GameState.Playing)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    currentState = GameState.MainMenu;
                }
                if (gameIndex == 0 || gameIndex == 1)
                {
                    player.Update(gameTime);
                    spriteI.Update(gameTime);

                    foreach (IController controller in controllerList)
                    {
                        controller.Update(gameTime);
                    }

                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Update(gameTime, enemies1, player);
                    }
                    if (enemies1.Count > 0)
                    {
                        foreach (ISprite e in enemies1)
                        {
                            if (CollisionDetector.DetectCollision(player.Bounds, e.Bounds))
                            {
                                if (health > 0)
                                {
                                    player.damaged(gameTime);
                                    health--;
                                }
                                else
                                {
                                    player.damaged(gameTime);
                                    health = 2;
                                }
                            }
                            e.Update(gameTime);
                        }
                    }
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        currentState = GameState.MainMenu;
                    }
                    foreach (IController controller in controllerList)
                    {
                        controller.Update(gameTime);
                    }
                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Update(gameTime, enemies, player);
                    }
                    if (enemies.Count > 0)
                    {
                        enemies[0].Update(gameTime);
                        if (CollisionDetector.DetectCollision(player.Bounds, enemies[0].Bounds))
                        {
                             player.damaged(gameTime);
                        }
                    }

                    player.Update(gameTime);
                    block.Update(gameTime, player);
                }
            }
            else if (currentState == GameState.Paused)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    currentState = GameState.Playing;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (currentState == GameState.MainMenu)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();
                map.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, "Super Mario", new Vector2(GetScreenBounds().Width / 2 - font.MeasureString("Main Menu").X, GetScreenBounds().Height / 2 - 200), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
                for (int i = 0; i < 3; i++)
                {

                    if (i != gameIndex)
                    {
                        string text = "LEVEL " + (i + 1) + " ";
                        _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * i), Color.White);
                    }
                    else
                    {
                        string text = "LEVEL " + (i + 1) + ">";
                        _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * i), Color.Yellow);
                    }
                }
                _spriteBatch.DrawString(font, "Select the level to enter the game", new Vector2(GetScreenBounds().Width / 2 - (font.MeasureString("Select the level to enter the game").X * 0.75f), GetScreenBounds().Height / 2 + 100), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
                _spriteBatch.End();
            }
            else if (currentState == GameState.Playing)
            {
                if (gameIndex == 0 || gameIndex == 1)
                {
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    _spriteBatch.Begin();
                    map.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    foreach(ISprite e in enemies1)
                    {
                        e.Draw(_spriteBatch);
                    }
                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Draw(_spriteBatch);
                    }
                    _spriteBatch.End();
                }
                else
                {
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    _spriteBatch.Begin();
                    player.Draw(_spriteBatch);
                    if (enemies.Count > 0)
                    {
                        enemies[0].Draw(_spriteBatch);

                    }
                    spriteI.Draw(_spriteBatch);
                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Draw(_spriteBatch);
                    }
                    block.Draw(_spriteBatch);
                    _spriteBatch.End();
                }
            }
            else if (currentState == GameState.Paused)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();
                _spriteBatch.DrawString(font, "Paused", new Vector2(100, 100), Color.White);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public void changeBlock(int index)
        {
            block.changeBlock(index);
        }

        public void takeDamage(GameTime gameTime)
        {
            player.damaged(gameTime);
        }

        public void shotFireBall()
        {
            projectiles.Add(player.fireball());
        }

        public void jump()
        {
            player.jump();
        }
        
        public void crouch()
        {
            player.crouch();
        }

        public void crouchStop()
        {
            player.crouchStop();
        }

        public void moveLeft()
        {
            player.moveLeft();
        }
        
        public void moveRight()
        {
            player.moveRight();
        }
        
        public void leftStop()
        {
            player.moveLeftStop();
        }

        public void rightStop()
        {
            player.moveRightStop();
        }

        public Vector2 initialPosition()
        {
            return new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, _graphics.PreferredBackBufferHeight / 2);
        }
        public void AddEnemy(ISprite enemy)
        {
            enemies1.Add(enemy);
        }
        public void reset()
        {
            player.reset();
            enemies.Clear();
            enemies.Add(new FlowerEmeny(texture, EnemyPosition));
            enemies.Add(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds()));
            enemies.Add(new TortoiseEnemy(enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Add(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds()));
            enemies.Add(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds()));
            projectiles.Clear();
        }

    }
}
