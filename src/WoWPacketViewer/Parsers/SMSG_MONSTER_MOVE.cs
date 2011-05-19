using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_MONSTER_MOVE)]
    [Parser(OpCodes.SMSG_MONSTER_MOVE_TRANSPORT)]
    class MonsterMoveParser : Parser
    {
        public override void Parse()
        {
            AppendFormatLine("Monster GUID: 0x{0:X16}", Reader.ReadPackedGuid());

            if (Packet.Code == OpCodes.SMSG_MONSTER_MOVE_TRANSPORT)
            {
                AppendFormatLine("Transport GUID: 0x{0:X16}", Reader.ReadPackedGuid());
                AppendFormatLine("Transport Seat: 0x{0:X2}", Reader.ReadByte());
            }

            AppendFormatLine("Monster unk byte: {0}", Reader.ReadByte()); // toggle MOVEFLAG2 & 0x40

            var curr = Reader.ReadCoords3();
            AppendFormatLine("Current Position: {0}", curr);
            AppendFormatLine("Ticks Count: 0x{0:X8}", Reader.ReadUInt32());

            var movementType = (SplineType)Reader.ReadByte(); // 0-4
            AppendFormatLine("SplineType: {0}", movementType);

            switch (movementType)
            {
                case SplineType.Normal:
                    break;
                case SplineType.Stop:
                    // client sets following values to:
                    // movementFlags = 0x1000;
                    // moveTime = 0;
                    // splinesCount = 1;
                    // and returns
                    return;
                case SplineType.FacingSpot:
                    AppendFormatLine("Facing Point: {0}", Reader.ReadCoords3());
                    break;
                case SplineType.FacingTarget:
                    AppendFormatLine("Facing GUID: 0x{0:X16}", Reader.ReadUInt64());
                    break;
                case SplineType.FacingAngle:
                    AppendFormatLine("Facing Angle: {0}", Reader.ReadSingle());
                    break;
                default:
                    break;
            }

            #region Block1

            // block1
            var splineFlags = (SplineFlags)Reader.ReadUInt32();
            AppendFormatLine("Spline Flags: {0}", splineFlags);

            if (splineFlags.HasFlag(SplineFlags.UNK3))
            {
                var unk_0x200000 = Reader.ReadByte(); // anim type
                var unk_0x200000_ms_time = Reader.ReadUInt32(); // time

                AppendFormatLine("SplineFlags 0x200000: anim type 0x{0:X8} and time 0x{1:X8}", unk_0x200000, unk_0x200000_ms_time);
            }

            var moveTime = Reader.ReadUInt32();
            AppendFormatLine("Spline Time: 0x{0:X8}", moveTime);

            if (splineFlags.HasFlag(SplineFlags.TRAJECTORY))
            {
                var unk_float_0x800 = Reader.ReadSingle();
                var unk_int_0x800 = Reader.ReadUInt32();

                AppendFormatLine("SplineFlags 0x800: float {0} and int 0x{1:X8}", unk_float_0x800, unk_int_0x800);
            }

            var splinesCount = Reader.ReadUInt32();
            AppendFormatLine("Splines Count: {0}", splinesCount);

            #endregion

            #region Block2

            // block2
            if (splineFlags.HasFlag(SplineFlags.FLYING) || splineFlags.HasFlag(SplineFlags.CATMULLROM))
            {
                var startPos = Reader.ReadCoords3();
                AppendFormatLine("Splines Start Point: {0}", startPos);

                if (splinesCount > 1)
                {
                    for (var i = 0; i < splinesCount - 1; ++i)
                    {
                        AppendFormatLine("Spline Point {0}: {1}", i, Reader.ReadCoords3());
                    }
                }
            }
            else
            {
                var dest = Reader.ReadCoords3();

                var mid = new Coords3();
                mid.X = (curr.X + dest.X) * 0.5f;
                mid.Y = (curr.Y + dest.Y) * 0.5f;
                mid.Z = (curr.Z + dest.Z) * 0.5f;

                AppendFormatLine("Dest position: {0}", dest);

                if (splinesCount > 1)
                {
                    for (var i = 0; i < splinesCount - 1; ++i)
                    {
                        var packedOffset = Reader.ReadInt32();
                        AppendFormatLine("Packed Vector: 0x{0:X8}", packedOffset);

                        #region Unpack

                        var x = ((packedOffset & 0x7FF) << 21 >> 21) * 0.25f;
                        var y = ((((packedOffset >> 11) & 0x7FF) << 21) >> 21) * 0.25f;
                        var z = ((packedOffset >> 22 << 22) >> 22) * 0.25f;
                        AppendFormatLine("Path Point {0}: {1}, {2}, {3}", i, mid.X - x, mid.Y - y, mid.Z - z);

                        #endregion

                        #region Pack

                        var packed = 0;
                        packed |= ((int)(x / 0.25f) & 0x7FF);
                        packed |= ((int)(y / 0.25f) & 0x7FF) << 11;
                        packed |= ((int)(z / 0.25f) & 0x3FF) << 22;
                        AppendFormatLine("Test packing 0x{0:X8}", packed);

                        if (packedOffset != packed)
                            AppendFormatLine("Not equal!");

                        #endregion
                    }
                }
            }

            #endregion
        }
    }
}
