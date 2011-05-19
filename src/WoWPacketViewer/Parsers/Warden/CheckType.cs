namespace WoWPacketViewer
{
    enum CheckType
    {
        MEM_CHECK = 0,      // byte strIndex + uint Offset + byte Len (checks to ensure memory isn't modified?)
        PAGE_CHECK_A = 1,   // uint Seed + byte[20] SHA1 + uint Addr + byte Len
        PAGE_CHECK_B = 2,   // uint Seed + byte[20] SHA1 + uint Addr + byte Len
        MPQ_CHECK = 3,      // byte strIndex (checks to ensure MPQ file isn't modified?)
        LUA_STR_CHECK = 4,  // byte strIndex (checks to ensure global LUA string isn't used?)
        DRIVER_CHECK = 5,   // uint Seed + byte[20] SHA1 + byte strIndex (checks to ensure driver isn't loaded?)
        TIMING_CHECK = 6,   // empty (checks to ensure TickCount isn't detoured?)
        PROC_CHECK = 7,     // uint Seed + byte[20] SHA1 + byte strIndex1 + byte strIndex2 + uint Offset + byte Len (checks to ensure proc isn't detoured?)
        MODULE_CHECK = 8    // uint Seed + byte[20] SHA1 (checks to ensure module isn't loaded)
    }
}
