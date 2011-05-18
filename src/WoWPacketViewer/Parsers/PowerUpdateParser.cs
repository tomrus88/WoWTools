using WowTools.Core;

namespace WoWPacketViewer.Parsers
{
    [Parser(OpCodes.SMSG_POWER_UPDATE)]
    class PowerUpdateParser : Parser
    {
        private enum PowerType
        {
            Mana,
            Rage,
            Focus,
            Energy,
            Happiness,
            Rune,
            RunicPower,
        }

        public override void Parse()
        {
            AppendFormatLine("GUID: 0x{0:X16}", Reader.ReadPackedGuid());
            AppendFormatLine("Type: {0}", (PowerType)Reader.ReadByte());
            AppendFormatLine("Value: {0}", Reader.ReadUInt32());
        }
    }
}
