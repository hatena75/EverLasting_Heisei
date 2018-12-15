using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class SniperNemesis : Enemy
    {
        private const int maxHP = 10;
        private int HP;

        private asd.Vector2DF moveVelocity;

        public SniperNemesis(asd.Vector2DF pos, Player player)
            : base(pos, player)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/5g.png");
            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);
            moveVelocity = new asd.Vector2DF();
            HP = maxHP;
            Radius = Texture.Size.Y / 2.0f;
            //破壊時の効果音を読み込む
            //deathSound = asd.Engine.Sound.CreateSoundSource("Resources/Explode2.wav", true);
        }

        protected override void OnUpdate()
        {
            if (count < 30)
            {
                moveVelocity = new asd.Vector2DF(0, 30-count);
                Position += moveVelocity * 0.1f;
            }
            else if (count < 330)
            {
                if (count % 100 == 0 || count % 103 == 0 || count % 106 == 0)
                {
                    asd.Engine.AddObject2D(new HighRushingEnemy(Position, player));
                }
            }
            else
            {
                moveVelocity = new asd.Vector2DF(0, -(count - 330) );
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