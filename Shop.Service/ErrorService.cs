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
    public interface IErrorService
    {
        void Create(Error er);
        void SaveChange();
    }
    public class ErrorService : IErrorService
    {
        IErrorRepository _ErrorReponsitory;
        IUnitOfWork _unitOfWork;
        public ErrorService(IErrorRepository postcategoryreponsitory, IUnitOfWork unitOfWork)
        {
            this._ErrorReponsitory = postcategoryreponsitory;
            this._unitOfWork = unitOfWork;
        }
        public void Create(Error er)
        {
            _ErrorReponsitory.Add(er);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
