using System.Collections.Generic;

namespace UpdatePacketParser
{
    public interface IPacketReader
    {
        IEnumerable<Packet> ReadPackets();
    }
}
