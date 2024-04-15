using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0;

public class Flag : ISprite
{
    private Texture2D textureQigan, textureQizi;
    private Vector2 position,position2;
    private int currentFrame;
    private double timeSinceLastFrame;
    private Rectangle[] frames;
    private Game1 game;
    public bool beginMove;
    public bool over;

    public Flag(Game1 game, Texture2D textureQigan,Texture2D textureQizi, Vector2 position)
    {
        this.game = game;
        this.textureQigan = textureQigan;
        this.textureQizi = textureQizi;
        this.position = position;
        this.position2 = position + new Vector2(0,15);
        frames = new Rectangle[2];
        frames[0] = new Rectangle(12, 2, 16, 167);
        frames[1] = new Rectangle(0, 0, 16, 15);
        currentFrame = 0;
        timeSinceLastFrame = 0;
        beginMove = false;
        over = false;
    }

    public void Update(GameTime gameTime, IPlayer player)
    {
        if (CollisionDetector.DetectCollision(Bounds, player.Bounds))
        {
            beginMove = true;
        }

        if (beginMove && !over)
            position2 += new Vector2(0, 1);
        if (position2.Y == 370)
            over = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(textureQigan, position, frames[0], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        spriteBatch.Draw(textureQizi, position2, frames[1],Color.White);
    }
    public Rectangle Bounds
    {
        get
        {
            Rectangle bounds = new Rectangle(
                (int)position.X,
                (int)position.Y,
                frames[0].Width,
                frames[0].Height
            );

            return bounds;
        }
    }
}
