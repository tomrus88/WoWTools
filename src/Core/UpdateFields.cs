using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WowTools.Core
{
    #region UpdateTypes
    /// <summary>
    /// WoW Update Types.
    /// </summary>
    public enum UpdateTypes
    {
        /// <summary>
        /// Update type that update only object field values.
        /// </summary>
        UPDATETYPE_VALUES = 0,
        /// <summary>
        /// Update type that update only object movement.
        /// </summary>
        UPDATETYPE_MOVEMENT = 1,
        /// <summary>
        /// Update type that create an object (full update).
        /// </summary>
        UPDATETYPE_CREATE_OBJECT = 2,
        /// <summary>
        /// Update type that create an object (gull update, self use).
        /// </summary>
        UPDATETYPE_CREATE_OBJECT2 = 3,
        /// <summary>
        /// Update type that update only objects out of range.
        /// </summary>
        UPDATETYPE_OUT_OF_RANGE_OBJECTS = 4,
        /// <summary>
        /// Update type that update only near objects.
        /// </summary>
        UPDATETYPE_NEAR_OBJECTS = 5,
    }
    #endregion

    #region UpdateFlags
    /// <summary>
    /// WoW Update Flags
    /// </summary>
    [Flags]
    public enum UpdateFlags : ushort
    {
        /// <summary>
        /// No flag set.
        /// </summary>
        UPDATEFLAG_NONE = 0x00,
        /// <summary>
        /// Update flag for self.
        /// </summary>
        UPDATEFLAG_SELFTARGET = 0x01,
        /// <summary>
        /// Update flag for transport object.
        /// </summary>
        UPDATEFLAG_TRANSPORT = 0x02,
        /// <summary>
        /// Update flag with target guid.
        /// </summary>
        UPDATEFLAG_TARGET_GUID = 0x04,
        /// <summary>
        /// Update flag unknown...
        /// </summary>
        UPDATEFLAG_LOWGUID = 0x08,
        /// <summary>
        /// Common update flag.
        /// </summary>
        UPDATEFLAG_HIGHGUID = 0x10,
        /// <summary>
        /// Update flag for living objects.
        /// </summary>
        UPDATEFLAG_LIVING = 0x20,
        /// <summary>
        /// Update flag for world objects (players, units, go, do, corpses).
        /// </summary>
        UPDATEFLAG_HAS_POSITION = 0x40,
        /// <summary>
        /// Unknown, added in WotLK Beta
        /// </summary>
        UPDATEFLAG_VEHICLE = 0x80,
        /// <summary>
        /// Unknown, added in 3.1
        /// </summary>
        UPDATEFLAG_GO_POSITION = 0x100,
        /// <summary>
        /// Unknown, added in 3.1
        /// </summary>
        UPDATEFLAG_GO_ROTATION = 0x200,
        /// <summary>
        /// Unknown, added in 3.1+
        /// </summary>
        UPDATEFLAG_UNK1 = 0x400,
    }
    #endregion

    #region ObjectTypes
    /// <summary>
    /// WoW object types.
    /// </summary>
    public enum ObjectTypes : byte
    {
        /// <summary>
        /// An object.
        /// </summary>
        TYPEID_OBJECT = 0,
        /// <summary>
        /// Item.
        /// </summary>
        TYPEID_ITEM = 1,
        /// <summary>
        /// Container (item).
        /// </summary>
        TYPEID_CONTAINER = 2,
        /// <summary>
        /// Unit.
        /// </summary>
        TYPEID_UNIT = 3,
        /// <summary>
        /// Player (unit).
        /// </summary>
        TYPEID_PLAYER = 4,
        /// <summary>
        /// Game object.
        /// </summary>
        TYPEID_GAMEOBJECT = 5,
        /// <summary>
        /// Dynamic object.
        /// </summary>
        TYPEID_DYNAMICOBJECT = 6,
        /// <summary>
        /// Player corpse (not used for units?).
        /// </summary>
        TYPEID_CORPSE = 7
    }
    #endregion

    [Flags]
    public enum ObjectTypeMask
    {
        TYPEMASK_NONE = 0x0000,
        TYPEMASK_OBJECT = 0x0001,
        TYPEMASK_ITEM = 0x0002,
        TYPEMASK_CONTAINER = 0x0004,
        TYPEMASK_UNIT = 0x0008,
        TYPEMASK_PLAYER = 0x0010,
        TYPEMASK_GAMEOBJECT = 0x0020,
        TYPEMASK_DYNAMICOBJECT = 0x0040,
        TYPEMASK_CORPSE = 0x0080
    };

    #region Updatefield struct
    public struct UpdateField
    {
        public int Identifier;
        public string Name;
        public uint Type;
        public uint Format;
        public uint Value;

        public UpdateField(int id, string name, uint type, uint format, uint value)
        {
            Identifier = id;
            Name = name;
            Type = type;
            Format = format;
            Value = value;
        }
    }
    #endregion

    enum FieldType
    {
        FIELD_TYPE_NONE = 0,
        FIELD_TYPE_END = 1,
        FIELD_TYPE_ITEM = 2,
        FIELD_TYPE_UNIT = 3,
        FIELD_TYPE_GO = 4,
        FIELD_TYPE_DO = 5,
        FIELD_TYPE_CORPSE = 6
    };

    public class UpdateFieldsLoader
    {
        /// <summary>
        /// Object update fields end.
        /// </summary>
        public static uint OBJECT_END;
        /// <summary>
        /// Item update fields end.
        /// </summary>
        public static uint ITEM_END;
        /// <summary>
        /// Container update fields end.
        /// </summary>
        public static uint CONTAINER_END;
        /// <summary>
        /// Unit update fields end.
        /// </summary>
        public static uint UNIT_END;
        /// <summary>
        /// Player update fields end.
        /// </summary>
        public static uint PLAYER_END;
        /// <summary>
        /// Game object update fields end.
        /// </summary>
        public static uint GO_END;
        /// <summary>
        /// Dynamic object fields end.
        /// </summary>
        public static uint DO_END;
        /// <summary>
        /// Corpse fields end.
        /// </summary>
        public static uint CORPSE_END;

        public static Dictionary<int, UpdateField> item_uf = new Dictionary<int, UpdateField>(); // item + container
        public static Dictionary<int, UpdateField> unit_uf = new Dictionary<int, UpdateField>(); // unit + player
        public static Dictionary<int, UpdateField> go_uf = new Dictionary<int, UpdateField>();
        public static Dictionary<int, UpdateField> do_uf = new Dictionary<int, UpdateField>();
        public static Dictionary<int, UpdateField> corpse_uf = new Dictionary<int, UpdateField>();

        public static UpdateField GetUpdateField(ObjectTypes type, int index)
        {
            // index parameter must be checked first
            switch (type)
            {
                case ObjectTypes.TYPEID_ITEM:
                case ObjectTypes.TYPEID_CONTAINER:
                    return item_uf[index];
                case ObjectTypes.TYPEID_UNIT:
                case ObjectTypes.TYPEID_PLAYER:
                    return unit_uf[index];
                case ObjectTypes.TYPEID_GAMEOBJECT:
                    return go_uf[index];
                case ObjectTypes.TYPEID_DYNAMICOBJECT:
                    return do_uf[index];
                case ObjectTypes.TYPEID_CORPSE:
                    return corpse_uf[index];
                default:
                    return unit_uf[index];
            }
        }

        public static void LoadUpdateFields(uint build)
        {
            ClearUpdateFields();

            var file = String.Format(Application.StartupPath + "\\" + "updatefields\\{0}.dat", build);
            var type = FieldType.FIELD_TYPE_NONE;
            var sr = new StreamReader(file);
            while (sr.Peek() >= 0)
            {
                if (type == FieldType.FIELD_TYPE_END)
                {
                    OBJECT_END = Convert.ToUInt32(sr.ReadLine());
                    ITEM_END = Convert.ToUInt32(sr.ReadLine());
                    CONTAINER_END = Convert.ToUInt32(sr.ReadLine());
                    UNIT_END = Convert.ToUInt32(sr.ReadLine());
                    PLAYER_END = Convert.ToUInt32(sr.ReadLine());
                    GO_END = Convert.ToUInt32(sr.ReadLine());
                    DO_END = Convert.ToUInt32(sr.ReadLine());
                    CORPSE_END = Convert.ToUInt32(sr.ReadLine());
                    type = FieldType.FIELD_TYPE_NONE;
                    continue;
                }

                var curline = sr.ReadLine();

                if (curline.StartsWith("#") || curline.StartsWith("/")) // skip commentary lines
                    continue;

                if (curline.Length == 0)    // empty line
                    continue;

                if (curline.StartsWith(":"))    // label lines
                {
                    if (curline.Contains("ends"))
                        type = FieldType.FIELD_TYPE_END;
                    if (curline.Contains("item"))
                        type = FieldType.FIELD_TYPE_ITEM;
                    if (curline.Contains("unit+player"))
                        type = FieldType.FIELD_TYPE_UNIT;
                    else if (curline.Contains("gameobject"))
                        type = FieldType.FIELD_TYPE_GO;
                    else if (curline.Contains("dynamicobject"))
                        type = FieldType.FIELD_TYPE_DO;
                    else if (curline.Contains("corpse"))
                        type = FieldType.FIELD_TYPE_CORPSE;

                    continue;
                }

                var arr = curline.Split('	');

                if (arr.Length < 3)
                    continue;

                var id = Convert.ToInt32(arr[0]);
                var name = arr[1];
                var type1 = Convert.ToUInt32(arr[2]);
                //uint format = Convert.ToUInt32(arr[3]);
                const uint format = 0;

                var uf = new UpdateField(id, name, type1, format, 0);
                switch (type)
                {
                    case FieldType.FIELD_TYPE_END:
                        break;
                    case FieldType.FIELD_TYPE_ITEM:
                        item_uf.Add(id, uf);
                        break;
                    case FieldType.FIELD_TYPE_UNIT:
                        unit_uf.Add(id, uf);
                        break;
                    case FieldType.FIELD_TYPE_GO:
                        go_uf.Add(id, uf);
                        break;
                    case FieldType.FIELD_TYPE_DO:
                        do_uf.Add(id, uf);
                        break;
                    case FieldType.FIELD_TYPE_CORPSE:
                        corpse_uf.Add(id, uf);
                        break;
                }
            }

            CheckIntegrity();
        }

        public static void ClearUpdateFields()
        {
            item_uf.Clear();
            unit_uf.Clear();
            go_uf.Clear();
            do_uf.Clear();
            corpse_uf.Clear();
        }

        /// <summary>
        /// Check updatefields.dat integrity
        /// </summary>
        public static void CheckIntegrity()
        {
            // program will crash there if updatefields.dat contains errors
            for (var i = 0; i < item_uf.Count; i++)
            {
                var uf = item_uf[i];
            }

            for (var i = 0; i < unit_uf.Count; i++)
            {
                var uf = unit_uf[i];
            }

            for (var i = 0; i < go_uf.Count; i++)
            {
                var uf = go_uf[i];
            }

            for (var i = 0; i < do_uf.Count; i++)
            {
                var uf = do_uf[i];
            }

            for (var i = 0; i < corpse_uf.Count; i++)
            {
                var uf = corpse_uf[i];
            }
        }
    }
}
