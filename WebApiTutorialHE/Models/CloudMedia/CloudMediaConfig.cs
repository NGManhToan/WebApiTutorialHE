namespace WebApiTutorialHE.Models.CloudMedia
{
    public class CloudMediaConfig
    {
        public string Folder { get; set; }
        public string FileName { get; set; }
        public List<CloudFileConfig> CloudFiles { get; set; }
    }
}
