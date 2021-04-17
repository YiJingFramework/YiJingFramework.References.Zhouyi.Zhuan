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
    public class XiangZhuanTests
    {
        [TestMethod()]
        public void XiangZhuanTest()
        {
            _ = new XiangZhuan("xiang.json");
            using var fileStream = new FileStream("xiang.json", FileMode.Open);
            var tuanzhuan = new XiangZhuan(fileStream);

            Zhouyi zhouyi = new Zhouyi();
            var xiang = tuanzhuan[zhouyi.GetHexagram(1)];
            Assert.AreEqual("天行健，君子以自强不息。", xiang);
            xiang = tuanzhuan[zhouyi.GetHexagram(64)];
            Assert.AreEqual("火在水上，未济。君子以慎辨物居方。", xiang);

            xiang = tuanzhuan[zhouyi.GetHexagram(2).ApplyNinesOrApplySixes];
            Assert.AreEqual("用六“永贞”，以大终也。", xiang);
            xiang = tuanzhuan[zhouyi.GetHexagram(1).SixthLine];
            Assert.AreEqual("“亢龙有悔”，盈不可久也。", xiang);
        }
    }
}