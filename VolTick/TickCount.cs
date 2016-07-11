using System.Collections.Generic;
using TSLab.DataSource;
using TSLab.Script;
using TSLab.Script.Handlers;

namespace RusAlgo.Handlers.Public.TickProcessors
{
    // число сделок на покупку/продажу.
    [HandlerCategory("RusAlgo")]
    [HandlerName("TickCount")]
    public class TickCount : IBar2DoubleHandler
    {
        [HandlerParameter(Name = "Направление", NotOptimized = true)]
        public TradeDirection Direction { get; set; }

        public IList<double> Execute(ISecurity security)
        {
            var count = security.Bars.Count;
            var values = new double[count];
            for (var i = 0; i < count; i++)
            {
                var trades = security.GetTrades(i);
                var value = 0;
                for (var k = 0; k < trades.Count; k++)
                    value += trades[k].Direction == Direction ? 1 : 0;

                values[i] = value;
            }
            return values;
        }
    }
}