using ExampleSalesApp.Controllers;
using ExampleSalesApp.Models;
using ExampleSalesApp.Repositories;
using ExampleSalesApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace ExampleSalesApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IUnityContainer container = new UnityContainer();

            container.RegisterType<IRepository<Customer>, 
                        CustomerRepository>(new ContainerControlledLifetimeManager()); ;
            container.RegisterType<ISalesFormView, SalesFormView>();
            container.RegisterType<ISalesFormController, SalesFormController>();
            container.RegisterType<ISalesModel, SalesModel>();

            ISalesFormController controller = container.Resolve<ISalesFormController>();

            Form form = controller.GetView() as Form;
            

            Application.Run(form);
        }
    }
}
