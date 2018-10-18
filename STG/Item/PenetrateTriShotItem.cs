﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG.Item
{
    class PenetrateTriShotItem : Item
    {
        public PenetrateTriShotItem(asd.Vector2DF pos) : base(pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Item8.png");
        }

        public override void OnCollide(CollidableObject obj)
        {
            base.OnCollide(obj);
            Singleton.singleton.itemhaving = 5;
            Singleton.singleton.itemcount = 0;
        }

        private void CollideWith(CollidableObject obj)
        {
            if (obj == null)
                return;

            if (obj is Bullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    bullet.OnCollide(this);
                    asd.Engine.AddObject2D(new ItemGetEffect_PenetrateTriShot(new asd.Vector2DF(320, 240)));

                }
            }

            if (obj is PenetrateBullet)
            {
                CollidableObject bullet = obj;

                if (IsCollide(bullet))
                {
                    OnCollide(bullet);
                    //bullet.OnCollide(this);
                    asd.Engine.AddObject2D(new ItemGetEffect_PenetrateTriShot(new asd.Vector2DF(320, 240)));

                }
            }
        }

        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith((obj as CollidableObject));
        }

    }
}