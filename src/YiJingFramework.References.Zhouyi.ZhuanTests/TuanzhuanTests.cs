using Microsoft.VisualStudio.TestTools.UnitTesting;
using YiJingFramework.References.Zhouyi.Zhuan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YiJingFramework.References.Zhouyi.Zhuan.Tests
{
    [TestClass()]
    public class TuanzhuanTests
    {
        [TestMethod()]
        public void TuanzhuanTest()
        {
            _ = new Tuanzhuan("tuan.json");
            using var fileStream = new FileStream("tuan.json", FileMode.Open);
            var tuanzhuan = new Tuanzhuan(fileStream);

            Zhouyi zhouyi = new Zhouyi();
            var tuan = tuanzhuan[zhouyi.GetHexagram(1)];
            Assert.AreEqual("大哉乾元，万物资始，乃统天。" +
                "云行雨施，品物流形。" +
                "大明终始，六位时成。" +
                "时乘六龙以御天。" +
                "乾道变化，各正性命。" +
                "保合大和，乃利贞。" +
                "首出庶物，万国威宁。", tuan);
            tuan = tuanzhuan[zhouyi.GetHexagram(64)];
            Assert.AreEqual("“未济，亨”，柔得中也。" +
                "“小狐汔济”，未出中也。" +
                "“濡其尾，无攸利”，不续终也。" +
                "虽不当位，刚柔应也。", tuan);
        }
    }
}