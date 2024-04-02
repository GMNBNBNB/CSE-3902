using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


public static class CollisionHelper
{
	public enum CollisionDirection
	{
		Top,
		Bottom,
		Left,
		Right,
		None 
	}

	public static float getX(Rectangle rectA, Rectangle rectB)
    {
        Point centerA = new Point(rectA.Center.X, rectA.Center.Y);
        Point centerB = new Point(rectB.Center.X, rectB.Center.Y);
        return (rectA.Width / 2.0f + rectB.Width / 2.0f) - Math.Abs(centerA.X - centerB.X);
    }

    public static float getY(Rectangle rectA, Rectangle rectB)
    {
        Point centerA = new Point(rectA.Center.X, rectA.Center.Y);
        Point centerB = new Point(rectB.Center.X, rectB.Center.Y);
		return (rectA.Height / 2.0f + rectB.Height / 2.0f) - Math.Abs(centerA.Y - centerB.Y);
    }

    public static CollisionDirection DetermineCollisionDirection(Rectangle rectA, Rectangle rectB)
	{
		Point centerA = new Point(rectA.Center.X, rectA.Center.Y);
		Point centerB = new Point(rectB.Center.X, rectB.Center.Y);

		float depthX = (rectA.Width / 2.0f + rectB.Width / 2.0f) - Math.Abs(centerA.X - centerB.X);
		float depthY = (rectA.Height / 2.0f + rectB.Height / 2.0f) - Math.Abs(centerA.Y - centerB.Y);

		if (depthX >= depthY)
		{
			if (centerA.Y < centerB.Y)
			{
				return CollisionDirection.Top;
			}
			else
			{
				return CollisionDirection.Bottom;
			}
		}
		else
		{
			if (centerA.X < centerB.X)
			{
				return CollisionDirection.Left;
			}
			else
			{
				return CollisionDirection.Right;
			}
		}
	}
}