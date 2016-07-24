using System;
using System.Collections.ObjectModel;

namespace G_coder.Model
{
    public class Fields : ObservableCollection<Field>
    {
        public Fields()
        {

        }
        
        public void CalculateDistances()
        {
            foreach (var line in this)
            {
                line.Disances.Clear();
            }

            foreach (var line in this)
            {
                foreach (var line1 in this)
                {
                    if (line == line1)
                        line.Disances.Add(-1);
                    else
                    {
                        var a = Math.Abs(line.StartPoint.X - line1.StartPoint.X);
                        var b = Math.Abs(line.StartPoint.Y - line1.StartPoint.Y);
                        line.Disances.Add(Math.Round(Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)), 1));
                    }
                }
            }
        }

        public void SetNearestToCenterAsP0()
        {
            double closestDistance = int.MaxValue;
            var closestPoint = 0;
            for (var i = 0; i < this.Count; i++)
            {
                var distance =
                    Math.Round(Math.Sqrt(Math.Pow(this[i].StartPoint.X, 2) + Math.Pow(this[i].StartPoint.Y, 2)),
                        1);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = i;
                }
            }
            if (closestPoint != 0)
                Swap(0, closestPoint);
        }

        private void Swap(int firstPoint, int secondPoint)
        {
            var tmpX0 = this[secondPoint].StartPoint.X;
            var tmpY0 = this[secondPoint].StartPoint.Y;

            this[secondPoint].StartPoint.X = this[firstPoint].StartPoint.X;
            this[secondPoint].StartPoint.Y = this[firstPoint].StartPoint.Y;
            this[firstPoint].StartPoint.X = tmpX0;
            this[firstPoint].StartPoint.Y = tmpY0;
        }

    }
}