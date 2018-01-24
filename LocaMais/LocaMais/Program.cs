using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace TrabalhoLocaMais
{
    class Program
    {
        //Instâncias das classes
        ///instancia da classe pesquisa
        private static Pesquisa pesquisar = new Pesquisa();
        ///instancia da classe cadastro
        private static Cadastro cadastro = new Cadastro();
        ///instancia da classe veiculo
        private static Veiculo veiculo = new Veiculo();

        //váriaveis globais
        private static string tipo, fator;
        ///tipo vai determinar o parâmetro que a função recebe ai ela toda vai trocar valores para "OCUPADO" ou "DISPONÍVEL" por exemplo
        ///, também pode dar como parâmetro nome do arquivo para uma função, isso ocorre no pesquisar cliente. Já o fator vai ser o nome 
        ///que vai aparecer constantemente na função pra não precisar fazer duas delas como "Cliente" ou "funcionário"
        private static int opcao;

        //Definições da tela e menus
        static void Main(string[] args)
        {
            logo();
            Console.CursorVisible = true;
            Console.Clear();
            Console.Title = "LocaMais - Os melhores veículos para você";
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            //Menu
            principal();
            Console.ReadKey();
        }


        //menus
        private static void principal()
        {
            do
            {
                Console.Clear();
                telaPrincipal();
                do
                {
                    Console.Write("Opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao < 1 || opcao > 6)
                    {
                        Console.Clear();
                        telaPrincipal();
                        Console.WriteLine("Opção Inválida! Digite novamente.");
                    }
                } while (opcao < 1 || opcao > 6);
                switch (opcao)
                {
                    case 1:
                        menuCadastro();
                        break;
                    case 2:
                        menuPesquisa();
                        break;
                    case 3:
                        menuLocacao();
                        break;
                    case 4:
                        menuManutencao();
                        break;
                    case 5:
                        menuFidelidade();
                        break;
                    case 6:
                        Console.Clear();
                        saida();
                        break;
                }
            } while (opcao != 6);
        }
        private static void telaPrincipal()
        {
            Console.WriteLine("\t\tPÁGINA INICIAL");
            Console.WriteLine("[1] - Cadastrar");
            Console.WriteLine("[2] - Pesquisar");
            Console.WriteLine("[3] - Locação");
            Console.WriteLine("[4] - Manutenção");
            Console.WriteLine("[5] - Fidelidade");
            Console.WriteLine("[6] - Sair");
        }

        private static void menuCadastro()
        {
            do
            {
                Console.Clear();
                telaCadastro();
                do
                {
                    Console.Write("Opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao < 1 || opcao > 4)
                    {
                        Console.Clear();
                        telaCadastro();
                        Console.WriteLine("Opção Inválida! Digite novamente.");
                    }
                } while (opcao < 1 || opcao > 4);
                switch (opcao)
                {
                    case 1:     //cadastra um cliente, retorna o tipo cliente para o cadastrarPessoa que vai direcionar para as coisas especifícas
                        do      ///do cliente
                        {
                            tipo = "cliente";
                        } while (cadastro.cadastrarpessoa(tipo, opcao) == 'S');
                        break;
                    case 2:     //cadastra um funcionário, retorna o tipo funcionário para o cadastrarPessoa que vai direcionar para as coisas 
                        do      ///especifícas do funcionário
                        {
                            tipo = "funcionário";
                        } while (cadastro.cadastrarpessoa(tipo, opcao) == 'S');
                        break;
                    case 3:
                        veiculo.CadastrarVeiculo();
                        break;
                    case 4:
                        break;
                }
            } while (opcao != 4);
        }
        private static void telaCadastro()
        {
            Console.WriteLine("\t\tCADASTRAR");
            Console.WriteLine("[1] - Cliente");
            Console.WriteLine("[2] - Funcionário");
            Console.WriteLine("[3] - Veículo");
            Console.WriteLine("[4] - Voltar ao menu anterior");
        }

        private static void menuPesquisa()
        {
            do
            {
                Console.Clear();
                telaPesquisa();
                do
                {
                    Console.Write("Opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao < 1 || opcao > 5)
                    {
                        Console.Clear();
                        telaPesquisa();
                        Console.WriteLine("Opção Inválida! Digite novamente.");
                    }
                } while (opcao < 1 || opcao > 5);
                switch (opcao)
                {
                    case 1:    //passa como parâmetro o arquivo cliente.txt para a função pesquisar.FunCli trabalhar com ele, além do texto cliente 
                        tipo = "cliente.txt";                                                       ///para ele exibir
                        fator = "cliente";
                        pesquisar.pesquisarFunCli(tipo, fator);
                        break;
                    case 2:   //passa como parâmetro o arquivo funcionario.txt para a função pesquisar.FunCli trabalhar com ele, além do texto 
                        tipo = "funcionario.txt";                                                   ///funcionario para ele exibir
                        fator = "funcionário";
                        pesquisar.pesquisarFunCli(tipo, fator);
                        break;
                    case 3:
                        pesquisar.pesquisarLocacao();
                        break;
                    case 4:
                        pesquisar.pesquisarVeiculo();
                        break;
                    case 5:
                        break;
                }
            } while (opcao != 5);
        }
        private static void telaPesquisa()
        {
            Console.WriteLine("\t\tPESQUISAR");
            Console.WriteLine("[1] - Clientes");
            Console.WriteLine("[2] - Funcionários");
            Console.WriteLine("[3] - Locação");
            Console.WriteLine("[4] - Veículos");
            Console.WriteLine("[5] - Voltar ao menu anterior");
        }

        private static void menuLocacao()
        {
            do
            {
                Console.Clear();
                telaLocacao();
                do
                {
                    Console.Write("Opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao < 1 || opcao > 3)
                    {
                        Console.Clear();
                        telaLocacao();
                        Console.WriteLine("Opção Inválida! Digite novamente.");
                    }
                } while (opcao < 1 || opcao > 3);
                switch (opcao)
                {
                    case 1:
                        veiculo.Locar();
                        break;
                    case 2:     //o parametro disponivel serve para a função dar baixa o usa-lo para alterar valor
                        tipo = "DISPONÍVEL";
                        Console.Clear();
                        Console.WriteLine("\t\tDAR BAIXA EM LOCAÇÂO");
                        veiculo.DarBaixa(tipo);
                        break;
                    case 3:
                        break;
                }
            } while (opcao != 3);
        }
        private static void telaLocacao()
        {
            Console.WriteLine("\t\tLOCAÇÃO");
            Console.WriteLine("[1] - Locar");
            Console.WriteLine("[2] - Dar Baixa");
            Console.WriteLine("[3] - Voltar ao menu anterior");
        }

        private static void menuManutencao()
        {
            do
            {
                Console.Clear();
                telaManutencao();
                do
                {
                    Console.Write("Opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao < 1 || opcao > 3)
                    {
                        Console.Clear();
                        telaManutencao();
                        Console.WriteLine("Opção Inválida! Digite novamente.");
                    }
                } while (opcao < 1 || opcao > 3);
                switch (opcao)
                {
                    case 1:   //o parametro disponivel serve para a função para a função BaixaManutenção o usa-lo para alterar valor
                        tipo = "DISPONÍVEL";
                        veiculo.BaixaManutencao(tipo);
                        break;
                    case 2:
                        tipo = "MANUTENÇÃO";
                        veiculo.BaixaManutencao(tipo);
                        break;
                    case 3:
                        break;
                }
            } while (opcao != 3);
        }
        private static void telaManutencao()
        {
            Console.WriteLine("\t\tMANUTENÇÃO");
            Console.WriteLine("[1] - Dar Baixa");
            Console.WriteLine("[2] - Declarar manutenção");
            Console.WriteLine("[3] - Voltar ao menu anterior");
        }

        private static void menuFidelidade()
        {
            do
            {
                Console.Clear();
                telaFidelidade();
                do
                {
                    Console.Write("Opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (opcao < 1 || opcao > 3)
                    {
                        Console.Clear();
                        telaFidelidade();
                        Console.WriteLine("Opção Inválida! Digite novamente.");
                    }
                } while (opcao < 1 || opcao > 3);
                switch (opcao)
                {
                    case 1:
                        pesquisar.exibirFidelidade();
                        break;
                    case 2:
                        pesquisar.pesquisarFidelidade();
                        break;
                    case 3:
                        break;
                }
            } while (opcao != 3);
        }
        private static void telaFidelidade()
        {
            Console.WriteLine("\t\tFIDELIDADE");
            Console.WriteLine("[1] - Clientes premiados");
            Console.WriteLine("[2] - Pesquisar clientes fiéis");
            Console.WriteLine("[3] - Voltar ao menu anterior");
        }

        //tela de saida e entrada
        private static void logo()
        {
            Console.Title = "LocaMais";
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            int x = 8;
            string[] logo = new string[] {"______                       ______  ___         _____         ",
                                          "___  / ______ __________________   |/  /__________ (_)________",
                                          "__  /  _  __ \\   ___/_  __  /__  /|_/ / _  __  /__  / __  ___/",
                                          "_  /___/ /_/ // /__  / /_/ / _  /  / /  / /_/ / _  /  _(__  ) ",
                                          "/_____/\\____/ \\___/  \\____/  /_/  /_/   \\____/  /_/   /____/",
                                                              };
            for (int l = 0; l < logo.Length; l++, x++)
            {
                Console.SetCursorPosition(10, x);
                Console.WriteLine(logo[l]);
            }
            x++;
            x++;
            x++;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(23, x++);
            Console.WriteLine("OS MELHORES VEÍCULOS PARA VOCÊ!");

            Console.SetCursorPosition(21, x++);
            Console.WriteLine("\n");

            Console.SetCursorPosition(25, x++);
            Console.WriteLine("Tecle ENTER para continuar.");
            Console.ReadKey();
        }
        private static void saida()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "LocaMais";
            Console.CursorVisible = false;
            int x = 8;
            string[] logo = new string[] {"______                       ______  ___         _____         ",
                                          "___  / ______ __________________   |/  /__________ (_)________",
                                          "__  /  _  __ \\   ___/_  __  /__  /|_/ / _  __  /__  / __  ___/",
                                          "_  /___/ /_/ // /__  / /_/ / _  /  / /  / /_/ / _  /  _(__  ) ",
                                          "/_____/\\____/ \\___/  \\____/  /_/  /_/   \\____/  /_/   /____/",
                                                              };
            for (int l = 0; l < logo.Length; l++, x++)
            {
                Console.SetCursorPosition(10, x);
                Console.WriteLine(logo[l]);
            }
            x++;
            x++;
            x++;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(29, x++);
            Console.WriteLine("AGRADECE A SUA VISITA!");

        }
    }
}
