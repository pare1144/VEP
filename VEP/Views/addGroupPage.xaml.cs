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
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;

namespace VEP.Views
{
    /// <summary>
    /// Interaction logic for addGroupPage.xaml
    /// </summary>
    public partial class addGroupPage : UserControl
    {
        public List<Tuple<string, string>> list = new List<Tuple<string, string>>();
        //var list = new List<Tuple<string, string>>();
        public class Item
        {
            public String GroupLineID { get; set; }
            public String GroupName { get; set; }

            public String GroupID { get; set; }
        }
        public addGroupPage()
        {
            InitializeComponent();
            deleteBtn.IsEnabled = false;
            editBtn.IsEnabled = false;
            //Construct the datagrid
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "GroupLineID";
            c1.Binding = new Binding("GroupLineID");
            c1.Width = 170;
            dataGrid.Columns.Add(c1);
            DataGridTextColumn c2 = new DataGridTextColumn();
            c2.Header = "GroupName";
            c2.Width = 170;
            c2.Binding = new Binding("GroupName");
            dataGrid.Columns.Add(c2);
            DataGridTextColumn c3 = new DataGridTextColumn();
            c3.Header = "GroupID";
            c3.MaxWidth = 0;
            c3.Binding = new Binding("GroupID");
            dataGrid.Columns.Add(c3);

            List<string> resultAlarm = new List<string>();
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlGroup = "SELECT * FROM `grouptb`";
            MySqlCommand QueryGroup = new MySqlCommand(sqlGroup, Con);
            QueryGroup.ExecuteNonQuery();
            using (MySqlDataReader QueryGroupReader = QueryGroup.ExecuteReader())
            {
                int i = 0;
                
               
                while (QueryGroupReader.Read())
                {
                    dataGrid.Items.Add(new Item() { GroupLineID = QueryGroupReader.GetString(1), GroupName = QueryGroupReader.GetString(2), GroupID = QueryGroupReader.GetString(0) });


                    i++;
                }

            }
            Con.Close();
            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if(groupID.Text !="" && groupName.Text!= "")
            {
                dataGrid.Items.Add(new Item() { GroupLineID = groupID.Text, GroupName = groupName.Text });              
                list.Add(Tuple.Create(groupID.Text, groupName.Text));
                string ConnetionString;
                ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
                MySqlConnection Con = new MySqlConnection(ConnetionString);
                Con.Open();
                foreach (var val in list)
                {
                    var groupLineID = val.Item1;
                    var groupName = val.Item2;
                    var sql = "INSERT INTO grouptb (GroupLineID, GroupName) VALUES('" + groupLineID + "','" + groupName + "')";

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
                var groupLineID = val.Item1;
                var groupName = val.Item2;
                var sql = "INSERT INTO grouptb (GroupLineID, GroupName) VALUES('" + groupLineID + "','" + groupName + "')";
             
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
            Con.Close();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            var editgroupID = groupID.Text;
            var editgroupName = groupName.Text;
            //var oldgroupID = dataGrid.Columns[0].GetCellContent();
            var editID = group_id.Text;

            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlUpdate = "UPDATE `grouptb` SET `GroupLineID`='" + editgroupID + "',`GroupName`='" + editgroupName + "' WHERE ID=" + editID;
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
            var sqlGroup = "SELECT * FROM `grouptb`";
            MySqlCommand QueryGroup = new MySqlCommand(sqlGroup, Con);
            QueryGroup.ExecuteNonQuery();
            using (MySqlDataReader QueryGroupReader = QueryGroup.ExecuteReader())
            {
                int i = 0;


                while (QueryGroupReader.Read())
                {
                    dataGrid.Items.Add(new Item() { GroupLineID = QueryGroupReader.GetString(1), GroupName = QueryGroupReader.GetString(2),GroupID = QueryGroupReader.GetString(0) });


                    i++;
                }

            }
            Con.Close();
            groupID.Text = "";
            groupName.Text = "";
            group_id.Text = "";
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var delgroupID = groupID.Text;
            var delgroupName = groupName.Text;
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            Con.Open();
            var sqlDel = "DELETE FROM `grouptb` WHERE `GroupName`='" + delgroupName + "' and `GroupLineID`='" + delgroupID + "'";
            MySqlCommand DelSql = new MySqlCommand(sqlDel, Con);
            DelSql.ExecuteNonQuery();
            clearDatagrid();
            refreshList();
            
        }

       
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            
            var cellInfoGroupID = gd.SelectedCells[0];
            var cellInfoGroupName = gd.SelectedCells[1];
            var cellInfoID = gd.SelectedCells[2];
            if (cellInfoGroupID.Column != null && cellInfoGroupName.Column != null)
            {
                var contentGroupID = (cellInfoGroupID.Column.GetCellContent(cellInfoGroupID.Item) as TextBlock).Text;
                var contentGroupName = (cellInfoGroupName.Column.GetCellContent(cellInfoGroupName.Item) as TextBlock).Text;
                var contentID = (cellInfoID.Column.GetCellContent(cellInfoID.Item) as TextBlock).Text;
                groupID.Text = contentGroupID;
                groupName.Text = contentGroupName;
                group_id.Text = contentID;
                deleteBtn.IsEnabled = true;
                editBtn.IsEnabled = true;
                addBtn.IsEnabled = false;
            }
                
        }
    }
}
