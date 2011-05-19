using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_PET_SPELLS)]
    class PetSpellsParser : Parser
    {
        public override void Parse()
        {
            var petGUID = Reader.ReadUInt64();
            AppendFormatLine("GUID: {0:X16}", petGUID);

            if (petGUID != 0)
            {
                AppendFormatLine("Pet family: {0}", Reader.ReadUInt16());

                var unk1 = Reader.ReadUInt32();
                var unk2 = Reader.ReadUInt32();
                AppendFormatLine("Unk1: {0}, Unk2: {1:X8}", unk1, unk2);

                for (var i = 0; i < 10; ++i)
                {
                    var spellOrAction = Reader.ReadUInt16();
                    var type = Reader.ReadUInt16();

                    AppendFormatLine("SpellOrAction: id {0}, type {1:X4}", spellOrAction, type);
                }

                var spellsCount = Reader.ReadByte();
                AppendFormatLine("Spells count: {0}", spellsCount);

                for (var i = 0; i < spellsCount; ++i)
                {
                    var spellId = Reader.ReadUInt16();
                    var active = Reader.ReadUInt16();

                    AppendFormatLine("Spell {0}, active {1:X4}", spellId, active);
                }

                var cooldownsCount = Reader.ReadByte();
                AppendFormatLine("Cooldowns count: {0}", cooldownsCount);

                for (var i = 0; i < cooldownsCount; ++i)
                {
                    var spell = Reader.ReadUInt32();
                    var category = Reader.ReadUInt16();
                    var cooldown = Reader.ReadUInt32();
                    var categoryCooldown = Reader.ReadUInt32();

                    AppendFormatLine("Cooldown: spell {0}, category {1}, cooldown {2}, categoryCooldown {3}", spell, category, cooldown, categoryCooldown);
                }
            }
        }
    }
}
