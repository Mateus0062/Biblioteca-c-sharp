/* Sistema básico de gerenciamento e aluguel de livros feito em C#. 
 * 
 *Funcionalidades de cadastro, login e diferentes painéis para administradores (gerenciar livros) e usuários comuns (alugar livros)
*/

class Program
{
    class User
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool jaAlugou { get; set; }
        public Livro? Titulo { get; set; }
    }

    class Livro
    {
        private static int _proximoId = 1;
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public bool Alugado { get; set; }
        public User? UserAluguel { get; set; }

        public Livro()
        {
            this.Id = _proximoId;
            _proximoId++;

            this.Titulo = string.Empty;
            this.Autor = string.Empty;
            this.Alugado = false;
            this.UserAluguel = null;
        }
    }

    static void Main(string[] args)
    {
        List<User> cadastros = new List<User>();

        List<Livro> livros = new List<Livro>();

        int op = -1;

        while (op != 0)
        {
            Console.WriteLine("===== Selecione uma opção =====");
            Console.WriteLine("1 - Fazer cadastro ");
            Console.WriteLine("2 - Fazer login");
            Console.WriteLine("0 - Sair");

            op = int.Parse(Console.ReadLine()!);

            switch (op)
            {
                case 1:
                    Registro(cadastros);
                    break;
                case 2:
                    Console.WriteLine("Digite seu email");
                    string email = Console.ReadLine() ?? string.Empty;

                    Console.WriteLine("Digite sua senha: ");
                    string senha = Console.ReadLine() ?? string.Empty;

                    Login(cadastros, email, senha, livros);
                    break;
            }
        }
    }

    static void Registro(List<User> cadastros)
    {
        User user = new User();

        Console.WriteLine("Insira seu nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Insira seu e-mail: ");
        string email = Console.ReadLine() ?? string.Empty;

        if (cadastros.Any(q => q.Email == email))
        {
            Console.WriteLine("Este cadastro já existe! Tente Logar !");
            return;
        }

        Console.WriteLine("Insira sua senha");
        string senha = Console.ReadLine() ?? string.Empty;

        cadastros.Add(new User
        {
            Name = nome,
            Email = email,
            Password = senha,
            jaAlugou = false
        });

        Console.WriteLine("Cadastro realizado com sucesso! ");
    }

    /* Função para realizar Login no sistema de aluguel de livros */
    static void Login(List<User> cadastros, string email, string senha, List<Livro> livros)
    {
        User? userEncontrado = cadastros.FirstOrDefault(s => s.Email == email && s.Password == senha);

        if (userEncontrado?.Email == "admin@gmail.com")
        {
            Console.WriteLine($"\nOlá, {userEncontrado.Name}. Redirecionando para o painel de administrador");
            GerenciarLivro(livros);
        }
        else
        {
            if (userEncontrado != null)
            {
                Console.WriteLine($"Senha bem vindo(a): {userEncontrado.Name}");
                Console.WriteLine("Redirecionando para o painel de Aluguel de Livros ");
                AlugarLivro(livros, cadastros, userEncontrado);
            }
            else
            {
                Console.WriteLine("Usuário não encontrado");
                return;
            }
        }
    }

    /* Função específica para o perfil de Administrador */
    static void GerenciarLivro(List<Livro> livros)
    {
        Console.WriteLine("===== Selecione uma opção =====");
        Console.WriteLine("1 - Adicionar Livro");
        Console.WriteLine("2 - Remover Livro");
        Console.WriteLine("3 - Ver lista de livros");
        Console.WriteLine("0 - Sair");
        int op = int.Parse(Console.ReadLine()!);

        while (op != 0)
        {
            switch (op)
            {
                case 1:
                    Console.WriteLine("Digite o Título do livro");
                    string? titulo = Console.ReadLine();

                    Console.WriteLine("Digite o Autor do livro");
                    string? autor = Console.ReadLine();

                    livros.Add(new Livro
                    {
                        Titulo = titulo,
                        Autor = autor,
                        Alugado = false
                    });

                    Console.WriteLine("Deseja adicionar outro livro? (1 - Sim, 0 - Não)");
                    op = int.Parse(Console.ReadLine()!);
                    break;
                case 2:
                    Console.WriteLine("Digite o Id do livro que deseja remover: ");
                    int idRemove = int.Parse(Console.ReadLine()!);

                    Livro? livroparaRemover = livros.FirstOrDefault(l => l.Id == idRemove);

                    if (livroparaRemover != null)
                    {
                        livros.Remove(livroparaRemover);
                        Console.WriteLine($"Livro: '{livroparaRemover.Titulo}'. Removido com sucesso");
                    }
                    else
                    {
                        Console.WriteLine("Remoção falhou. Insira um Id válido");
                        return;
                    }
                    break;
                case 3:
                    foreach (var l in livros)
                    {
                        Console.WriteLine($"Id do livro: {l.Id}\n Titulo do Livro: {l.Titulo}\n Autor do livro: {l.Autor}\n Livro já foi alugado: {l.Alugado}");
                    }
                    break;
                case 0:
                    Console.WriteLine("Saindo do programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida, tente novamente");
                    break;
            }
        }
    }

    /* Função para usuários Comuns. Onde podem apenas Alugar livros */
    static void AlugarLivro(List<Livro> livros, List<User> cadastros, User userLogado)
    {
        Console.WriteLine("===== Selecione uma opção =====");
        Console.WriteLine("1 - Alugar um Livro");
        Console.WriteLine("0 - Sair");
        int op = int.Parse(Console.ReadLine()!);

        switch (op)
        {
            case 1:
                Console.WriteLine("Veja a lista de livros disponível para aluguel\n");

                List<Livro> livrosdisponiveis = livros.Where(d => d.Alugado == false).ToList();
                foreach (var i in livrosdisponiveis)
                {
                    Console.WriteLine($"Id do livro: {i.Id}\n Titulo do Livro: {i.Titulo}\n Autor do livro: {i.Autor}\n Livro já foi alugado: {i.Alugado}");
                }

                Console.WriteLine("\nInforme o Id do livro que deseja alugar");
                int idLivroAlugado = int.Parse(Console.ReadLine()!);

                Livro? livroaluguel = livros.FirstOrDefault(la => la.Id == idLivroAlugado);

                if (livroaluguel != null)
                {
                    livroaluguel.Alugado = true;
                    livroaluguel.UserAluguel = userLogado;

                    userLogado.jaAlugou = true;
                    userLogado.Titulo = livroaluguel;

                    Console.WriteLine($"Sucesso, você alugou '{livroaluguel.Titulo}'.");
                }
                else
                {
                    Console.WriteLine("Erro ao alugar livro, informe um Id de livro válido");
                }
                break;
            case 0:
                Console.WriteLine("Encerrando o sistema de aluguel.");
                break;
        }
    }
}