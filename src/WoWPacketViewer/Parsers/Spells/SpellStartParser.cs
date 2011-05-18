using WowTools.Core;

namespace WoWPacketViewer.Parsers.Spells
{
    [Parser(OpCodes.SMSG_SPELL_START)]
    class SpellStartParser : SpellParserBase
    {
        public override void Parse()
        {
            AppendFormatLine("Caster: 0x{0:X16}", Reader.ReadPackedGuid());
            AppendFormatLine("Target: 0x{0:X16}", Reader.ReadPackedGuid());
            AppendFormatLine("Pending Cast: {0}", Reader.ReadByte());
            AppendFormatLine("Spell Id: {0}", Reader.ReadUInt32());
            var cf = (CastFlags)Reader.ReadUInt32();
            AppendFormatLine("Cast Flags: {0}", cf);
            AppendFormatLine("Timer: {0}", Reader.ReadUInt32());

            ReadTargets();

            if (cf.HasFlag(CastFlags.CAST_FLAG_12))
            {
                AppendFormatLine("PredictedPower: {0}", Reader.ReadUInt32());
            }

            if (cf.HasFlag(CastFlags.CAST_FLAG_22))
            {
                var v1 = Reader.ReadByte();
                AppendFormatLine("RuneState Before: {0}", (CooldownMask)v1);
                var v2 = Reader.ReadByte();
                AppendFormatLine("RuneState Now: {0}", (CooldownMask)v2);

                for (var i = 0; i < 6; ++i)
                {
                    var v3 = (i << i);

                    if ((v3 & v1) != 0)
                    {
                        if ((v3 & v2) == 0)
                        {
                            var v4 = Reader.ReadByte();
                            AppendFormatLine("Cooldown for {0} is {1}", (CooldownMask)v3, v4);
                        }
                    }
                }
            }

            if (cf.HasFlag(CastFlags.CAST_FLAG_06))
            {
                AppendFormatLine("Projectile displayid {0}, inventoryType {1}", Reader.ReadUInt32(), Reader.ReadUInt32());
            }
        }
    }
}
