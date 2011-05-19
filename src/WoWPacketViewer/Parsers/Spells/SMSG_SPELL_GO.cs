using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_SPELL_GO)]
    class SpellGoParser : SpellParserBase
    {
        public override void Parse()
        {
            AppendFormatLine("Caster: 0x{0:X16}", Reader.ReadPackedGuid());
            AppendFormatLine("Target: 0x{0:X16}", Reader.ReadPackedGuid());
            AppendFormatLine("Pending Cast: {0}", Reader.ReadByte());
            AppendFormatLine("Spell Id: {0}", Reader.ReadUInt32());
            var cf = (CastFlags)Reader.ReadUInt32();
            AppendFormatLine("Cast Flags: {0}", cf);
            AppendFormatLine("TicksCount: {0}", Reader.ReadUInt32());

            ReadSpellGoTargets();

            var tf = ReadTargets();

            if (cf.HasFlag(CastFlags.CAST_FLAG_12))
            {
                AppendFormatLine("PredictedPower: {0}", Reader.ReadUInt32());
            }

            if (cf.HasFlag(CastFlags.CAST_FLAG_22))
            {
                var v1 = Reader.ReadByte();
                AppendFormatLine("Cooldowns Before: {0}", (CooldownMask)v1);
                var v2 = Reader.ReadByte();
                AppendFormatLine("Cooldowns Now: {0}", (CooldownMask)v2);

                for (var i = 0; i < 6; ++i)
                {
                    var v3 = (1 << i);

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

            if (cf.HasFlag(CastFlags.CAST_FLAG_18))
            {
                AppendFormatLine("0x20000: Unk float {0}, unk int {1}", Reader.ReadSingle(), Reader.ReadUInt32());
            }

            if (cf.HasFlag(CastFlags.CAST_FLAG_06))
            {
                AppendFormatLine("Projectile displayid {0}, inventoryType {1}", Reader.ReadUInt32(), Reader.ReadUInt32());
            }

            if (cf.HasFlag(CastFlags.CAST_FLAG_20))
            {
                AppendFormatLine("cast flags & 0x80000: Unk int {0}, uint int {1}", Reader.ReadUInt32(), Reader.ReadUInt32());
            }

            if (tf.HasFlag(TargetFlags.TARGET_FLAG_DEST_LOCATION))
            {
                AppendFormatLine("targetFlags & 0x40: byte {0}", Reader.ReadByte());
            }
        }

        public void ReadSpellGoTargets()
        {
            var hitCount = Reader.ReadByte();

            for (var i = 0; i < hitCount; ++i)
            {
                AppendFormatLine("GO Hit Target {0}: 0x{1:X16}", i, Reader.ReadUInt64());
            }

            var missCount = Reader.ReadByte();

            for (var i = 0; i < missCount; ++i)
            {
                AppendFormatLine("GO Miss Target {0}: 0x{1:X16}", i, Reader.ReadUInt64());
                var missReason = Reader.ReadByte();
                AppendFormatLine("GO Miss Reason {0}: {1}", i, missReason);
                if (missReason == 11) // reflect
                {
                    AppendFormatLine("GO Reflect Reason {0}: {1}", i, Reader.ReadByte());
                }
            }
        }
    }
}
