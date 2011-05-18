using System.IO;
using System.Text;

namespace WowTools.Core
{
    public class TransportInfo
    {
        public ulong Guid { get; set; }
        public Coords3 Position { get; set; }
        public float Facing { get; set; }
        public uint Time { get; set; }
        public byte Seat { get; set; }
        public uint Time2 { get; set; }

        public static TransportInfo Read(BinaryReader reader, MovementFlags2 flags2)
        {
            var tInfo = new TransportInfo();
            tInfo.Guid = reader.ReadPackedGuid();
            tInfo.Position = reader.ReadCoords3();
            tInfo.Facing = reader.ReadSingle();
            tInfo.Time = reader.ReadUInt32();
            tInfo.Seat = reader.ReadByte();
            if (flags2.HasFlag(MovementFlags2.InterpolatedPlayerMovement))
                tInfo.Time2 = reader.ReadUInt32();
            return tInfo;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Transport Guid: 0x{0:X16}", Guid).AppendLine();
            sb.AppendFormat("Transport Position: {0}", Position).AppendLine();
            sb.AppendFormat("Transport Facing: {0}", Facing).AppendLine();
            sb.AppendFormat("Transport TimeStamp: {0}", Time).AppendLine();
            sb.AppendFormat("Transport Seat: {0}", Seat).AppendLine();
            sb.AppendFormat("Transport TimeStamp2: {0}", Time2).AppendLine();
            return sb.ToString();
        }
    }
}
