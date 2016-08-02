using G_coder.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace G_coder.GCodeCreator
{
    public interface IGCodeCreator
    {
        ObservableCollection<string> GCode { get; set; }

        void CreateGCode(Fields fields, GCodeSettings settings);

        void SaveGCode(string fileName);
    }
}