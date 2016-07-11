using System.Data;

namespace G_coder.Constructs
{
    public class Result
    {
        public DataTable Path { get; private set; }
        public DataTable PathCost { get; private set; }

        public Result(int linesCount)
        {
            Path = new DataTable();
            PathCost = new DataTable();
            
            for (var i = 0; i < linesCount; i++)
            {
                Path.Columns.Add(i.ToString(), typeof(byte));
            }

            PathCost.Columns.Add("cost", typeof(short));
        }

        public void AddTour(double tourCost, int [] tour)
        {
            PathCost.Rows.Add(tourCost);

            var workRow = Path.NewRow();
            for (var signNumber = 0; signNumber < tour.Length - 1; signNumber++)
            {
                workRow[signNumber] = tour[signNumber + 1];
            }
            Path.Rows.Add(workRow);
        }

    }
}