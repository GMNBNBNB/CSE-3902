using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;

public class DamagedCommand : ICommand
{
    private Game1 game;

    public DamagedCommand(Game1 game)
    {
        this.game = game;
    }

    public void Execute()
    {
        game.takeDamage();
    }
}