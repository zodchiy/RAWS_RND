using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAWS.Web.Models
{
    public class SymbolBookTicket
    {
        [JsonProperty("u")]
        public long OrderBookUpdateId { get; set; }
        [JsonProperty("s")]
        public string Symbol { get; set; }
        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }
        [JsonProperty("B")]
        public decimal BestBidQty { get; set; }
        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }
        [JsonProperty("A")]
        public decimal BestAskQty { get; set; }
        public override string ToString()
        {
            return $"OrderBookUpdateId: {OrderBookUpdateId}, Symbol : {Symbol}, BestBidPrice : {BestBidPrice}, BestBidQty : {BestBidQty}, BestAskPrice: {BestAskPrice}, BestAskQty:{BestAskQty}";
        }
    }
}
