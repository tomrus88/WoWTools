using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WowTools.Core
{
    public abstract class Parser
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Append(string str)
        {
            stringBuilder.Append(str);
        }

        public void AppendLine()
        {
            stringBuilder.AppendLine();
        }

        public void AppendLine(string str)
        {
            stringBuilder.AppendLine(str);
        }

        public void AppendFormat(string format, params object[] args)
        {
            stringBuilder.AppendFormat(format, args);
        }

        public void AppendFormatLine(string format, params object[] args)
        {
            stringBuilder.AppendFormat(format, args).AppendLine();
        }

        public void CheckPacket()
        {
            if (Reader.BaseStream.Position != Reader.BaseStream.Length)
            {
                string msg = String.Format("{0}: Packet size changed, should be {1} instead of {2}", Packet.Code, Reader.BaseStream.Position, Reader.BaseStream.Length);
                MessageBox.Show(msg);
            }
        }

        protected Packet Packet { get; private set; }

        protected BinaryReader Reader { get; private set; }

        protected Parser() { }

        public virtual void Initialize(Packet packet)
        {
            Packet = packet;

            if (packet != null)
            {
                Reader = Packet.CreateReader();
                Parse();
                CheckPacket();
            }
        }

        public abstract void Parse();

        public override string ToString()
        {
            return stringBuilder.ToString();
        }

        public byte ReadUInt8(string format, params object[] args)
        {
            var ret = Reader.ReadByte();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        // for enums
        public T ReadUInt8<T>(string format, params object[] args)
        {
            var obj = Enum.ToObject(typeof(T), Reader.ReadByte());
            AppendFormatLine(format, MergeArguments(args, obj));
            return (T)obj;
        }

        public int ReadInt32(string format, params object[] args)
        {
            var ret = Reader.ReadInt32();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        public uint ReadUInt32(string format, params object[] args)
        {
            var ret = Reader.ReadUInt32();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        public ulong ReadUInt64(string format, params object[] args)
        {
            var ret = Reader.ReadUInt64();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        public string ReadCString(string format, params object[] args)
        {
            var ret = Reader.ReadCString();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        public float ReadSingle(string format, params object[] args)
        {
            var ret = Reader.ReadSingle();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        public ulong ReadPackedGuid(string format, params object[] args)
        {
            var ret = Reader.ReadPackedGuid();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        public T Read<T>(string format, params object[] args) where T : struct
        {
            var ret = Reader.ReadStruct<T>();
            AppendFormatLine(format, MergeArguments(args, ret));
            return ret;
        }

        private object[] MergeArguments(object[] args, object arg)
        {
            var newArgs = new List<object>();
            newArgs.AddRange(args);
            newArgs.Add(arg);
            return newArgs.ToArray();
        }

        public void For(int count, Action func)
        {
            for (var i = 0; i < count; ++i)
                func();
        }

        public void For(int count, Action<int> func)
        {
            for (var i = 0; i < count; ++i)
                func(i);
        }
    }
}
