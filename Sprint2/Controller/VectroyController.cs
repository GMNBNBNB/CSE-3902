using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0.Controller;

public class VectoryController : IController
{
    private Game1 game;
    private KeyboardState previousKeyboardState;
    private MouseState previousMouseState;


    public VectoryController(Game1 game)
    {
        this.game = game;
    }
    public void Update(GameTime gameTime)
    {
        KeyboardState state = Keyboard.GetState();

        MouseState mouseState = Mouse.GetState();

        //up for move up
        if (state.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
        {
            game.changeVectory();
        }

        //down for move down
        if (state.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
        {
            game.changeVectory();
        }

        //enter for select
        if (state.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
        {
            game.reStart();
            if (game.getVectoryIndex() == 0)
            {
                game.currentState = Game1.GameState.Playing;
            }
            else
            {
                game.currentState = Game1.GameState.MainMenu;
            }
        }

        //new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X/2, GetScreenBounds().Height / 2 - 100 + 50 * i)
        if (mouseState.X > game.GetScreenBounds().Width / 2 - game.font.MeasureString("Restart").X / 2 && mouseState.X < game.GetScreenBounds().Width / 2 + game.font.MeasureString("Back to menu").X / 2)
        {

            if (mouseState.Y > game.GetScreenBounds().Height / 2 - 100 &&
                mouseState.Y < game.GetScreenBounds().Height / 2 - 50)
            {
                game.focusVectory(0);
            }
            else if (mouseState.Y > game.GetScreenBounds().Height / 2 - 50 &&
                     mouseState.Y < game.GetScreenBounds().Height / 2)
            {
                game.focusVectory(1);
            }

            if (mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                if (game.getVectoryIndex() == 0)
                {
                    game.reStart();
                    game.music.startMusic();
                    game.currentState = Game1.GameState.Playing;
                }
                else
                {
                    game.currentState = Game1.GameState.MainMenu;
                    game.reStart();
                    game.music.stopMusic();
                }
            }
        }



        previousKeyboardState = state;
        previousMouseState = mouseState;

    }
}
