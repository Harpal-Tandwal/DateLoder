using System.Collections.ObjectModel;
using System.Security.Policy;


namespace DateLoder
{
     public class DataModel
    {

        public string? name { get; set; } = "TURN";
        public string? SerialNumber { get; set; } = "Z101123456789";

        public ObservableCollection<string> barcodes { get; set; }
         public DataModel()
        {
            barcodes = new ObservableCollection<string>();  
        }

    }
}
