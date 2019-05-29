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
    public interface IProductCategoryService
    {
        void Add(ProductCategory ProductCategory);
        void Update(ProductCategory ProductCategory);
        void Delete(int id);
        ProductCategory GetById(int id);
        IEnumerable<ProductCategory> Getall();
        IEnumerable<ProductCategory> Getall(string keys);
        void SaveChanges();
    }
    public class ProductCategoryService : IProductCategoryService
    {
        IProductCategoryRepository _ProductCategoryRepository;
        IUnitOfWork _UnitOfWork;
        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository, IUnitOfWork UnitOfWork)
        {
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._UnitOfWork = UnitOfWork;
        }
        public void Add(ProductCategory ProductCategory)
        {
            _ProductCategoryRepository.Add(ProductCategory);
        }

        public void Delete(int id)
        {
            _ProductCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> Getall()
        {
            return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> Getall(string keys)
        {
            if (!string.IsNullOrEmpty(keys))
                return _ProductCategoryRepository.GetMulti(x => x.Name.Contains(keys) || x.Description.Contains(keys));
            return _ProductCategoryRepository.GetAll();
        }

        public ProductCategory GetById(int id)
        {
            return _ProductCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _UnitOfWork.Commit();
        }

        public void Update(ProductCategory ProductCategory)
        {
            _ProductCategoryRepository.Update(ProductCategory);
        }
    }
}
