using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Player;
using Player2;
using System;
using Sprint0.Controller;
using Sprint2;
using Sprint2.Block;
using Sprint2.Icon;
using System.Runtime.CompilerServices;

namespace Sprint0
{
    public partial class Game1 : Game
    {
        public Texture2D texture;
        public Texture2D enemyAttack;
        public Texture2D mapTexture;
        public Texture2D textureI;
        public Texture2D textureB;
        public Texture2D caveTexture;
        public Texture2D pipeTexture;
        public Vector2 position;

        public Vector2 positionI;
        Vector2 EnemyPosition;
        Vector2 BlockPosition;

        public ISprite sprite;
        public List<object> controllerList;
        public IPlayer player;
        public IPlayer player2;
        public List<object> projectiles;
        public List<ISprite> enemies;
        public List<ISprite> DestroyEnemies;
        public List<ISprite> enemies1;
        public List<IBlock> blocks;
        public List<IBlock> DestroyBlock;
        public List<ISprite> Items;
        public List<ISprite> DestroyItems;
        public List<IBlock> blocksC;
        public List<ISprite> ItemsC;

        public List<ISprite> enemies2;
        public List<IBlock> blocks2;
        public List<ISprite> Items2;

        public List<ISprite> enemies3;
        public List<IBlock> blocks3;
        public List<ISprite> Items3;
        public block block;

        public ISprite item;


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Camera _camera;

        public Map map;
        public Map map2;
        public Map map3;
        public Cave cave;
        public BlockCollision blockCollision;

        public GameState currentState = GameState.MainMenu;
        public SpriteFont font;
        private MenuController menuController;
        private PauseMenuController pauseController;
        private VectoryController vectoryController;
        private GameOverController gameoverController;
        public Health mario_health;
        public TimeBlock timer;

        public CheatCodeManager CheatCodeManager;
        public enum GameState
        {
            MainMenu,
            Playing,
            Paused,
            Cave,
            Vectory,
            GameOver,
            Gointopipe
        }
        private int gameIndex = 0;
        private int pauseIndex = 0;
        private int vectoryIndex = 0;

        public Sounds music;

        private UpdateManager updateManager;
        private DrawManager drawManager;

        Vector2 playerPosition;
        Vector2 cavePosition;

        //flag
        public Texture2D textureQiGan;
        public Texture2D textureQiZi;
        public Vector2 positionQiZi;

        public CoinCount coin_count;
        public Score score_point;

        FogEffect fogEffect;
        KeyboardState previousS;
        float delayTimer;
        Boolean check;

