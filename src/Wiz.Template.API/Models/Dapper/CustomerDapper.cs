using System;

namespace Wiz.Template.API.Models.Dapper
{
    public class CustomerDapper
    {
        private CustomerDapper() { }
        public CustomerDapper(int id, string name, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime DateCreated { get; private set; }
    }
}
