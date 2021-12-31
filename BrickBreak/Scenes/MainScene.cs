using BrickBreak.GameObjects;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreak.Scenes
{
    class MainScene : GameState
    {
        public enum State
        {
            Setup,
            Playing,
            Begin,
            Win,
            Loss
        }

        public static State CurrentState { get; set; } = State.Begin;
        
        Ball ball = new Ball();
        Paddle paddle = new Paddle();
        

        const int ROWS = 12;
        const int COLS = 6;

        int numberOfBricks = 0;

        SpriteGameObject[,] brickSet = new SpriteGameObject[ROWS, COLS];

        MessageBox messageBox = new MessageBox();

        public MainScene()
        {
            
            gameObjects.AddChild(ball);
            gameObjects.AddChild(paddle);
            gameObjects.AddChild(messageBox);
            gameObjects.AddChild(messageBox.Header);
            gameObjects.AddChild(messageBox.Message);

            messageBox.Header.Parent = messageBox;
            messageBox.Message.Parent = messageBox;

            for (int x = 0; x < ROWS; x++)
            {
                for (int y = 0; y < COLS; y++)
                {
                    brickSet[x, y] = new SpriteGameObject("Sprites/bricks@6x1", .6f);
                    brickSet[x, y].LocalPosition = new Vector2(1f + x * 49f, 64 + y * 25f);

                    switch (y)
                    {
                        case 0:
                            brickSet[x, y].SheetIndex = 0; break;
                        case 1:
                            brickSet[x, y].SheetIndex = 1; break;
                        case 2:
                            brickSet[x, y].SheetIndex = 2; break;
                        case 3:
                            brickSet[x, y].SheetIndex = 3; break;
                        case 4:
                            brickSet[x, y].SheetIndex = 4; break;
                        case 5:
                            brickSet[x, y].SheetIndex = 5; break;
                        default:
                            brickSet[x, y].SheetIndex = 0; break;
                    }


                    gameObjects.AddChild(brickSet[x, y]);
                }
            }

            

            Reset();
            CurrentState = State.Begin;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //between .5 and 1

            for (int x = 0; x < ROWS; x++)
            {
                for (int y = 0; y < COLS; y++)
                {
                    if (CollisionDetection.ShapesIntersect(ball.BoundingBox, brickSet[x, y].BoundingBox) && brickSet[x, y].Visible)
                    {
                        ball.CutSound = false;
                        ExtendedGame.AssetManager.PlaySoundEffect("Audio/contact_block");
                        //If the ball is within the top and bottom alignment of the brick and collides......
                        if (ball.LocalPosition.Y <= brickSet[x, y].BoundingBox.Bottom && 
                            ball.LocalPosition.Y >= brickSet[x, y].BoundingBox.Top)
                        {

                            //Make the ball go the opposite direction on the X axis
                            if (ball.SpeedX < 0)
                                ball.SpeedX = Math.Abs(ball.SpeedX);
                            else if (ball.SpeedX > 0)
                                ball.SpeedX = -ball.SpeedX;
                        }

                        //if the ball is withing the left and right alignment of the brick and collides........
                        if (ball.LocalPosition.X <= brickSet[x, y].BoundingBox.Right &&
                            ball.LocalPosition.X >= brickSet[x, y].BoundingBox.Left)
                        {
                            //....then make the ball go on the opposite direction on the Y axis
                            if (ball.SpeedY < 0)
                                ball.SpeedY = Math.Abs(ball.SpeedY);
                            else if (ball.SpeedY > 0)
                                ball.SpeedY = -ball.SpeedY;
                        }



                        //Make the brick in visible and break the loop
                        brickSet[x, y].Visible = false;
                        numberOfBricks++;
                        break;
                    }
                }
            }

            //If player wins then reset with 'win' header
            if (numberOfBricks == COLS * ROWS)
            {
                CurrentState = State.Win;
                messageBox.Visible = true;
                ball.Reset();
            } else if (ball.LocalPosition.Y > 480) //If player loses then reset with 'lose' header
            {
                if (CurrentState != State.Loss)
                    ExtendedGame.AssetManager.PlaySoundEffect("Audio/loss");
                CurrentState = State.Loss;
                messageBox.Visible = true;
                ball.Reset();
            }



        }

        public Paddle Paddle {
            get
            {
                return paddle;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
           
                if (messageBox.PlayButton.Contains(inputHelper.MousePositionWorld) && inputHelper.MouseLeftButtonDown() && messageBox.Visible)
                {
                    messageBox.Visible = false;
                    Reset();
                }
            


        }

        public override void Reset()
        {
            base.Reset();
            for (int x = 0; x < ROWS; x++)
                for (int y = 0; y < COLS; y++)
                    brickSet[x, y].Visible = true;

            numberOfBricks = 0;
            CurrentState = State.Setup;

            ball.Parent = paddle;
            ball.LocalPosition = new Vector2(0, -11);
            

            ball.Reset();
        }
    }
}
