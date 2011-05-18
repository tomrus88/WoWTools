using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WowTools.Core;

namespace WoWPacketViewer.Parsers.Warden
{
    static class WardenData
    {
        public static IList<CheckInfo> CheckInfos = new List<CheckInfo>();
        public static IDictionary<byte, CheckType> CheckTypes = new Dictionary<byte, CheckType>();
        private static FrmWardenDebug wardenDebugForm;

        public static void InitCheckTypes()
        {
            if (wardenDebugForm == null || wardenDebugForm.IsDisposed)
                return;
            CheckTypes = wardenDebugForm.CheckTypes;
        }

        public static void ShowForm(IEnumerable<string> strings, byte[] checks, byte checkByte, long position)
        {
            if (wardenDebugForm == null || wardenDebugForm.IsDisposed)
                wardenDebugForm = new FrmWardenDebug();

            wardenDebugForm.XorByte = checks[checks.Length - 1];

            wardenDebugForm.Text = String.Format("Warden Debug: 0x{0:X2}", checkByte);

            wardenDebugForm.SetInfo(CreateTextInfo(strings, checks), position);

            wardenDebugForm.CheckTypes = CheckTypes;

            if (!wardenDebugForm.Visible)
                wardenDebugForm.Show();
        }

        private static string CreateTextInfo(IEnumerable<string> strings, byte[] checks)
        {
            var sb = new StringBuilder();
            sb.Append(checks.HexLike(0, checks.Length));
            foreach (string s in strings)
                sb.AppendLine(s);
            return sb.ToString();
        }

        public static string ValidateCheckSum(uint checkSum, byte[] data)
        {
            var hash = new SHA1CryptoServiceProvider().ComputeHash(data);
            var res = new uint[5];

            for (var i = 0; i < 5; ++i)
                res[i] = BitConverter.ToUInt32(hash, 4 * i);

            var newCheckSum = res[0] ^ res[1] ^ res[2] ^ res[3] ^ res[4];

            if (checkSum != newCheckSum)
                return "is not valid!";
            else
                return "is valid!";
        }
    }
}
