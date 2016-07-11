namespace G_coder.Constructs
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double xCoordinate, double yCoordinate )
        {
            X = xCoordinate;
            Y = yCoordinate;
        }
    }
}