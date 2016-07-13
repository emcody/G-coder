using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using G_coder.Constructs;
using G_coder.Properties;
using Microsoft.Win32;

namespace G_coder.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _path;
        private readonly Extruder _extruder = new Extruder();
        private ObservableCollection<Field> _fields;
        private Field _selectedField;
        private Canvas _drawingPlace;
        private int _width;
        private int _height;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged(nameof(Path));
            }
        }

        public ObservableCollection<Field> Fields
        {
            get { return _fields; }
            set
            {
                _fields = value;
                OnPropertyChanged(nameof(Fields));
            }
        }

        public Field SelectedField
        {
            get { return _selectedField; }
            set
            {
                _selectedField = value; 
                OnPropertyChanged(nameof(SelectedField));
            }
        }

        public MainWindowViewModel()
        {
            Height = 550;
            Width = 800;
        }

        public ICommand OpenFile
        {
            get { return new RelayCommand(OpenFileExecute, CanOpenFileExecute); }
        }

        public void OpenFileExecute()
        {
            var ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == true)
            {
                Path = ofd.SafeFileName;
                Fields = _extruder.Begin(ofd.FileName);
            }
        }

        private bool CanOpenFileExecute()
        {
            return true;
        }

        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}