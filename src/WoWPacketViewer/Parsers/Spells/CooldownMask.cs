using System;

namespace WoWPacketViewer.Parsers.Spells
{
    [Flags]
    enum CooldownMask : byte
    {
        RUNE_NONE = 0,
        RUNE_BLOOD_0 = 1,
        RUNE_BLOOD_1 = 2,
        RUNE_UNHOLY_0 = 4,
        RUNE_UNHOLY_1 = 8,
        RUNE_FROST_0 = 16,
        RUNE_FROST_1 = 32
    }
}