using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            return CollisionDetector.DetectCollision(enemyFireball.Bounds, player.Bounds);
        }

    }
}