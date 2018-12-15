using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Player : CollidableObject
    {
        public int count = 0;
        int retire_year = int.MaxValue;

        int Pspeed = 5; //移動速度
        int item_select = -1;
        static bool item_get = false;

        static public void Item_get()
        {
            item_get = true;
        }

        //ボムを発動したときの効果音
        private asd.SoundSource bombSound;

        //アイテム適用時効果音
        private asd.SoundSource selectSound;

        //再生中のアイテム適用時効果音を扱うためのID
        private int selectID;

        private asd.Vector2DF moveVelocity;

        public static int year = 1;

        public static bool retire_success = false; //リタイア成功フラグ

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

        private void Bomb(asd.Vector2DF pos)
        {
            string bomb_tex = ""; //ボムの画像を保存する関数

            for (int i = 0; i < 8; i++)
            {
                // ボムを生成
                switch (i)
                {
                    case 0:
                        bomb_tex = "zangiri";
                        break;
                    case 1:
                        bomb_tex = "sukiyaki";
                        break;
                    case 2:
                        bomb_tex = "sinbunsi";
                        break;
                    case 3:
                        bomb_tex = "renga";
                        break;
                    case 4:
                        bomb_tex = "randoseru";
                        break;
                    case 5:
                        bomb_tex = "gasutou";
                        break;
                    case 6:
                        bomb_tex = "jouki";
                        break;
                    case 7:
                        bomb_tex = "yukiti";
                        break;
                }

                Bomb bomb = new Bomb(pos, 360 / 8 * i, bomb_tex);
                // ボム オブジェクトをエンジンに追加
                asd.Engine.AddObject2D(bomb);
            }
            asd.Engine.Sound.Play(bombSound);
            ItemController.itemlimit[4]++;
        }

        public Player()
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Position = new asd.Vector2DF(320, 240);


            Radius = Texture.Size.X / 5.0f;

            //ボム発動の効果音を読み込む
            bombSound = asd.Engine.Sound.CreateSoundSource("Resources/Bomb.wav", true);

            selectSound = asd.Engine.Sound.CreateSoundSource("Resources/select.wav", true);

        }

        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith(obj as CollidableObject);

            // マウスカーソルの座標を取得。
            asd.Vector2DF posp = asd.Engine.Mouse.Position;
            
            //マウスカーソルの向きへの単位ベクトルを取得
            moveVelocity = (posp - Position).Normal;

            //マウスの座標に向けて移動させる(ifはぶるぶる防止)
            if((posp - Position).Length > 5.0f)
            {
                Position += moveVelocity * Pspeed;
            }

            if (count % 16 == 0) //元は4
            {

                if(ItemController.itemlist[0] == true && ItemController.itemlimit[0] > 0)
                {
                    asd.Engine.AddObject2D(new ChangeableBullet(Position + new asd.Vector2DF(0.0f, -15.0f), new asd.Vector2DF(0, -25), "Tower_tokyo", true));
                    ItemController.itemlimit[0]--;
                }
                else if (ItemController.itemlist[0] == true && ItemController.itemlimit[0] <= 0)
                {
                    ItemController.itemlimit[0] = 50;
                    ItemController.itemlist[0] = false;
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

                if (ItemController.itemlist[0] != true)
                {
                    asd.Engine.AddObject2D(new Bullet(Position + new asd.Vector2DF(0.0f, -15.0f)));
                }
 
            }

            //アイテム強化物獲得時の処理
            if (item_get == true)
            {               
                if (item_select == -1)
                {
                    ItemController.itemselecting[0] = true;
                }
                else if(item_select == 5)
                {
                    ItemController.itemselecting[0] = true;
                    ItemController.itemselecting[5] = false;
                    item_select = -1;
                }
                else
                {
                    ItemController.itemselecting[item_select] = false;
                    ItemController.itemselecting[item_select+1] = true;
                }
                item_select++;
                item_get = false;
            }

            if ((asd.Engine.Mouse.LeftButton.ButtonState == asd.MouseButtonState.Push) && 0 <= item_select && item_select <= 5 && ItemController.itemlist[item_select] == false)
            {
                ItemController.itemlist[item_select] = true; //アイテム使用
                ItemController.itemselecting[item_select] = false; //枠消去
                item_select = -1; //アイテム選択位置初期化

                selectID = asd.Engine.Sound.Play(selectSound);
                asd.Engine.Sound.SetVolume(selectID, 0.4f);
            }

            //ボム追加可能なら戻す。
            if (ItemController.itemlist[4] == true && ItemController.itemlimit[4] > 1)
            {
                ItemController.itemlimit[4]--;
                ItemController.itemlist[4] = false;
            }

            //右クリックでボム発動
            if (asd.Engine.Mouse.RightButton.ButtonState == asd.MouseButtonState.Push && ItemController.itemlimit[4] <= 2)
            {
                Bomb(Position);
            }

            //リタイア宣言
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push && retire_year == int.MaxValue)
            {
                retire_year = year;
            }

            //オプション追加処理
            if (ItemController.itemlist[5] == true && ItemController.itemlimit[5] != 0)
            {
                if (ItemController.itemlimit[5] == 2)
                {
                    asd.Engine.AddObject2D(new Option(new asd.Vector2DF(40.0f, 0.0f), this));
                    ItemController.itemlist[5] = false;
                    
                }
                else
                {
                    asd.Engine.AddObject2D(new Option(new asd.Vector2DF(-40.0f, 0.0f), this));

                }

                ItemController.itemlimit[5]--;
            }

            //スピードアップ
            if (ItemController.itemlist[2] == true && ItemController.itemlimit[2] > 0)
            {
                Pspeed += 2;
                var Wasi = new Wasi_mark(Position);
                asd.Engine.AddObject2D(Wasi);

                ItemController.itemlimit[2]--;
                ItemController.itemlist[2] = false;

                //スピードアップ追加不可能なら使用不能にする。
                if (ItemController.itemlimit[2] <= 0)
                {
                    ItemController.itemlist[2] = true;
                }
            }

            //選挙権投げ使用可能判定
            if (ItemController.itemlist[3] == true && ItemController.itemlimit[3] > 0)
            {
                ItemController.itemlimit[3]--;
                ItemController.itemlist[3] = false;
                if (ItemController.itemlimit[3] <= 0)
                {
                    ItemController.itemlist[3] = true;
                }
            }

            
            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            count++;

            //リタイア成功処理
            if (retire_year + 2 == year && IsAlive == true)
            {
                retire_success = true;
                Dispose();
            }

            /*
            //リタイア宣言時から徐々に消えていく
            if (retire_year != int.MaxValue && count % 12 == 0)
            {
                var color = this.Color;
                color.A -= 5; //Aの初期値255
                this.Color = color;
            }
            */

            if (count % (240 - (Pspeed - 5) * 30) == 0 && IsAlive == true) //生きているなら4秒で1年経つ
            {
                year++;
            }

        }
    }
}
