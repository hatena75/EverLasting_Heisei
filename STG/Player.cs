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
        
        //ショットの効果音
        private asd.SoundSource shotSound;

        //ボムを発動したときの効果音
        private asd.SoundSource bombSound;


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

        public enum playershot
        {
            PenetrateShot,TwoShot,ThreeShot,TriShot
        }
        
        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith(obj as CollidableObject);

            // マウスカーソルの座標を取得。
            asd.Vector2DF posp = asd.Engine.Mouse.Position;

            //マウスカーソルの座標の角度（度）を計算。
            double rad = Math.Atan2(posp.Y - Position.Y, posp.X - Position.X);

            double rad2 = rad * 180 / Math.PI;

            //プレイヤーの向きをマウスカーソルの向きに変える
            Angle = (float)rad2;

            //マウスの左ボタンを押している間弾が発射される
            if (count % 2 == 0)
            {
                asd.Vector2DF dir = posp - Position;

                asd.Vector2DF moveVelocity = dir.Normal * 10.0f;

                asd.Engine.AddObject2D(new Bullet(Position, moveVelocity));
                
                // ショットの効果音を再生
                //asd.Engine.Sound.Play(shotSound);
            }

            //左クリックでボム発動
            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.MouseButtonState.Push)
            {
                Singleton.Getsingleton();

                if (Singleton.singleton.bomblimit > 0)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        // ボムを生成
                        Bomb bomb = new Bomb(Position, 360 / 24 * i);

                        // ボム オブジェクトをエンジンに追加
                        asd.Engine.AddObject2D(bomb);
                    }
                    asd.Engine.Sound.Play(bombSound);

                    Singleton.singleton.bomblimit--;
                }
            }

            asd.Vector2DF position = Position;

            position.X = asd.MathHelper.Clamp(position.X, asd.Engine.WindowSize.X - Texture.Size.X / 2.0f, Texture.Size.X / 2.0f);
            position.Y = asd.MathHelper.Clamp(position.Y, asd.Engine.WindowSize.Y - Texture.Size.Y / 2.0f, Texture.Size.Y / 2.0f);

            Position = position;

            count++;
        }
    }
}
