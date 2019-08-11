using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MtConnectPlot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Innitialize conn2
        MySqlConnection conn2;
        bool IsConnected = false;

        //Making a connection to the database
        void ConnectIfNeeded()
        {
            string connStr = "server=localhost;user=root;database=revised_connect;port=3306;password=mtconnect";
            if (IsConnected == false)
            {
                conn2 = new MySqlConnection(connStr);
                IsConnected = true;
            }
        }

        void TestConnection()
        {
            //MessageBox.Show("Connecting...");

            ConnectIfNeeded();
            conn2.Open();
            //MessageBox.Show("Connected");

            string tempYear = dateTimePicker1.Value.Year.ToString();
            string tempMonth = dateTimePicker1.Value.Month.ToString();
            string tempDay = dateTimePicker1.Value.Day.ToString();


            string tempYearEnd = dateTimePicker2.Value.Year.ToString();
            string tempMonthEnd = dateTimePicker2.Value.Month.ToString();
            string tempDayEnd = dateTimePicker2.Value.Day.ToString();

            string selectName = comboBox1.SelectedItem.ToString();
            Console.WriteLine(selectName);

            string selectQuery = "SELECT * FROM wallaceconnect."+selectName +"  WHERE Time >= '"+tempYear+ "-" + tempMonth + "-" + tempDay + " 08:00:00' AND Time <= '" + tempYearEnd + "-" + tempMonthEnd + "-" + tempDayEnd + " 08:00:00'";
            MySqlCommand myCmd = new MySqlCommand(selectQuery, conn2);
            //MySqlDataReader dr = myCmd.ExecuteReader();


            try
            {
                MySqlDataAdapter myAdapater = new MySqlDataAdapter();
                myAdapater.SelectCommand = myCmd;        
                DataTable myDataTable = new DataTable();
                myAdapater.Fill(myDataTable);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = myDataTable;
                dataGridView1.DataSource = bSource;
                myAdapater.Update(myDataTable);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        conn2.Close();

        }

        private void ButtonSave_Click_1(object sender, EventArgs e)
        {
            TestConnection();
        }

        private void ButtonExport_Click_1(object sender, EventArgs e)
        {
            //TestConnection();

            
            //Build the CSV file data as a Comma separated string.
            string csv = string.Empty;

            //Add the Header row for CSV file.
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                csv += column.HeaderText + ',';
            }

            //Add new line.
            csv += "\r\n";

            DataGridViewRow row;
            int i;
            for (i =0; i<dataGridView1.Rows.Count-1; i++)
            {
                row = dataGridView1.Rows[i];

                foreach (DataGridViewCell cell in row.Cells)
                {
                    //Add the Data rows.
                    if (cell.Value is null)
                    {
                        i = i;
                    }
                    else
                    {
                        csv += cell.Value.ToString().Replace(",", ";") + ',';

                    }
                }

                //Add new line.
                csv += "\r\n";
            }
            //MessageBox.Show("Hit");

            //Exporting to CSV.
            string folderPath = "C:\\Users\\samuel.mwangi\\Desktop\\CSV\\";
            File.WriteAllText(folderPath + "DataGridViewExport.csv", csv);
        }
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            labelValue.Text = comboBox1.Text;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("oven_temp");
            comboBox1.Items.Add("modelfollowing");
            comboBox1.Items.Add("xfollowing");
            comboBox1.Items.Add("input_ups_voltage");
            comboBox1.Items.Add("yfollowing");
            comboBox1.Items.Add("zfollowing");
            comboBox1.Items.Add("supportfollowing");
            comboBox1.SelectedItem = "input_ups_voltage";
           
        }

        private void DateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void DateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
