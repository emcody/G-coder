using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using G_coder.Constructs;

namespace G_coder
{
    public delegate void OnCreatedLinesHandler(Fields fields);

    internal class Extruder
    {
        private const string XStartMarker = " 10";
        private const string XEndMarker = " 11";    
        private const string YStartMarker = " 20";
        private const string YEndMarker = " 21";
        private const byte LinesToSearch = 13;

        public Fields Fields { get; set; } = new Fields();


        private string[] _dxfContent;


        public OnCreatedLinesHandler CreatedLinesHandler;
        //        public event OnCreatedLinesHandler OnCreatedLines;

        public Extruder()
        {
        }

        public Fields Begin(string pathToFile)
        {
            LoadFile(pathToFile);
            var lines = FindLines();
            lines.SetNearestToCenterAsP0();
            lines.CalculateDistances();
            RaiseCreatedLinesHandler(lines);
            return lines;
        }

        private void LoadFile(string path)
        {
            //TODO add exception when path string is incorrect
            _dxfContent = File.ReadAllLines(path);
            ChangeDotToComma();
        }

        private void ChangeDotToComma()
        {
            for (var i = 0; i < _dxfContent.Length; i++)
            {
                _dxfContent[i] = Convert.ToString(_dxfContent[i].Replace('.', ','));
            }
        }

        private Fields FindLines()
        {
            var lines = new Fields();
            double xStart = 0,
                    yStart = 0,
                    xEnd = 0,
                    yEnd = 0;

            try
            {
                for (var i = 0; i < _dxfContent.Length; i++)
                {
                    if (_dxfContent[i] == "SILIKON" || _dxfContent[i] == "Silikon" || _dxfContent[i] == "silikon")
                    {
                        for (var j = i; j < i + LinesToSearch; j++)
                        {
                            if (_dxfContent[j] == XStartMarker)
                            {
                                xStart = Math.Round(Convert.ToDouble(_dxfContent[j + 1]), 1);
                            }
                            if (_dxfContent[j] == XEndMarker)
                            {
                                xEnd = Math.Round(Convert.ToDouble(_dxfContent[j + 1]), 1);
                            }
                            if (_dxfContent[j] == YStartMarker)
                            {
                                yStart = Math.Round(Convert.ToDouble(_dxfContent[j + 1]), 1);
                            }
                            if (_dxfContent[j] == YEndMarker)
                            {
                                yEnd = Math.Round(Convert.ToDouble(_dxfContent[j + 1]), 1);
                                lines.Add(new Field(xStart, xEnd, yStart, yEnd));
                            }

                        }
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(@"Open file first.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
            }
            return lines;

        }

        public Fields GetRandomLines(int countOfFields, int graphWidth, int graphHeight)
        {
            var randomLines = new Fields();
            const int bufferToFrame = 30;
            //            var numberOfPoints = countOfFields + 6;
            var rand = new Random();

            for (var i = 0; i < countOfFields; i++)
            {
                var startX = rand.Next(0, graphWidth - bufferToFrame);
                var endX = rand.Next(0, graphWidth - bufferToFrame);
                var startY = rand.Next(5, graphHeight - 10);
                var endY = rand.Next(5, graphHeight - 10);
                randomLines.Add(new Field(startX, endX, startY, endY));
            }
            randomLines.SetNearestToCenterAsP0();
            randomLines.CalculateDistances();

            RaiseCreatedLinesHandler(randomLines);

            return randomLines;
        }

        private void RaiseCreatedLinesHandler(Fields randomFields)
        {
            if (CreatedLinesHandler != null)
                CreatedLinesHandler(randomFields);
        }
    }
}
