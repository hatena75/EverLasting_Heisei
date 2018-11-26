using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class ChangeableBullet : CollidableObject
    {
        bool Penetrate = false;

        asd.Vector2DF Vector;

        public ChangeableBullet(asd.Vector2DF position, asd.Vector2DF vec, string texture, bool flg)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/{texture}.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            DrawingPriority = -10;

            Position = position;

            Radius = Texture.Size.X / 2.0f;

            Vector = vec;

            Penetrate = flg;
        }

        public override void OnCollide(CollidableObject obj)
        {
            if (Penetrate == false)
            {
                Dispose();
            }
        }

        protected override void OnUpdate()
        {
            Position += Vector;
            var windowSize = asd.Engine.WindowSize;
            if (Position.Y < -Texture.Size.Y || Position.Y > windowSize.Y + Texture.Size.Y || Position.X < -Texture.Size.X || Position.X > windowSize.X + Texture.Size.X)
            {
                Dispose();
            }
        }
    }
}
