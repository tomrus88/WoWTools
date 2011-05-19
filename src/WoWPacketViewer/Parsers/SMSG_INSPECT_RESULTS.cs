using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_INSPECT_RESULTS)]
    class InspectTalentParser : Parser
    {
        public override void Parse()
        {
            AppendFormatLine("GUID: {0:X16}", Reader.ReadPackedGuid());
            AppendFormatLine("Free talent points: {0}", Reader.ReadUInt32());
            var talentGroupsCount = Reader.ReadByte();
            AppendFormatLine("Talent groups count: {0}", talentGroupsCount);
            AppendFormatLine("Talent group index: {0}", Reader.ReadByte());

            if (talentGroupsCount > 0)
            {
                var talentsCount = Reader.ReadByte();
                AppendFormatLine("Talents count {0}", talentsCount);

                for (var i = 0; i < talentsCount; ++i)
                {
                    AppendFormatLine("Talent {0}: id {1}, rank {2}", i, Reader.ReadUInt32(), Reader.ReadByte());
                }

                var glyphsCount = Reader.ReadByte();
                AppendFormatLine("Glyphs count {0}", glyphsCount);

                for (var i = 0; i < glyphsCount; ++i)
                {
                    AppendFormatLine("Glyph {0}: id {1}", i, Reader.ReadUInt16());
                }
            }

            var slotUsedMask = Reader.ReadUInt32();

            for (var i = 0; i < 19; ++i)    // max equip slot
            {
                if (((1 << i) & slotUsedMask) != 0)
                {
                    AppendFormatLine("Item {0}: entry {1}", i, Reader.ReadUInt32());

                    var enchantmentMask = Reader.ReadUInt16();

                    for (var j = 0; j < 12; ++j)    // max enchantments
                    {
                        if (((1 << j) & enchantmentMask) != 0)
                        {
                            AppendFormatLine("Item {0}: enchant {1}, id {2}", i, j, Reader.ReadUInt16());
                        }
                    }

                    AppendFormatLine("Item {0}: unk1 {1:X4}", i, Reader.ReadUInt16());
                    AppendFormatLine("Item {0}: unk2 {1:X16}", i, Reader.ReadPackedGuid());
                    AppendFormatLine("Item {0}: unk3 {1:X8}", i, Reader.ReadUInt32());
                }
            }
        }
    }
}
