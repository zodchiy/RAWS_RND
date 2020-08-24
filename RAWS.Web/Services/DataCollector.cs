using RAWS.Web.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RAWS.Web.Services
{
    public class DataCollector
    {
        private ConcurrentDictionary<string, SymbolBookTicket> _dict;
        public DataCollector()
        {
            _dict = new ConcurrentDictionary<string, SymbolBookTicket>();
        }

        public void Add(SymbolBookTicket tiket)
        {
            _ = _dict.AddOrUpdate(tiket.Symbol, tiket, (key, existingVal) => { return existingVal; });
        }
        public List<SymbolBookTicket> Get()
        {
           return _dict.Select(x => x.Value).ToList();
        }
    }
}
