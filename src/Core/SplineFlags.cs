using System;

namespace WowTools.Core
{
    [Flags]
    public enum SplineFlags : uint
    {
        NONE = 0x00000000,
        FORWARD = 0x00000001,
        BACKWARD = 0x00000002,
        STRAFELEFT = 0x00000004,
        STRAFERIGHT = 0x00000008,
        TURNLEFT = 0x00000010,
        TURNRIGHT = 0x00000020,
        PITCHUP = 0x00000040,
        PITCHDOWN = 0x00000080,
        DONE = 0x00000100,
        FALLING = 0x00000200,
        NOSPLINE = 0x00000400,
        TRAJECTORY = 0x00000800,
        WALKMODE = 0x00001000,
        FLYING = 0x00002000,
        KNOCKBACK = 0x00004000,
        FINALPOINT = 0x00008000,
        FINALTARGET = 0x00010000,
        FINALORIENT = 0x00020000,
        CATMULLROM = 0x00040000,
        UNK1 = 0x00080000,
        UNK2 = 0x00100000,
        UNK3 = 0x00200000,
        UNK4 = 0x00400000,
        UNK5 = 0x00800000,
        UNK6 = 0x01000000,
        UNK7 = 0x02000000,
        UNK8 = 0x04000000,
        UNK9 = 0x08000000,
        UNK10 = 0x10000000,
        UNK11 = 0x20000000,
        UNK12 = 0x40000000,
        UNK13 = 0x80000000,
    };

    public enum SplineMode : byte
    {
        Linear = 0,
        CatmullRom = 1,
        Bezier3 = 2
    }

    public enum SplineType : byte
    {
        Normal = 0,
        Stop = 1,
        FacingSpot = 2,
        FacingTarget = 3,
        FacingAngle = 4
    }
}
