using Shop.Data.Infrastructure;
using Shop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(int tag, int page, int pagesize, out int totalrow);
    }
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<Post> GetAllByTag(int tag, int page, int pagesize, out int totalrow)
        {

            var query = (from c in DbContext.Posts
                         join o in DbContext.PostTags
                         on c.ID equals o.PostID
                         where o.TagID == tag && c.Status == true
                         orderby c.CreatedDate descending
                         select c);

            totalrow = query.Count();
            query = query.Skip((page - 1) * pagesize).Take(pagesize);
            return query;
        }
    }
}
