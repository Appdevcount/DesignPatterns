using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare a Unity Container
            var unityContainer = new UnityContainer();

            // Register IGame so when dependecy is detected , it provides a TrivialPursuit instance
            unityContainer.RegisterType<IGame, TrivialPursuit>();

            // Instance a Table class object through Unity
            var table = unityContainer.Resolve<Table>();

            table.AddPlayer();
            table.AddPlayer();
            table.Play();

            Console.WriteLine(table.GameStatus());

            Console.ReadLine();
            // Inject a property when dependency is resolved
            InjectionProperty injectionProperty = new InjectionProperty("Name", "Trivial Pursuit Genus Edition");
            unityContainer.RegisterType<IGame, TrivialPursuit>(injectionProperty);

            // Override the constructor parameter of Table class
            var table2 = unityContainer.Resolve<Table>(new ParameterOverride("game", new TicTacToe()));

            table2.AddPlayer();
            table2.AddPlayer();
            table2.Play();

            Console.WriteLine(table2.GameStatus());
            Console.ReadLine();
        }
    }
}


//Once this is installed, this will add 2 files, UnityConfig.cs and UnityMvcActivator.cs class in MVC. You can find both the classes in App_Start folder.
//We only need to make changes into UnityConfig.cs file, so open the same and find the RegisterTypes method.If you notice, there are some comments, there are 2 options to configure DI (Dependency Injection) either using web.cofig file or using code. We will see the second option using code.
//You will find
//// TODO: Register your types here<br />
//// container.RegisterType<IProductRepository, ProductRepository>();
//just below that add, we will configure our classes.

//using Microsoft.Practices.Unity;
//using StudentApp.Repository;
//using StudentApp.Repository.Interface;
//using StudentApp.Service;
//using StudentApp.Service.Interface;
//using System;

//namespace StudentApp.App_Start
//{
//    /// <summary>
//    /// Specifies the Unity configuration for the main container.
//    /// </summary>
//    public class UnityConfig
//    {
//        #region Unity Container
//        private static Lazy<IUnityContainer>
//        container = new Lazy<IUnityContainer>(() =>
//        {
//            var container = new UnityContainer();
//            RegisterTypes(container);
//            return container;
//        });

//        /// <summary>
//        /// Gets the configured Unity container.
//        /// </summary>
//        public static IUnityContainer GetConfiguredContainer()
//        {
//            return container.Value;
//        }
//        #endregion

//        /// <summary>Registers the type mappings with the Unity container.</summary>
//        /// <param name="container">The unity container to configure.</param>
//        /// <remarks>There is no need to register concrete types 
//        /// such as controllers or API controllers (unless you want to 
//        /// change the defaults), as Unity allows resolving a concrete type 
//        /// even if it was not previously registered.</remarks>
//        public static void RegisterTypes(IUnityContainer container)
//        {
//            // NOTE: To load from web.config uncomment the line below. 
//            // Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
//            // container.LoadConfiguration();

//            // TODO: Register your types here
//            // container.RegisterType<IProductRepository, ProductRepository>();
//            container.RegisterType<IStudentService, StudentService>();
//            container.RegisterType<IStudentRepository, StudentRepository>();
//        }
//    }
//}