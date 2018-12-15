using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    class GameOverScene : asd.Scene
    {
        asd.TextObject2D kaigou,shougou;

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
            kaigou = new asd.TextObject2D();

            // 描画に使うフォントを設定する。
            kaigou.Font = font;

            // 描画位置を指定する。
            kaigou.Position = new asd.Vector2DF(70, 240);

            

            // 描画する文字列を指定する。
            if (Player.retire_success == true)
            {
                kaigou.Text = "平成は" + Player.year + "年で改号された";
            }
            else
            {
                kaigou.Text = "";
            }

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layertext.AddObject(kaigou);


            // フォントを生成する。
            var font2 = asd.Engine.Graphics.CreateDynamicFont("", 30, new asd.Color(255, 255, 255, 255), 1, new asd.Color(255, 255, 255, 255));

            // 文字描画オブジェクトを生成する。
            shougou = new asd.TextObject2D();

            // 描画に使うフォントを設定する。
            shougou.Font = font;

            // 描画位置を指定する。
            shougou.Position = new asd.Vector2DF(70, 340);

            // 描画する文字列を指定する。
            if (Player.retire_success == true)
            {
                if (Player.year == 3)
                {
                    shougou.Text = "リタイアRTA";
                }
                else if (Player.year == 4)
                {
                    shougou.Text = "テストプレイヤー";
                }
                else if (Player.year == 5)
                {
                    shougou.Text = "まだポケベル";
                }
                else if (Player.year <= 7)
                {
                    shougou.Text = "飽きた?";
                }
                else if (Player.year <= 11)
                {
                    shougou.Text = "2000年まで後\n" + (12 -Player.year) + "年だから頑張れ";
                }
                else if (Player.year == 12)
                {
                    shougou.Text = "2000年です";
                }
                else if (Player.year <= 14)
                {
                    shougou.Text = "コメント考えるの\n疲れた";
                }
                else if (Player.year == 15)
                {
                    shougou.Text = "Length of 大正";
                }
                else if (Player.year == 16)
                {
                    shougou.Text = "平成 / 2";
                }
                else if (Player.year <= 21)
                {
                    shougou.Text = "懐かしみが深い";
                }
                else if (Player.year <= 24)
                {
                    shougou.Text = "スカイツリー\n建ててた頃";
                }
                else if (Player.year <= 27)
                {
                    shougou.Text = "この頃の出来事\n1,2年前位に感じる";
                }
                else if (Player.year <= 29)
                {
                    shougou.Text = "ここまで来れたなら\n31年まで行ける!";
                }
                else if (Player.year == 30)
                {
                    shougou.Text = "年号と年同時に\n変わってくれと\n思ってる?";
                }
                else if (Player.year == 31)
                {
                    shougou.Text = "ここまでプレイして\nくれてありがとう!";
                }
                else if (Player.year == 32)
                {
                    shougou.Text = "平成2度目の日本\nオリンピック開催";
                }
                else if (Player.year <= 37)
                {
                    shougou.Text = "歴史の改変者";
                }
                else if (Player.year <= 43)
                {
                    shougou.Text = "新時代も平成";
                }
                else if (Player.year == 44)
                {
                    shougou.Text = "Length of 明治";
                }
                else if (Player.year <= 53)
                {
                    shougou.Text = "長さ歴代2位の年号";
                }
                else if (Player.year <= 61)
                {
                    shougou.Text = "ポスト昭和";
                }
                else if (Player.year == 62)
                {
                    shougou.Text = "Length of 昭和";
                }
                else if (Player.year <= 70)
                {
                    shougou.Text = "歴代最長年号";
                }
                else if (Player.year <= 80)
                {
                    shougou.Text = "パラレルワールド\nも平成";
                }
                else if (Player.year <= 92)
                {
                    shougou.Text = "伝説の年号";
                }
                else if (Player.year == 93)
                {
                    shougou.Text = "平成×3";
                }
                else if (Player.year <= 99)
                {
                    shougou.Text = "あとちょっとで3桁！";
                }
                else if (Player.year == 100)
                {
                    shougou.Text = "遂に3桁！！";
                }
                else if (Player.year <= 105)
                {
                    shougou.Text = "一生平成";
                }
                else if (Player.year <= 115)
                {
                    shougou.Text = "天変地異が\n起きても平成";
                }
                else if (Player.year <= 121)
                {
                    shougou.Text = "転生しても平成";
                }
                else if (Player.year <= 135)
                {
                    shougou.Text = "人を超えた存在";
                }
                else if (Player.year <= 149)
                {
                    shougou.Text = "永遠の平成";
                }
                else if (Player.year >= 150)
                {
                    shougou.Text = "ここまでプレイ\nしたあなた自身\nが平成です。";
                }

            }
            else
            {
                shougou.Text = "";
            }

            // 文字描画オブジェクトのインスタンスをエンジンへ追加する。
            layertext.AddObject(shougou);
        }

        bool isOverChanging = false;

        protected override void OnUpdated()
        {

            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.MouseButtonState.Push && isOverChanging == false)
            {
                asd.Engine.ChangeSceneWithTransition(new GameScene(), new asd.TransitionFade(1.0f, 1.0f));

                isOverChanging = true;

                
                Player.year = 1;
                Player.retire_success = false;
                ItemController.Initialize();
            }
        }
        
    }
}
