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
using MySql.Data.MySqlClient;

namespace VEP.Views
{
    /// <summary>
    /// Interaction logic for addAlarmPage.xaml
    /// </summary>
    public partial class addAlarmPage : UserControl
    {
        public List<string> list = new List<string>();
        
        public class Item
        {
            public String alarmName { get; set; }
            //public String GroupName { get; set; }
        }
        public addAlarmPage()
        {
            InitializeComponent();
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Alarm";
            c1.Binding = new Binding("alarmName");
            c1.Width = 170;
            dataGrid.Columns.Add(c1);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (almName.Text != "")
            {
                dataGrid.Items.Add(new Item() { alarmName = almName.Text });
                list.Add(almName.Text);

            }

        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            foreach (var val in list)
            {
                var alarmName = val;
                var sql = "INSERT INTO `alarmtb`( `AlarmName`) VALUES ('" + alarmName + "')";

                MySqlCommand InsertGroupID = new MySqlCommand(sql, Con);
                
                try
                {
                    InsertGroupID.ExecuteNonQuery();
                    //MessageBox.Show("Data insert successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!"+ ex);
                }
            }

        }

    }
}
