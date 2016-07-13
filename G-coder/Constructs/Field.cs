using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using G_coder.Annotations;

namespace G_coder.Constructs
{
    public class Field 
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        private List<double> _disances = new List<double>();

        public List<double> Disances
        {
            get { return _disances; }
            set
            {
                _disances = value;
            }
        }

        public int Id { get; set; }

        public double Length => Math.Round(Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2)),1);

        public Field()
        {
            
        }

        public Field(int fieldId, double xStart, double xEnd, double yStart, double yEnd)
        {
            this.Id = fieldId;
            StartPoint = new Point(xStart, yStart);
            EndPoint = new Point(xEnd, yEnd);
        }

    }
}
