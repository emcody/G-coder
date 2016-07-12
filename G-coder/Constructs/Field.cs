﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using G_coder.Annotations;

namespace G_coder.Constructs
{
    public class Field : INotifyPropertyChanged
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
                OnPropertyChanged(nameof(Disances));
            }
        }

        public double Length => Math.Round(Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2)),1);

        public Field()
        {
            
        }
        public Field(double xStart, double xEnd, double yStart, double yEnd)
        {
            StartPoint = new Point(xStart, yStart);
            EndPoint = new Point(xEnd, yEnd);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}