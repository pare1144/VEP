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
using System.Windows.Shapes;
using VEP.ModelViews;

namespace VEP
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            
        }

        private void addGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new addGroupModel();
        }

        private void addAlarmBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new addAlarmModel();
        }

        private void setupBtn_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new setupNotiModel();
        }
    }
}
