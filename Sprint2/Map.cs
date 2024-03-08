using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Map
{
    private Vector2 position;
    //private Vector2 velocity;
    private Rectangle screenBounds;
    private Texture2D texture;
    //private bool canScroll;
    private float scale;
    //public float baseY;

    public void LoadContent(Texture2D texture)
    {
        scale = (float)screenBounds.Height / texture.Height;
        this.texture = texture;

    }
    
    public Map(Texture2D texture, float baseY, Rectangle screenBounds)
    {
        //this.baseY = baseY;
        this.screenBounds = screenBounds;
        position = Vector2.Zero;
        //velocity = Vector2.Zero;
        //canScroll = false;
        
        LoadContent(texture);
    }

    public void resetPosition()
    {
        position = Vector2.Zero;
    }

    //public bool Update(GameTime gameTime, bool faceRight, Vector2 playerPosition, Vector2 playerVelocity)
    //{
        
    //    canScroll = (playerPosition.X <= (screenBounds.Width + 16) / 2 && position.X > 0) || (playerPosition.X >= (screenBounds.Width - 16) / 2 && position.X < texture.Width - screenBounds.Width); 
    //    if (!canScroll) return false;
        
    //    float lastPosition = position.X;
        
    //    var change = playerVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
    //    position.X += faceRight ? change : -change;
    //    position.X = MathHelper.Clamp(position.X, 0, texture.Width - screenBounds.Width);
    //    return position.X - lastPosition != 0;
    //}

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Vector2.Zero, new Rectangle((int)(position.X/scale),0,screenBounds.Width,screenBounds.Height), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}