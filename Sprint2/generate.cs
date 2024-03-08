using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;

public class generate
{
    private Game1 game;
    private Texture2D texture;

    public generate(string csv, Game1 game, Texture2D texture)
    {
        this.game = game;
        this.texture = texture;

        using (var reader = new StreamReader(csv))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                string type = values[0];
                string name = values[1];
                int x = int.Parse(values[2]);
                int y = int.Parse(values[3]);

                CreateGameObject(type, name, new Vector2(x, y));
            }
        }
    }

    public void CreateGameObject(string type, string name, Vector2 position)
    {
        switch (type)
        {
            case "block":
                CreateBlock(name, position);
                break;
            case "item":
                CreateItem(name, position);
                break;
            case "enemy":
                CreateEnemy(name, position);
                break;
            default:
                break;
        }
    }

    
    public void CreateBlock(string name, Vector2 position)
    {

    }


    public void CreateItem(string name, Vector2 position)
    {
        switch (name)
        {
            //case "Box":
                //game
        }
    }

    public void CreateEnemy(string name, Vector2 position)
    {
        switch (name)
        {
            case "Goomba":
                game.AddEnemy(new Goomba(texture, position, game.GetScreenBounds()));
                break;
        }
    }
}