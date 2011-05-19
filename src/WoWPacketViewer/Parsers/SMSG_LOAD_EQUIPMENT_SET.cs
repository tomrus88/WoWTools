using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_LOAD_EQUIPMENT_SET)]
    class EquipmentSetListParser : Parser
    {
        public override void Parse()
        {
            var count = Reader.ReadUInt32();
            AppendFormatLine("Count: {0}", count);

            for (var i = 0; i < count; ++i)
            {
                var setguid = Reader.ReadPackedGuid();
                var setindex = Reader.ReadUInt32();
                var name = Reader.ReadCString();
                var iconname = Reader.ReadCString();

                AppendFormatLine("EquipmentSet {0}: guid {1}, index {2}, name {3}, iconname {4}", i, setguid, setindex, name, iconname);

                for (var j = 0; j < 19; ++j)
                    AppendFormatLine("EquipmentSetItem {0}: guid {1}", j, Reader.ReadPackedGuid().ToString("X16"));

                AppendLine();
            }
        }
    }
}
