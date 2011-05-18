using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using WowTools.Core;

namespace UpdatePacketParser
{
    public class Parser
    {
        private readonly Dictionary<ulong, WoWObject> objects = new Dictionary<ulong, WoWObject>();

        public Parser(IPacketReader reader)
        {
            foreach (var packet in reader.ReadPackets())
            {
                var gr = packet.CreateReader();
                var code = packet.Code;
                if (code == OpCodes.SMSG_COMPRESSED_UPDATE_OBJECT)
                {
                    code = OpCodes.SMSG_UPDATE_OBJECT;
                    Decompress(ref gr);
                }
                if (code == OpCodes.SMSG_UPDATE_OBJECT)
                {
                    ParseRest(gr);
                    CheckPacket(gr);
                }

                gr.Close();
            }
        }

        private WoWObject GetWoWObject(ulong guid)
        {
            WoWObject obj;
            objects.TryGetValue(guid, out obj);
            return obj;
        }

        private static void CheckPacket(BinaryReader gr)
        {
            if (gr.BaseStream.Position != gr.BaseStream.Length)
                MessageBox.Show(String.Format("Packet parsing error, diff {0}", gr.BaseStream.Length - gr.BaseStream.Position));
        }

        private void ParseRest(BinaryReader gr)
        {
            var objectsCount = gr.ReadUInt32();

            for (var i = 0; i < objectsCount; i++)
            {
                var updateType = (UpdateTypes)gr.ReadByte();
                switch (updateType)
                {
                    case UpdateTypes.UPDATETYPE_VALUES:
                        ParseValues(gr);
                        break;
                    case UpdateTypes.UPDATETYPE_MOVEMENT:
                        ParseMovement(gr);
                        break;
                    case UpdateTypes.UPDATETYPE_CREATE_OBJECT:
                    case UpdateTypes.UPDATETYPE_CREATE_OBJECT2:
                        ParseCreateObjects(gr);
                        break;
                    case UpdateTypes.UPDATETYPE_OUT_OF_RANGE_OBJECTS:
                        ParseOutOfRangeObjects(gr);
                        break;
                    case UpdateTypes.UPDATETYPE_NEAR_OBJECTS:
                        ParseNearObjects(gr);
                        break;
                    default:
                        Console.WriteLine("Unknown updatetype {0}", updateType);
                        break;
                }
            }
        }

        private void ParseValues(BinaryReader gr)
        {
            ulong guid = gr.ReadPackedGuid();

            var woWObjectUpdate = WoWObjectUpdate.Read(gr);

            var wowobj = GetWoWObject(guid);
            if (wowobj != null)
                wowobj.AddUpdate(woWObjectUpdate);
            else
                MessageBox.Show("Boom!");
        }

        private static void ParseMovement(BinaryReader gr)
        {
            var guid = gr.ReadPackedGuid();

            var mInfo = MovementBlock.Read(gr);

            // is it even used by blizzard?
        }

        private void ParseCreateObjects(BinaryReader gr)
        {
            var guid = gr.ReadPackedGuid();

            var objectTypeId = (ObjectTypes)gr.ReadByte();

            var movement = MovementBlock.Read(gr);

            // values part
            var update = WoWObjectUpdate.Read(gr);

            var obj = GetWoWObject(guid);
            if (obj == null)
                objects.Add(guid, new WoWObject(objectTypeId, movement, update.Data));
            else
            {
                objects.Remove(guid);
                objects.Add(guid, new WoWObject(objectTypeId, movement, update.Data));
            }
        }

        private static void ParseOutOfRangeObjects(BinaryReader gr)
        {
            var count = gr.ReadUInt32();
            var guids = new ulong[count];
            for (var i = 0; i < count; ++i)
                guids[i] = gr.ReadPackedGuid();
        }

        private static void ParseNearObjects(BinaryReader gr)
        {
            var count = gr.ReadUInt32();
            var guids = new ulong[count];
            for (var i = 0; i < count; ++i)
                guids[i] = gr.ReadPackedGuid();
        }

