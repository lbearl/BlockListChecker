namespace Core.Models.ThirdParty.SendGrid
{
    public class Bounce
    {
        public string Created { get; set; }
        public string Email { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
