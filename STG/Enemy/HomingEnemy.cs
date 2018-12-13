using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class HomingEnemy : Enemy
    {
        private asd.Vector2DF moveVelocity;

        public HomingEnemy(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Wahei.png");
            moveVelocity = (player.Position - Position).Normal;
        }

        protected override void OnUpdate()
        {
            moveVelocity = (player.Position - Position).Normal;

            Position += moveVelocity * 2.0f;

            base.OnUpdate();
        }

        public override void OnCollide(CollidableObject obj)
        {
            base.OnCollide(obj);
        }

    }
}
