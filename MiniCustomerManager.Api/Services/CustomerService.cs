using MiniCustomerManager.Api.Models;

namespace MiniCustomerManager.Api.Services;

public class CustomerService
{
    // Tạo danh sách khách hàng
    private readonly List<Customer> _customers = new List<Customer>
    {
        new Customer { Id = 1, FullName = "Nguyen Van A", MembershipTier = "Gold", JoinYear = 2021, RewardPoints = 150 },
        new Customer { Id = 2, FullName = "Tran Thi B", MembershipTier = "Silver", JoinYear = 2023, RewardPoints = 50 },
        new Customer { Id = 3, FullName = "Le Van C", MembershipTier = "Bronze", JoinYear = 2024, RewardPoints = 0 },
        new Customer { Id = 4, FullName = "Tran Quoc D", MembershipTier = "Platinum", JoinYear = 2020, RewardPoints = 500 },
        new Customer { Id = 5, FullName = "Nguyen Thi E", MembershipTier = "Platinum", JoinYear = 2022, RewardPoints = 400 }
    };

    // Hàm trả về toàn bộ danh sách
    public List<Customer> GetAll() => _customers;

    // Phân loại trạng thái (Dựa vào RewardPoints)
    public string GetCustomerStatus(int rewardPoints)
    {
        if (rewardPoints == 0) return "Inactive"; // Ngừng tương tác
        if (rewardPoints <= 100) return "At Risk"; // Nguy cơ rời bỏ
        return "Active"; // Hoạt động tốt
    }

    // Hàm tính toán thống kê
    public object GetStats()
    {
        var totalCustomers = _customers.Count;
        var totalRewardPoints = _customers.Sum(x => x.RewardPoints);
        var activeCustomers = _customers.Count(x => GetCustomerStatus(x.RewardPoints) == "Active");

        return new
        {
            TotalCustomers = totalCustomers,
            TotalRewardPoints = totalRewardPoints,
            ActiveCustomers = activeCustomers
        };
    }    
}