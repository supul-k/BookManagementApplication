namespace BookManagementApplication.DTO
{
    public class GeneralResponseInternalDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public GeneralResponseInternalDTO(bool status, string message)
        {
            this.Status = status;
            this.Message = message;
        }
    }
}
