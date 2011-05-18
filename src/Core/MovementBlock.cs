using System.IO;

namespace WowTools.Core
{
    public class MovementBlock
    {
        public UpdateFlags UpdateFlags { get; private set; }

        public MovementInfo Movement { get; private set; }

        public readonly float[] speeds = new float[9];

        public SplineInfo Spline { get; private set; }

        public uint LowGuid { get; private set; }

        public uint HighGuid { get; private set; }

        public ulong AttackingTarget { get; private set; }

        public uint TransportTime { get; private set; }

        public uint VehicleId { get; private set; }
        public float VehicleAimAdjustement { get; private set; }

        public ulong GoRotationULong { get; private set; }

        public MovementBlock()
        {
            Movement = new MovementInfo();
            Spline = new SplineInfo();
        }

        public static MovementBlock Read(BinaryReader gr)
        {
            var movement = new MovementBlock();

            movement.UpdateFlags = (UpdateFlags)gr.ReadUInt16();

            if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_LIVING))
            {
                movement.Movement = MovementInfo.Read(gr);

                for (byte i = 0; i < movement.speeds.Length; ++i)
                    movement.speeds[i] = gr.ReadSingle();

                if (movement.Movement.Flags.HasFlag(MovementFlags.SPLINEENABLED))
                {
                    movement.Spline = SplineInfo.Read(gr);
                }
            }
            else
            {
                if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_GO_POSITION))
                {
                    movement.Movement.Transport.Guid = gr.ReadPackedGuid();
                    movement.Movement.Position = gr.ReadCoords3();
                    movement.Movement.Transport.Position = gr.ReadCoords3();
                    movement.Movement.Facing = gr.ReadSingle();
                    movement.Movement.Transport.Facing = gr.ReadSingle();
                }
                else if (movement.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_HAS_POSITION))
                {
                    movement.Movement.Position = gr.ReadCoords3();
                    movement.Movement.Facing = gr.ReadSingle();
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
