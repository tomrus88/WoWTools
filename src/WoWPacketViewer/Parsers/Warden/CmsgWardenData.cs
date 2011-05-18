using System.IO;
using System.Text;
using WowTools.Core;

namespace WoWPacketViewer.Parsers.Warden
{
    [Parser(OpCodes.CMSG_WARDEN_DATA)]
    class CmsgWardenData : Parser
    {
        public override void Parse()
        {
            byte wardenOpcode = Reader.ReadByte();
            //AppendFormatLine("C->S Warden Opcode: {0:X2}", wardenOpcode);

            switch (wardenOpcode)
            {
                case 0x00:
                    AppendFormatLine("Load module failed or module is missing...");
                    AppendLine();
                    break;
                case 0x01:
                    AppendFormatLine("Module loaded successfully...");
                    AppendLine();
                    break;
                case 0x02:
                    Parse_CHEAT_CHECKS_RESULTS();
                    break;
                case 0x04:
                    byte[] hash = Reader.ReadBytes(20); // SHA1 hash of tranformed seed
                    AppendFormatLine("SHA1: 0x{0}", hash.ToHexString());
                    AppendLine();
                    break;
                default:
                    AppendFormatLine("Unknown warden opcode {0}", wardenOpcode);
                    AppendLine();
                    break;
            }
        }

        private void Parse_CHEAT_CHECKS_RESULTS()
        {
            var bufLen = Reader.ReadUInt16();
            var checkSum = Reader.ReadUInt32();
            var result = Reader.ReadBytes(bufLen);
            //AppendFormatLine("Cheat check result:");
            //AppendFormatLine("Len: {0}", bufLen);
            //AppendFormatLine("Checksum: 0x{0:X8} {1}", checkSum, WardenData.ValidateCheckSum(checkSum, result));
            var reader = new BinaryReader(new MemoryStream(result), Encoding.ASCII);
            //AppendFormatLine("====== CHEAT CHECKS RESULTS START ======");
            //AppendLine();
            foreach (var check in WardenData.CheckInfos)
            {
                switch (check.m_type)
                {
                    case CheckType.MEM_CHECK:
                        Parse_MEM_CHECK_RESULT(reader, check);
                        break;
                    case CheckType.PAGE_CHECK_A:
                    case CheckType.PAGE_CHECK_B:
                        Parse_PAGE_CHECK_RESULT(reader);
                        break;
                    case CheckType.MPQ_CHECK:
                        Parse_MPQ_CHECK_RESULT(reader);
                        break;
                    case CheckType.LUA_STR_CHECK:
                        Parse_LUA_STR_CHECK_RESULT(reader);
                        break;
                    case CheckType.DRIVER_CHECK:
                        Parse_DRIVER_CHECK_RESULT(reader);
                        break;
                    case CheckType.TIMING_CHECK:
                        Parse_TIMING_CHECK_RESULT(reader);
                        break;
                    case CheckType.PROC_CHECK:
                        Parse_PROC_CHECK_RESULT(reader);
                        break;
                    default:
                        break;
                }
            }
            //AppendFormatLine("====== CHEAT CHECKS RESULTS END ======");

            WardenData.CheckInfos.Clear();

            if (reader.BaseStream.Position != reader.BaseStream.Length)
                AppendFormatLine("Packet under read!");

            AppendLine();
        }

        private void Parse_PROC_CHECK_RESULT(BinaryReader reader)
        {
            var res = reader.ReadByte();
            //AppendFormatLine("====== PROC_CHECK result START ======");
            //AppendFormatLine("Result: 0x{0:X2}", res);
            //AppendFormatLine("====== PROC_CHECK result END ======");
            //AppendLine();
        }

        private void Parse_TIMING_CHECK_RESULT(BinaryReader reader)
        {
            var res = reader.ReadByte();
            var unk = reader.ReadInt32();
            //AppendFormatLine("====== TIMING_CHECK result START ======");
            //AppendFormatLine("Result: 0x{0:X2}", res);
            //AppendFormatLine("Ticks: 0x{0:X8}", unk);
            //AppendFormatLine("====== TIMING_CHECK result END ======");
            //AppendLine();
        }

        private void Parse_DRIVER_CHECK_RESULT(BinaryReader reader)
        {
            var res = reader.ReadByte();
            //AppendFormatLine("====== DRIVER_CHECK result START ======");
            //AppendFormatLine("Result: 0x{0:X2}", res);
            //AppendFormatLine("====== DRIVER_CHECK result END ======");
            //AppendLine();
        }

        private void Parse_LUA_STR_CHECK_RESULT(BinaryReader reader)
        {
            var unk = reader.ReadByte();

            //AppendFormatLine("====== LUA_STR_CHECK result START ======");
            //AppendFormatLine("Result: 0x{0:X2}", unk);
            if(unk == 0)
            {
                var len = reader.ReadByte();
            //    AppendFormatLine("Len: {0}", len);
                if (len > 0)
                {
                    var data = reader.ReadBytes(len);
            //        AppendFormatLine("Data: 0x{0}", Utility.ByteArrayToHexString(data));
                }
            }

            //AppendFormatLine("====== LUA_STR_CHECK result END ======");
            //AppendLine();
        }

        private void Parse_MPQ_CHECK_RESULT(BinaryReader reader)
        {
            var res = reader.ReadByte();

            //AppendFormatLine("====== MPQ_CHECK result START ======");
            //AppendFormatLine("Result: 0x{0:X2}", res);
            if(res == 0)
            {
                var sha1 = reader.ReadBytes(20);
                AppendFormatLine("MPQ SHA1: {0}", sha1.ToHexString());
            }

            //AppendFormatLine("====== MPQ_CHECK result END ======");
            //AppendLine();
        }

        private void Parse_PAGE_CHECK_RESULT(BinaryReader reader)
        {
            var res = reader.ReadByte();
            //AppendFormatLine("====== PAGE_CHECK_A_B result START ======");
            //AppendFormatLine("Result: 0x{0:X2}", res);
            //AppendFormatLine("====== PAGE_CHECK_A_B result END ======");
            //AppendLine();
        }

        private void Parse_MEM_CHECK_RESULT(BinaryReader reader, CheckInfo check)
        {
            var res = reader.ReadByte();
            //AppendFormatLine("====== MEM_CHECK result START ======");
            //AppendFormatLine("Result: 0x{0:X2}", res);
            if (res == 0)
            {
                var bytes = reader.ReadBytes(check.m_length);
                AppendFormatLine("MEM Bytes: {0}", bytes.ToHexString());
            }
            //AppendFormatLine("====== MEM_CHECK result END ======");
            //AppendLine();
        }
    }
}
