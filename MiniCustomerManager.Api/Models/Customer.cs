namespace MiniCustomerManager.Api.Models;

public class Customer 
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string MembershipTier { get; set; } = "";
    public int JoinYear { get; set; }
    public int RewardPoints { get; set; }
}