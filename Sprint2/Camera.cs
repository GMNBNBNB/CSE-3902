using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint2
{
    internal class Camera
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }
        private readonly Viewport viewport;
        private Texture2D Map;  

        public Camera(Viewport viewport,Texture2D Map)
        {
            this.viewport = viewport;
            this.Map = Map;
        }

        public void Update(Vector2 targetPosition)
        {
            float maxX = Map.Width - viewport.Width;
            float halfViewportWidth = viewport.Width / 3f;
            float desiredX = targetPosition.X - halfViewportWidth;
            float clampedX = MathHelper.Clamp(desiredX, 0, maxX);
            // Center camera horizontally on target sprite
            Position = new Vector2(clampedX, 0);

            // Clamp position to stay within the bounds of the world
            // Adjust the clamp range according to your world size

            // Create the transformation matrix
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
        }
    }
}
