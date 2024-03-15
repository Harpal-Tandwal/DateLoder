using System.Windows;
using System.Net.Http;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using static DateLoder.MainWindow;
using System.Windows.Controls;


namespace DateLoder
{

    public partial class MainWindow : Window
    {
       public  DataModel dataModel;
        string filepath_prog_name = @"C:\Parser\ProgramName.txt";
        string filepath_operation_type = @"C:\Parser\OperationType.txt";
        string filepath_equipment = @"C:\Parser\Equipment.txt";
        public MainWindow()
        {
            DataContext = this;
            dataModel = new DataModel();
            InitializeComponent();
            CbInitializer();

            page_congif.Visibility = Visibility.Collapsed;
            page_data_loading.Visibility = Visibility.Collapsed;    

        }
       
        public void CbInitializer()
        {

            //loading program names 
            if (File.Exists(filepath_prog_name))
            {
                string[] lines = File.ReadAllLines(filepath_prog_name);
                 foreach(string line in lines)
                {
                    cb_prog_name.Items.Add(line);
                }
            }
            else { MessageBox.Show("Program  data file not found", "A FILE MISSING"); }
            
            //loading operation types
            if (File.Exists(filepath_operation_type))
            {
                string[] lines = File.ReadAllLines(filepath_operation_type);
                foreach (string line in lines)
                {
                    cb_operation_type.Items.Add(line);
                }
            }
            else { MessageBox.Show(" Operation data file not found", "A FILE MISSING"); }


            //loading equipment types
            if (File.Exists(filepath_equipment))
            {
                string[] lines = File.ReadAllLines(filepath_equipment);
                foreach (string line in lines)
                {
                    cb_equipment.Items.Add(line);
                }
            }
            else { MessageBox.Show("program file not found", "A FILE MISSING"); }




        }
        
       
        private async void dataSender()
        {
            // add listitems into barcode collections
           
             XmlSerializer serializer= new XmlSerializer(typeof(DataModel));
             StringWriter stringWriter = new StringWriter();
             serializer.Serialize(stringWriter, dataModel);
             string data = stringWriter.ToString();

          using (HttpClient client = new HttpClient())
            {
                string url = "enter destination url";
                MessageBox.Show($"{data} ", "Data Send Succesfully");

                HttpContent content =  new StringContent(data, Encoding.UTF8, "application/xml"); 
                try
                {
                    HttpResponseMessage res = await client.PostAsync(url, content);
                    if (res.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"{data} ", "Data Send Succesfully");
                    }
                    else
                    {
                        MessageBox.Show( res.StatusCode.ToString() , "Response From Server");
                    }

                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
           }

         }


        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            dataModel.prog_name = cb_prog_name.Text;
            dataModel.operation_type= cb_operation_type.Text;
            dataModel.equipment= cb_equipment.Text;
            dataModel.work_order= tb_work_order.Text;

            MessageBox.Show($" \n program name : {dataModel.prog_name}\n workorder :{ dataModel.work_order}  \n equipment : {dataModel.equipment}\n operation : {dataModel.operation_type} ", "Configuraion Saved !!");
            

        }


        private void btn_proceed_Click(object sender, RoutedEventArgs e)
        {
            if(dataModel.prog_name!=null && dataModel.operation_type!=null && dataModel.equipment!=null && dataModel.work_order != null)
            {
                page_congif.Visibility = Visibility.Collapsed;
                page_data_loading.Visibility = Visibility.Visible;


            }
            else
            {
                MessageBox.Show(" Please fill all Config Details. ", " Incomplete details");
            }
         
      
        }

        private void tb_barcode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                dataSender();
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if(tb_password.Text=="1234" && tb_user_name.Text=="OP1" || tb_password.Text == "admin" && tb_user_name.Text == "admin")
            {
                page_authenticate.Visibility = Visibility.Collapsed;
                page_congif.Visibility= Visibility.Visible;

            }
            else { MessageBox.Show("Wrong Credentials", "Authentiction Failed !!"); tb_user_name.Clear() ; tb_password.Clear(); }
        }

        private void btn_reset_config_Click(object sender, RoutedEventArgs e)
        {
           page_data_loading.Visibility = Visibility.Collapsed;
            page_congif.Visibility = Visibility.Visible;
        }
    }
}