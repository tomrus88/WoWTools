using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_TUTORIAL_FLAGS)]
    class TutorialFlagsParser : Parser
    {
        public override void Parse()
        {
            For(8, i => ReadUInt32("Mask {0}: 0x{1:X8}", i));
        }
    }
}
