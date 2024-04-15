using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Player;
using System;
using Sprint0;
using Sprint0.Controller;
using Sprint2;
using Sprint2.Block;

public class UpdateManager
{
    private Game1 game;

    public UpdateManager(Game1 game)
    {
        this.game = game;
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

    public void PlayUpdate(Vector2 playerPosition)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            game.currentState = Game1.GameState.MainMenu;
            game.reStart();
            game.music.stopMusic();
            game.music.playPause();
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.C))
        {
            game.currentState = Game1.GameState.Paused;
            game.music.stopMusic();
            game.music.playPause();
        }
        if (game.blockCollision.pipeAbove())
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                game.music.playPipe();
                game.currentState = Game1.GameState.Cave;
                game.player.inCave(true);
                game.player.setPosition(playerPosition);
            }
        }
        if (game.player.getPosition().X >= game._camera.Map.Width - 50)
        {
            game.currentState = Game1.GameState.Vectory;
            game.music.stopMusic();
            game.music.startEnd();
        }

        if (((Flag)(game.Items[game.Items.Count - 1])).over)
        {
            game.currentState = Game1.GameState.Vectory;
            game.music.stopMusic();
            game.music.startEnd();
        }
    }

    public void LevelUpdate(int gameIndex, GameTime gameTime)
    {
        if (gameIndex == 0 || gameIndex == 1)
        {
            game.item.Update(gameTime, game.player);
            game._camera.Update(game.player.getPosition(), game.currentState);

            foreach (IController controller in game.controllerList)
            {
                controller.Update(gameTime);
            }
            game.player.Update(gameTime);
            foreach (IBlock b in game.blocks)
            {
                b.Update(gameTime, game.player);
                if (CollisionDetector.DetectCollision(b.Bounds, game.player.Bounds))
                {
                    game.blockCollision.Update(b, game.player);
                }
            }
            foreach (IBlock b in game.DestroyBlock)
            {
                game.blocks.Remove(b);
            }
            foreach (IProjectiles pro in game.projectiles)
            {
                pro.Update(gameTime, game.enemies1, game.player, game.blocks);
            }
            if (game.enemies1.Count > 0)
            {
                foreach (ISprite e in game.enemies1)
                {

                    e.Update(gameTime, game.player);
                    game.mario_health.Update(gameTime, game.player, e);
                }
                foreach (ISprite e in game.DestroyEnemies)
                {
                    game.enemies1.Remove(e);
                }
            }
            foreach (ISprite I in game.Items)
            {
                I.Update(gameTime, game.player);
                game.coin_count.Update(gameTime, game.player, I);
            }
            foreach (ISprite I in game.DestroyItems)
            {
                game.Items.Remove(I);
            }
        }
        else
        {
            foreach (IController controller in game.controllerList)
            {
                controller.Update(gameTime);
            }
            foreach (IProjectiles pro in game.projectiles)
            {
                pro.Update(gameTime, game.enemies, game.player, game.blocks);
            }
            if (game.enemies.Count > 0)
            {
                game.enemies[0].Update(gameTime, game.player);
                if (CollisionDetector.DetectCollision(game.player.Bounds, game.enemies[0].Bounds))
                {
                    game.player.damaged(gameTime);
                }
            }
            game.player.Update(gameTime);
            game.block.Update(gameTime, game.player);
        }
    }

    public void PauseUpdate()
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
    Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            game.currentState = Game1.GameState.Playing;
        }
    }

    public void CaveUpdate(Vector2 playerPosition, GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            game.currentState = Game1.GameState.MainMenu;
            game.player.inCave(false);
            game.player.setPosition(playerPosition);
        }
        if (game.blockCollision.pipeAbove())
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                game.currentState = Game1.GameState.Playing;
                game.music.playPipe();
                game.player.inCave(false);
                game.player.setPosition(new Vector2(1000, 300));
            }
        }
        game.player.Update(gameTime);
        game._camera.Update(game.player.getPosition(), game.currentState);
        foreach (IBlock b in game.blocksC)
        {
            b.Update(gameTime, game.player);
            if (CollisionDetector.DetectCollision(b.Bounds, game.player.Bounds))
            {
                game.blockCollision.Update(b, game.player);
            }
        }
        foreach (ISprite I in game.ItemsC)
        {
            I.Update(gameTime, game.player);
        }
        foreach (IProjectiles pro in game.projectiles)
        {
            pro.Update(gameTime, game.enemies1, game.player, game.blocks);
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