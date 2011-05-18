using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using WowTools.Core;

namespace WoWPacketViewer
{
    public class SniffitztPacketReader : IPacketReader
    {
        public uint Build
        {
            get { return 0; }
        }

        public IEnumerable<Packet> ReadPackets(string file)
        {
            var packets = new List<Packet>();
            var uri = new Uri(Path.GetFullPath(file)).ToString();
            var doc = XDocument.Load(uri);
            foreach (var packet in doc.XPathSelectElements("*/packet"))
            {
                var direction = (string)packet.Attribute("direction") == "S2C" ? Direction.Server : Direction.Client;
                var opcode = (OpCodes)(uint)packet.Attribute("opcode");
                packets.Add(new Packet(direction, opcode, packet.Value.ToByteArray(), 0, 0));
            }
            return packets;
        }
    }
}
