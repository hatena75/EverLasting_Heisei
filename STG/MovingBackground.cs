using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class MovingBackground : asd.TextureObject2D
    {
        private float vel;

        public MovingBackground(asd.Vector2DF pos, string texturePath, float moveVelocity)
            : base()
        {
            Position = pos;

            Texture = asd.Engine.Graphics.CreateTexture2D(texturePath);

            vel = moveVelocity;

            AlphaBlend = asd.AlphaBlendMode.Add;

            DrawingPriority = (int)(vel * 100);
        }

        protected override void OnUpdate()
        {
            Position += new asd.Vector2DF(0.0f, vel);

            if (Position.Y >= asd.Engine.WindowSize.Y)
            {
                Position -= new asd.Vector2DF(0.0f, 2 * Texture.Size.Y);
            }
        }
    }
}
