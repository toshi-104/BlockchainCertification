namespace BlockChainClient.P2P {
    public enum ReasonType {
        ErrProtocolUnmatch = 0,
        ErrVersionUnmatch = 1,
        OkWithPayload = 2,
        OkWithoutPayload = 3
    }
}
