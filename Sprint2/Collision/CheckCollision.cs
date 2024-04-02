using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0
{
    public static class CollisionDetector
    {
        public static bool DetectCollision(Rectangle A, Rectangle B)
        {
            return A.Intersects(B);
        }
    }
}
