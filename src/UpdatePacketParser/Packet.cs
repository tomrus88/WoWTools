using System.IO;
using WowTools.Core;

namespace UpdatePacketParser
{
    public class Packet
    {
        public int Size { get; set; }
        public OpCodes Code { get; set; }
        public byte[] Data { get; set; }

        public BinaryReader CreateReader()
        {
            return new BinaryReader(new MemoryStream(Data));
        }
    }
}
