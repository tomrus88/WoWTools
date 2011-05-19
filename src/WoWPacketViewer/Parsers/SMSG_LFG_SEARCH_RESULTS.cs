using System;
using WowTools.Core;

namespace WoWPacketViewer
{
    enum LfgType
    {
        LFG_TYPE_NONE = 0,
        LFG_TYPE_DUNGEON = 1,
        LFG_TYPE_RAID = 2,
        LFG_TYPE_QUEST = 3,
        LFG_TYPE_ZONE = 4,
        LFG_TYPE_HEROIC_DUNGEON = 5
    };

    [Flags]
    enum LfgRoles
    {
        LEADER = 1,
        TANK = 2,
        HEALER = 4,
        DAMAGE = 8
    };

    [Parser(OpCodes.SMSG_LFG_SEARCH_RESULTS)]
    class LookingForGroupParser : Parser
    {
        public override void Parse()
        {
            AppendFormatLine("FLG Type: {0}", (LfgType)Reader.ReadUInt32());
            AppendFormatLine("FLG Entry: {0}", Reader.ReadUInt32());

            var unk1 = Reader.ReadByte();
            AppendFormatLine("unk1: {0}", unk1);

            if (unk1 != 0)
            {
                var count1 = Reader.ReadUInt32();
                AppendFormatLine("count1: {0}", count1);

                for (var i = 0; i < count1; ++i)
                {
                    AppendFormatLine("Unk1 GUID {0}: {1:X16}", i, Reader.ReadUInt64());
                }
            }

            AppendLine();

            var count2 = Reader.ReadUInt32();
            AppendFormatLine("count2: {0}", count2);
            AppendFormatLine("unk2: {0}", Reader.ReadUInt32());

            AppendLine();

            for (var i = 0; i < count2; ++i)
            {
                AppendFormatLine("count2 GUID {0}: {1:X16}", i, Reader.ReadUInt64());
                var flags = Reader.ReadUInt32();
                AppendFormatLine("count2 flags: 0x{0:X8}", flags);

                if ((flags & 0x2) != 0)
                {
                    AppendFormatLine("flags & 0x2 string: {0}", Reader.ReadCString());
                }

                if ((flags & 0x10) != 0)
                {
                    AppendFormatLine("flags & 0x10 byte: {0}", Reader.ReadByte());
                }

                if ((flags & 0x20) != 0)
                {
                    for (var j = 0; j < 3; ++j)
                        AppendFormatLine("flags & 0x20 byte {0}: {1}", j, Reader.ReadByte());
                }

                AppendLine();
            }

            var count3 = Reader.ReadUInt32();
            AppendFormatLine("count3: {0}", count3);
            AppendFormatLine("unk3: {0}", Reader.ReadUInt32());

            AppendLine();

            for (var i = 0; i < count3; ++i)
            {
                AppendFormatLine("Player GUID: {0:X16}", Reader.ReadUInt64());
                var flags = Reader.ReadUInt32();
                AppendFormatLine("count3 flags: 0x{0:X8}", flags);

                if ((flags & 0x1) != 0)
                {
                    AppendFormatLine("Level: {0}", Reader.ReadByte());
                    AppendFormatLine("Class: {0}", Reader.ReadByte());
                    AppendFormatLine("Race: {0}", Reader.ReadByte());

                    for (var j = 0; j < 3; ++j)
                        AppendFormatLine("talents in tab {0}: {1}", j, Reader.ReadByte());

                    AppendFormatLine("resistances1: {0}", Reader.ReadUInt32());
                    AppendFormatLine("spd/heal: {0}", Reader.ReadUInt32());
                    AppendFormatLine("spd/heal: {0}", Reader.ReadUInt32());
                    AppendFormatLine("combat_rating9: {0}", Reader.ReadUInt32());
                    AppendFormatLine("combat_rating10: {0}", Reader.ReadUInt32());
                    AppendFormatLine("combat_rating11: {0}", Reader.ReadUInt32());
                    AppendFormatLine("mp5: {0}", Reader.ReadSingle());
                    AppendFormatLine("flags & 0x1 float: {0}", Reader.ReadSingle());
                    AppendFormatLine("attack power: {0}", Reader.ReadUInt32());
                    AppendFormatLine("stat1: {0}", Reader.ReadUInt32());
                    AppendFormatLine("maxhealth: {0}", Reader.ReadUInt32());
                    AppendFormatLine("maxpower1: {0}", Reader.ReadUInt32());
                    AppendFormatLine("flags & 0x1 uint: {0}", Reader.ReadUInt32());
                    AppendFormatLine("flags & 0x1 float: {0}", Reader.ReadSingle());
                    AppendFormatLine("flags & 0x1 uint: {0}", Reader.ReadUInt32());
                    AppendFormatLine("flags & 0x1 uint: {0}", Reader.ReadUInt32());
                    AppendFormatLine("flags & 0x1 uint: {0}", Reader.ReadUInt32());
                    AppendFormatLine("flags & 0x1 uint: {0}", Reader.ReadUInt32());
                    AppendFormatLine("combat_rating20: {0}", Reader.ReadUInt32());
                    AppendFormatLine("flags & 0x1 uint: {0}", Reader.ReadUInt32());
                }

                if ((flags & 0x2) != 0)
                {
                    AppendFormatLine("Comment: {0}", Reader.ReadCString());
                }

                if ((flags & 0x4) != 0)
                {
                    AppendFormatLine("flags & 0x4 byte: {0}", Reader.ReadByte());
                }

                if ((flags & 0x8) != 0)
                {
                    AppendFormatLine("flags & 0x8 guid: {0:X16}", Reader.ReadUInt64());
                }

                if ((flags & 0x10) != 0)
                {
                    AppendFormatLine("flags & 0x10 byte: {0}", Reader.ReadByte());
                }

                if ((flags & 0x20) != 0)
                {
                    AppendFormatLine("Roles: {0}", (LfgRoles)Reader.ReadByte());
                }

                if ((flags & 0x40) != 0)
                {
                    AppendFormatLine("AreaId: {0}", Reader.ReadUInt32());
                }

                if ((flags & 0x100) != 0)
                {
                    AppendFormatLine("flags & 0x100 byte: {0}", Reader.ReadByte());
                }

                if ((flags & 0x80) != 0)
                {
                    for (var j = 0; j < 3; ++j)
                    {
                        var temp = Reader.ReadUInt32();
                        var type = (temp >> 24) & 0x000000FF;
                        var entry = temp & 0x00FFFFFF;
                        AppendFormatLine("LFG Slot {0}: LFG Type: {1}, LFG Entry: {2}", j, (LfgType)type, entry);
                    }
                }

                AppendLine();
            }
        }
    }
}
