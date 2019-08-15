using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using ExampleSalesApp.Controllers;
using ExampleSalesApp.Models;
using ExampleSalesApp.Repositories;
using ExampleSalesApp.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SalesAppUnitTest
{
    [TestClass]
    public class ModelUnitTests
    {
        [TestMethod]
        public void TestAddNewCustomer()
        {
            var view = new Mock<ISalesFormView>();
            ISalesModel model = new SalesModel(new CustomerRepository());

            ISalesFormController controller = new SalesFormController(view.Object, model);

            // Press the new-customer button
            controller.NewCustomer();

            // verify that the controller notifies the view to show the customer
            view.Verify(x => x.ShowCustomerInfo(It.IsAny<Customer>()));
            view.Verify(x => x.FocusCustomerEditFields(), Times.Once);

            // confirm that the customer is set to 'new customer'
            Assert.IsTrue(model.CurrentCustomer.Firstname == "New");
            Assert.IsTrue(model.CurrentCustomer.Surname == "Customer");
        }

        [TestMethod]
        public void TestAddNewCustomerAndSave()
        {
            var view = new Mock<ISalesFormView>();
            ISalesModel model = new SalesModel(new CustomerRepository());
            view.Setup(x => x.CustomerFirstname).Returns("Firstname");
            view.Setup(x => x.CustomerLastname).Returns("Lastname");
            
            ISalesFormController controller = new SalesFormController(view.Object, model);

            controller.NewCustomer();

            view.Verify(x => x.ShowCustomerInfo(It.IsAny<Customer>()));

            // Hit the save changes button
            controller.SaveChanges();

            // confirm that we have saves a new customer with the expected name
            Customer c = model.FindCustomerById(model.CurrentCustomer.Id);

            Assert.IsTrue(c.Firstname == "Firstname");
            Assert.IsTrue(c.Surname == "Lastname");                        
        }

        [TestMethod]
        public void TestDeleteCustomer()
        {
            IRepository<Customer> repo = new CustomerRepository();
            repo.Insert(new Customer() { Firstname = "Delete", Surname = "Me" });
            ISalesModel model = new SalesModel(repo);
            model.CurrentCustomer = model.GetAllCustomers()[0];

            var view = new Mock<ISalesFormView>();
            view.Setup(x => x.DisplayQuestion(It.IsAny<string>())).Returns(true);

            ISalesFormController controller = new SalesFormController(view.Object, model);

            controller.DeleteCustomer();
            view.Verify(x => x.DisplayQuestion(It.IsAny<string>()), Times.Once);
            view.Verify(x => x.ClearCustomerInfo(), Times.AtLeastOnce);

            Assert.IsTrue(model.GetAllCustomers().Count == 0);
        }

        [TestMethod]
        public void TestDeleteCustomerDeclinePrompt()
        {
            IRepository<Customer> repo = new CustomerRepository();
            repo.Insert(new Customer() { Firstname = "Delete", Surname = "Me" });
            ISalesModel model = new SalesModel(repo);
            model.CurrentCustomer = model.GetAllCustomers()[0];

            var view = new Mock<ISalesFormView>();
            view.Setup(x => x.DisplayQuestion(It.IsAny<string>())).Returns(false);

            ISalesFormController controller = new SalesFormController(view.Object, model);

            controller.DeleteCustomer();
            view.Verify(x => x.DisplayQuestion(It.IsAny<string>()), Times.Once);

            Assert.IsTrue(model.GetAllCustomers().Count == 1);
        }

        [TestMethod]
        public void TestSelectCustomer()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();
            customerRepo.Insert(new Customer("Donald", "Duck"));
            customerRepo.Insert(new Customer("Mickey", "Mouse"));
            customerRepo.Insert(new Customer("Buggs", "Bunny"));

            ISalesModel model = new SalesModel(customerRepo);
            var view = new Mock<ISalesFormView>();

            ISalesFormController controller = new SalesFormController(view.Object, model);

            for (int selectedCustomer = 0; selectedCustomer < customerRepo.GetAll().Length; selectedCustomer++)
            {
                controller.SelectedCustomerChanged(customerRepo.GetAll()[selectedCustomer].Id);

                // verify that we clear the customer and show the expected customer.
                view.Verify(x => x.ClearCustomerInfo(), Times.AtLeastOnce);
                view.Verify(x => x.ShowCustomerInfo(It.Is<Customer>(s => s.Id == customerRepo.GetAll()[selectedCustomer].Id)));

                Assert.IsTrue(model.CurrentCustomer == customerRepo.GetById(model.CurrentCustomer.Id));
            }
        }

        [TestMethod]
        public void TestDeleteCustomerDisabledNoSelection()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();
            customerRepo.Insert(new Customer("Test", "Customer"));

            ISalesModel model = new SalesModel(customerRepo);
            var view = new Mock<ISalesFormView>();

            ISalesFormController controller = new SalesFormController(view.Object, model);

            view.Verify(x => x.EnableDeleteCustomer(It.Is<bool>(y => y == false)));
        }

        [TestMethod]
        public void TestDeleteCustomerEnabledWithSelection()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();
            customerRepo.Insert(new Customer("Test", "Customer"));

            ISalesModel model = new SalesModel(customerRepo);
            var view = new Mock<ISalesFormView>();

            model.CurrentCustomer = model.GetAllCustomers()[0];

            ISalesFormController controller = new SalesFormController(view.Object, model);

            view.Verify(x => x.EnableDeleteCustomer(It.Is<bool>(y => y == true)));
        }
    }
}
