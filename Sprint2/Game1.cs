using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Player;
using System;
using Sprint0.Controller;

namespace Sprint0
{
    public class Game1 : Game
    {
        Texture2D texture;
        Texture2D enemyAttack;
        Vector2 position;
        Vector2 EnemyPosition;

        ISprite sprite;
        List<object> controllerList;
        IPlayer player;
        List<object> projectiles;
        Queue<ISprite> enemies;
        public List<ISprite> enemies1;

        ISprite spriteI;
        Texture2D textureI;
        Vector2 positionI;

        Texture2D textureB;
        int currentBlockIndex;
        Rectangle currentBlockRect;

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
        public void changeBlock(int index)
        {
            currentBlockIndex = index;
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
            controllerList = new List<object>();
            currentBlockIndex = 0;
            projectiles = new List<object>();
            enemies = new Queue<ISprite>();
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
            mapTexture = Content.Load<Texture2D>("1-1");

            map = new Map(mapTexture, 58, GetScreenBounds());
            spriteI = new Spring(textureI, positionI);
            player = new PlayerSprite(this, texture, enemyAttack, position, GetScreenBounds(), map);
            enemies.Enqueue(new FlowerEmeny(texture, EnemyPosition));
            enemies.Enqueue(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds()));
            enemies.Enqueue(new TortoiseEnemy(enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Enqueue(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds()));
            enemies.Enqueue(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds()));
            controllerList.Add(new KeyboardController(this, texture, enemyAttack, position, enemies, textureI, positionI, textureB));
            font = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime){

            if (currentState == GameState.MainMenu)
            {
                menuController.Update(gameTime);
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    currentState = GameState.Playing;
                }


            }
            else if (currentState == GameState.Playing)
            {
                if (gameIndex == 0 || gameIndex == 1)
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    player.Update(gameTime);
                    spriteI.Update(gameTime);

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

                        if (CollisionDetector.DetectCollision(player.Bounds, enemies.Peek().Bounds))
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
                        enemies.Peek().Update(gameTime);
                    }

                    currentBlockRect = new Rectangle(currentBlockIndex * 16, 0, 16, 16);
                }
                else
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        Exit();

                    player.Update(gameTime);

                    foreach (IController controller in controllerList)
                    {
                        controller.Update(gameTime);
                    }

                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Update(gameTime, enemies, player);
                    }

                    foreach (ISprite e in enemies1)
                    {
                        e.Update(gameTime);
                    }
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
                        enemies.Peek().Draw(_spriteBatch);
                    }
                    spriteI.Draw(_spriteBatch);
                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Draw(_spriteBatch);
                    }
                    _spriteBatch.Draw(textureB, new Vector2(300, 150), currentBlockRect, Color.White);
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

        public void resetMap()
        {
            map = new Map(mapTexture, 58, GetScreenBounds());
        }

        public void reset()
        {
            resetMap();
            player.reset();
            enemies.Clear();
            enemies.Enqueue(new FlowerEmeny(texture, EnemyPosition));
            enemies.Enqueue(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds()));
            enemies.Enqueue(new TortoiseEnemy(enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Enqueue(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds()));
            enemies.Enqueue(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds()));
            projectiles.Clear();
        }

    }
}
