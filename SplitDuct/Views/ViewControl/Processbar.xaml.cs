using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
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

namespace SplitDuct.Views.ViewControl
{
    /// <summary>
    /// Interaction logic for Processbar.xaml
    /// </summary>
    public partial class Processbar : UserControl
    {
        public Processbar()
        {
            InitializeMaterialDesign();
            InitializeComponent();
        }
        private void InitializeMaterialDesign()
        {
            // Create dummy objects to force the MaterialDesign assemblies to be loaded
            // from this assembly, which causes the MaterialDesign assemblies to be searched
            // relative to this assembly's path. Otherwise, the MaterialDesign assemblies
            // are searched relative to Eclipse's path, so they're not found.
            var card = new Card();
            var hue = new Hue("Dummy", Colors.Black, Colors.White);
        }


        public event EventHandler CloseRequested;

        public event EventHandler MaxRequested;
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
        private void MaxsimizeButton_Click(object sender, RoutedEventArgs e)
        {
            MaxRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
