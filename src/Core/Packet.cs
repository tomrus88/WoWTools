using System.IO;

namespace WowTools.Core
{
    public class Packet
    {
        public Direction Direction { get; private set; }

        public OpCodes Code { get; private set; }

        public byte[] Data { get; private set; }

        public uint UnixTime { get; private set; }

        public uint TicksCount { get; private set; }

        public Packet(Direction direction, OpCodes opcode, byte[] data, uint unixtime, uint tickscount)
        {
            Direction = direction;
            Code = opcode;
            Data = data;
            UnixTime = unixtime;
            TicksCount = tickscount;
        }

        public BinaryReader CreateReader()
        {
            return new BinaryReader(new MemoryStream(Data));
        }

        public void DecompressDataAndSetOpcode(OpCodes opcode)
        {
            Data = Data.Decompress();
            Code = opcode;
        }
    }
}
