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
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace testconnectdotnet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "DATA SOURCE=localhost:1521/xe;DBA PRIVILEGE=SYSDBA;TNS_ADMIN=C:\\Users\\Admin\\Oracle\\network\\admin;PERSIST SECURITY INFO=True;USER ID=SYS; PassWord=mgt;";
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    OracleCommand cmd = new OracleCommand("SELECT * FROM SACH", connection);
                    OracleDataReader reader = cmd.ExecuteReader();
                    Stack<string> results = new Stack<string>();
                    while (reader.Read())
                    {
                        string col3Value = reader.GetValue(2).ToString();
                        results.Push(col3Value);
                    }
                    string s = null;

                    for(int i = 0; i < 6; i++)
                    {
                        s += results.ElementAt(i)+" ";
                    }
                    MessageBox.Show(s);
                    reader.Close();
                }
            }
            catch (OracleException exp)
            {
                MessageBox.Show("Failed: " + exp.Message);
            }

        }
    }
}
