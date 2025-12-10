using System;
using System.Collections.Generic;
using System.Linq; 

namespace Biblioteca_C_
{
    public class UserService 
    {
        private readonly List<User> _cadastros;

        public UserService(List<User> cadastros)
        {
            _cadastros = cadastros;
        }

        public void RegistrarNovoUsuario()
        {
            Console.WriteLine("Insira seu nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Insira seu e-mail: ");
            string email = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome e e-mail não podem ser vazios.");
                return;
            }

            if (_cadastros.Any(u => u.Email!.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Este cadastro já existe! Tente Logar !");
                return;
            }

            Console.WriteLine("Insira sua senha");
            string senha = Console.ReadLine() ?? string.Empty;

            _cadastros.Add(new User
            {
                Name = nome,
                Email = email,
                Password = senha, 
            });

            Console.WriteLine("Cadastro realizado com sucesso! ");
        }
    }
}