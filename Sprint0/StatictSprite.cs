using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class StaticSprite: ISprite
{
	private Texture2D texture;
    private Vector2 position;

    public StaticSprite(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.position = position;
    }

    public void Update(GameTime gameTime){}

    public void Draw(SpriteBatch spriteBatch)
    {
        Rectangle sourceRectangle = new Rectangle(224, 44, 12, 16);
        spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
    }
}
