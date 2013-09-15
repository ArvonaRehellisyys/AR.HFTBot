using System;
using System.Collections.Generic;
using System.Linq;
using AR.Hft.Process.Domain;

namespace AR.Hft.Process.Tests.Tools
{
    public class StockMessageBatch
    {
        private readonly List<StockMessage> _availableMessages;
        private DateTime _batchBegin;

        public StockMessageBatch(List<StockMessage> availableMessages)
        {
            _availableMessages = availableMessages;
            _availableMessages.Sort((a,b) => a.Time.CompareTo(b.Time));
            _batchBegin = _availableMessages.First().Time;
        }

        public List<StockMessage> GetNextBatch(int seconds)
        {
            if(_availableMessages.Last().Time < _batchBegin)
                throw new InvalidOperationException("Fetched more items from batch but theres none left!");

            var batch = _availableMessages.Where(x => x.Time >= _batchBegin && x.Time < _batchBegin.AddSeconds(seconds)).ToList();
            _batchBegin = _batchBegin.AddSeconds(seconds);

            return batch;
        }

        public bool HasMore()
        {
            return _availableMessages.Last().Time > _batchBegin;
        }
    }
}
