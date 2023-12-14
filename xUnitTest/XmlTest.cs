using System.Xml;
using MechTE_480.xml;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test
{
    public class XmlTest
    {
        private const string PathXml = "test.xml";
        private readonly MXml _mXml = new MXml(PathXml);

        private readonly ITestOutputHelper _msg;

        public XmlTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        /// <summary>
        /// 读取指定路径和节点的属性值
        /// </summary>
        [Fact]
        public void 读取指定路径和节点的属性值()
        {
            MXml.PathXml = "test.xml";
            var data = MXml.ReadRequest("LE_Audio_Connect", "race_type", "value");
            _msg.WriteLine(data);
            Assert.Equal("0x5A", data);
        }

        /// <summary>
        /// 初始化主节点
        /// </summary>
        [Fact]
        public void ReadAllChild()
        {
            MXml.PathXml = "test.xml";
            var ret = _mXml.ReadAllChild("/CommandBook/LE_Audio_Connect/RequestFormat");
            //遍历所有子节点
            foreach (XmlNode r in ret)
            {
                //获取子节点的属性值
                var data = MXml.ReadRequest("LE_Audio_Connect", r.Name, "value");
            }
        }

        [Fact]
        public void GetDataSetByXml()
        {
            var ret = _mXml.GetDataSetByXml(@"D:\sw\class_library\MechTE\xUnitTest\bin\Debug\" + PathXml);
            foreach (var r in ret.Tables)
            {
            }
        }

        [Fact]
        public void Update()
        {
            MXml.UpdateRequest(PathXml, "LE_Audio_Connect", "race_type", "0x04");
        }

        [Fact]
        public void Delete()
        {
            MXml.Delete(PathXml, "LE_Audio_Connect2");
        }

        [Fact]
        public void InitializeTheNode()
        {
            MXml.PathXml = "test.xml";
            var ret = MXml.InitializeTheNode();
            Assert.Equal("true", ret);
        }

        /// <summary>
        /// 查询主节点创建子节点
        /// </summary>
        [Fact]
        public void CreateChildNode()
        {
            MXml.PathXml = "test.xml";
            MXml.FunName = "LE_Audio_Connect";
            var ret2 = MXml.CreateChildNode();
        }


        //新增节点
        [Fact]
        public void Insert()
        {
            MXml.PathXml = "test.xml";
            MXml.FunName = "LE_Audio_Connect";
            MXml.CreateChildNode();
            MXml.InsertRequestFormat("packet_type", "0x05");
            MXml.InsertRequestFormat("race_type", "0x5A");
            MXml.InsertRequestFormat("packet_len", "0x000A");
            MXml.InsertRequestFormat("race_id", "0x2C92");
            MXml.InsertRequestFormat("value1", "");

            MXml.InsertRequestFormatResult("headset_bt_address");

            MXml.InsertResponseFormat("packet_type", "0x05");
            MXml.InsertResponseFormat("race_type", "0x5B");
            MXml.InsertResponseFormat("packet_len", "");
            MXml.InsertResponseFormat("race_id", "0x2C92");
            MXml.InsertResponseFormat("value1", "");
            MXml.InsertResponseFormat("value2", "");
            MXml.InsertResponseFormatResult("result_value");
        }

        [Fact]
        public void Insert2()
        {
            MXml.PathXml = "test.xml";
            MXml.FunName = "LE_Audio_Connect2";
            MXml.CreateChildNode();
            MXml.InsertRequestFormat("packet_type", "0x05");
            MXml.InsertRequestFormat("race_type", "0x5A");
            MXml.InsertRequestFormat("packet_len", "0x000A");
            MXml.InsertRequestFormat("race_id", "0x2C92");
            MXml.InsertRequestFormat("value1", "");
            MXml.InsertRequestFormatResult("headset_bt_address");

            MXml.InsertResponseFormat("packet_type", "0x05");
            MXml.InsertResponseFormat("race_type", "0x5B");
            MXml.InsertResponseFormat("packet_len", "");
            MXml.InsertResponseFormat("race_id", "0x2C92");
            MXml.InsertResponseFormat("value1", "");
            MXml.InsertResponseFormat("value2", "");
            MXml.InsertResponseFormatResult("result_value");
        }
    }
}