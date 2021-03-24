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
using System.Data;
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
            public String alarmID { get; set; }
        }
        public addAlarmPage()
        {
            InitializeComponent();
            deleteBtn.IsEnabled = false;
            editBtn.IsEnabled = false;

            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Alarm";
            c1.Binding = new Binding("alarmName");
            c1.Width = 300;
            dataGrid.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "AlarmID";
            c2.Binding = new Binding("alarmID");
            c2.MaxWidth = 0;
            dataGrid.Columns.Add(c2);

            List<string> resultAlarm = new List<string>();
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlAlarm = "SELECT * FROM `alarmtb`";
            MySqlCommand QueryAlarm = new MySqlCommand(sqlAlarm, Con);
            QueryAlarm.ExecuteNonQuery();
            using (MySqlDataReader QueryAlarmReader = QueryAlarm.ExecuteReader())
            {
               

                while (QueryAlarmReader.Read())
                {
                    dataGrid.Items.Add(new Item() { alarmName = QueryAlarmReader.GetString(1), alarmID = QueryAlarmReader.GetString(0) });
                    
                }

            }
            Con.Close();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (almName.Text != "")
            {
                dataGrid.Items.Add(new Item() { alarmName = almName.Text });
                list.Add(almName.Text);
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
                        MessageBox.Show("Error!" + ex);
                    }
                }
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

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var editAlarmname = almName.Text;
            var editAlarmID = almID.Text;
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlUpdate = "UPDATE `alarmtb` SET `AlarmName`='"+ editAlarmname + "' WHERE `ID`="+ editAlarmID ;
            MySqlCommand QueryUpdate = new MySqlCommand(sqlUpdate, Con);
            QueryUpdate.ExecuteNonQuery();
            clearDatagrid();
            refreshList();
        }
        private void clearDatagrid()
        {
            dataGrid.Items.Clear();
            dataGrid.Items.Refresh();
        }

        private void refreshList()
        {
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlAlarm = "SELECT * FROM `alarmtb`";
            MySqlCommand QueryAlarm = new MySqlCommand(sqlAlarm, Con);
            QueryAlarm.ExecuteNonQuery();
            using (MySqlDataReader QueryAlarmReader = QueryAlarm.ExecuteReader())
            {
                int i = 0;


                while (QueryAlarmReader.Read())
                {
                    dataGrid.Items.Add(new Item() { alarmName = QueryAlarmReader.GetString(1), alarmID = QueryAlarmReader.GetString(0) });


                    i++;
                }

            }
            Con.Close();
            almName.Text = "";
            almID.Text = "";
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var delAlarmName = almName.Text;
            var delAlarmID = almID.Text;
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlDel = "DELETE FROM `alarmtb` WHERE `ID`='" + delAlarmID + "'";
            MySqlCommand DelSql = new MySqlCommand(sqlDel, Con);
            DelSql.ExecuteNonQuery();
            clearDatagrid();
            refreshList();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            var cellInfoAlarmName = gd.SelectedCells[0];
            var cellInfoAlarmID = gd.SelectedCells[1];
            if(cellInfoAlarmName.Column != null && cellInfoAlarmID.Column != null)
            {
                var contentAlarmName = (cellInfoAlarmName.Column.GetCellContent(cellInfoAlarmName.Item) as TextBlock).Text;
                var contentAlarmID = (cellInfoAlarmID.Column.GetCellContent(cellInfoAlarmID.Item) as TextBlock).Text;
                almName.Text = contentAlarmName;
                almID.Text = contentAlarmID;
                deleteBtn.IsEnabled = true;
                editBtn.IsEnabled = true;
                addBtn.IsEnabled = false;
            }
        }
    }
}
