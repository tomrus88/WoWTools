using WowTools.Core;

[Parser(OpCodes.SMSG_CHAR_ENUM)]
class SMSG_CHAR_ENUM : Parser
{
    public override void Parse()
    {
        const int INVENTORY_SLOT_BAG_END = 23;

        For(ReadUInt8("Characters count: {0}"), i =>
            {
                ReadUInt64("Character {0} GUID: {1:X16}", i);
                ReadCString("Character {0} Name: {1}", i);
                ReadUInt8("Character {0} Race: {1}", i);
                ReadUInt8("Character {0} Class: {1}", i);
                ReadUInt8("Character {0} Gender: {1}", i);
                ReadUInt8("Character {0} Skin: {1}", i);
                ReadUInt8("Character {0} Face: {1}", i);
                ReadUInt8("Character {0} Hair Style: {1}", i);
                ReadUInt8("Character {0} Hair Color: {1}", i);
                ReadUInt8("Character {0} Facial Hair: {1}", i);
                ReadUInt8("Character {0} Level: {1}", i);
                ReadUInt32("Character {0} Zone: {1}", i);
                ReadUInt32("Character {0} Map: {1}", i);
                ReadSingle("Character {0} X: {1}", i);
                ReadSingle("Character {0} Y: {1}", i);
                ReadSingle("Character {0} Z: {1}", i);
                ReadUInt32("Character {0} Guild Id: {1}", i);
                ReadUInt32("Character {0} Flags: 0x{1:X8}", i);
                ReadUInt32("Character {0} Customize Flags: 0x{1:X8}", i);
                ReadUInt8("Character {0} First Login?: {1}", i);
                ReadUInt32("Character {0} Pet Display Id: {1}", i);
                ReadUInt32("Character {0} Pet Level: {1}", i);
                ReadUInt32("Character {0} Pet Family: {1}", i);

                For(INVENTORY_SLOT_BAG_END, j =>
                    {
                        ReadUInt32("Character {0} Item {1} Display Id: {2}", i, j);
                        ReadUInt8("Character {0} Item {1} Inventory Type: {2}", i, j);
                        ReadUInt32("Character {0} Item {1} Enchant Aura: {2}", i, j);
                    });
            });
    }
}
