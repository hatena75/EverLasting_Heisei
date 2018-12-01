﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class Player : CollidableObject
    {
        int count = 0;
        int retire_count = int.MaxValue;
        string bomb_tex;
        
        //ショットの効果音
        private asd.SoundSource shotSound;

        //ボムを発動したときの効果音
        private asd.SoundSource bombSound;

        private asd.Vector2DF moveVelocity;

        public static int year = 1;

        public static bool retire_flg = false;

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

        public Player()
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Player.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Position = new asd.Vector2DF(320, 240);


            Radius = Texture.Size.X / 5.0f;

            //ショットの効果音を読み込む
            shotSound = asd.Engine.Sound.CreateSoundSource("Resources/Shot.wav", true);

            //ボム発動の効果音を読み込む
            bombSound = asd.Engine.Sound.CreateSoundSource("Resources/Bomb.wav", true);
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
                Position += moveVelocity * 5;
            }

            if (count % 16 == 0) //元は
            {
                asd.Engine.AddObject2D(new ChangeableBullet(Position + new asd.Vector2DF(0.0f,-15.0f), new asd.Vector2DF(0,-25), "Tower_tokyo", true));
                asd.Engine.AddObject2D(new ChangeableBullet(Position + new asd.Vector2DF(0.0f,-15.0f), new asd.Vector2DF(5,-5), "Vote", false));
                asd.Engine.AddObject2D(new ChangeableBullet(Position + new asd.Vector2DF(0.0f, -15.0f), new asd.Vector2DF(-5, -5), "Vote", false));
                // ショットの効果音を再生
                //asd.Engine.Sound.Play(shotSound);
            }

            //左クリックでボム発動
            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.MouseButtonState.Push)
            {
                Singleton.Getsingleton();

                if (Singleton.singleton.bomblimit > 0)
                {
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
                                bomb_tex = "jouki";
                                break;
                            case 6:
                                bomb_tex = "yukiti";
                                break;
                            case 7:
                                bomb_tex = "gasutou";
                                break;
                        }

                        Bomb bomb = new Bomb(Position, 360 / 8 * i, bomb_tex);
                        // ボム オブジェクトをエンジンに追加
                        asd.Engine.AddObject2D(bomb);
                    }
                    asd.Engine.Sound.Play(bombSound);

                    Singleton.singleton.bomblimit--;
                }
            }

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push && retire_count == int.MaxValue)
            {
                retire_count = count;
            }

            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            count++;

            if (retire_count + 600 == count && IsAlive == true)
            {
                retire_flg = true;
                Dispose();
            }

            if (retire_count != int.MaxValue && count % 12 == 0)
            {
                var color = this.Color;
                color.A -= 5; //Aの初期値255
                this.Color = color;
            }

            if (count % 300 == 0 && IsAlive == true) //生きているなら5秒で1年経つ
            {
                year++;
            }

        }
    }
}
