namespace WebApiTutorialHE.Models.Registation
{
    public class RegistationListModel
    {
        public int registation_id { get; set; }
        public int item_id { get; set; }
        public int register_id { get; set;}
        public DateTime registation_date { get; set; } 
        public string content { get; set; }
        public int register_status { get; set; }
        public DateTime approval_date { get; set;}
        public int register_notifi { get; set; }
    }
}
