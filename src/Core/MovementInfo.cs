using System.IO;

namespace WowTools.Core
{
    public class MovementInfo
    {
        public UpdateFlags UpdateFlags { get; private set; }

        public MovementFlags Flags { get; private set; }
        public MovementFlags2 Flags2 { get; private set; }
        public uint TimeStamp { get; private set; }

        public Coords3 Position { get; private set; }
        public float Facing { get; private set; }

        public TransportInfo Transport { get; private set; }

        public float Pitch { get; private set; }

        public uint FallTime { get; private set; }

        public float FallSinAngle { get; private set; }
        public float FallCosAngle { get; private set; }
        public float FallVelocity { get; private set; }
        public float FallSpeed { get; private set; }

        public float SplineElevation { get; private set; }

        public readonly float[] speeds = new float[9];

        public SplineInfo Spline { get; private set; }

        public uint LowGuid { get; private set; }

        public uint HighGuid { get; private set; }

        public ulong AttackingTarget { get; private set; }

        public uint TransportTime { get; private set; }

        public uint VehicleId { get; private set; }
        public float VehicleAimAdjustement { get; private set; }

        public ulong GoRotationULong { get; private set; }

        public MovementInfo()
        {
            Transport = new TransportInfo();
            Spline = new SplineInfo();
        }

        public static MovementInfo Read(BinaryReader gr)
        {
            var movement = new MovementInfo();

            movement.UpdateFlags = (UpdateFlags)gr.ReadUInt16();

            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_LIVING))
            {
                movement.Flags = (MovementFlags)gr.ReadUInt32();
                movement.Flags2 = (MovementFlags2)gr.ReadUInt16();
                movement.TimeStamp = gr.ReadUInt32();

                movement.Position = gr.ReadCoords3();
                movement.Facing = gr.ReadSingle();

                if (movement.Flags.HasFlag(MovementFlags.ONTRANSPORT))
                {
                    movement.Transport = TransportInfo.Read(gr, movement.Flags2);
                }

                if (movement.Flags.HasFlag(MovementFlags.SWIMMING) || movement.Flags.HasFlag(MovementFlags.FLYING) ||
                    movement.Flags2.HasFlag(MovementFlags2.AlwaysAllowPitching))
                {
                    movement.Pitch = gr.ReadSingle();
                }

                movement.FallTime = gr.ReadUInt32();

                if (movement.Flags.HasFlag(MovementFlags.FALLING))
                {
                    movement.FallVelocity = gr.ReadSingle();
                    movement.FallCosAngle = gr.ReadSingle();
                    movement.FallSinAngle = gr.ReadSingle();
                    movement.FallSpeed = gr.ReadSingle();
                }

                if (movement.Flags.HasFlag(MovementFlags.SPLINEELEVATION))
                {
                    movement.SplineElevation = gr.ReadSingle();
                }

                for (byte i = 0; i < movement.speeds.Length; ++i)
                    movement.speeds[i] = gr.ReadSingle();

                if (movement.Flags.HasFlag(MovementFlags.SPLINEENABLED))
                {
                    movement.Spline = SplineInfo.Read(gr);
                }
            }
            else
            {
                if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_GO_POSITION))
                {
                    movement.Transport.Guid = gr.ReadPackedGuid();
                    movement.Position = gr.ReadCoords3();
                    movement.Transport.Position = gr.ReadCoords3();
                    movement.Facing = gr.ReadSingle();
                    movement.Transport.Facing = gr.ReadSingle();
                }
                else if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_HAS_POSITION))
                {
                    movement.Position = gr.ReadCoords3();
                    movement.Facing = gr.ReadSingle();
                }
            }

            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_LOWGUID))
            {
                movement.LowGuid = gr.ReadUInt32();
            }

            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_HIGHGUID))
            {
                movement.HighGuid = gr.ReadUInt32();
            }

            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_TARGET_GUID))
            {
                movement.AttackingTarget = gr.ReadPackedGuid();
            }

            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_TRANSPORT))
            {
                movement.TransportTime = gr.ReadUInt32();
            }

            // WotLK
            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_VEHICLE))
            {
                movement.VehicleId = gr.ReadUInt32();
                movement.VehicleAimAdjustement = gr.ReadSingle();
            }

            // 3.1
            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_GO_ROTATION))
            {
                movement.GoRotationULong = gr.ReadUInt64();
            }
            return movement;
        }
    }
}
