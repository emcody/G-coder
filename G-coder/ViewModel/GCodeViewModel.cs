using G_coder.Annotations;
using G_coder.GCodeCreator;
using G_coder.Messages;
using G_coder.Model;
using G_coder.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace G_coder.ViewModel
{
    public class GCodeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Fields _fields;
        private IGCodeCreator _gCodeCreator = new GCodeCreator.GCodeCreator();
        private GCodeSettings _gCodeSettings = new GCodeSettings();
        private ObservableCollection<string> _gCode;
        private string _fileName;

        public ObservableCollection<string> GCode
        {
            get { return _gCode; }
            set
            {
                _gCode = value;
                OnPropertyChanged(nameof(GCode));
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

        public GCodeSettings GCodeSettings
        {
            get { return _gCodeSettings; }
            set
            {
                _gCodeSettings = value;
                OnPropertyChanged(nameof(GCodeSettings));
            }
        }

        public GCodeViewModel()
        {
            Messenger.Default.Register<MainWindowMessage>(this, OnMainWindowMessageReceived);
        }

        private void OnMainWindowMessageReceived(MainWindowMessage message)
        {
            Fields = message.Fields;
            _fileName = message.FileName;
        }

        private void OnFieldsReceived(Fields fields)
        {
            Fields = fields;
        }

        public ICommand CreateGCode
        {
            get { return new RelayCommand(CreateGCodeExecute, CanCreateGCodeExecute); }
        }

        private void CreateGCodeExecute()
        {
            _gCodeCreator.CreateGCode(Fields, GCodeSettings);
            GCode = _gCodeCreator.GCode;
        }

        private bool CanCreateGCodeExecute()
        {
            return true;
        }

        public ICommand SaveFile
        {
            get { return new RelayCommand(SaveFileExecute, CanSaveFileExecute); }
        }

        private void SaveFileExecute()
        {
            _gCodeCreator.SaveGCode(_fileName);
        }
        private bool CanSaveFileExecute()
        {
            return GCode != null;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}