using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;

namespace ORAGO_CC_Planning.Models
{
    public class AbisEntityBase : IAbISEntityBase
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(40)]
        public string? Name { get; set; }
        [Display(Name = "Created by"), MaxLength(50)]
        public string? CreateUser { get; set; }
        [Display(Name = "Create date"), DataType(DataType.DateTime)]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "Modified by"), MaxLength(50)]
        public string? ModifiedUser { get; set; }
        [Display(Name = "Modified date"), DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        // public AbisEntityBase()
        // {
        //     CreateUser = "No user";
        //     CreateDate = DateTime.Now;
        // }

        public async Task SetCreateInfo(AuthenticationStateProvider authenticationStateProivider)
        {
            var authState = await authenticationStateProivider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity is not null)
            {
                if (user.Identity.IsAuthenticated)
                {
                    CreateUser = user.Identity.Name;
                }
                else
                {
                    CreateUser = "No user.";
                }
            }
            CreateDate = DateTime.Now;
        }

        public async Task SetModifyInfo(AuthenticationStateProvider authenticationStateProivider)
        {
            var authState = await authenticationStateProivider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity is not null)
            {
                if (user.Identity.IsAuthenticated)
                {
                    ModifiedUser = user.Identity.Name;
                }
                else
                {
                    ModifiedUser = "No user.";
                }
            }
            ModifiedDate = DateTime.Now;
        }
    }
}
