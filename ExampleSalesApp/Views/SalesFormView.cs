using ExampleSalesApp.Controllers;
using ExampleSalesApp.Models;
using ExampleSalesApp.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleSalesApp
{
    public partial class SalesFormView : Form, ISalesFormView
    {
        ISalesFormController _controller = null;

        public string CustomerFirstname
        {
            get
            {
                return this.textBoxFirstname.Text;
            }
        }

        public string CustomerLastname
        {
            get
            {
                return this.textBoxLastname.Text;
            }
        }

        public SalesFormView()
        {
            InitializeComponent();
        }

        public void ClearCustomerList()
        {
            listBox1.Items.Clear();
        }

        public void AddUserToList(Customer c)
        {
            listBox1.DisplayMember = "Fullname";
            listBox1.ValueMember = "Id";
            listBox1.Items.Add(c);
        }

        public void SetController(ISalesFormController controller)
        {
            _controller = controller;
        }

        public void ClearCustomerInfo()
        {
            this.textBoxFirstname.Text = String.Empty;
            this.textBoxLastname.Text = String.Empty;
            this.label2.Text = string.Empty;
        }

        public void ShowCustomerInfo(Customer c)
        {
            this.textBoxFirstname.Text = c.Firstname;
            this.textBoxLastname.Text = c.Surname;
            this.label2.Text = c.Id.ToString();
            this.listBox1.SelectedItem = c;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _controller.SelectedCustomerChanged(((sender as ListBox).SelectedItem as Customer).Id);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _controller.NewCustomer();
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            _controller.SaveChanges();
        }
   
        public void EnableSaveChanges(bool enable)
        {
            this.buttonSaveChanges.Enabled = enable;
            this.textBoxFirstname.Enabled = enable;
            this.textBoxLastname.Enabled = enable;
        }

        public void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        public bool DisplayQuestion(string question)
        {
            return MessageBox.Show(question, "Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            _controller.DeleteCustomer();
        }

        public void EnableDeleteCustomer(bool enable)
        {
            this.buttonDeleteCustomer.Enabled = enable;
        }

        public void FocusCustomerEditFields()
        {
            this.textBoxFirstname.Focus();
        }
    }
}
