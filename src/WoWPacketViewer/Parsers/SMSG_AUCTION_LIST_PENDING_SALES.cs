using WowTools.Core;

namespace WoWPacketViewer.Parsers
{
    [Parser(OpCodes.SMSG_AUCTION_LIST_PENDING_SALES)]
    class AuctionListPendngSales : Parser
    {
        public override void Parse()
        {
            For(Reader.ReadInt32(), i =>
                {
                    ReadCString("Sale {0}: string {1}", i);
                    ReadCString("Sale {0}: string {1}", i);
                    ReadUInt32("Sale {0}: uint {1}", i);
                    ReadUInt32("Sale {0}: uint {1}", i);
                    ReadSingle("Sale {0}: float {1}", i);
                });
        }
    }
}
