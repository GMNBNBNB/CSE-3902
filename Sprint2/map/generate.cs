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
    private string csvC;
    private Boolean incave;
    private Texture2D BlockTexture;
    private Texture2D textureI;
    private Texture2D pipeTexture;
    public Generate(Game1 game, Texture2D texture, Texture2D enemy, Texture2D BlockTexture, Texture2D textureI, Texture2D pipeTexture, Boolean word, List<IBlock> block)
    {
        this.game = game;
        this.block = block;
        this.texture = texture;
        this.enemy = enemy;
        this.BlockTexture = BlockTexture;
        this.textureI = textureI;
        this.pipeTexture = pipeTexture;
        csvM = @"..\..\..\mapGen.csv";
        csvC = @"..\..\..\cavGen.csv";
        incave = word;
        if (word)
        {
            csvU = csvM;
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
                game.AddBlock(new BrownBlock1(BlockTexture, position, game), incave);
                break;
            case "pipe":
                game.AddBlock(new PipeEn(pipeTexture, position), incave);
                break;
            case "CoinBlock":
                game.AddBlock(new MusBlock(BlockTexture, position, textureI, game), incave);
                break;
        }
    }


    public void CreateItem(string name, Vector2 position)
    {
        switch (name)
        {
            case "Coin":
                game.AddItem(new Coin(game, textureI, position), incave);
                break;
            case "DFlower":
                game.AddItem(new DFlower(game, textureI, position), incave);
                break;
        }
    }

    public void CreateEnemy(string name, Vector2 position)
    {
        switch (name)
        {
            case "Goomba":
                game.AddEnemy(new Goomba(enemy, position, game.GetMap(), game, block));
                break;
        }
    }
}