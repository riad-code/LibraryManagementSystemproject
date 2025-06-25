namespace LIBRARYMANAGEMENT.Models
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
        public string SelectedRole { get; set; }
    }

}
