using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrabalhoLocaMais
{
    class Pesquisa
    {
        public void pesquisarFunCli(string tipo, string fator)
        {
            //conferi se o arquivo tem valor
            string pesquisa;
            pesquisa = fator;
            FileStream tamanho = new FileStream(tipo, FileMode.OpenOrCreate);
            if (tamanho.Length < 1)
            {
                Console.Clear();
                Console.WriteLine("\t\tPESQUISA DE " + pesquisa.ToUpper());
                Console.WriteLine("Não há " + fator + " registrado!");
                Console.WriteLine("Aperte ENTER para continuar.");
                Console.ReadKey();
                tamanho.Close();
            }
            else
            {
                tamanho.Close();
                char opcao;
                do
                {
                    Console.Clear();
                    Console.WriteLine("\t\tPESQUISA DE " + pesquisa.ToUpper());
                    string nome, linha;
                    bool teste = false;
                    int x = 0;
                    //como esta função pesquisa tanto funcionário quanto cliente, dependendo do parâmetro que fornece o arquivo que 
                    ///será aberto, quando este arquivo for de funcionário o x precisa ser 7 porque o x é usado dentro do for que
                    ///percorre as linhas as exibindo assim que encontra o funcionário na pesquisa, e por o formato do arquivo cliente e 
                    ///funcionario ser diferente, no funcionário escrevo 7 linhas abaixo do nome do funcionário correspondente a busca,
                    ///estas 7 linhas possuem todas as informações dele, já o do cliente só tem 5 linhas
                    if (tipo == "funcionario.txt")
                    {
                        x = 7;
                    }
                    else
                    {
                        x = 5;
                    }
                    //fator é o parâmetro que pode ser "cliente" ou "funcionário"
                    Console.WriteLine("Digite o nome do " + fator + " que deseja pesquisar:");
                    nome = Console.ReadLine().ToUpper();
                    Console.WriteLine(); //saltar linha só para ficar organizado
                    StreamReader pesq = new StreamReader(tipo);
                    do
                    {
                        linha = pesq.ReadLine();
                        if (linha == "Nome: " + nome) //encontra ou não a pessoa procurada
                        {
                            teste = true; //valida ter encontrado pelo menos um correspondente a pesquisa
                            //exibi as informações dela
                            Console.WriteLine(linha);
                            for (int i = 0; i < x; i++)
                            {
                                Console.WriteLine(pesq.ReadLine());
                            }
                        }
                    } while (linha != null);
                    if (teste == false)
                    {
                        //quando não encontra ninguém relacionado a pesquisa
                        Console.Clear();
                        Console.WriteLine("\t\tPESQUISA DE " + pesquisa.ToUpper());
                        Console.WriteLine("Não há " + fator + " com esse nome!");
                    }
                    else
                    {
                        //só pra dar um tempo pro usuário ler a vontade
                        Console.WriteLine("Aperte ENTER para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("\t\tPESQUISA DE " + pesquisa.ToUpper());
                    }
                    pesq.Close();
                    do
                    {
                        Console.WriteLine("Deseja pesquisar outro " + fator + "? (S-Sim/N-Não)");
                        opcao = char.Parse(Console.ReadLine().ToUpper());
                        if (!(opcao == 'N' || opcao == 'S'))
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tPESQUISA DE " + pesquisa.ToUpper());
                            Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                        }
                    } while (!(opcao == 'N' || opcao == 'S'));
                } while (opcao == 'S');
            }
        } //pesquisa funcionários ou clientes
        public void pesquisarLocacao()
        {
            FileStream tamanho = new FileStream("locações.txt", FileMode.OpenOrCreate);
            if (tamanho.Length < 1) //confere se o arquivo locação possui alguma locação
            {
                Console.Clear();
                Console.WriteLine("\t\tPESQUISAR LOCAÇÃO");
                Console.WriteLine("Não há locações registradas!");
                Console.WriteLine("Aperte ENTER para continuar.");
                Console.ReadKey();
                tamanho.Close();
            }
            else
            {
                char opcao;
                tamanho.Close();
                do
                {
                    string nome, linha;
                    int cont = 0;
                    bool teste = false;
                    Console.Clear();
                    Console.WriteLine("\t\tPESQUISAR LOCAÇÃO");
                    Console.WriteLine("Digite o nome do cliente que realizou a locação:");
                    nome = Console.ReadLine().ToUpper();
                    Console.WriteLine();
                    //conta as linhas do arquivo locação
                    StreamReader contar = new StreamReader("locações.txt");
                    do
                    {
                        linha = contar.ReadLine();
                        cont++;
                    } while (linha != null);
                    contar.Close();
                    //monta uma string para armazenar todas as locações
                    string[] locacoes = new string[cont];
                    //armazena na string as locações
                    StreamReader armazenar = new StreamReader("locações.txt");
                    for (int i = 0; i < cont; i++)
                    {
                        locacoes[i] = armazenar.ReadLine();
                    }
                    armazenar.Close();
                    //lê a string para exibir o que o cliente buscou
                    for (int i = 0; i < cont; i++)
                    {
                        if (locacoes[i] == "Nome do cliente:" + nome)
                        {
                            teste = true;
                            int x = i - 1;
                            do
                            {
                                //exibi as linhas anteriores ao nome do cliente, correspondente a pesqusa
                                if (locacoes[x] == "LOCAÇÃO ATIVA" || locacoes[x] == "LOCAÇÃO INATIVA")
                                {
                                    while (x != i)
                                    {
                                        Console.WriteLine(locacoes[x]);
                                        x++;
                                    }
                                    x++;
                                }
                                x--;
                            } while (locacoes[x] != "Nome do cliente:" + nome);
                            //exibi as outras linhas posteriores ao nome da locação
                            for (int y = 0; y < 10; x++, y++)
                            {
                                Console.WriteLine(locacoes[x]);
                            }
                        }
                    }
                    //verifica-se encontrou alguma locação do cliente escolhido
                    if (teste == false)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tPESQUISAR LOCAÇÃO");
                        Console.WriteLine("Não há locações no nome deste cliente!");
                    }
                    else
                    {
                        //só um tempo pro usuário ler caso haja algum resultado
                        Console.WriteLine("Aperte ENTER para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("\t\tPESQUISAR LOCAÇÃO");
                    }
                    do
                    {
                        Console.WriteLine("Deseja pesquisar outra locação? (S-Sim/N-Não)");
                        opcao = char.Parse(Console.ReadLine().ToUpper());
                        if (!(opcao == 'N' || opcao == 'S'))
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tPESQUISAR LOCAÇÃO");
                            Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                        }
                    } while (!(opcao == 'N' || opcao == 'S'));
                } while (opcao == 'S');
            }

        } //pesquisa locações
        public void exibirFidelidade()
        {
            FileStream arq = new FileStream("fidelidade.txt", FileMode.OpenOrCreate);
            if (arq.Length < 1)
            {
                //caso o arquivo fidelidade nunca tenha sido preenchido
                Console.Clear();
                Console.WriteLine("\t\tLISTA PREMIAÇÂO (+=500 PONTOS):");
                Console.WriteLine("Não há registros de fidelidade");
                Console.WriteLine("Aperte ENTER para continuar.");
                Console.ReadKey();
                arq.Close();
            }
            else
            {
                arq.Close();
                Console.Clear();
                string linha, linha2 = "", temp = "";
                int pontos = 0;
                bool teste = false, teste2 = false;
                Console.WriteLine("\t\tLISTA PREMIAÇÂO (+=500 PONTOS):");
                StreamReader pesq = new StreamReader("fidelidade.txt");
                do
                {
                    //procura no arquivo texto fidelidade a linha escrita "pontos" e logo em seguida pega a de baixo assim que acha a linha
                    ///pois a próxima linha contém a pontuação e o foreach extrai essa pontuação que cai no if que valida se ela é
                    ///ou não maior ou igual a 500, se for ele exibi, se não não exibi
                    linha = pesq.ReadLine();
                    if (linha == "PONTOS")
                    {
                        linha2 = pesq.ReadLine();
                        foreach (char c in linha2)
                        {
                            if (c == ':')
                            {
                                teste2 = true;
                            }
                            else if (teste2 == true)
                            {
                                temp += c;
                            }
                        }
                        pontos = int.Parse(temp);
                    }
                    if (pontos > 499)
                    {
                        teste = true; //valida ter encontrado pontos maior ou igual a 500
                        Console.WriteLine(linha);
                        Console.WriteLine(linha2);
                        Console.WriteLine(pesq.ReadLine());
                        Console.WriteLine(pesq.ReadLine());
                        Console.WriteLine(pesq.ReadLine());
                    }
                    pontos = 0;
                    temp = "";
                    teste2 = false;
                } while (linha != null);
                pesq.Close();
                if (teste == false)
                {
                    //se não tiver encontrado nenhum
                    Console.WriteLine("Não há clientes com mais de 500 pontos ou 500 pontos!");
                }
                //só um tempo para o usuário poder ler
                Console.WriteLine("Aperte ENTER para continuar");
                Console.ReadKey();
            }

        } //exibi fidelidades acima de 500 pontos
        public void pesquisarFidelidade()
        {
            FileStream arq = new FileStream("fidelidade.txt", FileMode.OpenOrCreate);
            if (arq.Length < 1)
            {
                //caso o arquivo fidelidade nunca tenha sido preenchido
                Console.Clear();
                Console.WriteLine("\t\tPESQUISAR FIDELIDADE");
                Console.WriteLine("Não há registros de fidelidade");
                Console.WriteLine("Aperte ENTER para continuar.");
                Console.ReadKey();
                arq.Close();
            }
            else
            {
                arq.Close();
                Console.Clear();
                Console.WriteLine("\t\tPESQUISAR FIDELIDADE");
                StreamReader contar = new StreamReader("fidelidade.txt");
                int cont = 0;
                //conta quantas as linhas o arquivo tem
                while (contar.ReadLine() != null)
                {
                    cont++;
                }
                contar.Close();
                //cria um vetor do tamanho do arquivo
                string[] linhas = new string[cont];
                //armazena as linhas do arquivo no vetor
                StreamReader armazenar = new StreamReader("fidelidade.txt");
                for (int i = 0; i < cont; i++)
                {
                    linhas[i] = armazenar.ReadLine();
                }
                armazenar.Close();
                //pesquisa o nome na string e exibi os dados
                string nome;
                char opcao;
                do
                {
                    bool teste = false;
                    Console.WriteLine("Digite o nome do cliente: ");
                    nome = Console.ReadLine().ToUpper();
                    Console.WriteLine();
                    for (int x = 0; x < cont; x++)
                    {
                        if (linhas[x] == "Nome do cliente:" + nome)
                        {
                            teste = true;
                            for (int i = 3; i >= 0; i--)
                            {
                                teste = true;
                                Console.WriteLine(linhas[x - i]);
                            }
                            Console.WriteLine(linhas[x + 1]);
                        }
                    }
                    if (teste == false)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tPESQUISAR FIDELIDADE");
                        Console.WriteLine("O cliente não possui registros de fidelidade!");
                        Console.WriteLine("Tecle ENTER para continuar.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Tecle ENTER para continuar.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    Console.WriteLine("\t\tPESQUISAR FIDELIDADE");
                    do
                    {
                        Console.WriteLine("Deseja pesquisar outro registro de fidelidade? (S-Sim/N-Não)");
                        opcao = char.Parse(Console.ReadLine().ToUpper());
                        if (!(opcao == 'N' || opcao == 'S'))
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tPESQUISAR FIDELIDADE");
                            Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                        }
                    } while (!(opcao == 'N' || opcao == 'S'));

                } while (opcao == 'S');


            }
        } //pesquisa fidelidades específicas
        public void pesquisarVeiculo()
        {
            FileStream tamanho = new FileStream("veiculos.txt", FileMode.OpenOrCreate);
            if (tamanho.Length < 1) //confere se o arquivo locação possui alguma locação
            {
                Console.Clear();
                Console.WriteLine("\t\tPESQUISAR VEÍCULO");
                Console.WriteLine("Não há veículos registrados!");
                Console.WriteLine("Aperte ENTER para continuar.");
                Console.ReadKey();
                tamanho.Close();
            }
            else
            {
                char opcao;
                tamanho.Close();
                do
                {
                    string placa, linha;
                    int cont = 0;
                    bool teste = false;
                    Console.Clear();
                    Console.WriteLine("\t\tPESQUISAR VEÍCULOS");
                    Console.WriteLine("Digite a placa:");
                    placa = Console.ReadLine().ToUpper();
                    Console.WriteLine();
                    //conta as linhas do arquivo veículo
                    StreamReader contar = new StreamReader("veiculos.txt");
                    while (contar.ReadLine() != null)
                    {
                        cont++;
                    }
                    contar.Close();
                    //monta uma string para armazenar todos os veiculos
                    string[] veiculos = new string[cont];
                    //armazena na string as locações
                    StreamReader armazenar = new StreamReader("veiculos.txt");
                    for (int i = 0; i < cont; i++)
                    {
                        veiculos[i] = armazenar.ReadLine();
                    }
                    armazenar.Close();
                    //lê a string para exibir o que o usuário buscou
                    for (int i = 0; i < cont; i++)
                    {
                        if (veiculos[i] == "Placa: " + placa)
                        {
                            teste = true;
                            int x = i - 6;
                            //exibi as linhas anteriores a placa do veículo
                            while (x != i)
                            {
                                Console.WriteLine(veiculos[x]);
                                x++;
                            }
                            x--;
                            //exibi as outras linhas posteriores a placa
                            for (int y = 0; y < 5; x++, y++)
                            {
                                Console.WriteLine(veiculos[x]);
                            }
                        }
                    }
                    //verifica-se encontrou alguma locação do cliente escolhido
                    if (teste == false)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tPESQUISAR VEÍCULOS");
                        Console.WriteLine("Não há veículos com esta placa!");
                    }
                    else
                    {
                        //só um tempo pro usuário ler caso haja algum resultado
                        Console.WriteLine("Aperte ENTER para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("\t\tPESQUISAR VEÍCULOS");
                    }
                    do
                    {
                        Console.WriteLine("Deseja pesquisar outro veículo? (S-Sim/N-Não)");
                        opcao = char.Parse(Console.ReadLine().ToUpper());
                        if (!(opcao == 'N' || opcao == 'S'))
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tPESQUISAR VEÍCULOS");
                            Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                        }
                    } while (!(opcao == 'N' || opcao == 'S'));
                } while (opcao == 'S');
            }

        }
    }
}
