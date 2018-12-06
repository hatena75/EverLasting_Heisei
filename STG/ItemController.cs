using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STG
{
    static class ItemController
    {
        //東京タワー、バブル、鷲のマーク、デモクラシー、文明開化、チョコレートの順

        public static bool[] itemlist = {false, false, false, false, false, false}; //アイテムが現在使い切られているか否か、使用済みでtrue

        /*枠の有り無しは、itemselectingによってのみ管理される。*/
        public static bool[] itemselecting = {false, false, false, false, false, false}; //アイテム欄のうち、現在どれが選ばれているか。選ばれている1つがtrue。

        public static int[] itemlimit = {1, 1, 3, 2, 3, 2 }; //アイテム使用回数の上限

        public static void Initialize()
        {
            for(int i = 0; i < itemlist.Length; i++)
            {
                itemlist[i] = false;
                itemselecting[i] = false;
            }
        }
    }
}
