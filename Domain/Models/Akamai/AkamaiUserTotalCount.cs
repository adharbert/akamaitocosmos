namespace Domain.Models.Akamai
{
    public class AkamaiUserTotalCount
    {
        public int total_count { get; set; }
        public string stat { get; set; }
        public string error { get; set; }
        public int code { get; set; }
        public string argument_name { get; set; }
    }
}
