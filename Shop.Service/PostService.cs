using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public interface IPostService
    {
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
        IEnumerable<Post> Getall();
        IEnumerable<Post> GetallPaging(int page, int pagesize, int totalrow);
        Post GetById(int id);
        IEnumerable<Post> GetAllByTagPaging(int tag, int page, int pagesize, out int totalRow);
        IEnumerable<Post> GetAllCategoryPaging(int tag, int page, int pagesize, out int totalRow);
        void SaveChanges();
    }
    public class PostService : IPostService
    {
        private IPostRepository _postrepository;
        private IUnitOfWork _unitofWork;
        public PostService(IPostRepository postRepository, IUnitOfWork unitofWork)
        {
            this._postrepository = postRepository;
            this._unitofWork = unitofWork;
        }
        public void Add(Post post)
        {

            _postrepository.Add(post);
        }

        public void Delete(int id)
        {
            _postrepository.Delete(id);
        }

        public IEnumerable<Post> Getall()
        {
            return _postrepository.GetAll();
        }

        public IEnumerable<Post> GetAllByTagPaging(int tag, int page, int pagesize, out int totalRow)
        {
            return _postrepository.GetAllByTag(tag, page, pagesize, out totalRow);
        }

        public IEnumerable<Post> GetAllCategoryPaging(int tag, int page, int pagesize, out int totalRow)
        {
            return _postrepository.GetMultiPaging(x => x.Status, out totalRow, page, pagesize, new string[] { "PostCategory" });
        }
        public IEnumerable<Post> GetallPaging(int page, int pagesize, int totalrow)
        {
            return _postrepository.GetMultiPaging(x => x.Status, out totalrow, page, pagesize);
        }

        public Post GetById(int id)
        {
            return _postrepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitofWork.Commit();
        }

        public void Update(Post post)
        {
            _postrepository.Update(post);
        }
    }
}
