using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Sprint2;

public interface IProjectiles
{
    void Update(GameTime gameTime, List<ISprite> enemies, IPlayer player, List<IBlock> blocks);
    void Draw(SpriteBatch spirtBatch);
    Rectangle Bounds { get; }
}