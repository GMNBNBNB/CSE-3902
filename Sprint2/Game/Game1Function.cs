using Microsoft.Xna.Framework;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Sprint0
{
    public partial class Game1 : Game
    {
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
            item = newSprite;
        }

        public int getPauseIndex()
        {
            return pauseIndex;
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

        public void changePause()
        {
            pauseIndex = (pauseIndex + 1) % 2;
        }


        public void changeVectory()
        {
            vectoryIndex = (vectoryIndex + 1) % 2;
        }

        public int getVectoryIndex()
        {
            return vectoryIndex;
        }

        public void focusLevel(int i)
        {
            gameIndex = i;
        }


        public void focusPause(int i)
        {
            pauseIndex = i;
        }

        public void focusVectory(int i)
        {
            vectoryIndex = i;
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

        public Rectangle GetMap()
        {
            return new Rectangle(0, 0, 3584, 240);
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
        public void AddBlock(IBlock block, Boolean world)
        {
            if (world)
            {
                blocks.Add(block);
            }
            else
            {
                blocksC.Add(block);
            }
            
        }
        public void AddItem(ISprite item, Boolean world)
        {
            if (world) 
            { 
                Items.Add(item);
            }
            else
            {
                ItemsC.Add(item);
            }
        }
        public void DestroyItem(ISprite item)
        {
            DestroyItems.Add(item);
        }
        public void DestroyEnemy(ISprite enemies)
        {
            DestroyEnemies.Add(enemies);
        }
        public void reset()
        {
            player.reset();
            enemies.Clear();
            enemies.Add(new FlowerEmeny(texture, EnemyPosition));
            enemies.Add(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds()));
            enemies.Add(new TortoiseEnemy(this, enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Add(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds(),this,blocks));
            enemies.Add(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds()));
            projectiles.Clear();
        }

    }
}
