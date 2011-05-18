using System.IO;

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
    }
}
