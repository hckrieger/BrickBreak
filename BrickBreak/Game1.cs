using BrickBreak.Scenes;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickBreak
{
    public class Game1 : ExtendedGame
    {

        public Game1()
        {
            IsMouseVisible = true;

            windowSize = new Point(589, 450);
            worldSize = new Point(589, 450);

            IsMouseVisible = true;
        }



        protected override void LoadContent()
        {
            base.LoadContent();

            // TODO: use this.Content to load your game content here
            GameStateManager.AddGameState("MAIN_State", new MainScene());
            GameStateManager.AddGameState("TITLE_State", new TitleScreen());
            GameStateManager.SwitchTo("TITLE_State");
        }

    }
}
