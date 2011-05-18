using System;

namespace WowTools.Core
{
    [Flags]
    public enum MovementFlags : uint
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
        WALKMODE = 0x00000100,
        ONTRANSPORT = 0x00000200,
        LEVITATING = 0x00000400,
        ROOT = 0x00000800,
        FALLING = 0x00001000,
        FALLINGFAR = 0x00002000,
        PENDINGSTOP = 0x00004000,
        PENDINGSTRAFESTOP = 0x00008000,
        PENDINGFORWARD = 0x00010000,
        PENDINGBACKWARD = 0x00020000,
        PENDINGSTRAFELEFT = 0x00040000,
        PENDINGSTRAFERIGHT = 0x00080000,
        PENDINGROOT = 0x00100000,
        SWIMMING = 0x00200000,
        ASCENDING = 0x00400000,
        DESCENDING = 0x00800000,
        CAN_FLY = 0x01000000,
        FLYING = 0x02000000,
        SPLINEELEVATION = 0x04000000,
        SPLINEENABLED = 0x08000000,
        WATERWALKING = 0x10000000,
        SAFEFALL = 0x20000000,
        HOVER = 0x40000000
    };

    [Flags]
    public enum MovementFlags2 : ushort
    {
        None = 0x0000,
        Unknown1 = 0x0001,
        Unknown2 = 0x0002,
        Unknown3 = 0x0004,
        FullSpeedTurning = 0x0008,
        FullSpeedPitching = 0x0010,
        AlwaysAllowPitching = 0x0020,
        Unknown4 = 0x0040,
        Unknown5 = 0x0080,
        Unknown6 = 0x0100,
        Unknown7 = 0x0200,
        InterpolatedPlayerMovement = 0x0400,
        InterpolatedPlayerTurning = 0x0800,
        InterpolatedPlayerPitching = 0x1000,
        Unknown8 = 0x2000,
        Unknown9 = 0x4000,
        Unknown10 = 0x8000
    };
}
