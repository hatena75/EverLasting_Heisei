using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class ItemListMenu : asd.TextureObject2D
    {
        bool selected;

        asd.Texture2D picture;

        bool pre_selected;

        int id;

        ItemSelectFrame select;

        public ItemListMenu(asd.Vector2DF pos, string texture, int num)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/ItemList/Itemlist_{texture}.png");

            picture = Texture;

            Position = pos;

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            selected = false;

            id = num;
        }

        protected override void OnUpdate()
        {
            selected = ItemController.itemselecting[id]; //自分が今選択されているか否かを見る。

            if (pre_selected != selected)
            {

                if (selected == true)
                {
                    select = new ItemSelectFrame(Position, id); //選ばれていれば枠を生成
                    asd.Engine.AddObject2D(select);
                }
                else
                {
                    select.Dispose(); //選ばれなくなったら枠を破棄
                }

            }
            
            if (ItemController.itemlist[id] != true) //自分が使われているか否かを取得
            {
                Texture = picture;
            }
            else
            {
                Texture = asd.Engine.Graphics.CreateTexture2D("Resources/ItemList/Itemlist_null.png");
            }

            pre_selected = selected;
        }


    }
}
