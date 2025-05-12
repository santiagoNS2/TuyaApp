using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuyaApp.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CC { get; private set; }

        
        protected Customer() { }

        public Customer(string name, string email, string cc)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetEmail(email);
            SetCC(cc);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es requerido.");
            Name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("El email es inválido.");
            Email = email;
        }

        public void SetCC(string cc)
        {
            if (string.IsNullOrWhiteSpace(cc))
                throw new ArgumentException("La cédula (CC) es requerida.");
            CC = cc;
        }
    }
}


