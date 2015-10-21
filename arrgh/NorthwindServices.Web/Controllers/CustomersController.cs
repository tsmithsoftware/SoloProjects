using System.Collections.Generic;
using System.Linq;
using NorthwindServices.Data;
using System.Web.Http;
using NorthwindServices.Entities;
using System;

namespace NorthwindServices.Web.Controllers
{
    public class CustomersController : ApiController
    {
       NorthwindDbContext _Context = new NorthwindDbContext();

        public IEnumerable<Customer> GetCustomers()
        {
            return _Context.Customers.ToArray();//disposable object, should dispose
        }

        //Obtain single customer from id, using optional parameter in route mapping
        public Customer GetCustomer(string id)
        {
            return _Context.Customers.FirstOrDefault(c => c.CustomerID == id);
        }

        //PUT/POST/DELETE
        //AddCustomer
        public void PostCustomer(Customer customer)
        {
            _Context.Customers.Add(customer);
        }

        //id based on route parameter, modify customers
        public void PutCustomer(string id, Customer customer)
        {
            var matchingCustomer = _Context.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (matchingCustomer != null)
            {
                SetChangedProperties(matchingCustomer, customer);
                _Context.SaveChanges();
            }
        }

        private void SetChangedProperties(Customer matchingCustomer, Customer customer)
        {
            //Attaches the given entity to the context underlying the set. That is, the entity is placed into the context in the Unchanged state, just as if it had been read from the database.
            _Context.Customers.Attach(customer);
            _Context.SaveChanges();
        }

        //DeleteCustomer
        public void DeleteCustomer(string id)
        {
            var matchingCustomer = _Context.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (matchingCustomer != null)
            {
                _Context.Customers.Remove(matchingCustomer);
                _Context.SaveChanges();
            }
        }

        //disposable object so
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _Context.Dispose();
        }
    }
}