using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Player;
using System;
using Sprint0.Controller;
using Sprint2;
using Sprint2.Block;

namespace Sprint0
{
    public partial class Game1 : Game
    {
        Texture2D texture;
        Texture2D enemyAttack;
        Texture2D vectory;
        Vector2 position;
        Vector2 EnemyPosition;
        Vector2 BlockPosition;

        ISprite sprite;
        List<object> controllerList;
        IPlayer player;
        List<object> projectiles;
        List<ISprite> enemies;
        List<ISprite> DestroyEnemies;
        public List<ISprite> enemies1;
        List<IBlock> blocks;
        List<ISprite> Items;
        List<ISprite> DestroyItems;
        List<IBlock> blocksC;
        List<ISprite> ItemsC;
        block block;

        ISprite item;
        Texture2D textureI;
        Vector2 positionI;

        Texture2D textureB;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        static int health = 2;

        Camera _camera;

        Texture2D mapTexture;
        Map map;
        Texture2D caveTexture;
        Texture2D pipeTexture;
        Cave cave;
        BlockCollision blockCollision;

        public GameState currentState = GameState.MainMenu;
        public SpriteFont font;
        private MenuController menuController;
        private PauseMenuController pauseController;
        private VectoryController vectoryController;
        public enum GameState
        {
            MainMenu,
            Playing,
            Paused,
            Cave,
            Vectory
        }
        private int gameIndex = 0;
        private int pauseIndex = 0;
        private int vectoryIndex = 0;

        public Sounds music;

        protected override void Initialize()
        {
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, _graphics.PreferredBackBufferHeight / 2);
            EnemyPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight / 2 + 130);
            positionI = new Vector2(400, 300);
            BlockPosition = new Vector2(300, 300);
            controllerList = new List<object>();
            projectiles = new List<object>();
            enemies = new List<ISprite>();
            DestroyEnemies = new List<ISprite>();
            enemies1 = new List<ISprite>();
            Items = new List<ISprite>();
            DestroyItems = new List<ISprite>();
            blocks = new List<IBlock>();
            ItemsC = new List<ISprite>();
            blocksC = new List<IBlock>();

            blockCollision = new BlockCollision();
            menuController = new MenuController(this);
            pauseController = new PauseMenuController(this);
            vectoryController = new VectoryController(this);

            music = new Sounds(Content);
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
            pipeTexture = Content.Load<Texture2D>("pipe");
            caveTexture = Content.Load<Texture2D>("cave");
            _camera = new Camera(GraphicsDevice.Viewport, mapTexture);


