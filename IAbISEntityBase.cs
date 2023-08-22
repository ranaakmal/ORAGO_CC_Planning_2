using Microsoft.AspNetCore.Components.Authorization;

namespace ORAGO_CC_Planning.Models;
public interface IAbISEntityBase
{
    int Id { get; set; }
    string? Name { get; set; }
    string? CreateUser { get; set; }
    DateTime? CreateDate { get; set; }
    string? ModifiedUser { get; set; }
    DateTime? ModifiedDate { get; set; }
    Task SetCreateInfo(AuthenticationStateProvider authenticationStateProvider);
    Task SetModifyInfo(AuthenticationStateProvider authenticationStateProvider);
}