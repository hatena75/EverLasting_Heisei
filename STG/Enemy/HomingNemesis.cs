using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class HomingNemesis : Enemy
    {
        private const int maxHP = 15;
        private int HP;

        private asd.Vector2DF moveVelocity;

        public HomingNemesis(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/auto_car.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            HP = maxHP;
            Radius = Texture.Size.Y / 2.0f;
            //破壊時の効果音を読み込む
            //deathSound = asd.Engine.Sound.CreateSoundSource("Resources/Explode2.wav", true);
        }

        protected override void OnUpdate()
        {
            if (count < 60)
            {
                moveVelocity = new asd.Vector2DF(0, 60-count);
                Position += moveVelocity * 0.05f;
            }
            else if (count < 480)
            {
                if (count % 200 == 0)
                {
                    asd.Engine.AddObject2D(new HomingEnemy(Position, player));
                }
            }
            else
            {
                moveVelocity = new asd.Vector2DF(0, -(count - 480) );
                Position += moveVelocity * 0.05f;
            }
            
            

            base.OnUpdate();
        }

        public override void OnCollide(CollidableObject obj)
        {
            if (HP > 0)
            {
                HP -= 1;
            }
            else
            {
                base.OnCollide(obj);
            }


        }
    }
}