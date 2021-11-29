using System;
using YiJingFramework.References.Zhouyi;

using YiJingFramework.References.Zhouyi.Zhuan;

namespace Examples
{
    class Program
    {
        static void Main()
        {
            // Here we don't need a translation file for Zhouyi.
            Zhouyi zhouyi = new Zhouyi();

            // Here we use a combined translation file.
            XiangZhuan xiangZhuan = new XiangZhuan("zhuan.json");
            Tuanzhuan tuanzhuan = new Tuanzhuan("zhuan.json");

            var jian = zhouyi.GetHexagram(53);
            Console.WriteLine($"{tuanzhuan[jian]}---{xiangZhuan[jian]}---{xiangZhuan[jian.FirstLine]}");
            Console.WriteLine();
            // Output: 渐之进也，女归吉也。进得位，往有功也。进以正，可以正邦也。其位刚得中也。止而巽，动不穷也。---山上有木，渐。君子以居贤德 善俗。---“小子之厉”，义无咎也。
        }
    }
}
