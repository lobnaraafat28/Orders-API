namespace Order.PL.API.DTOs
{
    public class UserInRoleDTO
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }

    }
}
