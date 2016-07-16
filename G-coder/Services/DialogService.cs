using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using G_coder.View;
using G_coder.ViewModel;

namespace G_coder.Services
{
    class DialogService
    {

        Window gCodeView = null;

        public void ShowDialog()
        {
            gCodeView = new GCodeView();
            gCodeView.ShowDialog();
        }

        public void CloseDialog()
        {
            if(gCodeView != null)
                gCodeView.Close();
        }
    }
}
