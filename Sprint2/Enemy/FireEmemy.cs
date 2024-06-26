﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FireEmemy : ISprite
{
    private Texture2D texture;
    private Vector2 position;
    private int currentFrame;
    private Rectangle[] frames;


    public FireEmemy(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
        frames = new Rectangle[2];
        frames[0] = new Rectangle(0, 154, 15, 16);
        frames[1] = new Rectangle(61, 154, 15, 16);
        currentFrame = 0;

    }

    public void Update(GameTime gameTime, IPlayer p)
    {
        position += new Vector2(0, 2);
        if (position.Y > 450)
            position = new Vector2(position.X, -50);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, frames[currentFrame], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
    }
    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                frames[currentFrame].Width * 2,
                frames[currentFrame].Height * 2
            );

            return bounds;
        }
    }
}