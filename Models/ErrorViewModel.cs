namespace Contacts.Models
{

    public class Contact
    {
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Email{ get; set; }
    }
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