        protected override void Initialize()
        {
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, _graphics.PreferredBackBufferHeight / 2);
            EnemyPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight / 2 + 130);
            playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 200, _graphics.PreferredBackBufferHeight / 2);
            cavePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 - 300, _graphics.PreferredBackBufferHeight / 2);
            positionI = new Vector2(400, 300);
            BlockPosition = new Vector2(300, 300);
            positionQiZi = new Vector2(3200, 85);
            controllerList = new List<object>();
            projectiles = new List<object>();
            enemies = new List<ISprite>();
            DestroyEnemies = new List<ISprite>();
            enemies1 = new List<ISprite>();
            Items = new List<ISprite>();
            DestroyItems = new List<ISprite>();
            blocks = new List<IBlock>();
            DestroyBlock = new List<IBlock>();
            ItemsC = new List<ISprite>();
            blocksC = new List<IBlock>();

            Items2 = new List<ISprite>();
            blocks2 = new List<IBlock>();
            enemies2 = new List<ISprite>();

            Items3 = new List<ISprite>();
            blocks3 = new List<IBlock>();
            enemies3 = new List<ISprite>();
            delayTimer = 1.0f;
            check = true;

            blockCollision = new BlockCollision();
            menuController = new MenuController(this);
            pauseController = new PauseMenuController(this);
            vectoryController = new VectoryController(this);
            gameoverController = new GameOverController(this);
            updateManager = new UpdateManager(this);
            drawManager = new DrawManager(this);

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
            textureQiGan = Content.Load<Texture2D>("ganzi");
            textureQiZi = Content.Load<Texture2D>("flag");
            font = Content.Load<SpriteFont>("File");
            _camera = new Camera(GraphicsDevice.Viewport, mapTexture);


            block = new block(textureB, BlockPosition);
            map = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks, 1);
            map2 = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks2, 2);
            map3 = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks3, 3);
            cave = new Cave(caveTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks, 0);
            item = new Spring(textureI, positionI);
            font = Content.Load<SpriteFont>("File");
            timer = new TimeBlock(textureB, font);
            mario_health = new Health(texture, font, this);
            coin_count = new CoinCount(textureI, font, this);
            score_point = new Score(font, this);
            fogEffect = new FogEffect(GraphicsDevice, GetScreenBounds());

            reStart();
        }


        protected override void Update(GameTime gameTime)
        {

            if (currentState == GameState.MainMenu)
            {
                menuController.Update(gameTime);
                updateManager.MainMenuUpdate();
            }
            else if (currentState == GameState.Playing)
            {
                updateManager.PlayUpdate(playerPosition, previousS);

                if (gameIndex == 0)
                {
                    updateManager.Level1Update(gameIndex, gameTime, Items, blocks, enemies1);
                }
                else if (gameIndex == 1)
                {
                    updateManager.Level1Update(gameIndex, gameTime, Items2, blocks2, enemies2);
                    fogEffect.Update(player.getPosition());
                    fogEffect.Update(player2.getPosition());
                }
                else if (gameIndex == 2)
                {
                    updateManager.Level1Update(gameIndex, gameTime, Items3, blocks3, enemies3);
                }
            }
            else if (currentState == GameState.Paused)
            {
                pauseController.Update(gameTime);
                updateManager.PauseUpdate();
            }
            else if (currentState == GameState.Vectory)
            {
                vectoryController.Update(gameTime);
            }
            else if (currentState == GameState.GameOver)
            {
                gameoverController.Update(gameTime);
            }
            else if (currentState == GameState.Gointopipe)
            {
                if (delayTimer > 0)
                {
                    delayTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    if (check)
                    {
                        currentState = GameState.Cave;
                        check = false;
                    }
                    else
                    {
                        currentState = GameState.Playing;
                        check = true;
                    }
                    delayTimer = 1.0f;
                }
            }
            else if (currentState == GameState.Cave)
            {
                updateManager.CaveUpdate(cavePosition, gameTime, previousS);
            }
            previousS = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (currentState == GameState.MainMenu)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();
                drawManager.MainMenuDraw(_spriteBatch, GetScreenBounds(), font, gameIndex);
                _spriteBatch.End();
            }
            else if (currentState == GameState.Playing)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                if (gameIndex == 0)
                {
                    drawManager.Level1Draw(_spriteBatch, gameIndex, map, Items, blocks, enemies1);
                }
                else if (gameIndex == 1)
                {
                    drawManager.Level1Draw(_spriteBatch, gameIndex, map2, Items2, blocks2, enemies2);
                    fogEffect.Draw(_spriteBatch, _camera.Position);
                }
                else if (gameIndex == 2)
                {
                    drawManager.Level1Draw(_spriteBatch, gameIndex, map3, Items3, blocks3, enemies3);
                }
                _spriteBatch.End();
                CheatCodeManager.Draw(_spriteBatch);
                mario_health.Draw(_spriteBatch);
                coin_count.Draw(_spriteBatch);
                score_point.Draw(_spriteBatch);
                if (gameIndex == 2)
                {
                    timer.Draw(_spriteBatch);
                }
            }
            else if (currentState == GameState.Paused)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();
                drawManager.PauseDraw(_spriteBatch, GetScreenBounds(), font, pauseIndex);
                _spriteBatch.End();
            }
            else if (currentState == GameState.Cave)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin(transformMatrix: _camera.Transform);
                drawManager.CaveDraw(_spriteBatch);
                _spriteBatch.End();

                mario_health.Draw(_spriteBatch);
                coin_count.Draw(_spriteBatch);
                score_point.Draw(_spriteBatch);
            }
            else if (currentState == GameState.Gointopipe)
            {
                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin();
                if (check)
                {
                    _spriteBatch.DrawString(font, "go into pipe", new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height - 30), Color.White);
                }
                else
                {
                    _spriteBatch.DrawString(font, "out pipe", new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height - 30), Color.White);
                }
                _spriteBatch.End();

                mario_health.Draw(_spriteBatch);
                coin_count.Draw(_spriteBatch);
            }
            else if (currentState == GameState.Vectory)
            {
                _spriteBatch.Begin();
                drawManager.VectoryDraw(_spriteBatch, GetScreenBounds(), font, vectoryIndex);
                _spriteBatch.End();
            }
            else if (currentState == GameState.GameOver)
            {
                _spriteBatch.Begin();
                drawManager.GameOverDraw(_spriteBatch, GetScreenBounds(), font, vectoryIndex);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}