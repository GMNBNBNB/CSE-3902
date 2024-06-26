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
using System.Linq;

public class UpdateManager
{
    private Game1 game;
    private Boolean check;

    public UpdateManager(Game1 game)
    {
        this.game = game;
        check = false;
    }

    public void MainMenuUpdate()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
            game.Exit();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            game.currentState = Game1.GameState.Playing;
            game.music.stopMusic();
            game.music.startMusic();
        }
    }

    public void PlayUpdate(Vector2 playerPosition, KeyboardState previousS)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            game.currentState = Game1.GameState.MainMenu;
            game.reStart();
            game.music.stopMusic();
            game.music.playPause();
        }
        if (game.blockCollision.pipeAbove())
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.S) && !previousS.IsKeyDown(Keys.S)) || (Keyboard.GetState().IsKeyDown(Keys.Down) && !previousS.IsKeyDown(Keys.Down)))
            {
                game.music.playPipe();
                game.currentState = Game1.GameState.Gointopipe;
                game.player.inCave(true);
                game.player.setPosition(playerPosition);
                game.player2.inCave(true);
                game.player2.setPosition(playerPosition);
            }
        }
        if (game.player.getPosition().X >= game._camera.Map.Width - 50)
        {
            game.currentState = Game1.GameState.Vectory;
            game.music.stopMusic();
            game.music.startEnd();
        }
        if (game.player2.getPosition().X >= game._camera.Map.Width - 50)
        {
            game.currentState = Game1.GameState.Vectory;
            game.music.stopMusic();
            game.music.startEnd();
        }
        if (((Flag)(game.Items[game.Items.Count - 1])).over || ((Flag)(game.Items2[game.Items2.Count - 1])).over 
            || ((Flag)(game.Items3[game.Items3.Count - 1])).over 
            || ((Flag)(game.Items4[game.Items4.Count - 1])).over)
        {
            game.currentState = Game1.GameState.Vectory;
            game.music.stopMusic();
            game.music.startEnd();
        }
    }
    public void Level1Update(int gameindex, GameTime gameTime, List<ISprite> Item, List<IBlock> block, List<ISprite> enemie)
    {
        check = false;
        if (gameindex == 2)
        {
            game.timer.Update(gameTime, game);
        }
        game.item.Update(gameTime, game.player);
        game.item.Update(gameTime, game.player2);
        if (game.player.getPosition().X > game.player2.getPosition().X)
        {
            game._camera.Update(game.player.getPosition(), game.currentState);
        }
        else
        {
            game._camera.Update(game.player2.getPosition(), game.currentState);
        }
        foreach (IController controller in game.controllerList)
        {
            controller.Update(gameTime);
        }
        game.player.Update(gameTime);
        game.player2.Update(gameTime);
        foreach (IBlock b in block)
        {
            b.Update(gameTime, game.player);
            b.Update(gameTime, game.player2);
            if (CollisionDetector.DetectCollision(b.Bounds, game.player.Bounds))
            {
                game.blockCollision.Update(b, game.player);
            }
            if (CollisionDetector.DetectCollision(b.Bounds, game.player2.Bounds))
            {
                game.blockCollision.Update(b, game.player2);
            }
        }
        foreach (IBlock b in game.DestroyBlock)
        {
            block.Remove(b);
        }
        foreach (IProjectiles pro in game.projectiles)
        {
            pro.Update(gameTime, enemie, game.player, block);
            pro.Update(gameTime, enemie, game.player2, block);
        }
        foreach (ISprite I in Item)
        {
            I.Update(gameTime, game.player);
            I.Update(gameTime, game.player2);
        }
        if (enemie.Count > 0)
        {
            foreach (ISprite e in enemie)
            {
                e.Update(gameTime, game.player);
                e.Update(gameTime, game.player2);
                game.mario_health.Update(gameTime, game.player, game.player2, e);
                game.score_point.UpdateEnemyScore(gameTime, game.player, e);
                game.score_point.UpdateEnemyScore(gameTime, game.player2, e);

            }
            foreach (ISprite e in game.DestroyEnemies)
            {
                if (!game.scoreNeed.Contains(e))
                {
                    game.scoreNeed.Add(e);
                    check = true;
                    game.enemySposition.X = e.Bounds.X;
                    game.enemySposition.Y = e.Bounds.Y;
                }
                enemie.Remove(e);
            }
        }
        foreach (ISprite I in game.DestroyItems)
        {
            Item.Remove(I);
        }
        game.scorePopup.Update(gameTime, check);
    }

    public void PauseUpdate()
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
    Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            game.currentState = Game1.GameState.Playing;
        }
    }

    public void CaveUpdate(Vector2 playerPosition, GameTime gameTime, KeyboardState previousS)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            game.currentState = Game1.GameState.MainMenu;
            game.player.inCave(false);
            game.player.setPosition(playerPosition);
            game.player2.inCave(false);
            game.player2.setPosition(playerPosition);
        }
        if (game.blockCollision.pipeAbove())
        {
            if ((Keyboard.GetState().IsKeyDown(Keys.S) && !previousS.IsKeyDown(Keys.S)) || (Keyboard.GetState().IsKeyDown(Keys.Down) && !previousS.IsKeyDown(Keys.Down)))
            {
                game.currentState = Game1.GameState.Gointopipe;
                game.music.playPipe();
                game.player.inCave(false);
                game.player.setPosition(new Vector2(1000, 300));
                game.player2.inCave(false);
                game.player2.setPosition(new Vector2(1000, 300));
            }
        }
        game.player.Update(gameTime);
        game.player2.Update(gameTime);
        if (game.player.getPosition().X > game.player2.getPosition().X)
        {
            game._camera.Update(game.player.getPosition(), game.currentState);
        }
        else
        {
            game._camera.Update(game.player2.getPosition(), game.currentState);
        }
        foreach (IBlock b in game.blocksC)
        {
            b.Update(gameTime, game.player);
            b.Update(gameTime, game.player2);
            if (CollisionDetector.DetectCollision(b.Bounds, game.player.Bounds))
            {
                game.blockCollision.Update(b, game.player);
            }
            if (CollisionDetector.DetectCollision(b.Bounds, game.player2.Bounds))
            {
                game.blockCollision.Update(b, game.player2);
            }
        }
        foreach (ISprite I in game.ItemsC)
        {
            I.Update(gameTime, game.player);
            I.Update(gameTime, game.player2);
        }
        foreach (IProjectiles pro in game.projectiles)
        {
            pro.Update(gameTime, game.enemies1, game.player, game.blocks);
            pro.Update(gameTime, game.enemies1, game.player2, game.blocks);
        }
        foreach (IController controller in game.controllerList)
        {
            controller.Update(gameTime);
        }
        foreach (ISprite I in game.DestroyItems)
        {
            game.ItemsC.Remove(I);
        }
    }
}