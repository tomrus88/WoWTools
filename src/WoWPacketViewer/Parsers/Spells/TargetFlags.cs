using System;

namespace WoWPacketViewer
{
    [Flags]
    enum TargetFlags : uint
    {
        TARGET_FLAG_SELF = 0x00000000,
        TARGET_FLAG_UNUSED1 = 0x00000001,               // not used in any spells as of 3.0.3 (can be set dynamically)
        TARGET_FLAG_UNIT = 0x00000002,               // pguid
        TARGET_FLAG_UNUSED2 = 0x00000004,               // not used in any spells as of 3.0.3 (can be set dynamically)
        TARGET_FLAG_UNUSED3 = 0x00000008,               // not used in any spells as of 3.0.3 (can be set dynamically)
        TARGET_FLAG_ITEM = 0x00000010,               // pguid
        TARGET_FLAG_SOURCE_LOCATION = 0x00000020,               // 3 float
        TARGET_FLAG_DEST_LOCATION = 0x00000040,               // 3 float
        TARGET_FLAG_OBJECT_UNK = 0x00000080,               // used in 7 spells only
        TARGET_FLAG_UNIT_UNK = 0x00000100,               // looks like self target (480 spells)
        TARGET_FLAG_PVP_CORPSE = 0x00000200,               // pguid
        TARGET_FLAG_UNIT_CORPSE = 0x00000400,               // 10 spells (gathering professions)
        TARGET_FLAG_OBJECT = 0x00000800,               // pguid, 2 spells
        TARGET_FLAG_TRADE_ITEM = 0x00001000,               // pguid, 0 spells
        TARGET_FLAG_STRING = 0x00002000,               // string, 0 spells
        TARGET_FLAG_UNK1 = 0x00004000,               // 199 spells, opening object/lock
        TARGET_FLAG_CORPSE = 0x00008000,               // pguid, resurrection spells
        TARGET_FLAG_UNK2 = 0x00010000,               // pguid, not used in any spells as of 3.0.3 (can be set dynamically)
        TARGET_FLAG_GLYPH = 0x00020000                // used in glyph spells
    }
}
