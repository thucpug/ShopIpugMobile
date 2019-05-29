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
    public interface IPostTagService
    {
        void Add(PostTag postTag);
        void Update(PostTag postTag);
        void Delete(int id);
        PostTag GetById(int id);
        void SaveChanges();
        IEnumerable<PostTag> Getall();
        //IEnumerable<PostTag> GetallPaging(int page, int pagesize, int totalrow);

        //IEnumerable<PostTag> GetAllByTagPaging(int tag, int page, int pagesize, out int totalRow);
        //IEnumerable<PostTag> GetAllCategoryPaging(int tag, int page, int pagesize, out int totalRow);


    }
    public class PostTagService : IPostTagService
    {
        IPostTagRepository _PostTagRepository;
        IUnitOfWork _IUnitOfWork;
        public PostTagService(IPostTagRepository PostTagRepository, IUnitOfWork IUnitOfWork)
        {
            this._PostTagRepository = PostTagRepository;
            this._IUnitOfWork = IUnitOfWork;
        }
        public void Add(PostTag postTag)
        {
            _PostTagRepository.Add(postTag);
        }

        public void Delete(int id)
        {
            _PostTagRepository.Delete(id);
        }

        public IEnumerable<PostTag> Getall()
        {
            return _PostTagRepository.GetAll();
        }

        public PostTag GetById(int id)
        {
            return _PostTagRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _IUnitOfWork.Commit();
        }

        public void Update(PostTag postTag)
        {
            _PostTagRepository.Update(postTag);
        }
    }
}
