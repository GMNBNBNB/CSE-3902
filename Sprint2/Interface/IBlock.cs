using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public interface IBlock
{
    void Update(GameTime gameTime, IPlayer player);
    void Draw(SpriteBatch spriteBatch);
    Rectangle Bounds { get; }
}
