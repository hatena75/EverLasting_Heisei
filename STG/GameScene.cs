using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class GameScene : asd.Scene
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

        asd.PostEffectLightBloom lightBloom = new asd.PostEffectLightBloom();

        public asd.Layer2D layertext = new asd.Layer2D();
        
        

        protected override void OnRegistered()
        {
            gameLayer = new asd.Layer2D();
            AddLayer(gameLayer);

            asd.Layer2D backgroundLayer = new asd.Layer2D();
            backgroundLayer.DrawingPriority = -10;
            AddLayer(backgroundLayer);


            
            layertext.DrawingPriority = +10;
            AddLayer(layertext);

            Background bg = new Background(new asd.Vector2DF(0.0f, 0.0f), "Resources/BackGround.png");

            backgroundLayer.AddObject(bg);


            // ライトブルームのぼかしの強さを設定する。
            //lightBloom.Intensity = 100.0f;

            // ライトブルームの露光の強さを設定する。
            //lightBloom.Exposure = 3.0f;

            // ライトブルームで光らせる明るさのしきい値を設定する。
            //lightBloom.Threshold = 0.6f;

            // レイヤーにライトブルームのポストエフェクトを適用。
            //backgroundLayer.AddPostEffect(lightBloom);

            // 動く背景を生成する。
            MovingBackground bgMove1 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f), "Resources/haikei20.png", 10.0f);
            MovingBackground bgMove2 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f - bgMove1.Texture.Size.Y), "Resources/haikei21.png", 10.0f);
            MovingBackground bgMove3 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f), "Resources/haikei20.png", 5.0f);
            MovingBackground bgMove4 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f - bgMove1.Texture.Size.Y), "Resources/haikei21.png", 5.0f);
            //MovingBackground bgMove5 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f), "Resources/haikei2.png", 2.0f);
            //MovingBackground bgMove6 = new MovingBackground(new asd.Vector2DF(-2.0f, 30.0f - bgMove1.Texture.Size.Y), "Resources/haikei3.png", 2.0f);

            var bgcolor = bgMove1.Color;
            bgcolor.A = 128;
            bgMove1.Color = bgcolor;

            bgcolor = bgMove2.Color;
            bgcolor.A = 128;
            bgMove2.Color = bgcolor;

            // 動く背景を文字レイヤーに追加する。（プレイヤーなどより上に表示させたいため）
            backgroundLayer.AddObject(bgMove1);
            backgroundLayer.AddObject(bgMove2);
            backgroundLayer.AddObject(bgMove3);
            backgroundLayer.AddObject(bgMove4);
            //backgroundLayer.AddObject(bgMove5);
            //backgroundLayer.AddObject(bgMove6);

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
            obj.Text = "平成元年";

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

            var syouwa1 = new ItemListMenu(new asd.Vector2DF(30,615), "syouwa1", 0);
            var syouwa2 = new ItemListMenu(new asd.Vector2DF(120, 615), "syouwa2", 1);
            var taisyou1 = new ItemListMenu(new asd.Vector2DF(200, 615), "taisyou1", 2);
            var taisyou2 = new ItemListMenu(new asd.Vector2DF(280, 615), "taisyou2", 3);
            var meiji1 = new ItemListMenu(new asd.Vector2DF(360, 615), "meiji1", 4);
            var meiji2 = new ItemListMenu(new asd.Vector2DF(440, 615), "meiji2", 5);

            layertext.AddObject(syouwa1);
            layertext.AddObject(syouwa2);
            layertext.AddObject(taisyou1);
            layertext.AddObject(taisyou2);
            layertext.AddObject(meiji1);
            layertext.AddObject(meiji2);


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
                //asd.Engine.AddObject2D(new Option(new asd.Vector2DF(40.0f, 0.0f), player));
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
                        asd.Engine.AddObject2D(new RushingEnemy(new asd.Vector2DF(randomNumber1, 0.0f), player));
                    }
                }

                if(count % 900 == 0)
                {
                    asd.Engine.AddObject2D(new ItemEnemy(new asd.Vector2DF(randomNumber1, 0.0f), player));
                }
            }

            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.R) == asd.KeyState.Push)
            {
                // 描画する文字列を指定する。
                obj2.Text = "リタイア宣言中";
            }

            //平成の表示を更新
            if (Player.year == 1)
            {
                obj.Text = "平成元年";
            }
            else
            {
                obj.Text = "平成" + Player.year + "年";
            }

            count++;

            //lightBloom.Intensity = 10.0f + 10.0f * Math.Abs((float)Math.Sin(count / 60.0 / 1.0));
            //lightBloom.Exposure =  0.5f * Math.Abs( (float)Math.Sin(count / 60.0 / 1.0) );
        }
    }
}
