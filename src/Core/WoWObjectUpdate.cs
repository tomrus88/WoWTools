using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace WowTools.Core
{
    public class WoWObjectUpdate
    {
        public IDictionary<int, uint> Data { get; private set; }

        protected WoWObjectUpdate(IDictionary<int, uint> data)
        {
            Data = data;
        }

        public static WoWObjectUpdate Read(BinaryReader gr)
        {
            byte blocksCount = gr.ReadByte();
            var updatemask = new int[blocksCount];

            for (int i = 0; i < updatemask.Length; ++i)
                updatemask[i] = gr.ReadInt32();

            var mask = new BitArray(updatemask);

            var values = new Dictionary<int, uint>();

            for (int i = 0; i < mask.Count; ++i)
                if (mask[i])
                    values[i] = gr.ReadUInt32();

            return new WoWObjectUpdate(values);
        }
    }
}
