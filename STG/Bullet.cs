using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Bullet : CollidableObject
    {

        public override void OnCollide(CollidableObject obj)
        {
            Dispose();
        }


        public Bullet(asd.Vector2DF position)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Def_spiral.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            DrawingPriority = -10;

            Position = position;

            Radius = Texture.Size.X / 2.0f;
        }

        protected override void OnUpdate()
        {
            Position += new asd.Vector2DF(0, -10);
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
                Dispose();
            }
        }
    }
}
