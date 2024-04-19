using Microsoft.Xna.Framework;
using Player;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Sprint2;
using System.Collections.Generic;

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

        public void reStart()
        {
            music.startMusic();
            player = new PlayerSprite(this, GetScreenBounds());
            enemies.Clear();
            CheatCodeManager = new CheatCodeManager(font, player, mario_health, this);
            controllerList.Add(new KeyboardController(this));
            blocks.Clear();
            Items.Clear();
            enemies1.Clear();
            map = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks,1);
            map2 = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks,2);
            cave = new Cave(caveTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks,0);
            item = new Spring(textureI, positionI);
            Items.Add(new Flag(this, textureQiGan, textureQiZi, positionQiZi));
            projectiles.Clear();
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
            object f = player.fireball();
            if (f != null)
            {
            projectiles.Add(player.fireball());
        }
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
        public void AddEnemy(ISprite enemy, int level)
        {
            if (level == 1)
            {
                enemies1.Add(enemy);
            }
            if (level == 2)
            {
                enemies2.Add(enemy);
            }
        }
        public void AddBlock(IBlock block, int level)
        {
            if (level == 1)
            {
                blocks.Add(block);
            }
            else if (level == 2)
            {
                blocks2.Add(block);
            }else
            {
                blocksC.Add(block);
            }
            
        }
        public void DestroyBlocks(IBlock block, Boolean world)
        {
            DestroyBlock.Add(block);
        }
        public void AddItem(ISprite item, int level)
        {
            if (level == 1) 
            { 
                Items.Add(item);
            }
            else if(level == 2)
            {
                Items2.Add(item);
            }else
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
            enemies.Add(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds(), this, blocks2));
            enemies.Add(new TortoiseEnemy(this, enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Add(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds(), this, blocks));
            enemies.Add(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds(), this, blocks2));
            projectiles.Clear();
        }

    }
}
