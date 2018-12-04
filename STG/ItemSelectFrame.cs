using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    public class ItemSelectFrame : asd.TextureObject2D
    {
        int id;

        public ItemSelectFrame(asd.Vector2DF pos, int num)
        {
            Texture = asd.Engine.Graphics.CreateTexture2D($"Resources/ItemList/Itemlist_select.png");       

            Position = pos;

            CenterPosition = new asd.Vector2DF(Texture.Size.X / 2.0f, Texture.Size.Y / 2.0f);

            id = num;
        }
    }
}
