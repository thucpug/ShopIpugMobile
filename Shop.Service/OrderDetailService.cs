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
    public interface IOrderDetailService
    {
        void Add(OrderDetail t);
        void Update(OrderDetail t);
        void Delete(int id);
        OrderDetail GetById(int id);
        IEnumerable<OrderDetail> Getall();
        void SaveChanges();
    }
   public class OrderDetailService : IOrderDetailService
    {
        IOrderDetailRepository _OrderDetailRepository;
        IUnitOfWork _UnitOfWork;
        public OrderDetailService(IOrderDetailRepository x1, IUnitOfWork UnitOfWork)
        {
            this._OrderDetailRepository = x1;
            this._UnitOfWork = UnitOfWork;
        }
        public void Add(OrderDetail t)
        {
            _OrderDetailRepository.Add(t);
        }

        public void Delete(int id)
        {
            _OrderDetailRepository.Delete(id);
        }

        public IEnumerable<OrderDetail> Getall()
        {
          return  _OrderDetailRepository.GetAll();
        }

        public OrderDetail GetById(int id)
        {
            return _OrderDetailRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _UnitOfWork.Commit();
        }

        public void Update(OrderDetail t)
        {
            _OrderDetailRepository.Update(t);
        }
    }
}
