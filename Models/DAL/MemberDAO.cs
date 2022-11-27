using Readit.Models.Entities;
using Readit.Models.DAL;

namespace Readit.Models.DAO {
    public class MemberDAO {
        private readonly IConfiguration configuration;
        public mproulx_5w6_readitContext dbContext { get; set; }

        public MemberDAO(IConfiguration _configuration) {
            this.configuration = _configuration;
            this.dbContext = new mproulx_5w6_readitContext();
        }

        public bool AddMember(Member member) {
            dbContext.Add<Member>(member);
            dbContext.SaveChanges();
            return true;
        }

        public IList<Member> GetMembers() {
            return dbContext.Members.ToList();
        }

        public Member GetMemberByEmail(string email) {
            return dbContext.Members.Where(e => e.Email == email).First();
        }

    }
}
