namespace WoWPacketViewer
{
    public static class PacketReaderFactory
    {
        public static IPacketReader Create(string extension)
        {
            switch (extension)
            {
                case ".pkt":
                case ".bin":
                    return new WowCorePacketReader();
                case ".sqlite":
                    return new SqLitePacketReader();
                case ".xml":
                    return new SniffitztPacketReader();
                default:
                    return null;
            }
        }
    }
}
