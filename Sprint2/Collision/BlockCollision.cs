using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0;
using static CollisionHelper;
using Sprint2.Block;

namespace Sprint2
{
    public class BlockCollision
    {
        Boolean isAbove;
        CollisionHelper.CollisionDirection collisionDirection;
        public void Update(IBlock block, IPlayer player)
        {
            isAbove = false; ;
            collisionDirection = CollisionHelper.DetermineCollisionDirection(block.Bounds, player.Bounds);
            if (collisionDirection == CollisionHelper.CollisionDirection.Top)
            {
                player.IfCollisionTop(block.Bounds, player.Bounds);
            }
            if (collisionDirection == CollisionHelper.CollisionDirection.Bottom)
            {
                if (block.Bounds.Width==64)
                {
                    isAbove = true;
                }
                player.IfCollisionBot(block.Bounds, player.Bounds);
            }
            if (collisionDirection == CollisionHelper.CollisionDirection.Left)
            {
                player.IfCollisionLSide(block.Bounds, player.Bounds);
            }
            if (collisionDirection == CollisionHelper.CollisionDirection.Right)
            {
                player.IfCollisionRSide(block.Bounds, player.Bounds);
            }
        }
        public Boolean pipeAbove()
        {
            return isAbove;
        }
    }
}

