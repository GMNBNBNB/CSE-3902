using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0;
using System.Text;
using Sprint2.Icon;

namespace Sprint2
{
    public class CheatCodeManager
    {
        private StringBuilder inputText = new StringBuilder();
        private SpriteFont font;
        private bool isActive;
        private KeyboardState previousKeyboardState;
        private IPlayer player;
        private Health health;
        private Game1 game;

        public bool IsActive { get => isActive; private set => isActive = value; }

        public CheatCodeManager(SpriteFont font, IPlayer player, Health health, Game1 game) {
            this.font = font;
            isActive = false;
            this.player = player;
            this.health = health;
            this.game = game;   
        }
        public void Update()
        {
            if (!isActive)
                return;

            var currentKeyboardState = Keyboard.GetState();
            var keys = currentKeyboardState.GetPressedKeys();

            foreach (var key in keys)
            {
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    if (key == Keys.Back && inputText.Length > 0) 
                    {
                        inputText.Remove(inputText.Length - 1, 1);
                    }
                    else if (key == Keys.Enter) 
                    {
                        isActive = false;
                        ProcessCheatCode(inputText.ToString());
                        inputText.Clear();
                    }
                    else
                    {
                        var keyString = ConvertKeyToString(key);
                        if (keyString != null)
                            inputText.Append(keyString);
                    }
                }
            }
            previousKeyboardState = currentKeyboardState;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if (!isActive)
                return;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.Identity);
            spriteBatch.DrawString(font, inputText.ToString(), new Vector2(100, 100), Color.White);
            spriteBatch.End();  
        }

        public void Activate()
        {
            isActive = true;
            inputText.Clear();
            previousKeyboardState = Keyboard.GetState();
        }

        private string ConvertKeyToString(Keys key)
        {
            if (key >= Keys.A && key <= Keys.Z)
            {
                return key.ToString().ToLower();
            }
            else if (key == Keys.Space)
            {
                return " ";
            }
            else if (key >= Keys.D0 && key <= Keys.D9)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    switch (key)
                    {
                        case Keys.D1: return "!";
                        case Keys.D2: return "@";
                        case Keys.D3: return "#";
                        default: return key.ToString().Substring(1);  
                    }
                }
                else
                {
                    return key.ToString().Substring(1); 
                }
            }

            return null;
        }

        private void ProcessCheatCode(string code)
        {
            // Process the cheat code
            if (code == "increase health")
            {
                health.increaseHealth();
            }
            if (code.Length >= 5)
            {
                string firstFiveChars = code.Substring(0, 5);
                string value = code.Substring(6);

                switch (firstFiveChars)
                {
                    case "speed":
                        player.setSpeed(float.Parse(value));
                        break;
                    case "jumpS":
                        break;
                }
            }
        }
    }
}
