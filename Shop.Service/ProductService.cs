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
    public interface IProductService
    {
        void Add(Product t);
        void Update(Product t);
        void Delete(int id);
        IEnumerable<Product> Getall(string keys);
        Product GetById(int id);
        IEnumerable<Product> Getall();
        IEnumerable<Product> GetProductHot(int top);
        IEnumerable<Product> GetProductByIDPaging(int id, int page, int pagesize, out int totalRow);
        IEnumerable<Product> GetListProductByName(string keyword);
        IEnumerable<Product> GetReatedproduct(int id, int top);
        IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow);
        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        IProductRepository _ProductRepository;
        IUnitOfWork _UnitOfWork;
        public ProductService(IProductRepository xProductRepository, IUnitOfWork UnitOfWork)
        {
            this._ProductRepository = xProductRepository;
            this._UnitOfWork = UnitOfWork;
        }
        public void Add(Product t)
        {
            _ProductRepository.Add(t);
        }

        public void Delete(int id)
        {
            _ProductRepository.Delete(id);
        }

        public IEnumerable<Product> Getall()
        {
            return _ProductRepository.GetAll();
        }

        public IEnumerable<Product> Getall(string keys)
        {
            if (!string.IsNullOrEmpty(keys))
                return _ProductRepository.GetMulti(x => x.Name.Contains(keys) || x.Description.Contains(keys));
            return _ProductRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _ProductRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetListProductByName(string keyword)
        {
            return _ProductRepository.GetMulti(x => x.Name.Contains(keyword));
        }

        public IEnumerable<Product> GetProductByIDPaging(int id, int page, int pagesize, out int totalRow)
        {
            var query = _ProductRepository.GetMulti(x => x.CategoryID == id);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pagesize).Take(pagesize);
        }
        public IEnumerable<Product> GetProductHot(int top)
        {
            return _ProductRepository.GetMulti(x => x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetReatedproduct(int id, int top)
        {
            var product = _ProductRepository.GetSingleById(id);
            return _ProductRepository.GetMulti(x => x.HotFlag == true && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public void SaveChanges()
        {
            _UnitOfWork.Commit();
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow)
        {
            var query = _ProductRepository.GetMulti(x => x.Name.Contains(keyword));
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(Product t)
        {
            _ProductRepository.Update(t);
        }
    }
}
