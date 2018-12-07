using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class SplitBullet : CollidableObject
    {
        /*
        public override void OnCollide(CollidableObject obj)
        {
            Dispose();
        }
        */

        private int count, splitcount;

        public SplitBullet(asd.Vector2DF position)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Bubble.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            DrawingPriority = +10;

            Position = position;

            Radius = Texture.Size.X / 2.0f;

            count = 0;

            splitcount = 35;
        }

        protected override void OnUpdate()
        {
            Position += new asd.Vector2DF(0, -8);

            if (count == splitcount)
            {
                
                    for (int i = 0; i < 4; ++i)
                    {
                        asd.Vector2DF dir = new asd.Vector2DF(1, 0);
                        dir.Length = 10.0f;
                        dir.Degree = 45 + i * 90;
                        asd.Engine.AddObject2D(new ChangeableBullet(Position, dir, "Money_fly",false));

                    }

                    Dispose();
       
            }

            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
                Dispose();
            }

            count++;
        }
    }
}
