//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

using ExampleSalesApp.Controllers;
using ExampleSalesApp.Models;
using ExampleSalesApp.Repositories;
using ExampleSalesApp.Views;
using System;
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

            container.RegisterType<IRepository<Customer>, CustomerEFRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISalesFormView, SalesFormView>();
            container.RegisterType<ISalesFormController, SalesFormController>();
            container.RegisterType<ISalesModel, SalesModel>();

            ISalesFormController controller = container.Resolve<ISalesFormController>();

            Form form = controller.GetView() as Form;


            Application.Run(form);
        }
    }
}
