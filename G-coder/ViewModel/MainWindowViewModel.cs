using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using G_coder.Constructs;
using G_coder.Properties;
using G_coder.Services;
using G_coder.Utility;
using Microsoft.Win32;

namespace G_coder.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly DxfConverter _dxfConverter = new DxfConverter();
        private Canvas _drawingPlace;
        private ObservableCollection<Field> _fields;
        private int _height;
        private string _path;
        private Field _selectedField;
        private int _width;
        private DialogService _dialogService= new DialogService();

        public MainWindowViewModel()
        {
            Height = 550;
            Width = 800;
        }

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

        public ICommand OpenFile
        {
            get { return new RelayCommand(OpenFileExecute, CanOpenFileExecute); }
        }

        public ICommand OpenGCodeView
        {
            get { return new RelayCommand(OpenGcodeViewExecute, CanOpenGCodeExecute); }
        }


        public void OpenFileExecute()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Dxf file (*.dxf)|*.dxf";
            var result = ofd.ShowDialog();
            if (result == true)
            {
                Path = ofd.SafeFileName;
                Fields = _dxfConverter.Begin(ofd.FileName);
            }
        }

        private bool CanOpenFileExecute()
        {
            return true;
        }

        private void OpenGcodeViewExecute()
        {
            Messenger.Default.Send(Fields);
            _dialogService.ShowDialog();
        }

        private bool CanOpenGCodeExecute()
        {
            return Fields != null;
        }
        

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}