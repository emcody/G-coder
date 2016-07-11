using System;
using System.Collections.Generic;

namespace G_coder.Constructs
{
    public class Line
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        private List<double> _disances = new List<double>();
        public List<double> Disances
        {
            get { return _disances; }
            set { _disances = value; }
        }

        public double Length => Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));

        public Line()
        {
            
        }
        public Line(double xStart, double xEnd, double yStart, double yEnd)
        {
            StartPoint = new Point(xStart, yStart);
            EndPoint = new Point(xEnd, yEnd);
        }
    }
}
