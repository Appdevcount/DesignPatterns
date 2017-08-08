using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class RepositoryPatternUnitOfWork
    {
    }
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable GetAll();
        IEnumerable Find(Expression predicate);
        TEntity Get(object Id);
        void Add(TEntity entity);
        void AddRange(IEnumerable entities);
        void Update(TEntity entity);
        void Remove(object Id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable entities);
    }
    //// implementation
    //public class RepositoryImpl : IRepository<TEntity> where TEntity : class
    //{
    //    protected readonly DbContext db;
    //    public Repository(DbContext _db)
    //    {
    //        db = _db;
    //    }
    //    public IEnumerable GetAll()
    //    {
    //        return db.Set<TEntity>().ToList();
    //    }
    //    public IEnumerable Find(Expression> predicate)
    //    {
    //        return db.Set().Where(predicate);
    //    }
    //    public TEntity Get(object Id)
    //    {
    //        return db.Set().Find(Id);
    //    }
    //    public void Add(TEntity entity)
    //    {
    //        db.Set().Add(entity);
    //    }
    //    public void AddRange(IEnumerable entities)
    //    {
    //        db.Set().AddRange(entities);
    //    }
    //    public void Remove(TEntity entity)
    //    {
    //        db.Set().Remove(entity);
    //    }
    //    public void RemoveRange(IEnumerable entities)
    //    {
    //        db.Set().RemoveRange(entities);
    //    }
    //    public void Remove(object Id)
    //    {
    //        TEntity entity = db.Set().Find(Id);
    //        this.Remove(entity);
    //    }
    //    public void Update(TEntity entity)
    //    {
    //        db.Entry(entity).State = EntityState.Modified;
    //    }
    //}
    //public interface IUnitOfWork : IDisposable
    //{
    //    ICategoryRepository Categories { get; }
    //    IProductRepository Products { get; }

    //    int SaveChanges();
    //}

    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly DatabaseContext db;

    //    public UnitOfWork()
    //    {
    //        db = new DatabaseContext();
    //    }

    //    private ICategoryRepository _Categories;
    //    public ICategoryRepository Categories
    //    {
    //        get
    //        {
    //            if (this._Categories == null)
    //            {
    //                this._Categories = new CategoryRepository(db);
    //            }
    //            return this._Categories;
    //        }
    //    }

    //    private IProductRepository _Products;
    //    public IProductRepository Products
    //    {
    //        get
    //        {
    //            if (this._Products == null)
    //            {
    //                this._Products = new ProductRepository(db);
    //            }
    //            return this._Products;
    //        }
    //    }

    //    public int SaveChanges()
    //    {
    //        return db.SaveChanges();
    //    }

    //    public void Dispose()
    //    {
    //        db.Dispose();
    //    }
    //}
    //http://www.c-sharpcorner.com/UploadFile/b1df45/unit-of-work-in-repository-pattern/

    //Func<T> denotes a delegate that is pretty much a pointer to a method and Expression<Func<T>> denotes a tree data structure for a lambda expression
    //    gets the expression and converts it to the equivalent SQL statement and submits it to the server rather than executing the lambda
    //Country GetCountry(Expression<Func<Country, bool>> linqWhereCountry);
    //public Country GetCountry(Expression<Func<Country, bool>> linqWhereCountry)
    //{
    //    return _context.Countries.Where(linqWhereCountry).FirstOrDefault();
    //}
    //var countryName = _fRepo.GetCountry(linqWhereUserCountry: x => x.UserID == user.ID).Name;
    //for multiple optional where boxes , we can check condition availabilty and then we can pass required predicate alone in parameter
    public class TESTEXPRESSION //Lambda expression for predicate -- https://stackoverflow.com/questions/13769780/how-to-assign-a-value-via-expression
    {
        Expression<Func<Product, bool>> expr1 = null;

        //if(a is not empty and others are emptyboxes)
        Expression<Func<Product, bool>> expr2 = s => s.Name.Contains("");//
        //elseif(b is not empty and others are empty boxes)
        Expression<Func<Product, bool>> expr3 = s => s.Name.Contains("") && s.Price == 2;//
        //elseif(all are not empty boxes for condition values)
        Expression<Func<Product, bool>> expr4 = s => s.Name.Contains("") && s.inStock == true;//
        //else //all are empty
        Expression<Func<Product, bool>> expr5 = null;//


        Func<Product, bool> Fexpr1 = null;


        //Expression is immutable; nothing you can do will change it.The only thing you can do is create a new Expression based on the change(s) you want and 
        //ensure that the relevant variables refer to the new Expression
        //https://stackoverflow.com/questions/14324842/change-expression-delegate-body-at-runtime

        //But achieved assignining value to Expression tree and Func by assigning null value during its declaration and then assigning desired value later
        //Referred similar implementation for predicate with Func in PayitDealerGlobalPayit Project
        public void TESTExpr()
        {
            if (1 == 1)
            {
                expr1 = c => c.Name.Contains("");
            }
            else if (2 == 2)
            {
                expr1 = c => c.Name.Contains("");
            }
            else
            {
                expr1 = c => c.Name.Contains("");
            }
            TESTExprCall(expr1);
        }


        public void TESTExprCall(Expression<Func<Product, bool>> E)
        {
            if (E != null)
            {
            }
        }
        public void TESTFEXPR()
        {
            if (1 == 1)
            {
                Fexpr1 = c => c.Name.Contains("");
            }
            else if (2 == 2)
            {
                Fexpr1 = c => c.Name.Contains("");
            }
            else
            {
                Fexpr1 = c => c.Name.Contains("");
            }
            TESTFEXPRcall(Fexpr1);
        }
        public void TESTFEXPRcall(Func<Product, bool> Fexpr1)
        {
            if (Fexpr1 != null)
            {
            }
        }

    }


    //public static IEnumerable<Employee> GetEmployees(EmployeeDataContext source, Expression<Func<Employee, bool>> predicate, int number)
    //{
    //    return source.Employee.Where(predicate)
    //        .OrderBy(s => s.EmpSal)
    //        .Take(number);
    //}
    //public static IQueryable<Employee> GetEmployees(this IQueryable<Employee> source, Expression<Func<Employee, bool>> predicate, int number)
    //{
    //    return source.Where(predicate)
    //        .OrderBy(s => s.EmpSal)
    //        .Take(number);
    //}
    //IEnumerable gives performance benefits since it doesn't hit the database, rather it works with in-memory collections
    //https://stackoverflow.com/questions/2876616/returning-ienumerablet-vs-iqueryablet

}




