using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Player;
using System;

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
    void IfCollisionTop(Rectangle rectA, Rectangle rectB);
    void IfCollisionBot(Rectangle rectA, Rectangle rectB);
    void IfCollisionLSide(Rectangle rectA, Rectangle rectB);
    void IfCollisionRSide(Rectangle rectA, Rectangle rectB);
    Vector2 getPosition();
    void setPosition(Vector2 position);
    void inCave(Boolean incave);
    void ChangeCurrentState();

    void CheckCollisionWithEnemy(bool IfJump);
}