            block = new block(textureB, BlockPosition);
            map = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks);
            cave = new Cave(caveTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks);
            item = new Spring(textureI, positionI);
            reStart();
            font = Content.Load<SpriteFont>("File");
        }

        public void reStart()
        {
            music.startMusic();
            player = new PlayerSprite(this, texture, enemyAttack, position, mapTexture, map, block, GetScreenBounds(), caveTexture, cave);
            enemies.Clear();
            controllerList.Add(new KeyboardController(this, texture, enemyAttack, position, enemies, textureI, positionI, textureB));
            blocks.Clear();
            Items.Clear();
            enemies1.Clear();
            player = new PlayerSprite(this, texture, enemyAttack, position, mapTexture, map, block, GetScreenBounds(), caveTexture, cave);
            map = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks);
            cave = new Cave(caveTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks);
            item = new Spring(textureI, positionI);
            projectiles.Clear();
        }

        protected override void Update(GameTime gameTime)
        {

            if (currentState == GameState.MainMenu)
            {
                menuController.Update(gameTime);
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    Exit();

                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    currentState = GameState.Playing;
                    music.stopMusic();
                    music.startMusic();
                }
            }
            else if (currentState == GameState.Playing)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    currentState = GameState.MainMenu;
                    music.stopMusic();
                    music.playPause();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    currentState = GameState.Paused;
                    music.stopMusic();
                    music.playPause();
                }
                if (blockCollision.pipeAbove())
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        music.playPipe();
                        currentState = GameState.Cave;
                        player.inCave(true);
                        player.setPosition(new Vector2(_graphics.PreferredBackBufferWidth / 2 - 200, _graphics.PreferredBackBufferHeight / 2));
                    }
                }
                if (player.getPosition().X >= _camera.Map.Width - 50)
                {
                    this.currentState = GameState.Vectory;
                    music.stopMusic();
                    music.startEnd();
                }

                if (gameIndex == 0 || gameIndex == 1)
                {
                    item.Update(gameTime, player);
                    _camera.Update(player.getPosition(), currentState);

                    foreach (IController controller in controllerList)
                    {
                        controller.Update(gameTime);
                    }
                    player.Update(gameTime);
                    foreach (IBlock b in blocks)
                    {
                        b.Update(gameTime, player);
                        if (CollisionDetector.DetectCollision(b.Bounds, player.Bounds))
                        {
                            blockCollision.Update(b, player);
                        }
                    }
                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Update(gameTime, enemies1, player, blocks);
                    }
                    if (enemies1.Count > 0)
                    {
                        foreach (ISprite e in enemies1)
                        {
                            if (CollisionDetector.DetectCollision(player.Bounds, e.Bounds))
                            {
                                //if (health > 0)
                                //{
                                //    player.damaged(gameTime);
                                //    health--;
                                //}
                                //else
                                //{
                                //    player.damaged(gameTime);
                                //    health = 2;
                                //}
                            }
                            e.Update(gameTime, player);
                        }
                        foreach (ISprite e in DestroyEnemies)
                        {
                            enemies1.Remove(e);
                        }
                    }
                    foreach (ISprite I in Items)
                    {
                        I.Update(gameTime, player);
                    }
                    foreach (ISprite I in DestroyItems)
                    {
                        Items.Remove(I);
                    }
                }
                else
                {
                    foreach (IController controller in controllerList)
                    {
                        controller.Update(gameTime);
                    }
                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Update(gameTime, enemies, player, blocks);
                    }
                    if (enemies.Count > 0)
                    {
                        enemies[0].Update(gameTime, player);
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
                pauseController.Update(gameTime);
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    currentState = GameState.Playing;
                }
            }
            else if (currentState == GameState.Vectory)
            {
                vectoryController.Update(gameTime);
            }
            else if (currentState == GameState.Cave)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    currentState = GameState.MainMenu;
                    player.inCave(false);
                    player.setPosition(new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, _graphics.PreferredBackBufferHeight / 2));
                }
                if (blockCollision.pipeAbove())
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.B))
                    {
                        currentState = GameState.Playing;
                        player.inCave(false);
                        player.setPosition(new Vector2(1000, 300));
                    }
                }
                player.Update(gameTime);
                _camera.Update(player.getPosition(), currentState);
                foreach (IBlock b in blocksC)
                {
                    b.Update(gameTime, player);
                    if (CollisionDetector.DetectCollision(b.Bounds, player.Bounds))
                    {
                        blockCollision.Update(b, player);
                    }
                }
                foreach (ISprite I in ItemsC)
                {
                    I.Update(gameTime, player);
                }
                foreach (IProjectiles pro in projectiles)
                {
                    pro.Update(gameTime, enemies1, player, blocks);
                }
                foreach (IController controller in controllerList)
                {
                    controller.Update(gameTime);
                }
                foreach (ISprite I in DestroyItems)
                {
                    ItemsC.Remove(I);
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

                    _spriteBatch.Begin(transformMatrix: _camera.Transform);
                    map.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    foreach (ISprite e in enemies1)
                    {
                        e.Draw(_spriteBatch);
                    }
                    foreach (IProjectiles pro in projectiles)
                    {
                        pro.Draw(_spriteBatch);
                    }
                    foreach (IBlock b in blocks)
                    {
                        b.Draw(_spriteBatch);
                    }
                    foreach (ISprite I in Items)
                    {
                        I.Draw(_spriteBatch);
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
                    item.Draw(_spriteBatch);
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
                if (pauseIndex != 0)
                {
                    string text = "Continue";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 0), Color.White);
                }
                else
                {
                    string text = "Continue";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 0), Color.Yellow);
                }


                if (pauseIndex != 1)
                {
                    string text = "Back to menu";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 1), Color.White);
                }
                else
                {
                    string text = "Back to menu";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 1), Color.Yellow);
                }



                _spriteBatch.End();
            }
            else if (currentState == GameState.Cave)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                cave.Draw(_spriteBatch);
                player.Draw(_spriteBatch);
                foreach (IProjectiles pro in projectiles)
                {
                    pro.Draw(_spriteBatch);
                }
                foreach (IBlock b in blocksC)
                {
                    b.Draw(_spriteBatch);
                }
                foreach (ISprite I in ItemsC)
                {
                    I.Draw(_spriteBatch);
                }
                _spriteBatch.End();
            }
            else if (currentState == GameState.Vectory)
            {
                _spriteBatch.Begin();
                _spriteBatch.DrawString(font, "Vectory", new Vector2(GetScreenBounds().Width / 2 - font.MeasureString("Main Menu").X, GetScreenBounds().Height / 2 - 200), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
                if (vectoryIndex != 0)
                {
                    string text = "Restart";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 0), Color.White);
                }
                else
                {
                    string text = "Restart";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 0), Color.Yellow);
                }


                if (vectoryIndex != 1)
                {
                    string text = "Back to menu";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 1), Color.White);
                }
                else
                {
                    string text = "Back to menu";
                    _spriteBatch.DrawString(font, text, new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X / 2, GetScreenBounds().Height / 2 - 100 + 50 * 1), Color.Yellow);
                }



                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
