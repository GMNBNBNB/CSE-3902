using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using Sprint2;
using Sprint2.Block;
using static System.Reflection.Metadata.BlobBuilder;

public class Generate
{
    private Game1 game;
    private Texture2D texture;
    private Texture2D enemy;
    private List<IBlock> block;
    private string csvU;
    private string csvM;
    private string csvM2;
    private string csvM3;
    private string csvM4;
    private string csvC;
    private int level;
    private Texture2D BlockTexture;
    private Texture2D textureI;
    private Texture2D pipeTexture;
    public Generate(Game1 game, Texture2D texture, Texture2D enemy, Texture2D BlockTexture, Texture2D textureI, Texture2D pipeTexture, List<IBlock> block, int level)
    {
        this.game = game;
        this.block = block;
        this.texture = texture;
        this.enemy = enemy;
        this.BlockTexture = BlockTexture;
        this.textureI = textureI;
        this.pipeTexture = pipeTexture;
        csvM = @"..\..\..\mapGen.csv";
        csvM2 = @"..\..\..\mapGen2.csv";
        csvM3 = @"..\..\..\mapGen3.csv";
        csvM4 = @"..\..\..\mapGen4.csv";
        csvC = @"..\..\..\cavGen.csv";
        this.level = level;
        if (level == 1)
        {
            csvU = csvM;
        }
        else if (level == 2)
        {
            csvU = csvM2;
        }
        else if (level == 3)
        {
            csvU = csvM3;
        }
        else if (level == 4)
        {
            csvU = csvM4;
        }
        else
        {
            csvU = csvC;
        }

        using (var reader = new StreamReader(csvU))
        {
            bool isFirstLine = true;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                var values = line.Split(',');

                string type = values[0];
                string name = values[1];
                float x = float.Parse(values[2]);
                float y = float.Parse(values[3]);

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
        switch (name)
        {
            case "BrownBlock1":
                game.AddBlock(new BrownBlock1(BlockTexture, position, game), level);
                break;
            case "BrownBlock2":
                game.AddBlock(new BrownBlock2(BlockTexture, position, game), level);
                break;
            case "pipe":
                game.AddBlock(new PipeEn(pipeTexture, position), level);
                break;
            case "MusBlock":
                game.AddBlock(new MusBlock(BlockTexture, position, textureI, game), level);
                break;
            case "StarBlock":
                game.AddBlock(new StarBlock(BlockTexture, position, textureI, game), level);
                break;
            case "FlowerBlock":
                game.AddBlock(new FlowerBlock(BlockTexture, position, textureI, game), level);
                break;
            case "CoinBlock":
                game.AddBlock(new CoinBlock(BlockTexture, position, textureI, game), level);
                break;
        }
    }


    public void CreateItem(string name, Vector2 position)
    {
        switch (name)
        {
            case "Coin":
                game.AddItem(new Coin(game, textureI, position), level);
                break;
            case "DFlower":
                game.AddItem(new DFlower(game, textureI, position), level);
                break;
        }
    }

    public void CreateEnemy(string name, Vector2 position)
    {
        switch (name)
        {
            case "Goomba":
                game.AddEnemy(new Goomba(enemy, position, game.GetMap(), game, block), level);
                break;
            case "NonFlyTortoise":
                game.AddEnemy(new NonFlyTortoise(enemy, position, game.GetMap(), game, block), level);
                break;
            case "FlyTortoiseEnemy":
                game.AddEnemy(new FlyTortoiseEnemy(enemy, position, game.GetMap(), game, block), level);
                break;
            case "FlowerEnemy":
                game.AddEnemy(new FlowerEnemy(enemy, position, game.GetMap(), game, block), level);
                break;
            case "FireEmemy":
                game.AddEnemy(new FireEmemy(enemy, position), level);
                break;
            case "TortoiseEnemy":
                game.AddEnemy(new TortoiseEnemy(game, enemy, position, game.GetMap(),block ,game.projectiles), level);
                break;
        }
    }
}