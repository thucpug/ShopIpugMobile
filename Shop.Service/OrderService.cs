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
    public interface IOrderService
    {
        void Add(Order t);
        void Update(Order t);
        void Delete(int id);
        Order GetById(int id);
        IEnumerable<Order> Getall();
        void SaveChanges();
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _OrderRepository;
        IUnitOfWork _UnitOfWork;
        public OrderService(IOrderRepository x1, IUnitOfWork UnitOfWork)
        {
            this._OrderRepository = x1;
            this._UnitOfWork = UnitOfWork;
        }
        public void Add(Order t)
        {
            _OrderRepository.Add(t);
        }

        public void Delete(int id)
        {
            _OrderRepository.Delete(id);
        }

        public IEnumerable<Order> Getall()
        {
            return _OrderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            return _OrderRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _UnitOfWork.Commit();
        }

        public void Update(Order t)
        {
            _OrderRepository.Update(t);
        }
    }
}
