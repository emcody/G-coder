using G_coder.DxfConverter;
using G_coder.Messages;
using G_coder.Model;
using G_coder.Properties;
using G_coder.Services;
using G_coder.Utility;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace G_coder.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly DialogService _dialogService = new DialogService();
        private readonly IDxfConverter _dxfConverter = new DxfConverter.DxfConverter();
        private Fields _fields;
        private int _height;
        private string _fileName;
        private Field _selectedField;
        private int _width;

        public MainWindowViewModel()
        {
            Height = 550;
            Width = 800;
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public Fields Fields
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OpenFileExecute()
        {
            var ofd = new OpenFileDialog { Filter = "Dxf file (*.dxf)|*.dxf" };
            var result = ofd.ShowDialog();
            if (result == true)
            {
                FileName = ofd.SafeFileName;

                _dxfConverter.ConvertToFields(ofd.FileName);
                Fields = _dxfConverter.GetFields();
            }
        }

        private bool CanOpenFileExecute()
        {
            return true;
        }

        private void OpenGcodeViewExecute()
        {
            Messenger.Default.Send<MainWindowMessage>(new MainWindowMessage(FileName, Fields));
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