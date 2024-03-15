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
        
        public MainWindow()
        {
            DataContext = this;
            dataModel = new DataModel();
            InitializeComponent();
        
       }
       

       
        private async void btn_send_Click(object sender, RoutedEventArgs e)
        {
            // add listitems into barcode collections
            foreach (var item in lv_entries.Items)
            {
                dataModel.barcodes.Add(item.ToString());
            }




            XmlSerializer serializer= new XmlSerializer(typeof(DataModel));
             StringWriter stringWriter = new StringWriter();
             serializer.Serialize(stringWriter, dataModel);
             string data = stringWriter.ToString();

          using (HttpClient client = new HttpClient())
            {
                string url = "enter destination url";
                MessageBox.Show($"data send succesfully:- {data} ");

                HttpContent content =  new StringContent(data, Encoding.UTF8, "application/xml"); 
                try
                {
                    HttpResponseMessage res = await client.PostAsync(url, content);
                    if (res.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"data send succesfully:- {data} ");
                    }
                    else
                    {
                        MessageBox.Show( "Response from server \n " + res.StatusCode.ToString());
                    }

                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
           }

         }


        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
               

                lv_entries.Items.Add(tb_entry.Text);
               
                tb_entry.Clear();
            }
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {



            lv_entries.Items.Add(tb_entry.Text);
            
            tb_entry.Clear();

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
      

        }
    }
}