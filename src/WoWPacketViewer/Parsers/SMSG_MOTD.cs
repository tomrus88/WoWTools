using WowTools.Core;

[Parser(OpCodes.SMSG_MOTD)]
class SMSG_MOTD : Parser
{
    public override void Parse()
    {
        For(ReadInt32("Lines count: {0}"), i => ReadCString("Line {0} text: {1}", i));
    }
}
