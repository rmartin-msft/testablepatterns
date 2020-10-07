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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SalesAppUnitTest
{
    [TestClass]
    public class SalesFormUnitTests
    {
        /// <summary>
        /// Test that the model is updated and the view is notified when a new customer is added
        /// but not saved.
        /// </summary>
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

        /// <summary>
        /// Test that a new customer is added to the model when a new customer is 
        /// added with the expected name.
        /// </summary>
        [TestMethod]
        public void TestAddNewCustomerAndSave()
        {
            var view = new Mock<ISalesFormView>();
            ISalesModel model = new SalesModel(new CustomerRepository());
            view.Setup(x => x.CustomerFirstname).Returns("Anthony");
            view.Setup(x => x.CustomerLastname).Returns("Taylor");

            ISalesFormController controller = new SalesFormController(view.Object, model);

            controller.NewCustomer();

            view.Verify(x => x.ShowCustomerInfo(It.IsAny<Customer>()));

            // Hit the save changes button
            controller.SaveChanges();

            // confirm that we have saves a new customer with the expected name
            Customer c = model.FindCustomerById(model.CurrentCustomer.Id);

            Assert.IsTrue(c.Firstname == "Anthony");
            Assert.IsTrue(c.Surname == "Taylor");
        }

        /// <summary>
        /// Test that the right customer details are updated when a customer is selected
        /// and the names are changed and the save changes button is pressed.
        /// </summary>
        [TestMethod]
        public void TestChangeCustomerDetails()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();
            customerRepo.Insert(new Customer("Anthony", "Lucas"));
            customerRepo.Insert(new Customer("Sophia", "Rees"));
            customerRepo.Insert(new Customer("Thomas", "Perkins"));

            ISalesModel model = new SalesModel(customerRepo);
            model.CurrentCustomer = customerRepo.GetAll()[1];

            var view = new Mock<ISalesFormView>();
            view.Setup(x => x.CustomerFirstname).Returns("Victoria");
            view.Setup(x => x.CustomerLastname).Returns("Briggs");

            ISalesFormController controller = new SalesFormController(view.Object, model);

            controller.SaveChanges();

            // verify that the controller notifies the view to show the customer
            view.Verify(x => x.ShowCustomerInfo(It.IsAny<Customer>()));

            // confirm that the customer is set to 'new customer'
            Assert.IsTrue(model.CurrentCustomer.Firstname == "Victoria");
            Assert.IsTrue(model.CurrentCustomer.Surname == "Briggs");

            // Verify that the model has been updated.
            Assert.IsTrue(model.GetAllCustomers()[1].Firstname == "Victoria");
            Assert.IsTrue(model.GetAllCustomers()[1].Surname == "Briggs");
        }

        /// <summary>
        /// Test that the expected customer is deleted 
        /// </summary>
        [TestMethod]
        public void TestDeleteCustomerConfirmPrompt()
        {
            IRepository<Customer> repo = new CustomerRepository();
            repo.Insert(new Customer() { Firstname = "Delete", Surname = "Me" });
            repo.Insert(new Customer() { Firstname = "Don't Delete", Surname = "Me" });
            ISalesModel model = new SalesModel(repo);
            model.CurrentCustomer = model.GetAllCustomers()[0];

            // setup the view, so that the displayed question to confirm to delete 
            // the custoemr is confirmed.
            var view = new Mock<ISalesFormView>();
            view.Setup(x => x.DisplayQuestion(It.IsAny<string>())).Returns(true);

            ISalesFormController controller = new SalesFormController(view.Object, model);

            controller.DeleteCustomer();

            // verify the view was notified for confirmation to delete the customer
            view.Verify(x => x.DisplayQuestion(It.IsAny<string>()), Times.Once);
            view.Verify(x => x.ClearCustomerInfo(), Times.AtLeastOnce);

            // confirm there is only one customer in the repository now.

            Assert.IsTrue(model.GetAllCustomers().Count == 1);
        }

        /// <summary>
        /// Test the the view is notified when the delete customer option is triggered and
        /// that the customer is deleted when the UI triggersd 
        /// </summary>
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

        /// <summary>
        /// verify that the expected customer information is shown when a customer is selected       
        /// </summary>
        [TestMethod]
        public void TestSelectCustomer()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();
            customerRepo.Insert(new Customer("Anthony", "Lucas"));
            customerRepo.Insert(new Customer("Sophia", "Rees"));
            customerRepo.Insert(new Customer("Thomas", "Perkins"));

            ISalesModel model = new SalesModel(customerRepo);
            var view = new Mock<ISalesFormView>();

            ISalesFormController controller = new SalesFormController(view.Object, model);

            // check for each customer, that the view is notified when the selection changes.

            for (int selectedCustomer = 0; selectedCustomer < customerRepo.GetAll().Length; selectedCustomer++)
            {
                controller.SelectedCustomerChanged(customerRepo.GetAll()[selectedCustomer].Id);

                // verify that we clear the customer and show the expected customer.
                view.Verify(x => x.ClearCustomerInfo(), Times.Exactly(1 + selectedCustomer)); //  ;
                view.Verify(x => x.ShowCustomerInfo(It.Is<Customer>(s => s.Id == customerRepo.GetAll()[selectedCustomer].Id)));

                Assert.IsTrue(model.CurrentCustomer == customerRepo.GetById(model.CurrentCustomer.Id));
            }
        }

        /// <summary>
        /// test that the delete customer button is disabled when NO customer is selected
        /// </summary>
        [TestMethod]
        public void TestDeleteCustomerDisabledNoSelection()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();
            customerRepo.Insert(new Customer("Anthony", "Lucas"));
            customerRepo.Insert(new Customer("Sophia", "Rees"));
            customerRepo.Insert(new Customer("Thomas", "Perkins"));

            ISalesModel model = new SalesModel(customerRepo);
            var view = new Mock<ISalesFormView>();
            model.CurrentCustomer = null;

            ISalesFormController controller = new SalesFormController(view.Object, model);

            view.Verify(x => x.EnableDeleteCustomer(It.Is<bool>(y => y == false)), Times.Once);
        }

        /// <summary>
        /// Test that the delete customer button is enabled when a customer is selected
        /// </summary>
        [TestMethod]
        public void TestDeleteCustomerEnabledWithSelection()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();
            customerRepo.Insert(new Customer("Anthony", "Lucas"));
            customerRepo.Insert(new Customer("Sophia", "Rees"));
            customerRepo.Insert(new Customer("Thomas", "Perkins"));

            ISalesModel model = new SalesModel(customerRepo);
            var view = new Mock<ISalesFormView>();

            model.CurrentCustomer = model.GetAllCustomers()[0];

            ISalesFormController controller = new SalesFormController(view.Object, model);

            view.Verify(x => x.EnableDeleteCustomer(It.Is<bool>(y => y == true)));
        }

        /// <summary>
        /// Test that when a new customer is selected, the delete option is disabled.
        /// </summary>
        [TestMethod]
        public void TestDeleteDisableWithNewCustomerSelected()
        {
            IRepository<Customer> customerRepo = new CustomerRepository();

            ISalesModel model = new SalesModel(customerRepo);
            var view = new Mock<ISalesFormView>();
            bool? deleteCustomerEnabled = null;

            // Setup the view to monitor what parameters have been passed to the view mock
            view.Setup(x => x.EnableDeleteCustomer(It.IsAny<bool>()))
                .Callback<bool>((b) => deleteCustomerEnabled = b);

            ISalesFormController controller = new SalesFormController(view.Object, model);
            controller.NewCustomer();

            // verify that the EnableDeleteCustomer was called on the view and the last state was false 
            // to reflect that the new customer cannot be deleted from the database

            view.Verify(x => x.EnableDeleteCustomer(It.Is<bool>(y => y == false)), Times.AtLeastOnce);
            Assert.IsTrue(deleteCustomerEnabled == false, "Delete Customer Button is enabled for new customer");
        }
    }
}
