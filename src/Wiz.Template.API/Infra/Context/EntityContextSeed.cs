using System.Collections.Generic;
using System.Linq;
using Wiz.Template.API.Models;

namespace Wiz.Template.API.Infra.Context
{
    public class EntityContextSeed
    {
        public void SeedInitial(EntityContext context)
        {
            if (!context.Customers.Any())
            {
                var customers = new List<Customer>()
                {
                    new Customer(name: "Zier Zuveiku"),
                    new Customer(name: "Vikehel Pleamakh"),
                    new Customer(name: "Diuor PleaBolosmakh")
                };

                context.AddRange(customers);
                context.SaveChanges();
            }
        }
    }
}
