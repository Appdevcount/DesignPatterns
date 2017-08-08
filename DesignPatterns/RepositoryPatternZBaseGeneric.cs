using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DesignPatterns
{
    class RepositoryPatternZBaseGeneric
    {
        //Link has Complete Most Generic Repository Pattern implementation - https://code.msdn.microsoft.com/generic-repository-pattern-ddea2262 
        //Check in the BO_WEB project solution from the downloaded zip from above

    }
    public interface IZRepository<T> where T : class
    {
        /// <summary>
        /// Get a selected extiry by the object primary key ID
        /// </summary>
        /// <param name="id">Primary key ID</param>
        T GetSingle(Expression<Func<T, bool>> whereCondition);
        /// <summary> 
        /// Add entity to the repository 
        /// </summary> 
        /// <param name="entity">the entity to add</param> 
        /// <returns>The added entity</returns> 
        void Add(T entity);
        void Insert(T entity);
        void Update(T entity, T NewEntity);
        T Insert(T entity, bool status);
        /// <summary> 
        /// Mark entity to be deleted within the repository 
        /// </summary> 
        /// <param name="entity">The entity to delete</param> 
        void Delete(T entity);
        /// <summary> 
        /// Updates entity within the the repository 
        /// </summary> 
        /// <param name="entity">the entity to update</param> 
        /// <returns>The updates entity</returns> 
        void Attach(T entity);
        /// <summary> 
        /// Load the entities using a linq expression filter
        /// </summary> 
        /// <typeparam name="E">the entity type to load</typeparam> 
        /// <param name="where">where condition</param> 
        /// <returns>the loaded entity</returns> 
        IList<T> GetAll(Expression<Func<T, bool>> whereCondition);
        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();
        /// <summary> 
        /// Query entities from the repository that match the linq expression selection criteria
        /// </summary> 
        /// <typeparam name="E">the entity type to load</typeparam> 
        /// <param name="where">where condition</param> 
        /// <returns>the loaded entity</returns> 
        IQueryable<T> GetQueryable();
        /// <summary>
        /// Count using a filer
        /// </summary>
        long Count(Expression<Func<T, bool>> whereCondition);
        /// <summary>
        /// All item count
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        long Count();
    }

    public abstract class RepositoryBase<T> : IZRepository<T>
    where T : class
    {
        public abstract Expression<Func<T, bool>> SearchFilters(T obj);
        public abstract Expression<Func<T, bool>> GetFilters();
        //internal DbSet<TEntity> dbSet;
        private IDbSet<T> _objectSet;
        //internal MVCEntities context;
        private IRepositoryContext _Context;
        public RepositoryBase() : this(new BORepositoryContext())
        {
        }
        public RepositoryBase(IRepositoryContext repositoryContext)
        {
            //this.context = context;
            repositoryContext = repositoryContext ?? new BORepositoryContext(); //Commentable line
            _objectSet =  repositoryContext.GetObjectSet<T>(); //context.Set<TEntity>();
            _Context = repositoryContext;
        }
        public IDbSet<T> ObjectSet
        {
            get
            {
                return _objectSet;
            }
        }
        #region IRepository Members
        public void Add(T entity)
        {
            this.ObjectSet.Add(entity);
        }
        public void Insert(T entity)
        {
            this.ObjectSet.Add(entity);
            _Context.SaveChanges();
        }

        public void Update(T entity, T NewEntity)
        {
            this.ObjectSet.Attach(entity);
            _Context.ObjectContext.Entry(entity).CurrentValues.SetValues(NewEntity);
            _Context.ObjectContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.ObjectSet.Remove(entity);
            _Context.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return this.ObjectSet.ToList<T>();
        }

        public IList<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return this.ObjectSet.Where(whereCondition).ToList<T>();
        }

        public T GetSingle(Expression<Func<T, bool>> whereCondition)
        {
            return this.ObjectSet.Where(whereCondition).FirstOrDefault<T>();
        }

        public void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);
        }

        public IQueryable<T> GetQueryable()
        {
            return this.ObjectSet.AsQueryable<T>();
        }

        public long Count()
        {
            return this.ObjectSet.LongCount<T>();
        }

        public long Count(Expression<Func<T, bool>> whereCondition)
        {
            return this.ObjectSet.Where(whereCondition).LongCount<T>();
        }

        #endregion

        #region IRepository Member witn Return Entities
        public T Insert(T entity, bool status)
        {
            this.ObjectSet.Add(entity);
            _Context.SaveChanges();
            return entity;
        }
        #endregion
    }
    ///// <summary>
    ///// Context across all repositories
    ///// </summary>
    //public interface IRepositoryContext
    //{

    //    IDbSet<T> GetObjectSet<T>() where T : class;

    //    DbContext ObjectContext { get; }

    //    /// <summary>
    //    /// Save all changes to all repositories
    //    /// </summary>
    //    /// <returns>Integer with number of objects affected</returns>
    //    int SaveChanges();

    //    /// <summary>
    //    /// Terminates the current repository context
    //    /// </summary>
    //    void Terminate();
    //}
    //public class BORepositoryContext : IRepositoryContext
    //{
    //    private const string OBJECT_CONTEXT_KEY = "BO.Dal.EntityModels";
    //    public IDbSet<T> GetObjectSet<T>()
    //        where T : class
    //    {
    //        return ContextManager.GetObjectContext(OBJECT_CONTEXT_KEY).Set<T>();
    //    }

    //    /// <summary>
    //    /// Returns the active object context
    //    /// </summary>
    //    public DbContext ObjectContext
    //    {
    //        get
    //        {
    //            return ContextManager.GetObjectContext(OBJECT_CONTEXT_KEY);
    //        }
    //    }

    //    public int SaveChanges()
    //    {
    //        return this.ObjectContext.SaveChanges();
    //    }

    //    public void Terminate()
    //    {
    //        ContextManager.SetRepositoryContext(null, OBJECT_CONTEXT_KEY);
    //    }

    //}
    ///// <summary>
    ///// Manages the lifecycle of the EF's object context
    ///// </summary>
    ///// <remarks>Uses a context per http request approach or one per thread in non web applications</remarks>
    //public static class ContextManager
    //{
    //    #region Private Members

    //    // accessed via lock(_threadObjectContexts), only required for multi threaded non web applications
    //    private static readonly Hashtable _threadObjectContexts = new Hashtable();

    //    #endregion

    //    public static IDbSet<T> GetObjectSet<T>(T entity, string contextKey)
    //        where T : class
    //    {
    //        return GetObjectContext(contextKey).Set<T>();
    //    }
    //    /// <summary>
    //    /// Returns the active object context
    //    /// </summary>
    //    public static DbContext GetObjectContext(string contextKey)
    //    {
    //        DbContext objectContext = GetCurrentObjectContext(contextKey);
    //        if (objectContext == null) // create and store the object context
    //        {
    //            objectContext = new DEV_BackOffice_2016Entities();
    //            StoreCurrentObjectContext(objectContext, contextKey);
    //        }
    //        return objectContext;
    //    }

    //    /// <summary>
    //    /// Gets the repository context
    //    /// </summary>
    //    /// <returns>An object representing the repository context</returns>
    //    public static object GetRepositoryContext(string contextKey)
    //    {
    //        return GetObjectContext(contextKey);
    //    }

    //    /// <summary>
    //    /// Sets the repository context
    //    /// </summary>
    //    /// <param name="repositoryContext">An object representing the repository context</param>
    //    public static void SetRepositoryContext(object repositoryContext, string contextKey)
    //    {
    //        if (repositoryContext == null)
    //        {
    //            RemoveCurrentObjectContext(contextKey);
    //        }
    //        else if (repositoryContext is DbContext)
    //        {
    //            StoreCurrentObjectContext((DbContext)repositoryContext, contextKey);
    //        }
    //    }


    //    #region Object Context Lifecycle Management

    //    /// <summary>
    //    /// gets the current object context 		
    //    /// </summary>
    //    private static DbContext GetCurrentObjectContext(string contextKey)
    //    {
    //        DbContext objectContext = null;
    //        //if (HttpContext.Current == null)
    //        //    objectContext = GetCurrentThreadObjectContext(contextKey);
    //        //else
    //            objectContext = GetCurrentHttpContextObjectContext(contextKey);
    //        return objectContext;
    //    }

    //    /// <summary>
    //    /// sets the current session 		
    //    /// </summary>
    //    private static void StoreCurrentObjectContext(DbContext objectContext, string contextKey)
    //    {
    //        //if (HttpContext.Current == null)
    //        //    StoreCurrentThreadObjectContext(objectContext, contextKey);
    //        //else
    //            StoreCurrentHttpContextObjectContext(objectContext, contextKey);
    //    }

    //    /// <summary>
    //    /// remove current object context 		
    //    /// </summary>
    //    private static void RemoveCurrentObjectContext(string contextKey)
    //    {
    //        //if (HttpContext.Current == null)
    //        //    RemoveCurrentThreadObjectContext(contextKey);
    //        //else
    //            RemoveCurrentHttpContextObjectContext(contextKey);
    //    }

    //    #region private methods - HttpContext related

    //    /// <summary>
    //    /// gets the object context for the current thread
    //    /// </summary>
    //    private static DbContext GetCurrentHttpContextObjectContext(string contextKey)
    //    {
    //        DbContext objectContext = null;
    //        if (HttpContext.Current.Items.Contains(contextKey))
    //            objectContext = (DbContext)HttpContext.Current.Items[contextKey];
    //        return objectContext;
    //    }

    //    private static void StoreCurrentHttpContextObjectContext(DbContext objectContext, string contextKey)
    //    {
    //        if (HttpContext.Current.Items.Contains(contextKey))
    //            HttpContext.Current.Items[contextKey] = objectContext;
    //        else
    //            HttpContext.Current.Items.Add(contextKey, objectContext);
    //    }

    //    /// <summary>
    //    /// remove the session for the currennt HttpContext
    //    /// </summary>
    //    private static void RemoveCurrentHttpContextObjectContext(string contextKey)
    //    {
    //        DbContext objectContext = GetCurrentHttpContextObjectContext(contextKey);
    //        if (objectContext != null)
    //        {
    //            HttpContext.Current.Items.Remove(contextKey);
    //            objectContext.Dispose();
    //        }
    //    }

    //    #endregion

    //    #region private methods - ThreadContext related

    //    /// <summary>
    //    /// gets the session for the current thread
    //    /// </summary>
    //    //private static DbContext GetCurrentThreadObjectContext(string contextKey)
    //    //{
    //    //    DbContext objectContext = null;
    //    //    Thread threadCurrent = Thread.CurrentThread;
    //    //    if (threadCurrent.Name == null)
    //    //        threadCurrent.Name = contextKey;
    //    //    else
    //    //    {
    //    //        object threadObjectContext = null;
    //    //        lock (_threadObjectContexts.SyncRoot)
    //    //        {
    //    //            threadObjectContext = _threadObjectContexts[contextKey];
    //    //        }
    //    //        if (threadObjectContext != null)
    //    //            objectContext = (DbContext)threadObjectContext;
    //    //    }
    //    //    return objectContext;
    //    //}

    //    //private static void StoreCurrentThreadObjectContext(DbContext objectContext, string contextKey)
    //    //{
    //    //    lock (_threadObjectContexts.SyncRoot)
    //    //    {
    //    //        if (_threadObjectContexts.Contains(contextKey))
    //    //            _threadObjectContexts[contextKey] = objectContext;
    //    //        else
    //    //            _threadObjectContexts.Add(contextKey, objectContext);
    //    //    }
    //    //}

    //    //private static void RemoveCurrentThreadObjectContext(string contextKey)
    //    //{
    //    //    lock (_threadObjectContexts.SyncRoot)
    //    //    {
    //    //        if (_threadObjectContexts.Contains(contextKey))
    //    //        {
    //    //            DbContext objectContext = (DbContext)_threadObjectContexts[contextKey];
    //    //            if (objectContext != null)
    //    //            {
    //    //                objectContext.Dispose();
    //    //            }
    //    //            _threadObjectContexts.Remove(contextKey);
    //    //        }
    //    //    }
    //    //}

    //    /*
    //    private static string BuildContextThreadName()
    //    {
    //        return Thread.CurrentThread.Name;
    //    }

    //    private static string BuildHttpContextName()
    //    {
    //        return OBJECT_CONTEXT_KEY;
    //    }*/

    //    #endregion

    //    #endregion


    //}


    //public partial class DEV_BackOffice_2016Entities : DbContext
    //{
    //    public DEV_BackOffice_2016Entities()
    //        : base("name=DEV_BackOffice_2016Entities")
    //    {
    //    }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        throw new UnintentionalCodeFirstException();
    //    }

    //    //public virtual DbSet<Identity_MenuList> Identity_MenuList { get; set; }
    //    //public virtual DbSet<Identity_RoleDetail> Identity_RoleDetail { get; set; }
    //    //public virtual DbSet<Identity_RoleMaster> Identity_RoleMaster { get; set; }
    //    //public virtual DbSet<Identity_User> Identity_User { get; set; }
    //}

}
