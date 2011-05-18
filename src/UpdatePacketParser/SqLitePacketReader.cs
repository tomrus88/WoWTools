using System;
using System.Collections.Generic;
using System.Data.Common;
using WowTools.Core;

namespace UpdatePacketParser
{
    public class SqLitePacketReader : IPacketReader
    {
        private static readonly DbProviderFactory factory = System.Data.SQLite.SQLiteFactory.Instance;
        private readonly DbDataReader _reader;

        public SqLitePacketReader(string filename)
        {
            var connection = factory.CreateConnection();
            connection.ConnectionString = "Data Source=" + filename;
            connection.Open();

            //TODO: Добавить определение билда!
            var command = connection.CreateCommand();
            command.CommandText = "SELECT opcode, data FROM packets WHERE opcode=169 OR opcode=502 ORDER BY id;";
            command.Prepare();

            _reader = command.ExecuteReader();

            //TODO: Добавить определение билда!
            UpdateFieldsLoader.LoadUpdateFields(10026);
        }

        public Packet ReadPacket()
        {
            if (!_reader.Read())
            {
                _reader.Close();
                return null;
            }

            var packet = new Packet();
            packet.Code = (OpCodes)_reader.GetInt32(0);
            packet.Data = (byte[])_reader.GetValue(1);
            packet.Size = packet.Data.Length;
            return packet;
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
