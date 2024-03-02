using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Formats.Asn1.AsnWriter;

public class ScrollingBackground
{
    private Texture2D texture;
    private Vector2 position1;
    private int screenHeight;
    private int screenWidth;
    private float scrollSpeed;
    private Rectangle[] blocks;
    Rectangle Aera = new Rectangle(0, 0, 3378, 225);
    float scale;

    public ScrollingBackground(ContentManager content, string texturePath, int screenWidth, int screenHeight, float scrollSpeed = 1f)
    {
        texture = content.Load<Texture2D>(texturePath);

        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.scrollSpeed = scrollSpeed;

        position1 = new Vector2(0, 0);
        scale = Math.Max((float)screenWidth / Aera.Width, (float)screenHeight / Aera.Height);
        InitializeBlocks();
    }
    public Rectangle[] Blocks
    {
        get { return blocks; }
    }
    public void Update(GameTime gametime)
    {
        //position1.X -= scrollSpeed;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle destinationRect1 = new Rectangle((int)position1.X, (int)position1.Y, (int)(Aera.Width*scale), (int)(Aera.Height* scale));
        spriteBatch.Draw(texture, destinationRect1, Aera, Color.White);
    }
    private void InitializeBlocks()
    {
        blocks = new Rectangle[2];
        blocks[0] = new Rectangle(0, 207, 3378, 18);
        blocks[1] = new Rectangle(256,144, 15, 15);
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = new Rectangle(
               (int)(blocks[i].X* scale),
               (int)(blocks[i].Y* scale),
               (int)(blocks[i].Width * scale),
               (int)(blocks[i].Height * scale));

        }
    }
}
