using System.Xml;
using MechTE_480.MECH;
using Xunit;

namespace xUnit_Test
{
    public class XmlTest
    {
        private const string PathXml = "test.xml";
        private readonly MechXML _mechXml = new MechXML(PathXml);


        /// <summary>
        /// 读取指定路径和节点的属性值
        /// </summary>
        [Fact]
        public void 读取指定路径和节点的属性值()
        {
            MechXML.PathXml = "test.xml";
            var data = MechXML.ReadRequest("LE_Audio_Connect", "race_type", "value");
            Assert.Equal("0x5A", data);
        }


        /// <summary>
        /// 初始化主节点
        /// </summary>
        [Fact]
        public void ReadAllChild()
        {
            MechXML.PathXml = "test.xml";
            var ret = _mechXml.ReadAllChild("/CommandBook/LE_Audio_Connect/RequestFormat");
            //遍历所有子节点
            foreach (XmlNode r in ret)
            {
                //获取子节点的属性值
                var data = MechXML.ReadRequest("LE_Audio_Connect", r.Name, "value");
            }
        }

        [Fact]
        public void GetDataSetByXml()
        {
            var ret = _mechXml.GetDataSetByXml(@"D:\sw\class_library\MechTE\xUnitTest\bin\Debug\" + PathXml);
            foreach (var r in ret.Tables)
            {
            }
        }

        [Fact]
        public void Update()
        {
            MechXML.UpdateRequest(PathXml, "LE_Audio_Connect", "race_type", "0x04");
        }

        [Fact]
        public void Delete()
        {
            MechXML.Delete(PathXml, "LE_Audio_Connect2");
        }

        [Fact]
        public void InitializeTheNode()
        {
            MechXML.PathXml = "test.xml";
            var ret = MechXML.InitializeTheNode();
            Assert.Equal("true", ret);
        }

        /// <summary>
        /// 查询主节点创建子节点
        /// </summary>
        [Fact]
        public void CreateChildNode()
        {
            MechXML.PathXml = "test.xml";
            MechXML.funName = "LE_Audio_Connect";
            var ret2 = MechXML.CreateChildNode();
        }


        //新增节点
        [Fact]
        public void Insert()
        {
            MechXML.PathXml = "test.xml";
            MechXML.funName = "LE_Audio_Connect";
            MechXML.CreateChildNode();
            MechXML.InsertRequestFormat("packet_type", "0x05");
            MechXML.InsertRequestFormat("race_type", "0x5A");
            MechXML.InsertRequestFormat("packet_len", "0x000A");
            MechXML.InsertRequestFormat("race_id", "0x2C92");
            MechXML.InsertRequestFormat("value1", "");

            MechXML.InsertRequestFormatResult("headset_bt_address");

            MechXML.InsertResponseFormat("packet_type", "0x05");
            MechXML.InsertResponseFormat("race_type", "0x5B");
            MechXML.InsertResponseFormat("packet_len", "");
            MechXML.InsertResponseFormat("race_id", "0x2C92");
            MechXML.InsertResponseFormat("value1", "");
            MechXML.InsertResponseFormat("value2", "");
            MechXML.InsertResponseFormatResult("result_value");
        }

        [Fact]
        public void Insert2()
        {
            MechXML.PathXml = "test.xml";
            MechXML.funName = "LE_Audio_Connect2";
            MechXML.CreateChildNode();
            MechXML.InsertRequestFormat("packet_type", "0x05");
            MechXML.InsertRequestFormat("race_type", "0x5A");
            MechXML.InsertRequestFormat("packet_len", "0x000A");
            MechXML.InsertRequestFormat("race_id", "0x2C92");
            MechXML.InsertRequestFormat("value1", "");
            MechXML.InsertRequestFormatResult("headset_bt_address");

            MechXML.InsertResponseFormat("packet_type", "0x05");
            MechXML.InsertResponseFormat("race_type", "0x5B");
            MechXML.InsertResponseFormat("packet_len", "");
            MechXML.InsertResponseFormat("race_id", "0x2C92");
            MechXML.InsertResponseFormat("value1", "");
            MechXML.InsertResponseFormat("value2", "");
            MechXML.InsertResponseFormatResult("result_value");
        }
    }
}