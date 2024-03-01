using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Player;

public interface IPlayer
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spirtBatch);
    Rectangle Bounds { get; }
    Fireball fireball();
    void damaged(GameTime gameTime);
    void moveLeft();
    void moveRight();
    void moveLeftStop();
    void moveRightStop();
    void jump();
    void crouch();
    void crouchStop();
    void reset();
}
