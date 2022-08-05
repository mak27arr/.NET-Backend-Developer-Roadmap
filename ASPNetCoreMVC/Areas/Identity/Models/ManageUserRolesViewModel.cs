namespace ASPNetCoreMVC.Areas.Identity.Models
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
