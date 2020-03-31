using System;
using System.ComponentModel.DataAnnotations;
using Wiz.Template.API.Models.Dapper;

namespace Wiz.Template.API.Models
{
    public class Customer
    {
        private Customer() { }
        public Customer(string name)
        {
            Name = name;
        }

        public Customer(CustomerDapper customerDapper)
        {
            Id = customerDapper.Id;
            Name = customerDapper.Name;
            DateCreated = customerDapper.DateCreated;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Name' é obrigatório")]
        [MaxLength(150, ErrorMessage = "O campo 'Name' deve conter entre 3 e 150 caracteres")]
        [MinLength(3, ErrorMessage = "O campo 'Name' deve conter entre 3 e 150 caracteres")]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
