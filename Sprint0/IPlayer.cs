using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public interface IPlayer
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spirtBatch);
    void fireball();
    void damaged();
    void missile();
}
