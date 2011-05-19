namespace WoWPacketViewer
{
    struct CheckInfo
    {
        public int m_length;
        public CheckType m_type;

        public CheckInfo(CheckType type, int len)
        {
            m_type = type;
            m_length = len; // for MEM_CHECK result
        }
    }
}
