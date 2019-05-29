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
    public interface ITagService
    {
        void Add(Tag Tag);
        void Update(Tag Tag);
        void Delete(int id);
        Tag GetById(int id);
        IEnumerable<Tag> Getall();
        void SaveChanges();
    }
    public class TagService : ITagService
    {
        ITagRepository _TagRepository;
        IUnitOfWork _UnitOfWork;
        public TagService(ITagRepository TagRepository, IUnitOfWork UnitOfWork)
        {
            this._TagRepository = TagRepository;
            this._UnitOfWork = UnitOfWork;
        }
        public void Add(Tag Tag)
        {
            _TagRepository.Add(Tag);
        }

        public void Delete(int id)
        {
            _TagRepository.Delete(id);
        }

        public IEnumerable<Tag> Getall()
        {
            return _TagRepository.GetAll();
        }

        public Tag GetById(int id)
        {
            return _TagRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _UnitOfWork.Commit();
        }

        public void Update(Tag Tag)
        {
            _TagRepository.Update(Tag);
        }
    }
}
