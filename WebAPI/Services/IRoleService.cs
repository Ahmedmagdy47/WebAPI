using WebAPI.Contracts.Roles;

namespace WebAPI.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllAsync(bool includeDisabled = false, CancellationToken cancellationToken = default);
        Task<Result<RoleDetailResponse>> GetAsync(string id);
        Task<Result<RoleDetailResponse>> AddAsync(RoleRequest request);
        Task<Result> UpdateAsync(string id, RoleRequest request);
        Task<Result> ToggleIsDeletedAsync(string id);
    }
}
