using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class NormalEnemy : Enemy
    {
        private asd.Vector2DF moveVelocity;

        public NormalEnemy(asd.Vector2DF pos, asd.Vector2DF vec, Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = vec;
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Aihisa.png");
        }

        protected override void OnUpdate()
        {
            Position += moveVelocity;

            base.OnUpdate();
        }

        public override void OnCollide(CollidableObject obj)
        {
            base.OnCollide(obj);
        }

    }
}
