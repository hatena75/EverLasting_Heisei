using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class Wasi_mark : asd.TextureObject2D
    {
        //int count = 0;

        public Wasi_mark(asd.Vector2DF pos)
        {
            Position = pos;

            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Wasi.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            
            var scale = Scale;
            scale.Length -= 1.2f;
            Scale = scale;
            
        }

        protected override void OnUpdate()
        {
            var color = this.Color;
            color.A -= 5; //Aの初期値255
            this.Color = color;
            
            var scale = this.Scale;
            scale.Length += 0.03f;
            this.Scale = scale;

            //count++;

            if (color.A == 0)
            {
                Dispose();
            }
        }

    }
}
