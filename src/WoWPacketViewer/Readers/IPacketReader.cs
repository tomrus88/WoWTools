using System.Collections.Generic;
using WowTools.Core;

namespace WoWPacketViewer
{
    public interface IPacketReader
    {
        uint Build { get; }
        IEnumerable<Packet> ReadPackets(string file);
    }
}
