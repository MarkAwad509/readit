using Readit.Models.Entities;
using Readit.Models.DAL;
namespace Readit.Models.DAO
{
    public class LinkDAO
    {
        private readonly IConfiguration configuration;
        public mproulx_5w6_readitContext dbContext { get; set; }
        private MemberDAO mDAO;
        private string connectionString;

        public LinkDAO(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.dbContext = new mproulx_5w6_readitContext();
            this.mDAO = new MemberDAO(this.configuration);
            connectionString = configuration.GetConnectionString("Db");
        }
        public List<Link> getLinks()
        {
            return dbContext.Links.ToList();
        }
        public void AddLink(Link link){
            dbContext.Add<Link>(link);
            dbContext.SaveChanges();
        }

        public Link GetLinkByID(int linkId) {
            return dbContext.Links.Where(l => l.Id == linkId).First();
        }    

        public IList<Link> GetLinksByMember(int memberId) {
            return dbContext.Links.Where(l => l.MemberId == memberId).ToList();
        }

        public List<Vote> GetMemberVotes(int memberId) {
            return dbContext.Votes.Where(v => v.MemberId.Equals(memberId)).ToList();
        }

        public void DeleteLink(Link link) {
            dbContext.Links.Remove(link);
            var commentsToRem = dbContext.Comments.Where(c => c.LinkId == link.Id).ToList();
            foreach (var comment in commentsToRem) {
                dbContext.Comments.Remove(comment);
            }

            var votesToRem = dbContext.Votes.Where(v => v.LinkId == link.Id).ToList();
            foreach (var vote in votesToRem) {
                dbContext.Votes.Remove(vote);
            }
            dbContext.SaveChanges();
        }

        public bool AddVote(Vote vote) {
            if( vote != null) {
                dbContext.Votes.Add(vote);
                return true;
            }
            dbContext.SaveChanges();
            return false;
        }
    }
}
