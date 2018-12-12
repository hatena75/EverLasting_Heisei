using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class CircleNemesis : Enemy
    {
        private const int maxHP = 10;
        private int HP;

        private asd.Vector2DF moveVelocity;

        private asd.Vector2DF bulletVelecity;

        public CircleNemesis(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Boss2.png");
            HP = maxHP;
            //破壊時の効果音を読み込む
            //deathSound = asd.Engine.Sound.CreateSoundSource("Resources/Explode2.wav", true);
        }

        protected override void OnUpdate()
        {
            if (count < 60)
            {
                moveVelocity = new asd.Vector2DF(0, 60-count);
                Position += moveVelocity * 0.1f;
            }
            else if (count < 240)
            {
                if (count % 10 == 0)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        bulletVelecity = new asd.Vector2DF(0.0f, 7.0f);
                        bulletVelecity.Degree = i * 120 + (count / 10) * 15;
                        asd.Engine.AddObject2D(new NormalEnemy(new asd.Vector2DF(Position.X, Position.Y), bulletVelecity, player));

                    }
                }
            }
            else
            {
                moveVelocity = new asd.Vector2DF(0, -(count - 240) );
                Position += moveVelocity * 0.1f;
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