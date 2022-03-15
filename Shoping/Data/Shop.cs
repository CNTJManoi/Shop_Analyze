using Microsoft.ML.Data;

namespace Shoping.Data
{
    public class Shop
    {
        [LoadColumn(0)]
        public float profit;

        [LoadColumn(1)]
        public float visitors;

        [LoadColumn(2)]
        public float expenses;

        [LoadColumn(3)]
        public float staff;
    }
    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

        [ColumnName("Score")]
        public float[] Distances;
    }
}
