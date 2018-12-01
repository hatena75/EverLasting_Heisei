using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class GameOverScene : asd.Scene
    {
        asd.TextObject2D obj;

        protected override void OnRegistered()
        {
            asd.Layer2D layerpict = new asd.Layer2D();
            layerpict.DrawingPriority = -10;
            AddLayer(layerpict);

            asd.Layer2D layertext = new asd.Layer2D();
            AddLayer(layertext);

            asd.TextureObject2D background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Over.png");

            layerpict.AddObject(background);

            // フォントを生成する。
            var font = asd.Engine.Graphics.CreateDynamicFont("", 30, new asd.Color(255, 255, 255, 255), 1, new asd.Color(255, 255, 255, 255));

            // 文字描画オブジェクトを生成する。
            obj = new asd.TextObject2D();

            // 描画に使うフォントを設定する。
            obj.Font = font;

            // 描画位置を指定する。
            obj.Position = new asd.Vector2DF(70, 240);

            Singleton.Getsingleton();

            // 描画する文字列を指定する。
            if (Player.retire_flg == true)
            {
                obj.Text = "平成は" + Player.year + "年で改号された";
            }
            else
            {
                obj.Text = "";
            }

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layertext.AddObject(obj);


        }

        bool isOverChanging = false;

        protected override void OnUpdated()
        {

            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.MouseButtonState.Push && isOverChanging == false)
            {
                asd.Engine.ChangeSceneWithTransition(new GameScine(), new asd.TransitionFade(1.0f, 1.0f));

                isOverChanging = true;

                Singleton.singleton.score = 0;
                Singleton.singleton.itemhaving = 0;
                Singleton.singleton.itemcount = 0;
                Singleton.singleton.bomblimit = 1;
                Player.year = 1;
                Player.retire_flg = false;
            }
        }
        
    }
}
