using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    public static class CollisionDetector
    {
        public static bool DetectCollision(IPlayer A, ISprite B)
        {
            Rectangle bounds1 = A.Bounds;
            Rectangle bounds2 = B.Bounds;

            return bounds1.Intersects(bounds2);
        }
    }
}
