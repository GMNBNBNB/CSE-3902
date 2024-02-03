using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class TextSprite : ISprite
{
    private Vector2 position;
    private string text;
    private Color color;
    private SpriteFont font;

    public TextSprite(Vector2 position, SpriteFont font)
    {
        this.position = position;
        this.text = "Credits\nProgram Made By: Bowei Kou\nSpirtes from: https://www.mariouniverse.com/wp-content/img/sprites/nes/smb/characters.gif\n";
        this.color = Color.Black;
        this.font = font;
    }

    public void Update(GameTime gameTime) { }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, position, color);
    }
}
