using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WowTools.Core;

namespace WoWPacketViewer
{
    [Parser(OpCodes.SMSG_WARDEN_DATA)]
    class SmsgWardenData : Parser
    {
        public override void Parse()
        {
            WardenData.CheckInfos.Clear();

            var wardenOpcode = Reader.ReadByte();
            Reader.BaseStream.Position = 0;
            //AppendFormatLine("S->C Warden Opcode: {0:X2}", wardenOpcode);

            switch (wardenOpcode)
            {
                case 0x00:
                    {
                        var opcode = Reader.ReadByte();
                        var md5 = Reader.ReadBytes(16); // md5
                        var rc4 = Reader.ReadBytes(16); // rc4 key
                        var len = Reader.ReadInt32();   // len
                        AppendFormatLine("MD5: 0x{0}", md5.ToHexString());
                        AppendFormatLine("RC4: 0x{0}", rc4.ToHexString());
                        AppendFormatLine("Len: {0}", len);
                        AppendLine();
                    }
                    break;
                case 0x01:
                    {
                        var opcode = Reader.ReadByte();
                        var len = Reader.ReadInt16();
                        var chunk = Reader.ReadBytes(len);
                        AppendFormatLine("Received warden module chunk, len {0}", len);
                        AppendLine();
                    }
                    break;
                case 0x02:
                    Parse_CHEAT_CHECKS();
                    break;
                case 0x03:
                    {
                        while (Reader.BaseStream.Position != Reader.BaseStream.Length)
                        {
                            var opcode = Reader.ReadByte();
                            var len = Reader.ReadInt16();
                            var checkSum = Reader.ReadUInt32();
                            var data = Reader.ReadBytes(len);

                            AppendFormatLine("Len: {0}", len);
                            AppendFormatLine("Checksum: 0x{0:X8} {1}", checkSum, WardenData.ValidateCheckSum(checkSum, data));
                            AppendFormatLine("Data: 0x{0}", data.ToHexString());
                            AppendLine();
                        }
                    }
                    break;
                case 0x05:
                    {
                        var opcode = Reader.ReadByte();
                        var seed = Reader.ReadBytes(16);
                        AppendFormatLine("Seed: 0x{0}", seed.ToHexString());
                        AppendLine();
                    }
                    break;
                default:
                    AppendFormatLine("Unknown warden opcode {0}", wardenOpcode);
                    break;
            }
        }

        private void Parse_CHEAT_CHECKS()
        {
            //AppendFormatLine("====== CHEAT CHECKS START ======");
            //AppendLine();

            List<string> strings = new List<string>();
            strings.Add("");

            var opcode = Reader.ReadByte();

            byte len;
            while ((len = Reader.ReadByte()) != 0)
            {
                var strBytes = Reader.ReadBytes(len);
                var str = Encoding.ASCII.GetString(strBytes);
                strings.Add(str);
            }
            var rest = Reader.BaseStream.Length - Reader.BaseStream.Position;
            var checks = Reader.ReadBytes((int)rest);
            var reader = new BinaryReader(new MemoryStream(checks), Encoding.ASCII);

            while (reader.BaseStream.Position + 1 != reader.BaseStream.Length)
            {
                var xor = checks[checks.Length - 1];

                var check = reader.ReadByte();
                var checkType = (byte)(check ^ xor);

                CheckType checkType2;
                if (!WardenData.CheckTypes.TryGetValue(checkType, out checkType2))
                {
                    WardenData.ShowForm(strings, checks, check, reader.BaseStream.Position);
                    break;
                }

                switch (checkType2)
                {
                    case CheckType.TIMING_CHECK:
                        Parse_TIMING_CHECK(check, checkType);
                        break;
                    case CheckType.MEM_CHECK:
                        Parse_MEM_CHECK(strings, reader, check, checkType);
                        break;
                    case CheckType.PAGE_CHECK_A:
                    case CheckType.PAGE_CHECK_B:
                        Parse_PAGE_CHECK(reader, check, checkType);
                        break;
                    case CheckType.PROC_CHECK:
                        Parse_PROC_CHECK(strings, reader, check, checkType);
                        break;
                    case CheckType.MPQ_CHECK:
                        Parse_MPQ_CHECK(strings, reader, check, checkType);
                        break;
                    case CheckType.LUA_STR_CHECK:
                        Parse_LUA_STR_CHECK(strings, reader, check, checkType);
                        break;
                    case CheckType.DRIVER_CHECK:
                        Parse_DRIVER_CHECK(strings, reader, check, checkType);
                        break;
                    default:
                        AppendFormatLine("Unknown CheckType {0}", checkType2);
                        break;
                }
            }
            //AppendFormatLine("====== CHEAT CHECKS END ======");
            //AppendLine();
        }

        private void Parse_DRIVER_CHECK(IList<string> strings, BinaryReader reader, byte check, byte checkType)
        {
            var seed = reader.ReadUInt32();
            var sha1 = reader.ReadBytes(20);
            var stringIndex = reader.ReadByte();
            //AppendFormatLine("====== DRIVER_CHECK START ======");
            //AppendFormatLine("CheckType {0:X2} ({1:X2})", checkType, check);
            //AppendFormatLine("Seed: 0x{0:X8}", seed);
            //AppendFormatLine("SHA1: 0x{0}", Utility.ByteArrayToHexString(sha1));
            //AppendFormatLine("Driver: {0}", strings[stringIndex]);
            //AppendFormatLine("====== DRIVER_CHECK END ======");
            //AppendLine();

            AppendFormatLine("INSERT INTO warden_data_result (`check`,`data`,`address`,`length`,`str`,`result`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", checkType, BitConverter.GetBytes(seed).ToHexString() + sha1.ToHexString(), "", "", strings[stringIndex], "");

            WardenData.CheckInfos.Add(new CheckInfo(WardenData.CheckTypes[checkType], 0));
        }

