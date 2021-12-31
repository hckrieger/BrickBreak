using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreak.Scenes
{
    class TitleScreen : GameState
    {
        TextGameObject title = new TextGameObject("Fonts/title", 1f, Color.White, TextGameObject.Alignment.Center);
        TextGameObject clickToPlay = new TextGameObject("Fonts/clickToPlay", 1f, Color.White, TextGameObject.Alignment.Center);
        TextGameObject message = new TextGameObject("Fonts/message", 1f, Color.White, TextGameObject.Alignment.Center);
        float visibilityTimer, startTime;
        
        public TitleScreen()
        {
            gameObjects.AddChild(title);
            title.LocalPosition = new Vector2(295, 70);
            title.Text = "Brick Break";

            gameObjects.AddChild(clickToPlay);
            clickToPlay.LocalPosition = new Vector2(295, 230);
            clickToPlay.Text = "Click to Play";

            gameObjects.AddChild(message);
            message.LocalPosition = new Vector2(295, 350);
            message.Text = "Programmed by Hunter Krieger";

            visibilityTimer = .666f;
            startTime = visibilityTimer;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            visibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (visibilityTimer <= 0)
            {
                if (clickToPlay.Visible)
                    clickToPlay.Visible = false;
                else if (!clickToPlay.Visible)
                    clickToPlay.Visible = true;

                visibilityTimer = startTime;
            } 

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            
            if (inputHelper.MouseLeftButtonPressed())
            {
                    ExtendedGame.GameStateManager.SwitchTo("MAIN_State");
            }    
        }
    }
}
