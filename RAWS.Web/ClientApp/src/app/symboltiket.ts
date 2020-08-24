export interface Symboltiket {
    OrderBookUpdateId:  bigint;
    Symbol: string;
    BestBidPrice: number;
    BestBidQty: number;
    BestAskPrice: number;
    BestAskQty: number;
}
