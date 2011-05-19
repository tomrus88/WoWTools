using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_MAIL_LIST_RESULT)]
    class MailListResult : Parser
    {
        public override void Parse()
        {
            var realCount = Reader.ReadUInt32();

            AppendFormatLine("Real Count: {0}", realCount);

            var displayCount = Reader.ReadByte();

            AppendFormatLine("Displayed Count: {0}", displayCount);

            for (var i = 0; i < displayCount; ++i)
            {
                var len = Reader.ReadUInt16();
                var id = Reader.ReadUInt32();
                var type = Reader.ReadByte();

                AppendFormatLine("Message {0}: data len {1}, id {2}, type {3}", i, len, id, type);

                switch(type)
                {
                    case 0:
                        {
                            var guid = Reader.ReadUInt64();
                            AppendFormatLine("Sender guid: {0:X16}", guid);
                            break;
                        }
                    default:
                        {
                            var entry = Reader.ReadUInt32();
                            AppendFormatLine("Sender entry: {0:X16}", entry);
                            break;
                        }
                }

                var cod = Reader.ReadUInt32();
                AppendFormatLine("COD: {0}", cod);
                var itemTextId = Reader.ReadUInt32();
                AppendFormatLine("Item Text Id: {0}", itemTextId);
                var stationary = Reader.ReadUInt32();
                AppendFormatLine("Stationary: {0}", stationary);
                var money = Reader.ReadUInt32();
                AppendFormatLine("Money: {0}", money);
                var flags = Reader.ReadUInt32();
                AppendFormatLine("Flags: {0}", flags);
                var time = Reader.ReadSingle();
                AppendFormatLine("Time: {0}", time);
                var templateId = Reader.ReadUInt32();
                AppendFormatLine("Template Id: {0}", templateId);
                var subject = Reader.ReadCString();
                AppendFormatLine("Subject: {0}", subject);
                var body = Reader.ReadCString();
                AppendFormatLine("Body: {0}", body);

                var itemsCount = Reader.ReadByte();
                AppendFormatLine("Items Count: {0}", itemsCount);

                for(var j = 0; j < itemsCount; ++j)
                {
                    AppendFormatLine("Item: {0}", j);
                    var slot = Reader.ReadByte();
                    var guid = Reader.ReadUInt32();
                    var entry = Reader.ReadUInt32();

                    AppendFormatLine("Slot {0}, guid {1}, entry {2}", slot, guid, entry);

                    for(var k = 0; k < 7; ++k)
                    {
                        var charges = Reader.ReadUInt32();
                        var duration = Reader.ReadUInt32();
                        var enchid = Reader.ReadUInt32();

                        AppendFormatLine("Enchant {0}: charges {1}, duration {2}, id {3}", k, charges, duration, enchid);
                    }

                    var randomProperty = Reader.ReadUInt32();
                    AppendFormatLine("Random Property: {0}", randomProperty);
                    var itemSuffixFactor = Reader.ReadUInt32();
                    AppendFormatLine("Item Suffix Factor: {0}", itemSuffixFactor);
                    var stackCount = Reader.ReadUInt32();
                    AppendFormatLine("Stack Count: {0}", stackCount);
                    var spellCharges = Reader.ReadUInt32();
                    AppendFormatLine("Spell Charges: {0}", spellCharges);
                    var maxDurability = Reader.ReadUInt32();
                    var durability = Reader.ReadUInt32();
                    AppendFormatLine("Durability: {0} (max {1})", durability, maxDurability);
                    var unk = Reader.ReadByte();
                    AppendFormatLine("Unk: {0}", unk);

                    AppendLine();
                }

                AppendLine();
            }
        }
    }
}
