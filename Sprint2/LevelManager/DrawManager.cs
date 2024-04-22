using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Player;
using Player2;
using System;
using Sprint0;
using Sprint0.Controller;
using Sprint2;
using Sprint2.Block;

public class DrawManager
{
    private Game1 game;

    public DrawManager(Game1 game)
    {
        this.game = game;
    }

    public void MainMenuDraw(SpriteBatch spriteBatch, Rectangle screen, SpriteFont font, int gameIndex)
    {
        game.map.Draw(spriteBatch);
        spriteBatch.DrawString(font, "Super Mario", new Vector2(screen.Width / 2 - font.MeasureString("Main Menu").X, screen.Height / 2 - 200), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        for (int i = 0; i < 3; i++)
        {

            if (i != gameIndex)
            {
                string text = "LEVEL " + (i + 1) + " ";
                spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * i), Color.White);
            }
            else
            {
                string text = "LEVEL " + (i + 1) + ">";
                spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * i), Color.Yellow);
            }
        }
        spriteBatch.DrawString(font, "Select the level to enter the game", new Vector2(screen.Width / 2 - (font.MeasureString("Select the level to enter the game").X * 0.75f), screen.Height / 2 + 100), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
    }

    public void Level1Draw(SpriteBatch spriteBatch, int gameIndex, Map map, List<ISprite> Item, List<IBlock> block, List<ISprite> enemie)
    {
        map.Draw(spriteBatch);
        game.player.Draw(spriteBatch);
        game.player2.Draw(spriteBatch);
        foreach (ISprite e in enemie)
        {
            e.Draw(spriteBatch);
        }
        foreach (IProjectiles pro in game.projectiles)
        {
            pro.Draw(spriteBatch);
        }
        foreach (IBlock b in block)
        {
            b.Draw(spriteBatch);
        }
        foreach (ISprite I in Item)
        {
            I.Draw(spriteBatch);
        }
    }

    public void PauseDraw(SpriteBatch spriteBatch, Rectangle screen, SpriteFont font, int pauseIndex)
    {
        spriteBatch.DrawString(font, "Paused", new Vector2(100, 100), Color.White);
        if (pauseIndex != 0)
        {
            string text = "Continue";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 0), Color.White);
        }
        else
        {
            string text = "Continue";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 0), Color.Yellow);
        }


        if (pauseIndex != 1)
        {
            string text = "Back to menu";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 1), Color.White);
        }
        else
        {
            string text = "Back to menu";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 1), Color.Yellow);
        }
    }

    public void CaveDraw(SpriteBatch spriteBatch)
    {
        game.cave.Draw(spriteBatch);
        game.player.Draw(spriteBatch);
        game.player2.Draw(spriteBatch);
        foreach (IProjectiles pro in game.projectiles)
        {
            pro.Draw(spriteBatch);
        }
        foreach (IBlock b in game.blocksC)
        {
            b.Draw(spriteBatch);
        }
        foreach (ISprite I in game.ItemsC)
        {
            I.Draw(spriteBatch);
        }
    }

    public void VectoryDraw(SpriteBatch spriteBatch, Rectangle screen, SpriteFont font, int vectoryIndex)
    {
        spriteBatch.DrawString(font, "Vectory", new Vector2(screen.Width / 2 - font.MeasureString("Main Menu").X, screen.Height / 2 - 200), Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        if (vectoryIndex != 0)
        {
            string text = "Restart";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 0), Color.White);
        }
        else
        {
            string text = "Restart";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 0), Color.Yellow);
        }


        if (vectoryIndex != 1)
        {
            string text = "Back to menu";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 1), Color.White);
        }
        else
        {
            string text = "Back to menu";
            spriteBatch.DrawString(font, text, new Vector2(screen.Width / 2 - font.MeasureString(text).X / 2, screen.Height / 2 - 100 + 50 * 1), Color.Yellow);
        }
    }
}