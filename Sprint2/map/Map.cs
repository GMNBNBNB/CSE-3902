using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using Sprint2.Block;


public class Map
{
    private Vector2 position;
    private Rectangle screenBounds;
    private Texture2D texture;
    private float scale;
    private Vector2 position1;

    public void LoadContent(Texture2D texture)
    {
        scale = Math.Max((float)screenBounds.Width / texture.Width, (float)screenBounds.Height / texture.Height);
        this.texture = texture;
        position1 = new Vector2(0, 0);

    }

    public Map(Texture2D texture, Texture2D enemy, Rectangle screenBounds, Game1 game, Texture2D BlockTexture,Texture2D textureI, Texture2D pipeTexture, List<IBlock> block, int level)
    {
        this.screenBounds = screenBounds;
        position = Vector2.Zero;
        LoadContent(texture);
        Generate gen = new Generate(game, texture, enemy, BlockTexture, textureI, pipeTexture, block, level);
    }
   
    public void Draw(SpriteBatch spriteBatch)
    {

        Rectangle destinationRect1 = new Rectangle((int)position1.X, (int)position1.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        spriteBatch.Draw(texture, destinationRect1,Color.White);
    }
}