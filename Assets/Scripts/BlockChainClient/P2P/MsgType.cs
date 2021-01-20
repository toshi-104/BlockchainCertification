namespace BlockChainClient.P2P {
    public enum MsgType {
        Add = 0,
        Remove = 1,
        CoreList = 2,
        RequestCoreList = 3,
        Ping = 4,
        AddAsEdge = 5,
        RemoveEdge = 6,
        NewTransaction = 7,
        NewBlock = 8,
        RequestFullChain = 9,
        FullChain = 10,
        Enhanced = 11
    }
}
