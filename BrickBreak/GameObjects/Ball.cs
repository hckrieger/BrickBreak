using BrickBreak.Scenes;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrickBreak.GameObjects
{
    class Ball : SpriteGameObject
    {

        public bool CutSound { get; set; }

        public Circle CircleBounds { get; set; }
        public float Angle { get; set; }

        private float speedX, speedY;
        private float speed;
        public Ball() : base("Sprites/ball", .5f)
        {
            SetOriginToCenter();


        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * speed;
            velocity = new Vector2(speedX, speedY);

            if (Parent == null)
            {
                if (CollisionDetection.ShapesIntersect(MainScene.Paddle.PaddleSections[0], CircleBounds)) { GetAngle(225); }
                if (CollisionDetection.ShapesIntersect(MainScene.Paddle.PaddleSections[1], CircleBounds)) { GetAngle(240); }
                if (CollisionDetection.ShapesIntersect(MainScene.Paddle.PaddleSections[2], CircleBounds)) { GetAngle(255); }
                if (CollisionDetection.ShapesIntersect(MainScene.Paddle.PaddleSections[3], CircleBounds)) { if (LocalPosition.X > 300)  
                                                                                                                GetAngle(268);  
                                                                                                            else 
                                                                                                                GetAngle(272); 
                                                                                                          }
                if (CollisionDetection.ShapesIntersect(MainScene.Paddle.PaddleSections[4], CircleBounds)) { GetAngle(285); }
                if (CollisionDetection.ShapesIntersect(MainScene.Paddle.PaddleSections[5], CircleBounds)) { GetAngle(300); }
                if (CollisionDetection.ShapesIntersect(MainScene.Paddle.PaddleSections[6], CircleBounds)) { GetAngle(315); }

                foreach(Rectangle rect in MainScene.Paddle.PaddleSections)
                {
                    if (CollisionDetection.ShapesIntersect(rect, CircleBounds))
                    {   
                        if (!CutSound)
                        {
                            ExtendedGame.AssetManager.PlaySoundEffect("Audio/contact_paddle");
                            CutSound = true;
                        }
                    } 
                    


                    
                }
            }

            


            if (LocalPosition.X >= 584 && speedX > 0)
            {
                speedX = -speedX;
                ExtendedGame.AssetManager.PlaySoundEffect("Audio/contact_wall");
                CutSound = false;
            }
            else if (LocalPosition.X <= 5 && speedX < 0)
            {
                speedX = Math.Abs(speedX);
                ExtendedGame.AssetManager.PlaySoundEffect("Audio/contact_wall");
                CutSound = false;
            }
                

            if (LocalPosition.Y <= 5 && speedY < 0)
            {
                speedY = Math.Abs(speedY);
                ExtendedGame.AssetManager.PlaySoundEffect("Audio/contact_wall");
                CutSound = false;
            }
                

            

            CircleBounds = new Circle(sprite.Height/2, LocalPosition);
        }

        public void GetAngle(float angle)
        {
            
            var angleToRadians = MathHelper.ToRadians(angle);
            speedX = (float)Math.Cos(angleToRadians) * speed;
            speedY = (float)Math.Sin(angleToRadians) * speed;
        }

        public MainScene MainScene
        {
            get
            {
                return (MainScene)ExtendedGame.GameStateManager.GetGameState("MAIN_State");
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonPressed() &&
               MainScene.CurrentState == MainScene.State.Setup)
            {
                var refVector = GlobalPosition;
                Parent = null;

                LocalPosition = refVector;
                speed = 375;
               
            }
                
        }

        public Vector2 Velocity
        {
            get { return velocity; }
        }

        public float SpeedX {
            get { return speedX; }
            set { speedX = value; }
        }
        public float SpeedY
        {
            get { return speedY; }
            set { speedY = value; }
        }

        public override void Reset()
        {
            base.Reset();
            speed = 0;
            speedX = 0;
            speedY = 0;
        }

    }
}
