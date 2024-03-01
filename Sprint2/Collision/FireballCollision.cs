using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

    }
}