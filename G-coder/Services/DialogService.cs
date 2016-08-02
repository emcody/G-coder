using System.Windows;
using G_coder.Model;
using G_coder.View;

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
