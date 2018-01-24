using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrabalhoLocaMais
{
    class Cadastro
    {
        private static string nome, endereco, telefone, cep;
        //Coleta o código, nome(nome + sobrenome), endereço(Bairro/Cidade-Estado), telefone, cep
        public char cadastrarpessoa(string tipo, int opcao)
        {
            Console.Clear();
            string cadastro = tipo;
            Console.WriteLine("\t\tCADASTRO DE " + cadastro.ToUpper());
            char continuar = ' ';
            //nome
            Console.WriteLine("1. Digite o nome completo do " + tipo + ":");
            nome = Console.ReadLine().ToUpper();
            //telefone
            Console.WriteLine("Digite o telefone: ((**)****-****)");
            telefone = Console.ReadLine();
            //cidade
            Console.WriteLine("Digite o endereço: (Bairro/Cidade-Estado)");
            endereco = Console.ReadLine().ToUpper();
            //cep
            Console.WriteLine("Digite o CEP: (*****-***)");
            cep = Console.ReadLine();
            switch (opcao)
            {
                case 1:
                    continuar = Cliente();
                    break;
                case 2:
                    continuar = Funcionario();
                    break;
            }
            return continuar;
        }
        private char Funcionario()
        {
            string cargo, cpf;
            double salario;
            char opcao;
            //cargo
            Console.WriteLine("Digite o cargo: ");
            cargo = Console.ReadLine().ToUpper();
            //salário
            Console.WriteLine("Digite o salário: ");
            salario = double.Parse(Console.ReadLine());
            //Conferir código gerado
            bool teste;
            do
            {
                FileStream arq = new FileStream("funcionario.txt", FileMode.OpenOrCreate);
                StreamReader ler = new StreamReader(arq);
                string linha;
                teste = true;
                Console.WriteLine("Digite o cpf: (***.***.***-**)");
                cpf = Console.ReadLine();
                do
                {
                    linha = ler.ReadLine();
                    if (linha == "CPF: " + cpf)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tCADASTRO DE FUNCIONÁRIO");
                        Console.WriteLine("CPF já cadastrado! Tente novamente.");
                        teste = false;
                    }
                } while (linha != null);
                ler.Close();
            } while (teste == false);
            //escrever no arquivo
            StreamWriter escreve = new StreamWriter("funcionario.txt", true);
            escreve.WriteLine("\t\tFUNCIONÁRIO");
            escreve.WriteLine("Nome: " + nome);
            escreve.WriteLine("Telefone: " + telefone);
            escreve.WriteLine("Endereço: " + endereco);
            escreve.WriteLine("CEP: " + cep);
            escreve.WriteLine("Cargo: " + cargo);
            escreve.WriteLine("Salário: R$" + salario);
            escreve.WriteLine("CPF: " + cpf);
            escreve.WriteLine("-----------------------------");
            escreve.Close();
            Console.Clear();
            Console.WriteLine("\t\tCADASTRO DE FUNCIONÁRIO");
            Console.WriteLine("Funcionário registrado com sucesso!");
            do
            {
                Console.WriteLine("Cadastrar novo funcionário? (S - Sim / N - Não)");
                opcao = char.Parse(Console.ReadLine().ToUpper());
                if (!(opcao == 'S' || opcao == 'N'))
                {
                    Console.Clear();
                    Console.WriteLine("\t\tCADASTRO DE FUNCIONÁRIO");
                    Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                }
            } while (!(opcao == 'S' || opcao == 'N'));
            return opcao;

        }
        private char Cliente()
        {
            //cpf é o código
            char opcao;
            bool teste;
            string linha, cpf;
            do
            {
                FileStream arq = new FileStream("cliente.txt", FileMode.OpenOrCreate);
                StreamReader ler = new StreamReader(arq);
                teste = true;
                Console.WriteLine("Digite o cpf: (***.***.***-**)");
                cpf = Console.ReadLine();
                do
                {
                    linha = ler.ReadLine();
                    if (linha == "CPF: " + cpf)
                    {
                        //conferi pra ver se ele já existe
                        Console.Clear();
                        Console.WriteLine("\t\tCADASTRO DE CLIENTE");
                        Console.WriteLine("CPF já cadastrado! Tente novamanete.");
                        teste = false;
                    }
                } while (linha != null);
                ler.Close();
            } while (teste == false);
            //preenchendo arquivo cliente
            StreamWriter escreve = new StreamWriter("cliente.txt", true);
            escreve.WriteLine("\t\tCLIENTE");
            escreve.WriteLine("Nome: " + nome);
            escreve.WriteLine("Telefone: " + telefone);
            escreve.WriteLine("Endereço: " + endereco);
            escreve.WriteLine("CEP: " + cep);
            escreve.WriteLine("CPF: " + cpf);
            escreve.WriteLine("-----------------------------");
            escreve.Close();
            Console.Clear();
            Console.WriteLine("\t\tCADASTRO DE CLIENTE");
            Console.WriteLine("Cliente registrado com sucesso!");
            do
            {
                Console.WriteLine("Deseja cadastrar outro cliente? (S-Sim/N-Não)");
                opcao = char.Parse(Console.ReadLine().ToUpper());
                if (!(opcao == 'N' || opcao == 'S'))
                {
                    Console.Clear();
                    Console.WriteLine("\t\tCADASTRO DE CLIENTE");
                    Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                }
            } while (!(opcao == 'N' || opcao == 'S'));
            return opcao;
        }
    }
}
