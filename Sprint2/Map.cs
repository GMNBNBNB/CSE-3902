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
    public void UpDate(GameTime gameTime)
    {

    }


    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Vector2.Zero, new Rectangle((int)(position.X/scale),0,screenBounds.Width,screenBounds.Height), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
    }
}