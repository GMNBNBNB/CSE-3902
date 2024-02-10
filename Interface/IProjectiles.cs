using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public interface IProjectiles
{
	void Update(GameTime gameTime);
	void Draw(SpriteBatch spirtBatch);
}