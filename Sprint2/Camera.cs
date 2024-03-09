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

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Update(Vector2 targetPosition)
        {
            // Center camera horizontally on target sprite
            float halfViewportWidth = viewport.Width / 2f;
            Position = new Vector2(targetPosition.X - halfViewportWidth, 0);

            // Clamp position to stay within the bounds of the world
            // Adjust the clamp range according to your world size

            // Create the transformation matrix
            Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
        }
    }
}
