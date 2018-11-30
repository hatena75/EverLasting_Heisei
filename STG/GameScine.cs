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

        asd.TextObject2D obj;

        asd.TextObject2D obj2;


        //BGM
        asd.SoundSource bgm;

        //再生中のBGMを扱うためのID
        int? playingBgmId;
        
        //乱数を用意する
        static Random rnd = new Random();
        private int randomNumber1 = rnd.Next(0, 480);
        private int randomNumber2 = rnd.Next(0,640);
        

        protected override void OnRegistered()
        {
            gameLayer = new asd.Layer2D();
            AddLayer(gameLayer);

            asd.Layer2D backgroundLayer = new asd.Layer2D();
            backgroundLayer.DrawingPriority = -10;
            AddLayer(backgroundLayer);

            asd.Layer2D layertext = new asd.Layer2D();
            layertext.DrawingPriority = +10;
            AddLayer(layertext);

            Background bg = new Background(new asd.Vector2DF(0.0f, 0.0f), "Resources/BackGround.png");

            backgroundLayer.AddObject(bg);

            // 動く背景を生成する。
            MovingBackground bgMove1 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f), "Resources/haikei0.png", 10.0f);
            MovingBackground bgMove2 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f - bgMove1.Texture.Size.Y), "Resources/haikei1.png", 10.0f);

            // 動く背景を文字レイヤーに追加する。（プレイヤーなどより上に表示させたいため）
            layertext.AddObject(bgMove1);
            layertext.AddObject(bgMove2);

            // フォントを生成する。
            var font = asd.Engine.Graphics.CreateDynamicFont("", 30, new asd.Color(255, 255, 255, 255), 1, new asd.Color(255, 255, 255, 255));

            // 文字描画オブジェクトを生成する。
            obj = new asd.TextObject2D();

            // 描画に使うフォントを設定する。
            obj.Font = font;

            // 描画位置を指定する。
            obj.Position = new asd.Vector2DF(0.0f, 0.0f);

            Singleton.Getsingleton();
            // 描画する文字列を指定する。
            obj.Text = "平成" + Player.year + "年";



            // フォントを生成する。
            var font2 = asd.Engine.Graphics.CreateDynamicFont("", 30, new asd.Color(255, 0, 0, 255), 0, new asd.Color(255, 0, 0, 255));

            // 文字描画オブジェクトを生成する。
            obj2 = new asd.TextObject2D();

            // 描画に使うフォントを設定する。
            obj2.Font = font2;

            // 描画位置を指定する。
            obj2.Position = new asd.Vector2DF(130.0f, 0.0f);

            Singleton.Getsingleton();

            //obj.Scale = new asd.Vector2DF(0.7f, 0.7f);

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layertext.AddObject(obj);
            layertext.AddObject(obj2);

            player = new Player();

            gameLayer.AddObject(player);

            //BGMを読み込む
            bgm = asd.Engine.Sound.CreateSoundSource("Resources/m-art_TSUKINIKAZE.ogg", false);

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
                asd.Engine.Sound.SetVolume((int)playingBgmId, 0.1f);
                asd.Engine.AddObject2D(new Option(new asd.Vector2DF(40.0f, 0.0f), player));
                //asd.Engine.AddObject2D(new Option(new asd.Vector2DF(-40.0f, 0.0f), player));
            }

            //敵が湧く処理
            if (count > 50)
            {
                if (count % 40 == 0)
                {
                    for(int i = 0; i < (Player.year / 2) + 2; i++)
                    {
                        randomNumber1 = rnd.Next(0, 480);
                        asd.Engine.AddObject2D(new RushingEnemy4(new asd.Vector2DF(randomNumber1, 0.0f), player));
                    }
                    

                }
            }

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push)
            {
                // 描画する文字列を指定する。
                obj2.Text = "リタイア宣言中";
            }

            //平成の表示を更新
            obj.Text = "平成" + Player.year + "年";
            
            count++;
        }
    }
}
