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

namespace Project1SSD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_LoadTicker_Click(object sender, EventArgs e)
        {
            openFileDialog_LoadTicker.ShowDialog();
            var fileContent = string.Empty;
            var filePath = string.Empty;
            if (openFileDialog_LoadTicker.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog_LoadTicker.FileName;

                //Read the contents of the file into a stream
                DataTable dt = new DataTable();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    string[] values;

                    // Read header row
                    line = reader.ReadLine();
                    values = line.Split(',');
                    foreach (string column in values)
                    {
                        dt.Columns.Add(column);
                    }

                    // Read data rows
                    while ((line = reader.ReadLine()) != null)
                    {
                        values = line.Split(',');
                        DataRow row = dt.NewRow();
                        for (int i = 0; i < values.Length; i++)
                        {
                            row[i] = values[i];
                        }
                        dt.Rows.Add(row);
                    }
                }
                dataGridView_Candlestick.DataSource = dt;
                // Set DataGridView DataSource
            }
            
            
       
        }

        private void openFileDialog_LoadTicker_FileOk(object sender, CancelEventArgs e)
        {
            Text = openFileDialog_LoadTicker.FileName;
        }
    }

    
}
