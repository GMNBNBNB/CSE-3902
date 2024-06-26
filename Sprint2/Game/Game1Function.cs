﻿using Microsoft.Xna.Framework;
using Player;
using Player2;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Sprint2;
using System.Collections.Generic;
using Sprint2.Icon;
using Sprint2.Block;

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
            enemies.Clear();
            blocks.Clear();
            blocks2.Clear();
            blocks3.Clear();
            blocks4.Clear();
            blocksC.Clear();
            Items.Clear();
            Items2.Clear();
            Items3.Clear();
            Items4.Clear();
            ItemsC.Clear();
            enemies1.Clear();
            enemies2.Clear();
            enemies3.Clear();
            enemies4.Clear();
            scoreNeed.Clear();
            projectiles.Clear();
            fogEffect.reset();

            mario_health = new Health(texture, font, this);
            coin_count = new CoinCount(textureI, font, this);
            score_point = new Score(font, this);    

            player = new PlayerSprite(this, GetScreenBounds());
            player2 = new PlayerSprite2(this, GetScreenBounds());
            CheatCodeManager = new CheatCodeManager(font, player, player2, mario_health, this);
            controllerList.Add(new KeyboardController(this));
            map = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks, 1);
            map2 = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks, 2);
            map3 = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks, 3);
            map4 = new Map(mapTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks, 4);
            cave = new Cave(caveTexture, enemyAttack, GetScreenBounds(), this, textureB, textureI, pipeTexture, blocks, 0);
            item = new Spring(textureI, positionI);
            timer = new TimeBlock(textureB,font);
            Items.Add(new Flag(this, textureQiGan, textureQiZi, positionQiZi));
            Items2.Add(new Flag(this, textureQiGan, textureQiZi, positionQiZi));
            Items3.Add(new Flag(this, textureQiGan, textureQiZi, positionQiZi));
            Items4.Add(new Flag(this, textureQiGan, textureQiZi, positionQiZi));
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
            player2.damaged(gameTime);
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

        public void moveLeftS()
        {
            player.moveLeftS();
        }

        public void moveRight()
        {
            player.moveRight();
        }

        public void moveRightS()
        {
            player.moveRightS();
        }

        public void leftStop()
        {
            player.moveLeftStop();
        }

        public void rightStop()
        {
            player.moveRightStop();
        }

        public void shotFireBall2()
        {
            object f = player2.fireball();
            if (f != null)
            {
                projectiles.Add(player2.fireball());
            }
        }

        public void jump2()
        {
            player2.jump();
        }

        public void crouch2()
        {
            player2.crouch();
        }

        public void crouchStop2()
        {
            player2.crouchStop();
        }

        public void moveLeft2()
        {
            player2.moveLeft();
        }

        public void moveLeftS2()
        {
            player2.moveLeftS();
        }

        public void moveRight2()
        {
            player2.moveRight();
        }

        public void moveRightS2()
        {
            player2.moveRightS();
        }

        public void leftStop2()
        {
            player2.moveLeftStop();
        }

        public void rightStop2()
        {
            player2.moveRightStop();
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
            if (level == 3)
            {
                enemies3.Add(enemy);
            }
            if (level == 4)
            {
                enemies4.Add(enemy);
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
            }
            else if (level == 3)
            {
                blocks3.Add(block);
            }
            else if (level == 4)
            {
                blocks4.Add(block);
            }
            else
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
            else if (level == 2)
            {
                Items2.Add(item);
            }
            else if (level == 3)
            {
                Items3.Add(item);
            }
            else if (level == 4)
            {
                Items4.Add(item);
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
        public void back()
        {
            player.reset();
            player2.reset();
        }

        public void reset()
        {
            player.reset();
            player2.reset();
            enemies.Clear();
            projectiles.Clear();
        }

    }
}
