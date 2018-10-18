using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class GameScine : asd.Scene
    {
        Player player;

        bool isSceneChanging = false;

        asd.Layer2D gameLayer;

        int count = 0;

        //BGM
        asd.SoundSource bgm;

        //再生中のBGMを扱うためのID
        int? playingBgmId;
        
        //乱数を用意する
        static Random rnd = new Random();
        private int randomNumber1 = rnd.Next(0, 640);
        private int randomNumber2 = rnd.Next(0,480);
        

        protected override void OnRegistered()
        {
            gameLayer = new asd.Layer2D();

            asd.Layer2D backgroundLayer = new asd.Layer2D();

            backgroundLayer.DrawingPriority = -10;
            
            AddLayer(gameLayer);
            AddLayer(backgroundLayer);

            Background bg = new Background(new asd.Vector2DF(0.0f, 0.0f), "Resources/Bg.png");

            backgroundLayer.AddObject(bg);

            player = new Player();

            gameLayer.AddObject(player);

            //BGMを読み込む
            bgm = asd.Engine.Sound.CreateSoundSource("Resources/Bgm.ogg", false);

            //BGMループ
            bgm.IsLoopingMode = true;

            //IDはnull(BGMは流れてない）
            playingBgmId = null;
        }

        

        protected override void OnUpdated()
        {           
            //ゲームオーバー時処理
            if (!isSceneChanging && !player.IsAlive)
            {
                asd.Engine.ChangeSceneWithTransition(new GameOverScene(), new asd.TransitionFade(1.0f, 1.0f));

                if (playingBgmId.HasValue)
                {
                    asd.Engine.Sound.FadeOut(playingBgmId.Value, 1.0f);
                    playingBgmId = null;
                }

                isSceneChanging = true;
            }

            //BGM再生
            if (count == 10)
            {
                playingBgmId = asd.Engine.Sound.Play(bgm);
            }

            //敵が湧く処理
            if (count > 50)
            {
                if (count % 50 == 0)
                {
                    randomNumber1 = rnd.Next(0, 640);
                    asd.Engine.AddObject2D(new RushingEnemy2(new asd.Vector2DF(randomNumber1, 0.0f), player));

                    randomNumber1 = rnd.Next(0, 640);
                    asd.Engine.AddObject2D(new RushingEnemy2(new asd.Vector2DF(randomNumber1, 0.0f), player));

                    randomNumber1 = rnd.Next(0, 640);
                    asd.Engine.AddObject2D(new RushingEnemy2(new asd.Vector2DF(randomNumber1, 0.0f), player));

                    randomNumber1 = rnd.Next(0, 640);
                    asd.Engine.AddObject2D(new RushingEnemy2(new asd.Vector2DF(randomNumber1, 0.0f), player));

                    randomNumber1 = rnd.Next(0, 640);
                    asd.Engine.AddObject2D(new RushingEnemy2(new asd.Vector2DF(randomNumber1, 0.0f), player));

                }



            }

            count++;
        }
    }
}
