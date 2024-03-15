using System.Collections.ObjectModel;
using System.Security.Policy;


namespace DateLoder
{
     public class DataModel
    {

        public string? prog_name { get; set; } 
        public string? operation_type { get; set; } 

        public string equipment {  get; set; }  
        public string work_order {  get; set; }
        public string cycle_time { get; set; } = "5";
        public string start_time { get; set; } = DateTime.Now.ToString();

       
    }
}
