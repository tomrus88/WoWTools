using System.Collections.Generic;
using System.Data.SQLite;
using WowTools.Core;

namespace WoWPacketViewer
{
    public class SqLitePacketReader : IPacketReader
    {
        public uint Build
        {
            get { return 0; }
        }

        public IEnumerable<Packet> ReadPackets(string file)
        {
            using (var connection = new SQLiteConnection("Data Source=" + file))
            {
                var packets = new List<Packet>();

                connection.Open();

                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM packets;";
                command.Prepare();

                var rows = (long)command.ExecuteScalar();

                command.CommandText = "SELECT direction, opcode, data FROM packets ORDER BY id;";
                command.Prepare();

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //worker.ReportProgress((int)((float)m_packets.Count / (float)rows * 100.0f));
                    try
                    {
                        var direction = (Direction)reader.GetByte(0);
                        var opcode = (OpCodes)reader.GetInt16(1);
                        var data = (byte[])reader.GetValue(2);

                        packets.Add(new Packet(direction, opcode, data, 0, 0));
                    }
                    catch
                    {
                    }
                }
                return packets;
            }
        }
    }
}
