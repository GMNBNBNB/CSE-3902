using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint0.Controller;

public class MenuController : IController
{
    private Game1 game;
    private KeyboardState previousKeyboardState;
    private MouseState previousMouseState;


    public MenuController(Game1 game)
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
            if (game.currentState == Game1.GameState.MainMenu)
            {
                game.changePreviousLevel();
            }
        }

        //down for move down
        if (state.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
        {
            if (game.currentState == Game1.GameState.MainMenu)
            {
                game.changeNextLevel();
            }
        }

        //enter for select


        if (state.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
        {
            if (game.currentState == Game1.GameState.MainMenu)
            {
                game.currentState = Game1.GameState.Playing;
            }
        }
        
        //new Vector2(GetScreenBounds().Width / 2 - font.MeasureString(text).X/2, GetScreenBounds().Height / 2 - 100 + 50 * i)
        if (mouseState.X > game.GetScreenBounds().Width / 2 - game.font.MeasureString("LEVEL 1").X / 2 && mouseState.X < game.GetScreenBounds().Width / 2 + game.font.MeasureString("LEVEL 1").X / 2)
        {
            
            if (mouseState.Y > game.GetScreenBounds().Height / 2 - 100 &&
                mouseState.Y < game.GetScreenBounds().Height / 2 - 50)
            {
                game.focusLevel(0);
            }
            else if (mouseState.Y > game.GetScreenBounds().Height / 2 - 50 &&
                     mouseState.Y < game.GetScreenBounds().Height / 2)
            {
                game.focusLevel(1);
            }
            else if (mouseState.Y > game.GetScreenBounds().Height / 2 &&
                     mouseState.Y < game.GetScreenBounds().Height / 2 + 50)
            {
                game.focusLevel(2);
            }
            else if (mouseState.Y > game.GetScreenBounds().Height / 2 + 50 &&
         mouseState.Y < game.GetScreenBounds().Height / 2 + 100)
            {
                game.focusLevel(3);
            }

            if (mouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                game.currentState = Game1.GameState.Playing;
                game.music.startMusic();
            }
        }
        
        

        previousKeyboardState = state;
        previousMouseState = mouseState;

    }
}