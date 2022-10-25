
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



internal class Program
{
    [System.Serializable]
    struct Cliente
    {
        public string nome;
        public string email;
        public string cpf;
    }

    static List<Cliente> clientes = new List<Cliente>();

    enum Menu { Listagem = 1, Adicionar = 2, Remover = 3, Sair = 4 }
    private static void Main(string[] args)
    {
        Carregar();
        bool escolheuSair = false;

        while (!escolheuSair)
        {
            Console.WriteLine("Sistemas de clientes - Bem-Vindo!");
            Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
            int intOp = int.Parse(Console.ReadLine());
            Menu opcao = (Menu)intOp;

            switch (opcao)
            {
                case Menu.Adicionar:
                    Adicionar();
                    break;
                case Menu.Listagem:
                    Listagem();
                    break;
                case Menu.Remover:
                    Remover();
                    break;
                case Menu.Sair:
                    escolheuSair = true;
                    break;
            }
            Console.Clear();
        }
    }
    // as funções tem que ser criadas fora da Main()
    static void Adicionar()
    {

        Cliente cliente = new Cliente();
        Console.WriteLine("Adicionar um cliente: ");
        Console.WriteLine("Digite o Nome: ");
        cliente.nome = Console.ReadLine();
        Console.WriteLine("Digite o Email: ");
        cliente.email = Console.ReadLine();
        Console.WriteLine("Digite o CPF: ");
        cliente.cpf = Console.ReadLine();

        clientes.Add(cliente);
        Salvar();
        Console.WriteLine("Cadastro Concluído! Aperte [enter] para sair.");
        Console.ReadLine();

    }

    static void Listagem()
    {
        if (clientes.Count > 0) // se tem pelo menos um cliente
        {
            Console.WriteLine("Lista de Clientes: ");
            int i = 0;
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine($"ID: {i}");
                Console.WriteLine($"Nome: {cliente.nome}");
                Console.WriteLine($"Email: {cliente.email}");
                Console.WriteLine($"CPF: {cliente.cpf}");
                Console.WriteLine("=======================");
                i++;

            }
        }
        else
        {
            Console.WriteLine("Nenhum cliente cadastrado!");
        }

        Console.WriteLine("Aperte [enter] para sair.");
        Console.ReadLine();

    }

    static void Remover()
    {
        Listagem();
        Console.WriteLine("Digite o ID do cliente que quer remover: ");
        int id = int.Parse(Console.ReadLine());
        if (id >= 0 && id < clientes.Count)
        {
            clientes.RemoveAt(id);
            Salvar();
        }
        else
        {
            Console.WriteLine("ID DIGITADO É INVÁLIDO!");
            Console.ReadLine();
        }
    }

    static void Salvar()
    {
        FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
        BinaryFormatter encoder = new BinaryFormatter();

        encoder.Serialize(stream, clientes);
        stream.Close();

    }

    static void Carregar()
    {
        FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

        try //tentar
        {

            BinaryFormatter encoder = new BinaryFormatter();

            clientes = (List<Cliente>)encoder.Deserialize(stream);


            if (clientes == null)
            {
                clientes = new List<Cliente>();
            }


        }
        catch (System.Exception) //qualquer erro no try usa isso
        {
            clientes = new List<Cliente>();
        }
        stream.Close();
    }
}