namespace IllinoisProject.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string InviterId { get; set; }
        public string InviteeId { get; set; }
        public string InviteStatus { get; set; }
    }
}
