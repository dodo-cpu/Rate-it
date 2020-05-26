using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Rateit.Events
{
    static class AggregatorProvider
    {
        // The event aggregator
        public static EventAggregator Aggregator = new EventAggregator();
    }
}
