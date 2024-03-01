using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

public class ScrollingBackground
{
    private Texture2D texture;
    private Vector2 position1;
    private Vector2 position2;
    private int screenHeight;
    private int screenWidth;
    private float scrollSpeed;
    Rectangle Aera = new Rectangle(0, 0, 3378, 225);

    public ScrollingBackground(ContentManager content, string texturePath, int screenWidth, int screenHeight, float scrollSpeed = 1f)
    {
        texture = content.Load<Texture2D>(texturePath);

        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.scrollSpeed = scrollSpeed;

        position1 = new Vector2(0, 0);
    }
    public void Update(GameTime gametime)
    {
        position1.X -= scrollSpeed;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        float scale = Math.Max((float)screenWidth / Aera.Width, (float)screenHeight / Aera.Height);
        Rectangle destinationRect1 = new Rectangle((int)position1.X, (int)position1.Y, (int)(Aera.Width*scale), (int)(Aera.Height* scale));
        spriteBatch.Draw(texture, destinationRect1, Aera, Color.White);
    }
}
