using System;
using WowTools.Core;

namespace WoWPacketViewer.Parsers
{
    [Parser(OpCodes.SMSG_AURA_UPDATE)]
    [Parser(OpCodes.SMSG_AURA_UPDATE_ALL)]
    class AuraUpdateParser : Parser
    {
        [Flags]
        private enum AuraFlags : byte
        {
            None = 0x00,
            Index1 = 0x01,
            Index2 = 0x02,
            Index3 = 0x04,
            NotOwner = 0x08,
            Positive = 0x10,
            Duration = 0x20,
            Unk1 = 0x40,
            Negative = 0x80,
        }

        public override void Parse()
        {
            ReadPackedGuid("GUID: {0:X16}");

            while (Reader.BaseStream.Position < Reader.BaseStream.Length)
            {
                ReadUInt8("Slot: {0:X2}");

                var spellId = ReadUInt32("Spell: {0:X8}");

                if (spellId != 0)
                {
                    var af = ReadUInt8<AuraFlags>("Flags: {0}");

                    ReadUInt8("Level: {0:X2}");
                    ReadUInt8("Charges: {0:X2}");

                    if (!af.HasFlag(AuraFlags.NotOwner))
                    {
                        ReadPackedGuid("GUID2: {0:X16}");
                    }

                    if (af.HasFlag(AuraFlags.Duration))
                    {
                        ReadUInt32("Full duration: {0:X8}");
                        ReadUInt32("Rem. duration: {0:X8}");
                    }
                }
                AppendLine();
            }
        }
    }
}
