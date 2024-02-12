using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;

public class MissileCommand : ICommand
{
    private Game1 game;

    public MissileCommand(Game1 game)
    {
        this.game = game;
    }

    public void Execute()
    {
        game.shotMissile();
    }
}