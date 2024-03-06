using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Player;
using System;
using Sprint2;
using System.Reflection.Metadata;

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
        Queue<ISprite> enemies;
        Queue<ISprite> allEnemies;

        ISprite spriteI;
        Texture2D textureI;
        Vector2 positionI;

        Texture2D textureB;
        int currentBlockIndex;
        Rectangle currentBlockRect;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        block block;

        static int health = 2;

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
            public Rectangle GetScreenBounds()
        {
            return new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            BlockPosition = new Vector2(300, 300);
            EnemyPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2 + 100, _graphics.PreferredBackBufferHeight / 2 + 130);
            positionI = new Vector2(400, 300);
            controllerList = new List<object>();
            projectiles = new List<object>();
            enemies = new Queue<ISprite>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            texture = Content.Load<Texture2D>("sheet");
            textureI = Content.Load<Texture2D>("items");
            textureB = Content.Load<Texture2D>("blocks");
            enemyAttack = Content.Load<Texture2D>("EnemyAttack");
            block = new block(textureB, BlockPosition);
            spriteI = new Spring(textureI, positionI);
            player = new PlayerSprite(texture, enemyAttack, position, GetScreenBounds());
            enemies.Enqueue(new FlowerEmeny(texture, EnemyPosition));
            enemies.Enqueue(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds()));
            enemies.Enqueue(new TortoiseEnemy(enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Enqueue(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds()));
            enemies.Enqueue(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds()));
            allEnemies = new Queue<ISprite>(enemies);
            controllerList.Add(new KeyboardController(this, texture, enemyAttack, position, enemies, textureI, positionI, textureB));
        }

        protected override void Update(GameTime gameTime){
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (IController controller in controllerList)
            {
                controller.Update(gameTime);
            }

            foreach (IProjectiles pro in projectiles)
            {
                pro.Update(gameTime, enemies, player);
            }
            if (enemies.Count>0) {

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
            player.Update(gameTime);
            block.Update(gameTime,player);
            spriteI.Update(gameTime);
          

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
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
            block.Draw(_spriteBatch);
            _spriteBatch.End();
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

        public void reset()
        {
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
