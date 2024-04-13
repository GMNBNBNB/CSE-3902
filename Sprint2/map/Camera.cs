using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sprint0.Game1;

namespace Sprint2
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        public Vector2 Position { get; private set; }
        public readonly Viewport viewport;
        public Texture2D Map;  

        public Camera(Viewport viewport,Texture2D Map)
        {
            this.viewport = viewport;
            this.Map = Map;
        }

        public void Update(Vector2 targetPosition, GameState currentState)
        {
            float maxX = Map.Width - viewport.Width;
            float halfViewportWidth = viewport.Width / 3f;
            float desiredX = targetPosition.X - halfViewportWidth;
            float clampedX = MathHelper.Clamp(desiredX, 0, maxX);
            if (currentState != GameState.Cave)
            {
                Position = new Vector2(clampedX, 0);
                Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0));
            }
            else
            {
                Transform = Matrix.CreateTranslation(Vector3.Zero);
            }
        }
    }
}
