using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint2;

namespace Sprint0
{
    public class FireballCollision
    {
        private Fireball fireball;

        public FireballCollision(Fireball fb)
        {
            fireball = fb;
        }

        public bool FireballHitEnemy(ISprite enemy)
        {
            return CollisionDetector.DetectCollision(fireball.Bounds, enemy.Bounds);
        }

        public bool FireballHitBlock(IBlock block)
        {
            return CollisionDetector.DetectCollision(fireball.Bounds, block.Bounds);
        }
    }
}