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
using System.IO;
using MySql.Data.MySqlClient;

namespace VEP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool Stop = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            //Edit EditPage = new Edit();
            //this.Content = EditPage;
            AdminWindow EditPage = new AdminWindow();
            EditPage.Show();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Start process write in textbox
            //DateTime StartDatetime = DateTime.Now;
            //textBox.AppendText("Start process at " + StartDatetime);
            //textBox.AppendText(Environment.NewLine);
            // Start process write in logfile
            //FileStream fs = new FileStream("PathFile", FileMode.Open, FileAccess.Read);
            //using (StreamReader sr = new StreamReader(fs))
            //{
            //    using (StreamWriter sw = new StreamWriter("NameFile"))
            //    {
            //        sw.WriteLine("");
            //    }
            //}
            // OpenConnection
            string ConnetionString;
            ConnetionString = @"Data Source=127.0.0.1;Database=alarmnotice_db;user id=root;Password=;CharSet=utf8";
            MySqlConnection Con = new MySqlConnection(ConnetionString);
            try
            {
                Con.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error!");
            }
            if (!Stop)
            {
                //Run text file read function (Add condition !Stop)
                // Get AlarmID
                String Alarmtextread = "AHU";
                int AlarmID = 0;
                List<int> ListGroupID = new List<int>();
                String SqlGetAlarmID = "Select ID From alarmtb Where AlarmName = '" + Alarmtextread + "'";
                MySqlCommand GetAlarmIDCMD = new MySqlCommand(SqlGetAlarmID, Con);
                MySqlDataReader reader = GetAlarmIDCMD.ExecuteReader();
                //AlarmID = Convert.ToInt32(reader.ToString());
                while (reader.Read())
                {
                    Alarmtextread = reader.GetString(0);
                }
                // Get GroupID
                String SqlGetGroupID = "Select GroupID From Group Where AlarmID = " + AlarmID;
                MySqlCommand GetGroupIDCMD = new MySqlCommand(SqlGetGroupID, Con);
                //Stamp Datetime sent
                DateTime SendDatetime = DateTime.Now;
                //Text box write (Data detail)
                textBox.AppendText("");
                textBox.AppendText(Environment.NewLine);
                //Log file write
                //using (StreamReader sr = new StreamReader(fs))
                //{
                //    using (StreamWriter sw = new StreamWriter("NameFile"))
                //    {
                //        sw.WriteLine("");
                //    }
                //}
            }


        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            Stop = true;
        }
    }

   
       

  
}
