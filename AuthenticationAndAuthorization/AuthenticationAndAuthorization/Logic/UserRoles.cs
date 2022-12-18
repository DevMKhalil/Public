using System.Xml.Linq;

namespace AuthenticationAndAuthorization.Logic
{
    public class UserRole
    {
        private UserRole(int id, string name)
        {
            if (!UserRoleList.Any(x => x.Id == id))
            {
                this.Id = id;
                this.Name = name;
                UserRoleList.Add(this);
            }
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public static List<UserRole> UserRoleList = new List<UserRole>();

        public static readonly UserRole admin = new UserRole(1, "Administrator");
        public static readonly UserRole seller = new UserRole(2, "Seller");
        public static readonly UserRole buyer = new UserRole(3, "Buyer");
        public static readonly UserRole worker = new UserRole(4, "Worker");
        public static readonly UserRole worker = new UserRole(5, "Standard");
    }
}
