using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_coder.Constructs;
using G_coder.Utility;

namespace G_coder.ViewModel
{
    class GCodeViewModel
    {
        private Fields _fields;
        public GCodeViewModel()
        {
            Messenger.Default.Register<Fields>(this,OnFieldsReceived);
        }

        private void OnFieldsReceived(Fields fields)
        {
            this._fields = fields;
        }
    }
}