        private void Parse_LUA_STR_CHECK(IList<string> strings, BinaryReader reader, byte check, byte checkType)
        {
            var stringIndex = reader.ReadByte();
            //AppendFormatLine("====== LUA_STR_CHECK START ======");
            //AppendFormatLine("CheckType {0:X2} ({1:X2})", checkType, check);
            //AppendFormatLine("String: {0}", strings[stringIndex]);
            //AppendFormatLine("====== LUA_STR_CHECK END ======");
            //AppendLine();

            AppendFormatLine("INSERT INTO warden_data_result (`check`,`data`,`address`,`length`,`str`,`result`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", checkType, "", "", "", strings[stringIndex], "");

            WardenData.CheckInfos.Add(new CheckInfo(WardenData.CheckTypes[checkType], 0));
        }

        private void Parse_MPQ_CHECK(IList<string> strings, BinaryReader reader, byte check, byte checkType)
        {
            var fileNameIndex = reader.ReadByte();
            //AppendFormatLine("====== MPQ_CHECK START ======");
            //AppendFormatLine("CheckType {0:X2} ({1:X2})", checkType, check);
            //AppendFormatLine("File: {0}", strings[fileNameIndex]);
            //AppendFormatLine("====== MPQ_CHECK END ======");
            //AppendLine();

            AppendFormatLine("INSERT INTO warden_data_result (`check`,`data`,`address`,`length`,`str`,`result`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", checkType, "", "", "", strings[fileNameIndex], "");

            WardenData.CheckInfos.Add(new CheckInfo(WardenData.CheckTypes[checkType], 0));
        }

        private void Parse_PROC_CHECK(IList<string> strings, BinaryReader reader, byte check, byte checkType)
        {
            var seed = reader.ReadUInt32();
            var sha1 = reader.ReadBytes(20);
            var module = reader.ReadByte();
            var proc = reader.ReadByte();
            var addr = reader.ReadUInt32();
            var bytesToRead = reader.ReadByte();
            //AppendFormatLine("====== PROC_CHECK START ======");
            //AppendFormatLine("CheckType {0:X2} ({1:X2})", checkType, check);
            //AppendFormatLine("Seed: 0x{0:X8}", seed);
            //AppendFormatLine("SHA1: 0x{0}", Utility.ByteArrayToHexString(sha1));
            //AppendFormatLine("Module: {0}", strings[module]);
            //AppendFormatLine("Proc: {0}", strings[proc]);
            //AppendFormatLine("Address: 0x{0:X8}", addr);
            //AppendFormatLine("Bytes to read: {0}", bytesToRead);
            //AppendFormatLine("====== PROC_CHECK END ======");
            //AppendLine();
            WardenData.CheckInfos.Add(new CheckInfo(WardenData.CheckTypes[checkType], 0));
        }

        private void Parse_PAGE_CHECK(BinaryReader reader, byte check, byte checkType)
        {
            var seed = reader.ReadUInt32();
            var sha1 = reader.ReadBytes(20);
            var addr = reader.ReadUInt32();
            var bytesToRead = reader.ReadByte();
            //AppendFormatLine("====== PAGE_CHECK_A_B START ======");
            //AppendFormatLine("CheckType {0:X2} ({1:X2})", checkType, check);
            //AppendFormatLine("Seed: 0x{0:X8}", seed);
            //AppendFormatLine("SHA1: 0x{0}", Utility.ByteArrayToHexString(sha1));
            //AppendFormatLine("Address: 0x{0:X8}", addr);
            //AppendFormatLine("Bytes to read: {0}", bytesToRead);
            //AppendFormatLine("====== PAGE_CHECK_A_B END ======");
            //AppendLine();

            AppendFormatLine("INSERT INTO warden_data_result (`check`,`data`,`address`,`length`,`str`,`result`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", checkType, BitConverter.GetBytes(seed).ToHexString() + sha1.ToHexString(), addr, bytesToRead, "", "");

            WardenData.CheckInfos.Add(new CheckInfo(WardenData.CheckTypes[checkType], 0));
        }

        private void Parse_MEM_CHECK(IList<string> strings, BinaryReader reader, byte check, byte checkType)
        {
            var strIndex = reader.ReadByte(); // string index
            var offset = reader.ReadUInt32(); // offset
            var readLen = reader.ReadByte();  // bytes to read
            //AppendFormatLine("====== MEM_CHECK START ======");
            //AppendFormatLine("CheckType {0:X2} ({1:X2})", checkType, check);
            //AppendFormatLine("Module: {0}", (strings[strIndex] == "") ? "base" : strings[strIndex]);
            //AppendFormatLine("Offset: 0x{0:X8}", offset);
            //AppendFormatLine("Bytes to read: {0}", readLen);
            //AppendFormatLine("====== MEM_CHECK END ======");
            //AppendLine();

            AppendFormatLine("INSERT INTO warden_data_result (`check`,`data`,`address`,`length`,`str`,`result`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", checkType, "", offset, readLen, strings[strIndex], "");

            WardenData.CheckInfos.Add(new CheckInfo(WardenData.CheckTypes[checkType], readLen));
        }

        private void Parse_TIMING_CHECK(byte check, byte checkType)
        {
            //AppendFormatLine("====== TIMING_CHECK START ======");
            //AppendFormatLine("CheckType {0:X2} ({1:X2})", checkType, check);
            //AppendFormatLine("====== TIMING_CHECK END ======");
            //AppendLine();
            WardenData.CheckInfos.Add(new CheckInfo(WardenData.CheckTypes[checkType], 0));
        }
    }
}
