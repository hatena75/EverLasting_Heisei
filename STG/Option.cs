using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Option : CollidableObject
    {
        int count = 0;

        //ショットの効果音
        private asd.SoundSource shotSound;

        private asd.Vector2DF pos;

        Player player;

        /*
        public override void OnCollide(CollidableObject obj)
        {
            Layer.AddObject(new BreakObjectEffect(Position));
            Dispose();
        }

        
        protected void CollideWith(CollidableObject obj)
        {
            if (obj == null)
                return;
            if (obj is Enemy)
            {
                CollidableObject enemyBullet = obj;

                if (IsCollide(enemyBullet))
                {
                    OnCollide(enemyBullet);
                    enemyBullet.OnCollide(this);
                }
            }
        }
        */

        public Option(asd.Vector2DF pos, Player player)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Choco.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            this.player = player;

            Position = player.Position + pos; //posはPlayerとの距離で、ずっとこの距離を維持する。

            this.pos = pos;

            count = player.count + 1;
            //Radius = Texture.Size.X / 5.0f;

            //ショットの効果音を読み込む
            shotSound = asd.Engine.Sound.CreateSoundSource("Resources/Shot.wav", true);
            
        }

        protected override void OnUpdate()
        {
            //線形補間の原理でもっさりとプレイヤーについていく
            Position += ((player.Position + pos) - Position) * 0.3f;

            if (count % 16 == 0) //元は4
            {

                if (ItemController.itemlist[0] == true)
                {
                    asd.Engine.AddObject2D(new ChangeableBullet(Position + new asd.Vector2DF(0.0f, -15.0f), new asd.Vector2DF(0, -25), "Tower_tokyo", true));
                }

                if (ItemController.itemlist[1] == true)
                {
                    asd.Engine.AddObject2D(new SplitBullet(Position + new asd.Vector2DF(0.0f, -15.0f)));
                }

                if (ItemController.itemlimit[3] < 2)
                {
                    asd.Engine.AddObject2D(new ChangeableBullet(Position + new asd.Vector2DF(0.0f, -15.0f), new asd.Vector2DF(-5, -5), "Vote", false));

                    if (ItemController.itemlimit[3] < 1)
                    {
                        asd.Engine.AddObject2D(new ChangeableBullet(Position + new asd.Vector2DF(0.0f, -15.0f), new asd.Vector2DF(5, -5), "Vote", false));

                    }
                }

                if (ItemController.itemlist[0] != true && ItemController.itemlist[1] != true && ItemController.itemlist[3] != true)
                {
                    asd.Engine.AddObject2D(new Bullet(Position + new asd.Vector2DF(0.0f, -15.0f)));
                }
                // ショットの効果音を再生
                //asd.Engine.Sound.Play(shotSound);
            }

            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);
            Position = position;

            count++;
        }
    }
}
