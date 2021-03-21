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
    /// Interaction logic for setupNotiPage.xaml
    /// </summary>
    public class Item
    {
        public String AlarmName { get; set; }
        public String GroupName { get; set; }
    }
    public partial class setupNotiPage : UserControl
    {
        public List<Tuple<object>> listAdd = new List<Tuple<object>>();
        public setupNotiPage()
        {
            InitializeComponent();
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "AlarmName";
            c1.Binding = new Binding("AlarmName");
            c1.Width = 170;
            dataGrid.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "GroupName";
            c2.Width = 170;
            c2.Binding = new Binding("GroupName");
            dataGrid.Columns.Add(c2);

            List<string> resultAlarm = new List<string>();
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlAlarm = "SELECT * FROM `alarmtb`";
            var sqlGroup = "SELECT * FROM `grouptb`";
            MySqlCommand QueryAlarm = new MySqlCommand(sqlAlarm, Con);
            MySqlCommand QueryGroup = new MySqlCommand(sqlGroup, Con);
            QueryAlarm.ExecuteNonQuery();
            QueryGroup.ExecuteNonQuery();
            using (MySqlDataReader QueryAlarmReader = QueryAlarm.ExecuteReader())
            {
                int i = 0;
                AlarmcomboBox.DisplayMemberPath = "Text";
                AlarmcomboBox.SelectedValuePath = "Value";
                while (QueryAlarmReader.Read())
                {
                    
                    //AlarmcomboBox.Items.Insert(i,QueryAlarmReader.GetString(1));
                    AlarmcomboBox.Items.Add(new { Text = QueryAlarmReader.GetString(1), Value = QueryAlarmReader.GetString(0) });
                    i++;
                }

                
            }
            using (MySqlDataReader QueryGroupReader = QueryGroup.ExecuteReader())
            {
                int i = 0;
                GroupcomboBox.DisplayMemberPath = "Text";
                GroupcomboBox.SelectedValuePath = "Value";
                while (QueryGroupReader.Read())
                {
                    
                    GroupcomboBox.Items.Add(new { Text =QueryGroupReader.GetString(2),Value= QueryGroupReader.GetString(0) });
                    
                    i++;
                }

            }
            Con.Close();
            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AlarmcomboBox.Text != "" && GroupcomboBox.Text != "")
            {
                dataGrid.Items.Add(new Item() { AlarmName = AlarmcomboBox.Text, GroupName = GroupcomboBox.Text });
                listAdd.Add(Tuple.Create( AlarmcomboBox.SelectedItem));
                listAdd.Add(Tuple.Create(GroupcomboBox.SelectedItem));
            }
        }

        private void setupBtn_Click(object sender, RoutedEventArgs e)
        {
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            foreach (var val in listAdd)
            {
                var groupId = val.Item1.ToString();
                var alarmId = val.Item1;

            }
        }
    }
}
