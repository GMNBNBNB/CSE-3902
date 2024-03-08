using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

public interface IProjectiles
{
	void Update(GameTime gameTime, List<ISprite> enemies, IPlayer player);
	void Draw(SpriteBatch spirtBatch);
    Rectangle Bounds { get; }
}