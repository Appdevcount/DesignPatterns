using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class RepositoryPattern
    {
    }
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public bool inStock { get; set; }
    }
    public interface IProductRepository
    {
        void Add(Product p);
        void Edit(Product p);
        void Remove(int Id);
        IEnumerable GetProducts();
        Product FindById(int Id);
    }
    public class ProductContext : DbContext
    {
        public ProductContext()
           : base("name=ProductAppConnectionString")
        //<add name = "ProductAppConnectionString" connectionString="Data Source=(LocalDb)\v11.0;Initial Catalog=ProductAppJan;Integrated Security=True;MultipleActiveResultSets=true" providerName="System.Data.SqlClient"/>
        {
        }
        public DbSet<Product> Products { get; set; }
    }
    public class ProductInitalizeDB : System.Data.Entity.DropCreateDatabaseIfModelChanges<ProductContext>
    {

        protected override void Seed(ProductContext context)
        {
            context.Products.Add(new Product { Id = 1, Name = "Rice", inStock = true, Price = 30 });
            context.Products.Add(new Product { Id = 2, Name = "Sugar", inStock = false, Price = 40 });
            context.SaveChanges();
            base.Seed(context);
        }
    }
    public class ProductRepository : IProductRepository
    {
        ProductContext context = new ProductContext();
        public void Add(Product p)
        {
            context.Products.Add(p);
            context.SaveChanges();
        }

        public void Edit(Product p)
        {
            context.Entry(p).State = System.Data.Entity.EntityState.Modified;
        }

        public Product FindById(int Id)
        {
            var result = (from r in context.Products where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetProducts() { return context.Products; }
        public void Remove(int Id)
        {
            Product p = context.Products.Find(Id);
            context.Products.Remove(p);
            context.SaveChanges();
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

//[TestClass]
//public class ProductRepositoryTest
//{
//    ProductRepository Repo;
//    [TestInitialize]
//    public void TestSetup()
//    {
//        ProductInitalizeDB db = new ProductInitalizeDB();
//        System.Data.Entity.Database.SetInitializer(db);
//        Repo = new ProductRepository();
//    }
//    [TestMethod]
//    public void IsRepositoryInitalizeWithValidNumberOfData()
//    {
//        var result = Repo.GetProducts();
//        Assert.IsNotNull(result);
//        var numberOfRecords = result.ToList().Count;
//        Assert.AreEqual(2, numberOfRecords);
//    }
//    [TestMethod]
//    public void IsRepositoryAddsProduct()
//    {
//        Product productToInsert = new Product
//        {
//            Id = 3,
//            inStock = true,
//            Name = "Salt",
//            Price = 17

//        };
//        Repo.Add(productToInsert);
//        // If Product inserts successfully, 
//        //number of records will increase to 3 
//        var result = Repo.GetProducts();
//        var numberOfRecords = result.ToList().Count;
//        Assert.AreEqual(3, numberOfRecords);
//    }
//}