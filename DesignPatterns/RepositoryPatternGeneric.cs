using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class RepositoryPatternGeneric
    {
    }
    public class User
    {
        public int UserId { get; set; }
        public int Address { get; set; }
        public int Company { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public int Designation { get; set; }
        public int EMail { get; set; }
        public int PhoneNo { get; set; }
    }
    public class MVCEntities : DbContext
    {
        public MVCEntities()
           : base("name=ProductAppConnectionString")
        //<add name = "ProductAppConnectionString" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=ProductAppJan;Integrated Security=True;MultipleActiveResultSets=true" providerName="System.Data.SqlClient"/>
        {
        }
        public DbSet<User> Users { get; set; }
    }
    public interface IUserRepository : IDisposable
    {
        IEnumerable GetUsers();
        User GetUserByID(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
   
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal MVCEntities context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(MVCEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = dbSet;
            return query.ToList();
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
    public class UnitOfWork : IDisposable
    {
        private MVCEntities context = new MVCEntities();
        private GenericRepository<User> userRepository;
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new GenericRepository<User>(context);
                return userRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
