using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public interface IPlayer
{
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spirtBatch);
    Rectangle Bounds { get; }
    void fireball();
    void damaged();
    void missile();
    void moveLeft();
    void moveRight();
    void moveLeftStop();
    void moveRightStop();
    void jump();
    void crouch();
    void crouchStop();
    void reset();
}
