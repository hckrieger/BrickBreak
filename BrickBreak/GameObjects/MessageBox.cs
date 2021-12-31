using BrickBreak.Scenes;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreak.GameObjects
{
    class MessageBox : SpriteGameObject
    {
        public TextGameObject Header { get; set; } = new TextGameObject("Fonts/header", 1f, Color.Black, TextGameObject.Alignment.Center);
        public TextGameObject Message { get; set; } = new TextGameObject("Fonts/message", 1f, Color.Black, TextGameObject.Alignment.Center);
        public MessageBox() : base("Sprites/message", .9f)
        {
            SetOriginToCenter();
            LocalPosition = new Vector2(300, 225);


            Header.LocalPosition = new Vector2(0, 20) - new Vector2(0, Height/2);
            Message.LocalPosition = new Vector2(0, 60) - new Vector2(0, Height / 2);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Visible)
            {

                Message.Text = "\n\nTry Again?";
                if (MainScene.CurrentState == MainScene.State.Begin)
                {
                    Header.Text = "Instructions";

                    Message.Text = "-Use the mouse to move the paddle\n" +
                    "-Bounce the ball up to hit the blocks\n" +
                    "-Don't let the ball fall under\n" +
                    "-Click play to start\n" +
                    "-Click again to launch ball";
                }
                else if (MainScene.CurrentState == MainScene.State.Win)
                {
                    Header.Text = "You Win";

                    
                }
                else if (MainScene.CurrentState == MainScene.State.Loss)
                {
                    Header.Text = "Game Over";
                }
            } else
            {
                Header.Text = "";
                Message.Text = "";
            }

        }


        public Rectangle PlayButton {
            get {
                Rectangle button = new Rectangle((int)LocalPosition.X/2 + 121, (int)LocalPosition.Y/2 + 177, 54, 45);
                return button;
            }
        }

    }
}
