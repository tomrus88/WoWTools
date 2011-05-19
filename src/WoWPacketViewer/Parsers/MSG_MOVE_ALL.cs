using WowTools.Core;

[Parser(OpCodes.MSG_MOVE_START_FORWARD)]
[Parser(OpCodes.MSG_MOVE_START_BACKWARD)]
[Parser(OpCodes.MSG_MOVE_STOP)]
[Parser(OpCodes.MSG_MOVE_START_STRAFE_LEFT)]
[Parser(OpCodes.MSG_MOVE_START_STRAFE_RIGHT)]
[Parser(OpCodes.MSG_MOVE_STOP_STRAFE)]
[Parser(OpCodes.MSG_MOVE_JUMP)]
[Parser(OpCodes.MSG_MOVE_START_TURN_LEFT)]
[Parser(OpCodes.MSG_MOVE_START_TURN_RIGHT)]
[Parser(OpCodes.MSG_MOVE_STOP_TURN)]
[Parser(OpCodes.MSG_MOVE_START_PITCH_UP)]
[Parser(OpCodes.MSG_MOVE_START_PITCH_DOWN)]
[Parser(OpCodes.MSG_MOVE_STOP_PITCH)]
[Parser(OpCodes.MSG_MOVE_SET_RUN_MODE)]
[Parser(OpCodes.MSG_MOVE_SET_WALK_MODE)]
[Parser(OpCodes.MSG_MOVE_FALL_LAND)]
[Parser(OpCodes.MSG_MOVE_START_SWIM)]
[Parser(OpCodes.MSG_MOVE_STOP_SWIM)]
[Parser(OpCodes.MSG_MOVE_SET_FACING)]
[Parser(OpCodes.MSG_MOVE_SET_PITCH)]
[Parser(OpCodes.MSG_MOVE_HEARTBEAT)]
[Parser(OpCodes.CMSG_MOVE_FALL_RESET)]
[Parser(OpCodes.CMSG_MOVE_SET_FLY)]
[Parser(OpCodes.MSG_MOVE_START_ASCEND)]
[Parser(OpCodes.MSG_MOVE_STOP_ASCEND)]
[Parser(OpCodes.CMSG_MOVE_CHNG_TRANSPORT)]
[Parser(OpCodes.MSG_MOVE_START_DESCEND)]
class MovementOpcodes : Parser
{
    public override void Parse()
    {
        ReadPackedGuid("Mover Guid: 0x{0:X16}");
        AppendLine(MovementInfo.Read(Reader).ToString());
    }
}
