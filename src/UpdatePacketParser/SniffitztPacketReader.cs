using System;
using System.Collections.Generic;
using System.Xml;
using WowTools.Core;

namespace UpdatePacketParser
{
    public class SniffitztPacketReader : IPacketReader
    {
        private readonly XmlDocument _document;
        private readonly XmlNodeList _packets;
        private int _readPackets;

        public SniffitztPacketReader(string filename)
        {
            _document = new XmlDocument();
            _document.Load(filename);

            var build = Convert.ToUInt32(_document.GetElementsByTagName("header")[0].Attributes["clientBuild"].Value);

            _packets = _document.GetElementsByTagName("packet");

            UpdateFieldsLoader.LoadUpdateFields(build);
        }

        public Packet ReadPacket()
        {
            if (_readPackets >= _packets.Count)
            {
                return null;
            }

            var element = _packets[_readPackets];

            var data = element.InnerText;

            var len = data.Length / 2;

            var bytes = new byte[len];

            for (var i = 0; i < len; ++i)
            {
                var pos = i * 2;
                var str = data[pos].ToString();
                str += data[pos + 1];
                bytes[i] = byte.Parse(str, System.Globalization.NumberStyles.HexNumber);
            }

            var packet = new Packet();
            packet.Size = len;
            packet.Code = (OpCodes)Convert.ToInt32(element.Attributes["opcode"].Value);
            packet.Data = bytes;

            _readPackets++;
            return packet;
        }

        public virtual IEnumerable<Packet> ReadPackets()
        {
            var packets = new List<Packet>();
            Packet packet;
            while ((packet = ReadPacket()) != null)
            {
                packets.Add(packet);
            }
            return packets;
        }
    }
}
