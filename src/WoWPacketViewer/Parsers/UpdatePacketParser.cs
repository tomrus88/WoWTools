using System.Collections.Generic;
using WowTools.Core;

namespace WoWPacketViewer.Parsers
{
    [Parser(OpCodes.SMSG_UPDATE_OBJECT)]
    [Parser(OpCodes.SMSG_COMPRESSED_UPDATE_OBJECT)]
    class UpdatePacketParser : Parser
    {
        private readonly Dictionary<ulong, WoWObject> objects = new Dictionary<ulong, WoWObject>();

        public override void Initialize(Packet packet)
        {
            // covert SMSG_COMPRESSED_UPDATE_OBJECT to SMSG_UPDATE_OBJECT
            if (packet.Code == OpCodes.SMSG_COMPRESSED_UPDATE_OBJECT)
                packet.DecompressDataAndSetOpcode(OpCodes.SMSG_UPDATE_OBJECT);

            base.Initialize(packet);
        }

        public override void Parse()
        {
            For(ReadInt32("Objects count: {0}"), i =>
                {
                    var updateType = ReadUInt8<UpdateTypes>("UpdateType: {0}");

                    switch (updateType)
                    {
                        case UpdateTypes.UPDATETYPE_VALUES:
                            ParseValues(i);
                            break;
                        case UpdateTypes.UPDATETYPE_MOVEMENT:
                            ParseMovement(i);
                            break;
                        case UpdateTypes.UPDATETYPE_CREATE_OBJECT:
                        case UpdateTypes.UPDATETYPE_CREATE_OBJECT2:
                            ParseCreateObjects(i);
                            break;
                        case UpdateTypes.UPDATETYPE_OUT_OF_RANGE_OBJECTS:
                            ParseOutOfRangeObjects(i);
                            break;
                        case UpdateTypes.UPDATETYPE_NEAR_OBJECTS:
                            ParseNearObjects(i);
                            break;
                        default:
                            AppendFormatLine("Unknown updatetype {0}", updateType);
                            break;
                    }
                });
        }

        private void ParseValues(int i)
        {
            var guid = ReadPackedGuid("Object guid: 0x{0:X16}");

            var woWObjectUpdate = WoWObjectUpdate.Read(Reader);

            var wowobj = GetWoWObject(guid);
            if (wowobj != null)
                wowobj.AddUpdate(woWObjectUpdate);
            else
                AppendLine("Boom!");
        }

        private void ParseMovement(int i)
        {
            var guid = ReadPackedGuid("Object guid: 0x{0:X16}");

            var mInfo = MovementInfo.Read(Reader);
        }

        private void ParseCreateObjects(int i)
        {
            var guid = ReadPackedGuid("Object guid: {0:X16}");

            var objectTypeId = ReadUInt8<ObjectTypes>("Object Type: {0}");

            var movement = MovementInfo.Read(Reader);

            // values part
            var update = WoWObjectUpdate.Read(Reader);

            var obj = GetWoWObject(guid);
            if (obj == null)
                objects.Add(guid, new WoWObject(objectTypeId, movement, update.Data));
            else
            {
                objects.Remove(guid);
                objects.Add(guid, new WoWObject(objectTypeId, movement, update.Data));
            }
        }

        private void ParseOutOfRangeObjects(int i)
        {
            var count = ReadUInt32("OOR Objects count: {0}");
            var guids = new ulong[count];
            for (var j = 0; j < count; ++j)
                guids[j] = ReadPackedGuid("OOR Object Guid: 0x{0:X16}");
        }

        private void ParseNearObjects(int i)
        {
            var count = ReadUInt32("Near Objects count: {0}");
            var guids = new ulong[count];
            for (var j = 0; j < count; ++j)
                guids[j] = ReadPackedGuid("Near Object Guid: 0x{0:X16}");
        }

        private WoWObject GetWoWObject(ulong guid)
        {
            WoWObject obj;
            objects.TryGetValue(guid, out obj);
            return obj;
        }
    }
}
