namespace WoWPacketViewer
{
    public interface ISupportFind
    {
        void Search(string text, bool searchUp, bool ignoreCase);
    }
}
