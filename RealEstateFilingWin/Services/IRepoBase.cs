using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateFilingWin.Services
{
    public interface IRepoBase<T>
    {

        T FindByID(long ID);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        OperationResult Create(T entity);
        OperationResult Update(T entity);
        OperationResult Delete(T entity);
        //OperationResult AddLoge(long UserID, OperationType operationType, DateTime dateTime);
    }


    public interface IRepoFullBase<T>
    {

        T FindByID(long ID);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        OperationResult Create(T entity);
        OperationResult Update(T entity);
        OperationResult Delete(T entity);
        //OperationResult AddLoge(long UserID, OperationType operationType, DateTime dateTime);
    }
}
