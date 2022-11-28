using Readit.Models.Entities;
using Readit.Models.DAL;
namespace Readit.Models.DAO
{
    public class CommentDAO {

        private readonly IConfiguration _configuration;
        public mproulx_5w6_readitContext dbContext { get; set; }
        private MemberDAO mDAO;
        private LinkDAO lDAO;

        public CommentDAO(IConfiguration _configuration) {
            this._configuration = _configuration;
            this.dbContext = new mproulx_5w6_readitContext();
            this.mDAO = new MemberDAO(this._configuration);
            this.lDAO = new LinkDAO(this._configuration);

        }

        public void AddComment(Comment comment) {
            dbContext.Add<Comment>(comment);
            dbContext.SaveChanges();
        }

        public IList<Comment> GetCommentsByLink(int linkId) {
            return dbContext.Comments.Where(c => c.LinkId == linkId).ToList();
        }
    }
}
