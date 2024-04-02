using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Sprint0
{
    public partial class Game1 : Game
    {
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
        public void AddBlock(IBlock block)
        {
            blocks.Add(block);
        }
        public void AddItem(ISprite item)
        {
            Items.Add(item);
        }
        public void DestroyItem(ISprite item)
        {
            DestroyItems.Add(item);
        }
        public void reset()
        {
            player.reset();
            enemies.Clear();
            enemies.Add(new FlowerEmeny(texture, EnemyPosition));
            enemies.Add(new FlyTortoiseEnemy(texture, EnemyPosition, GetScreenBounds()));
            enemies.Add(new TortoiseEnemy(this, enemyAttack, EnemyPosition, GetScreenBounds(), projectiles));
            enemies.Add(new Goomba(enemyAttack, EnemyPosition, GetScreenBounds()));
            enemies.Add(new NonFlyTortoise(enemyAttack, EnemyPosition, GetScreenBounds()));
            projectiles.Clear();
        }

    }
}
