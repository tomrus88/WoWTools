using WowTools.Core;

namespace WoWPacketViewer.Parsers
{
    [Parser(OpCodes.SMSG_INITIAL_SPELLS)]
    class InitialSpellsParser : Parser
    {
        public override void Parse()
        {
            AppendFormatLine("Unk: {0}", Reader.ReadByte());

            var spellsCount = Reader.ReadUInt16();
            AppendFormatLine("Spells count: {0}", spellsCount);

            for (var i = 0; i < spellsCount; ++i)
            {
                var spellId = Reader.ReadUInt32();
                var spellSlot = Reader.ReadUInt16();

                AppendFormatLine("Spell: id {0}, slot {1}", spellId, spellSlot);
            }

            var cooldownsCount = Reader.ReadUInt16();
            AppendFormatLine("Cooldowns count: {0}", cooldownsCount);

            for (var i = 0; i < cooldownsCount; ++i)
            {
                var spellId = Reader.ReadUInt32();
                var itemId = Reader.ReadUInt16();
                var category = Reader.ReadUInt16();
                var coolDown1 = Reader.ReadUInt32();
                var coolDown2 = Reader.ReadUInt32();

                AppendFormatLine("Cooldown: spell {0}, item {1}, cat {2}, time1 {3}, time2 {4}", spellId, itemId, category, coolDown1, coolDown2);
            }
        }
    }
}
