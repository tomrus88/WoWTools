using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WowTools.Core;

namespace UpdatePacketParser
{
    public class WowCorePacketReader : IPacketReader
    {
        private readonly BinaryReader _reader;
        private ushort Version;

        public WowCorePacketReader(string filename)
        {
            _reader = new BinaryReader(new FileStream(filename, FileMode.Open, FileAccess.Read), Encoding.ASCII);
            _reader.ReadBytes(3);                   // PKT
            Version = _reader.ReadUInt16();     // sniff version (0x0201, 0x0202)
            ushort build;
            switch (Version)
            {
                case 0x0201:
                    build = _reader.ReadUInt16();   // build
                    _reader.ReadBytes(40);          // session key
                    break;
                case 0x0202:
                    _reader.ReadByte();             // 0x06
                    build = _reader.ReadUInt16();   // build
                    _reader.ReadBytes(4);           // client locale
                    _reader.ReadBytes(20);          // packet key
                    _reader.ReadBytes(64);          // realm name
                    break;
                case 0x0300:
                    _reader.ReadByte();                  // snifferId
                    build = (ushort)_reader.ReadUInt32();// client build
                    _reader.ReadBytes(4);                // client locale
                    _reader.ReadBytes(40);               // session key
                    var optionalHeaderLength = _reader.ReadInt32();
                    _reader.ReadBytes(optionalHeaderLength);
                    break;
                default:
                    throw new Exception(String.Format("Unknown sniff version {0:X2}", Version));
            }

            UpdateFieldsLoader.LoadUpdateFields(build);
        }

        public Packet ReadPacket()
        {
            if (_reader.PeekChar() < 0)
                return null;

            if (Version != 0x0300)
            {
                var direction = _reader.ReadByte();
                var unixtime = _reader.ReadUInt32();
                var tickcount = _reader.ReadUInt32();

                var packet = new Packet();
                packet.Size = _reader.ReadInt32() - (direction == 0xFF ? 2 : 4);
                packet.Code = (OpCodes)(direction == 0xFF ? _reader.ReadInt16() : _reader.ReadInt32());
                packet.Data = _reader.ReadBytes(packet.Size);
                return packet;
            }
            else
            {
                var direction = _reader.ReadUInt32();
                var unixtime = _reader.ReadUInt32();
                var tickcount = _reader.ReadUInt32();

                var packet = new Packet();
                var optSize = _reader.ReadInt32();
                packet.Size = _reader.ReadInt32() - 4;
                _reader.ReadBytes(optSize);
                packet.Code = (OpCodes)_reader.ReadInt32();
                packet.Data = _reader.ReadBytes(packet.Size);
                return packet;
            }
        }

        public virtual IEnumerable<Packet> ReadPackets()
        {
            var packets = new List<Packet>();
            Packet packet;
            while ((packet = ReadPacket()) != null)
            {
                packets.Add(packet);
            }
            return packets;
        }
    }
}
