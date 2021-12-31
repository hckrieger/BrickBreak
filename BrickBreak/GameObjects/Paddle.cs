using BrickBreak.Scenes;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreak.GameObjects
{
    class Paddle : SpriteGameObject
    {
        Rectangle section1, section2, section3, section4, section5,
                  section6, section7;
        float posX, posY;

        public Paddle() : base("Sprites/paddle", .5f)
        {
            SetOriginToCenter();
            posX = 300;
            posY = 440;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);


            LocalPosition = new Vector2(posX, posY);

            if (MainScene.CurrentState == MainScene.State.Playing ||
                MainScene.CurrentState == MainScene.State.Setup)
                posX = MathHelper.Clamp(inputHelper.MousePositionWorld.X, 28, 559);


        }

        public override void Update(GameTime gameTime)
        {


            section1 = CustomBounds(new Rectangle(0, 0, 9, 12));
            section2 = CustomBounds(new Rectangle(9, 0, 9, 12));
            section3 = CustomBounds(new Rectangle(18, 0, 9, 12));
            section4 = CustomBounds(new Rectangle(27, 0, 9, 12));
            section5 = CustomBounds(new Rectangle(36, 0, 9, 12));
            section6 = CustomBounds(new Rectangle(45, 0, 9, 12));
            section7 = CustomBounds(new Rectangle(54, 0, 9, 12));

            base.Update(gameTime);
        }

        public Rectangle[] PaddleSections
        {
            get
            {
                Rectangle[] boundingBoxes = new Rectangle[7] { section1, section2, section3, section4, section5,
                                                                section6, section7 };
                return boundingBoxes;
            }
        }

        public MainScene MainScene
        { 
            get
            {
                return (MainScene)ExtendedGame.GameStateManager.GetGameState("MAIN_Scene");
            }
        }

    }
}
