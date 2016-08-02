using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_coder.ViewModel;

namespace G_coder
{
    public class ViewModelLocator
    {
        public static GCodeViewModel GCodeViewModel { get; } = new GCodeViewModel();

        public static MainWindowViewModel MainWindowViewModel { get; } = new MainWindowViewModel();
    }
}