        private static void Decompress(ref BinaryReader gr)
        {
            var uncompressedLength = gr.ReadInt32();
            gr.ReadBytes(2); // unk junk from RFC 1950
            var input = gr.ReadBytes((int)(gr.BaseStream.Length - gr.BaseStream.Position));
            gr.Close();
            var dStream = new DeflateStream(new MemoryStream(input), CompressionMode.Decompress);
            var output = new byte[uncompressedLength];
            dStream.Read(output, 0, output.Length);
            dStream.Close();
            gr = new BinaryReader(new MemoryStream(output));
        }

        public void PrintObjects(ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (var pair in objects)
            {
                var guid = pair.Key;
                var type = pair.Value.TypeId;

                var final = String.Format("{0:X16} {1}", guid, type);
                listBox.Items.Add(final);
            }
        }

        public void PrintObjectsType(ListBox listBox, ObjectTypeMask mask, CustomFilterMask customMask)
        {
            listBox.Items.Clear();

            foreach (var pair in objects)
            {
                if ((pair.Value.GetType() & mask) != ObjectTypeMask.TYPEMASK_NONE)
                    continue;

                if (customMask != CustomFilterMask.CUSTOM_FILTER_NONE)
                {
                    var highGUID = (pair.Value.GetGUIDHigh() >> 16);
                    if (customMask.HasFlag(CustomFilterMask.CUSTOM_FILTER_UNITS) &&
                        (highGUID == 0xF130 || highGUID == 0xF530))
                        continue;
                    if (customMask.HasFlag(CustomFilterMask.CUSTOM_FILTER_PETS) &&
                        (highGUID == 0xF140 || highGUID == 0xF540))
                        continue;
                    if (customMask.HasFlag(CustomFilterMask.CUSTOM_FILTER_VEHICLES) &&
                        (highGUID == 0xF150 || highGUID == 0xF550))
                        continue;
                    if (customMask.HasFlag(CustomFilterMask.CUSTOM_FILTER_OBJECTS) &&
                        (highGUID == 0xF110 || highGUID == 0xF510))
                        continue;
                    if (customMask.HasFlag(CustomFilterMask.CUSTOM_FILTER_TRANSPORT) &&
                        (highGUID == 0xF120 || highGUID == 0xF520))
                        continue;
                    if (customMask.HasFlag(CustomFilterMask.CUSTOM_FILTER_MO_TRANSPORT) &&
                        (highGUID == 0x1FC0))
                        continue;
                }

                var guid = pair.Key;
                var type = pair.Value.TypeId;

                var final = String.Format("{0:X16} {1}", guid, type);
                listBox.Items.Add(final);
            }
        }

        public void PrintObjectInfo(ulong guid, ListView listView)
        {
            var obj = objects[guid];
            var type = obj.TypeId;

            foreach (var kvp in obj.Data)
            {
                var uf = UpdateFieldsLoader.GetUpdateField(type, kvp.Key);
                var value = GetValueBaseOnType(kvp.Value, uf.Type);
                listView.Items.Add(new ListViewItem(new[] { uf.Name, value }));
            }
        }

        public void PrintObjectUpdatesInfo(ulong guid, ListView listView)
        {
            var obj = objects[guid];

            // make a temp copy of original values
            var objData = new Dictionary<int, uint>();
            foreach (var v in obj.Data)
                objData[v.Key] = v.Value;

            var c = 0;
            foreach (var update in obj.Updates)
            {
                c++;
                var group = new ListViewGroup(String.Format("Update {0}:", c));
                listView.Groups.Add(group);

                foreach (var kvp in update.Data)
                {
                    var uf = UpdateFieldsLoader.GetUpdateField(obj.TypeId, kvp.Key);
                    var oldValue = GetValueBaseOnType((uint)0, uf.Type); // default value 0

                    if (objData.ContainsKey(kvp.Key))
                    {
                        oldValue = GetValueBaseOnType(objData[kvp.Key], uf.Type);
                        objData[kvp.Key] = kvp.Value;
                    }

                    var newValue = GetValueBaseOnType(kvp.Value, uf.Type);
                    listView.Items.Add(new ListViewItem(new[] { uf.Name, oldValue, newValue }, group));
                }
            }
        }

