using G_coder.Model;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace G_coder.GCodeCreator
{
    public class GCodeCreator : IGCodeCreator
    {
        private Fields _fields;
        private ObservableCollection<string> _gCode;
        private GCodeSettings _settings;

        public ObservableCollection<string> GCode
        {
            get { return _gCode; }
            set { _gCode = value; }
        }

        public void CreateGCode(Fields fields, GCodeSettings settings)
        {
            this._fields = fields;
            this._settings = settings;
            _gCode = Create();
        }


        private ObservableCollection<string> Create()
        {
            var gCode = new ObservableCollection<string>();
            if (_fields[0] != null)
            {
                for (int i = 0; i < _fields.Count; i++)
                {
                    gCode.Add("(Line no. " + (i + 1) + " )");
                    gCode.Add("G0 X" + _fields[i].StartPoint.X + " Y" + _fields[i].StartPoint.Y);
                    gCode.Add("Z" + _settings.WorkingHeightZ);
                    gCode.Add("M13");
                    gCode.Add("M19");
                    if (_fields[i].StartPoint.X == _fields[i].EndPoint.X)
                    {
                        gCode.Add("G1 Y" + _fields[i].EndPoint.Y + " F" + _settings.ForwardSpeed);
                    }
                    if (_fields[i].StartPoint.Y == _fields[i].EndPoint.Y)
                    {
                        gCode.Add("G1 X" + _fields[i].EndPoint.X + " F" + _settings.ForwardSpeed);
                    }
                    if (_fields[i].StartPoint.X != _fields[i].EndPoint.X &&
                        _fields[i].StartPoint.Y != _fields[i].EndPoint.Y)
                    {
                        gCode.Add("G1 X" + _fields[i].EndPoint.X + "G1 Y" + _fields[i].EndPoint.Y + " F" + _settings.ForwardSpeed);
                    }

                    gCode.Add("G0 Z" + _settings.SafeHeightZ);
                    gCode.Add("");
                }
                gCode.Add("G0 X2 Y2 Z20");
                gCode.Add("M16");
                gCode.Add("M17");
                gCode.Add("G0 X50 Y50");
                gCode.Add("M30");
            }
            return gCode;
        }

        public void SaveGCode( string fileName)
        {
            if (GCode == null)
                return;
            var saveFileDialog = new SaveFileDialog { FileName = Path.GetFileNameWithoutExtension(fileName) + ".txt", Filter = "TXT Files|*.txt"};
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var sw = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var line in GCode)
                    {
                        sw.WriteLine(line);
                    }
                    Process.Start(saveFileDialog.FileName);
                }
            }
        }

    }
}