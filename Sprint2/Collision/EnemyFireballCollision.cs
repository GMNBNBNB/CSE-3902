using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2;

namespace Sprint0
{
    public class EnemyFireballCollision
    {
        private EnemyFireball enemyFireball;

        public EnemyFireballCollision(EnemyFireball efb)
        {
            enemyFireball = efb;
        }

        public bool EnemyFireballHitMario(IPlayer player)
        {
            return CollisionDetector.DetectCollision(player.Bounds, enemyFireball.Bounds);
        }
        public bool EnemyFireballHitBlock(IBlock block)
        {
            return CollisionDetector.DetectCollision(enemyFireball.Bounds, block.Bounds);
        }
    }
}