        public void PrintObjectMovementInfo(ulong guid, RichTextBox richTextBox)
        {
            var obj = objects[guid];
            var mBlock = obj.Movement;
            var strings = new List<string>();

            strings.Add(String.Format("Update Flags: {0}", mBlock.UpdateFlags));

            if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_LIVING))
            {
                strings.Add(String.Format("Movement Flags: {0}", mBlock.Movement.Flags));
                strings.Add(String.Format("Unknown Flags: {0}", mBlock.Movement.Flags2));
                strings.Add(String.Format("Timestamp: {0:X8}", mBlock.Movement.TimeStamp));

                strings.Add(String.Format("Position: {0}", mBlock.Movement.Position));

                if (mBlock.Movement.Flags.HasFlag(MovementFlags.ONTRANSPORT))
                {
                    strings.Add(String.Format("Transport GUID: {0:X16}", mBlock.Movement.Transport.Guid));
                    strings.Add(String.Format("Transport POS: {0}", mBlock.Movement.Transport.Position));
                    strings.Add(String.Format("Transport Time: {0:X8}", mBlock.Movement.Transport.Time));
                    strings.Add(String.Format("Transport Seat: {0:X2}", mBlock.Movement.Transport.Seat));
                }

                if (mBlock.Movement.Flags.HasFlag(MovementFlags.SWIMMING) || mBlock.Movement.Flags.HasFlag(MovementFlags.FLYING) ||
                    mBlock.Movement.Flags2.HasFlag(MovementFlags2.AlwaysAllowPitching))
                {
                    strings.Add(String.Format("Swimming Pitch: {0}", mBlock.Movement.Pitch));
                }

                strings.Add(String.Format("Fall Time: {0:X8}", mBlock.Movement.FallTime));

                if (mBlock.Movement.Flags.HasFlag(MovementFlags.FALLING))
                {
                    strings.Add(String.Format("Jumping Unk: {0}", mBlock.Movement.FallVelocity));
                    strings.Add(String.Format("Jumping Sin: {0}", mBlock.Movement.FallCosAngle));
                    strings.Add(String.Format("Jumping Cos: {0}", mBlock.Movement.FallSinAngle));
                    strings.Add(String.Format("Jumping Speed: {0}", mBlock.Movement.FallSpeed));
                }

                if (mBlock.Movement.Flags.HasFlag(MovementFlags.SPLINEELEVATION))
                {
                    strings.Add(String.Format("Spline elevation: {0}", mBlock.Movement.SplineElevation));
                }

                for (byte i = 0; i < mBlock.speeds.Length; ++i)
                    strings.Add(String.Format("Speed{0}: {1}", i, mBlock.speeds[i]));

                if (mBlock.Movement.Flags.HasFlag(MovementFlags.SPLINEENABLED))
                {
                    strings.Add(String.Format("Spline Flags: {0}", mBlock.Spline.Flags));

                    if (mBlock.Spline.Flags.HasFlag(SplineFlags.FINALPOINT))
                    {
                        strings.Add(String.Format("Spline Point: {0}", mBlock.Spline.Point));
                    }

                    if (mBlock.Spline.Flags.HasFlag(SplineFlags.FINALTARGET))
                    {
                        strings.Add(String.Format("Spline GUID: {0:X16}", mBlock.Spline.Guid));
                    }

                    if (mBlock.Spline.Flags.HasFlag(SplineFlags.FINALORIENT))
                    {
                        strings.Add(String.Format("Spline Orient: {0}", mBlock.Spline.Rotation));
                    }

                    strings.Add(String.Format("Spline CurrTime: {0:X8}", mBlock.Spline.CurrentTime));
                    strings.Add(String.Format("Spline FullTime: {0:X8}", mBlock.Spline.FullTime));
                    strings.Add(String.Format("Spline Unk: {0:X8}", mBlock.Spline.Unknown1));

                    strings.Add(String.Format("Spline float1: {0}", mBlock.Spline.DurationMultiplier));
                    strings.Add(String.Format("Spline float2: {0}", mBlock.Spline.UnknownFloat2));
                    strings.Add(String.Format("Spline float3: {0}", mBlock.Spline.UnknownFloat3));

                    strings.Add(String.Format("Spline uint1: {0:X8}", mBlock.Spline.Unknown2));

                    strings.Add(String.Format("Spline Count: {0:X8}", mBlock.Spline.Count));

                    for (uint i = 0; i < mBlock.Spline.Count; ++i)
                    {
                        strings.Add(String.Format("Splines_{0}: {1}", i, mBlock.Spline.Splines[(int)i]));
                    }

                    strings.Add(String.Format("Spline mode: {0}", mBlock.Spline.SplineMode));

                    strings.Add(String.Format("Spline End Point: {0}", mBlock.Spline.EndPoint));
                }
            }
            else
            {
                if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_GO_POSITION))
                {
                    strings.Add(String.Format("GUID 0x100: {0:X16}", mBlock.Movement.Transport.Guid));
                    strings.Add(String.Format("Position 0x100: {0}", mBlock.Movement.Position));
                    strings.Add(String.Format("TransportPosition 0x100: {0}", mBlock.Movement.Transport.Position));
                    strings.Add(String.Format("Facing 0x100: {0}", mBlock.Movement.Facing));
                    strings.Add(String.Format("Transport Facing 0x100: {0}", mBlock.Movement.Transport.Facing));
                }
                else
                {
                    if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_HAS_POSITION))
                    {
                        strings.Add(String.Format("Position: {0}", mBlock.Movement.Position));
                    }
                }
            }

            if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_LOWGUID))
            {
                strings.Add(String.Format("Low GUID: {0:X8}", mBlock.LowGuid));
            }

            if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_HIGHGUID))
            {
                strings.Add(String.Format("High GUID: {0:X8}", mBlock.HighGuid));
            }

            if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_TARGET_GUID))
            {
                strings.Add(String.Format("Target GUID: {0:X16}", mBlock.AttackingTarget));
            }

            if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_TRANSPORT))
            {
                strings.Add(String.Format("Transport Time: {0:X8}", mBlock.TransportTime));
            }

            if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_VEHICLE))
            {
                strings.Add(String.Format("Vehicle Id: {0:X8}", mBlock.VehicleId));
                strings.Add(String.Format("Facing Adjustement: {0}", mBlock.VehicleAimAdjustement));
            }

            if (mBlock.UpdateFlags.HasFlag(UpdateFlags.UPDATEFLAG_GO_ROTATION))
            {
                strings.Add(String.Format("GO rotation: {0}", mBlock.GoRotationULong.ToString("X16")));
            }

            richTextBox.Lines = strings.ToArray();
        }

        private static string GetValueBaseOnType(Object value, uint type)
        {
            var bytes = BitConverter.GetBytes((uint)value);

            switch (type)
            {
                case 1:
                    return String.Format("int: {0:D}", BitConverter.ToInt32(bytes, 0));
                case 2:
                    return String.Format("2 x ushort: {0} {1}", BitConverter.ToUInt16(bytes, 0), BitConverter.ToUInt16(bytes, 2));
                case 3:
                    return String.Format("float: {0}", BitConverter.ToSingle(bytes, 0));
                case 4:
                    return String.Format("ulong part: 0x{0:X8}", (uint)value);
                case 5:
                    return String.Format("bytes: 0x{0:X8}", (uint)value);
                case 6:
                    return String.Format("ushort: {0}, byte {1}, byte {2}", BitConverter.ToUInt16(bytes, 0), bytes[2], bytes[3]);
                default:
                    return "0";
            }
        }

        public void Close()
        {
            objects.Clear();
        }
    }
}
