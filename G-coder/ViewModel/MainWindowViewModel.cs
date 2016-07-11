using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using G_coder.Annotations;
using Microsoft.Win32;

namespace G_coder.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _path;
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

        public ICommand OpenFile
        {
            get { return new RelayCommand(OpenFileExecute, CanOpenFileExecute); }
        }
        
        public MainWindowViewModel()
        {

        }

        public void OpenFileExecute()
        {
            var ofd = new OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == true)
                Path = ofd.SafeFileName;
        }

        private bool CanOpenFileExecute()
        {
            return true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}