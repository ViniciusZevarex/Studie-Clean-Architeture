using CleanArch.Domain.Validation;
using System.Collections.Generic;

namespace CleanArch.Domain.Entities
{
    public sealed  class Category : Entity
    {
        public string Name { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);
            Name = name;
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            ValidateDomain(name);
            Id = id;
            Name = name;
        }


        public void Update(string name)
        {
            ValidateDomain(name);
        }


        public ICollection<Product> Products { get; set; }


        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name)
                , "Invalid name. Name is required");

            DomainExceptionValidation.When(name.Length < 3, 
                "Invalid name, too short, minimum 3 characters");

            Name = name;
        }

    }
}
