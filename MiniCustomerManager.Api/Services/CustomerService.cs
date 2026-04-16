using MiniCustomerManager.Api.Models;

namespace MiniCustomerManager.Api.Services;

public class CustomerService
{
    private readonly List<Customer> _customers = new List<Customer>
    {
        new Customer { Id = 1, FullName = "Nguyen Van A", MembershipTier = "Gold", JoinYear = 2021, RewardPoints = 150 },
        new Customer { Id = 2, FullName = "Tran Thi B", MembershipTier = "Silver", JoinYear = 2023, RewardPoints = 50 },
        new Customer { Id = 3, FullName = "Le Van C", MembershipTier = "Bronze", JoinYear = 2024, RewardPoints = 0 },
        new Customer { Id = 4, FullName = "Tran Quoc D", MembershipTier = "Platinum", JoinYear = 2020, RewardPoints = 500 },
        new Customer { Id = 5, FullName = "Nguyen Thi E", MembershipTier = "Platinum", JoinYear = 2022, RewardPoints = 400 }
    };

    // Phân loại trạng thái
    public string GetCustomerStatus(int rewardPoints)
    {
        if (rewardPoints == 0) return "Inactive"; 
        if (rewardPoints <= 100) return "At Risk"; 
        return "Active"; 
    }

    // Thống kê nâng cao
    public object GetStats()
    {
        var totalCustomers = _customers.Count;
        var totalRewardPoints = _customers.Sum(x => x.RewardPoints);
        var activeCustomers = _customers.Count(x => GetCustomerStatus(x.RewardPoints) == "Active");
        
        var averagePoints = totalCustomers > 0 ? Math.Round((double)totalRewardPoints / totalCustomers, 2) : 0;
        
        // Tìm ra khách hàng VIP
        var topCustomer = _customers.OrderByDescending(c => c.RewardPoints).FirstOrDefault();

        return new
        {
            TotalCustomers = totalCustomers,
            ActiveCustomers = activeCustomers,
            TotalRewardPoints = totalRewardPoints,
            AveragePoints = averagePoints,
            TopCustomer = topCustomer != null ? new { topCustomer.FullName, topCustomer.MembershipTier, topCustomer.RewardPoints } : null
        };
    } 

    public object GetCustomers(string? search, string? sortBy, bool isDesc, int page, int pageSize)
    {
        var query = _customers.AsQueryable();

        // Tìm kiếm
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c => 
                c.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) || 
                c.MembershipTier.Equals(search, StringComparison.OrdinalIgnoreCase));
        }

        // Sắp xếp
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            query = sortBy.ToLower() switch
            {
                "name" => isDesc ? query.OrderByDescending(c => c.FullName) : query.OrderBy(c => c.FullName),
                "tier" => isDesc ? query.OrderByDescending(c => c.MembershipTier) : query.OrderBy(c => c.MembershipTier),
                "points" => isDesc ? query.OrderByDescending(c => c.RewardPoints) : query.OrderBy(c => c.RewardPoints),
                _ => isDesc ? query.OrderByDescending(c => c.Id) : query.OrderBy(c => c.Id)
            };
        }
        else
        {
            query = query.OrderBy(c => c.Id); // Mặc định sắp xếp theo ID
        }

        // Phân trang
        var totalCount = query.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var pagedData = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new
            {
                c.Id,
                c.FullName,
                c.MembershipTier,
                c.JoinYear,
                c.RewardPoints,
                Status = GetCustomerStatus(c.RewardPoints)
            }).ToList();

        // Trả về JSON
        return new
        {
            Metadata = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                HasNext = page < totalPages,
                HasPrevious = page > 1
            },
            Data = pagedData
        };
    }
}