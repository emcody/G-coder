using G_coder.Model;
using G_coder.Utility;

namespace G_coder.ViewModel
{
    internal class GCodeViewModel
    {
        private Fields _fields;

        public GCodeViewModel()
        {
            Messenger.Default.Register<Fields>(this, OnFieldsReceived);
        }

        private void OnFieldsReceived(Fields fields)
        {
            _fields = fields;
        }
    }
}