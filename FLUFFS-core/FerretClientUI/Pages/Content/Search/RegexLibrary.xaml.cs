using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerretClientUI.Pages.Content.Search
{
    /// <summary>
    /// Interaction logic for RegexLibrary.xaml
    /// </summary>
    public partial class RegexLibrary : UserControl
    {
        public RegexLibrary()
        {
            InitializeComponent();
            this.DataContext = new RegexLibraryViewModel();
        }
    }
}
