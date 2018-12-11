using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class RushingEnemy : Enemy
    {
        private asd.Vector2DF moveVelocity;

        public RushingEnemy(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Anshi.png");
            moveVelocity = (player.Position - Position).Normal;
        }

        protected override void OnUpdate()
        {
            Position += moveVelocity * 3.0f;

            base.OnUpdate();
        }

        public override void OnCollide(CollidableObject obj)
        {
            base.OnCollide(obj);
        }

    }
}
