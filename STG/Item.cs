namespace STG
{
    class Item : CollidableObject
    {

        protected asd.SoundSource itemGet;

        //再生中のBGMを扱うためのID
        protected int itemID;

        public Item(asd.Vector2DF pos)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D("Resources/kurumi.png");

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            Radius = Texture.Size.X / 2.0f;

            Position = pos;

            itemGet = asd.Engine.Sound.CreateSoundSource("Resources/Itemget.wav", true);
        }

        public override void OnCollide(CollidableObject obj)
        {
            itemID = asd.Engine.Sound.Play(itemGet);
            //asd.Engine.Sound.SetVolume(itemID, 0.2f);
            Player.Item_get();
            Dispose();
        }

        private void CollideWith(CollidableObject obj)
        {
            
            if (obj == null)
                return;

            if (obj is Player)
            {
                CollidableObject player = obj;

                if (IsCollide(player))
                {
                    OnCollide(player);
                    //player.OnCollide(this);

                }
            }            
        }

        protected override void OnUpdate()
        {
            foreach (var obj in Layer.Objects)
                CollideWith((obj as CollidableObject));

            Position += new asd.Vector2DF(0, 1);
        }

    }
}

