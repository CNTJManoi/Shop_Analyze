using Microsoft.ML.Data;

namespace Shoping.Data
{
    public class Price
    {
        [LoadColumn(0)]
        public string Id;

        [LoadColumn(1)]
        public float Income;

        [LoadColumn(2)]
        public float Visitors;
    }
    public class PricePredition
    {
        [ColumnName("Score")]
        public float Income;
    }
}
