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
    /// Interaction logic for setupNotiPage.xaml
    /// </summary>
    /// 
    public class comboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    public class Item
    {
        public String AlarmName { get; set; }
        public String GroupName { get; set; }
        public String ListID { get; set; }
    }
    public partial class setupNotiPage : UserControl
    {
        public List<Tuple<object>> listAdd = new List<Tuple<object>>();
        public setupNotiPage()
        {
            InitializeComponent();
            deleteBtn.IsEnabled = false;
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
            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "ListID";
            c3.MaxWidth = 0;
            c3.Binding = new Binding("ListID");
            dataGrid.Columns.Add(c3);

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
               
                while (QueryAlarmReader.Read())
                {
                    comboboxItem item = new comboboxItem();
                    item.Text = QueryAlarmReader.GetString(1);
                    item.Value = QueryAlarmReader.GetString(0);
                    AlarmcomboBox.Items.Add(item);

                }
               
            }
            using (MySqlDataReader QueryGroupReader = QueryGroup.ExecuteReader())
            {
                int i = 0;
                //GroupcomboBox.DisplayMemberPath = "Text";
                //GroupcomboBox.SelectedValuePath = "Value";
                while (QueryGroupReader.Read())
                {
                    comboboxItem item = new comboboxItem();
                    item.Text = QueryGroupReader.GetString(2);
                    item.Value = QueryGroupReader.GetString(0);
                    GroupcomboBox.Items.Add(item);
                    //GroupcomboBox.Items.Add(new { Text =QueryGroupReader.GetString(2),Value= QueryGroupReader.GetString(0) });

                    //i++;
                }

            }

            var sqlListALarm = "SELECT noticonfigtb.*,grouptb.GroupName, alarmtb.AlarmName FROM `noticonfigtb` INNER JOIN alarmtb on alarmtb.ID = noticonfigtb.AlarmID INNER JOIN grouptb ON grouptb.ID = noticonfigtb.GroupID";
            MySqlCommand QueryListAlarm = new MySqlCommand(sqlListALarm, Con);
            QueryListAlarm.ExecuteNonQuery();
            using (MySqlDataReader QueryListReader = QueryListAlarm.ExecuteReader())
            {
                while (QueryListReader.Read())
                {
                    dataGrid.Items.Add(new { AlarmName = QueryListReader.GetString(5), GroupName = QueryListReader.GetString(4), ListID = QueryListReader.GetString(0) });
                }
            }
            Con.Close();
            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AlarmcomboBox.Text != "" && GroupcomboBox.Text != "")
            {
                
                listAdd.Add(Tuple.Create(AlarmcomboBox.SelectedItem));
                listAdd.Add(Tuple.Create(GroupcomboBox.SelectedItem));
                var groupID = (GroupcomboBox.SelectedItem as comboboxItem).Value.ToString();
                var alarmID = (AlarmcomboBox.SelectedItem as comboboxItem).Value.ToString();
                

                string ConnetionString;
                ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
                MySqlConnection Con = new MySqlConnection(ConnetionString);
                Con.Open();
                var sqlInsert = "INSERT INTO `noticonfigtb`( `GroupID`, `AlarmID`) VALUES (" + groupID + "," + alarmID + ")";
                MySqlCommand Insertmapping = new MySqlCommand(sqlInsert, Con);
                try
                {
                    Insertmapping.ExecuteNonQuery();
                    //MessageBox.Show("Data insert successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!" + ex);
                }
                /////////
                clearDatagrid();
                refreshList();
                //dataGrid.Items.Add(new Item() { AlarmName = AlarmcomboBox.Text, GroupName = GroupcomboBox.Text });
            }
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
            var sqlListALarm = "SELECT noticonfigtb.*,grouptb.GroupName, alarmtb.AlarmName FROM `noticonfigtb` INNER JOIN alarmtb on alarmtb.ID = noticonfigtb.AlarmID INNER JOIN grouptb ON grouptb.ID = noticonfigtb.GroupID";
             MySqlCommand QueryAlarmList = new MySqlCommand(sqlListALarm, Con);
            QueryAlarmList.ExecuteNonQuery();
            using (MySqlDataReader QueryAlarmListReader = QueryAlarmList.ExecuteReader())
            {
                int i = 0;


                while (QueryAlarmListReader.Read())
                {
                    dataGrid.Items.Add(new { AlarmName = QueryAlarmListReader.GetString(5), GroupName = QueryAlarmListReader.GetString(4), ListID = QueryAlarmListReader.GetString(0) });

                    i++;
                }

            }
            Con.Close();
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var delListID = ListID.Text;
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlDel = "DELETE FROM `noticonfigtb` WHERE ID=" + delListID;
            MySqlCommand DelSql = new MySqlCommand(sqlDel, Con);
            DelSql.ExecuteNonQuery();
            clearDatagrid();
            refreshList();
            AlarmcomboBox.SelectedItem = null;
            GroupcomboBox.SelectedItem = null;
            ListID.Text = "";
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            var cellInfoAlarmName = gd.SelectedCells[0];
            var cellInfoGroupName = gd.SelectedCells[1];
            var cellInfoListID = gd.SelectedCells[2];
            if (cellInfoAlarmName.Column != null && cellInfoGroupName.Column != null)
            {
                var contentAlarmName = (cellInfoAlarmName.Column.GetCellContent(cellInfoAlarmName.Item) as TextBlock).Text;
                var contentGroupName = (cellInfoGroupName.Column.GetCellContent(cellInfoGroupName.Item) as TextBlock).Text;
                var contentListID = (cellInfoListID.Column.GetCellContent(cellInfoListID.Item) as TextBlock).Text;
                AlarmcomboBox.SelectedValue = contentAlarmName;
                GroupcomboBox.SelectedValue = contentGroupName;
                ListID.Text = contentListID;
                deleteBtn.IsEnabled = true;
                addBtn.IsEnabled = false;
            }
        }

        private void AlarmcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
