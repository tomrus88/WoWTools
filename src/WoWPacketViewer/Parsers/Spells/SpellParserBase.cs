using WowTools.Core;

namespace WoWPacketViewer
{
    abstract class SpellParserBase : Parser
    {
        protected TargetFlags ReadTargets()
        {
            var tf = (TargetFlags) Reader.ReadUInt32();
            AppendFormatLine("TargetFlags: {0}", tf);

            if (tf.HasFlag(TargetFlags.TARGET_FLAG_UNIT) ||
                tf.HasFlag(TargetFlags.TARGET_FLAG_PVP_CORPSE) ||
                tf.HasFlag(TargetFlags.TARGET_FLAG_OBJECT) ||
                tf.HasFlag(TargetFlags.TARGET_FLAG_CORPSE) ||
                tf.HasFlag(TargetFlags.TARGET_FLAG_UNK2))
            {
                AppendFormatLine("ObjectTarget: 0x{0:X16}", Reader.ReadPackedGuid());
            }

            if (tf.HasFlag(TargetFlags.TARGET_FLAG_ITEM) ||
                tf.HasFlag(TargetFlags.TARGET_FLAG_TRADE_ITEM))
            {
                AppendFormatLine("ItemTarget: 0x{0:X16}", Reader.ReadPackedGuid());
            }

            if (tf.HasFlag(TargetFlags.TARGET_FLAG_SOURCE_LOCATION))
            {
                AppendFormatLine("SrcTargetGuid: {0}", Reader.ReadPackedGuid().ToString("X16"));
                AppendFormatLine("SrcTarget: {0} {1} {2}", Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle());
            }

            if (tf.HasFlag(TargetFlags.TARGET_FLAG_DEST_LOCATION))
            {
                AppendFormatLine("DstTargetGuid: {0}", Reader.ReadPackedGuid().ToString("X16"));
                AppendFormatLine("DstTarget: {0} {1} {2}", Reader.ReadSingle(), Reader.ReadSingle(), Reader.ReadSingle());
            }

            if (tf.HasFlag(TargetFlags.TARGET_FLAG_STRING))
            {
                AppendFormatLine("StringTarget: {0}", Reader.ReadCString());
            }

            return tf;
        }
    }
}
