using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrabalhoLocaMais
{
    class Veiculo
    {
        //variáveis globais
        private static string[] alugado = new string[2];
        private static int ocupantes;
        private static string situacao;

        //funções principais
        public void CadastrarVeiculo()
        {
            //variáveis
            string modelo, cor, placa, situacao, descricao;
            double valordiaria;
            int ocupantes, codigo = 1;
            char opcao;
            do
            {
                Console.Clear();
                FileStream arq = new FileStream("veiculos.txt", FileMode.OpenOrCreate);
                arq.Close();
                Console.WriteLine("\t\tCADASTRAR VEÍCULO:");
                //coletando dados do veículo
                //modelo
                Console.WriteLine("Modelo: ");
                modelo = Console.ReadLine();
                //cor
                Console.WriteLine("Cor: ");
                cor = Console.ReadLine();
                //placa
                ///evita que sejam cadastradas placas iguais
                string text;
                bool teste;
                do
                {
                    teste = true;
                    Console.WriteLine("Placa: ");
                    placa = Console.ReadLine();
                    StreamReader conferir = new StreamReader("veiculos.txt");
                    do
                    {
                        text = conferir.ReadLine();
                        if (text == "Placa: " + placa)
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tCADASTRAR VEÍCULO:");
                            Console.WriteLine("Placa já cadastrada!");
                            teste = false;
                        }
                    } while (text != null);
                    conferir.Close();
                } while (teste == false);
                //Descrição
                Console.WriteLine("Descrição: ");
                descricao = Console.ReadLine();
                //valor diária
                do
                {
                    Console.WriteLine("Valor da diária: ");
                    valordiaria = double.Parse(Console.ReadLine());
                    if (valordiaria < 1)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tCADASTRAR VEÍCULO:");
                        Console.WriteLine("Valor Inválido!");
                    }
                } while (valordiaria < 1);
                //quantidade de ocupantes
                do
                {
                    Console.WriteLine("Quantidade de ocupantes: ");
                    ocupantes = int.Parse(Console.ReadLine());
                    if (ocupantes < 1)
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tCADASTRAR VEÍCULO:");
                        Console.WriteLine("Valor Inválido!");
                    }
                } while (ocupantes < 1);
                //Situação
                situacao = "DISPONÍVEL";
                //gerar e conferir código
                StreamReader ler = new StreamReader("veiculos.txt");
                string linha;
                do
                {
                    linha = ler.ReadLine();
                    if (linha == "Código: " + codigo)
                    {
                        codigo++;
                    }
                } while (linha != null);
                ler.Close();
                //preenchendo arquivo veículo
                StreamWriter escreve = new StreamWriter("veiculos.txt", true);
                escreve.WriteLine("\t\tVEÍCULOS");
                escreve.WriteLine("Quantidade de Ocupantes: " + ocupantes);
                escreve.WriteLine("Situação: " + situacao);
                escreve.WriteLine("Código: " + codigo);
                escreve.WriteLine("Modelo: " + modelo);
                escreve.WriteLine("Cor: " + cor);
                escreve.WriteLine("Placa: " + placa);
                escreve.WriteLine("Valor da Diária: R$" + valordiaria);
                escreve.WriteLine("Descrição: " + descricao);
                escreve.WriteLine("-----------------------------");
                escreve.Close();
                Console.Clear();
                Console.WriteLine("\t\tCADASTRAR VEÍCULO:");
                Console.WriteLine("Código: " + codigo);
                Console.WriteLine("Veículo cadastrado com sucesso!");
                do
                {
                    //para cadastrar de novo
                    Console.WriteLine("Deseja cadastrar um novo veículo? (S-Sim/N-Não)");
                    opcao = char.Parse(Console.ReadLine().ToUpper());
                    if (!(opcao == 'S' || opcao == 'N'))
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tCADASTRAR VEÍCULO:");
                        Console.WriteLine("Valores errados!Digite 'S' para SIM e 'N' para NÃO");
                    }
                } while (!(opcao == 'S' || opcao == 'N'));
            } while (opcao == 'S');

        }
        public void Locar()
        {
            Console.Clear();
            Console.WriteLine("\t\tLOCAÇÃO");

            //Coletar Horas manual
            string horas;
            Console.WriteLine("Informe as horas: (**:**)");
            horas = Console.ReadLine();
            DateTime Horas = DateTime.Parse(horas);

            //Automático
            ///DateTime Agora = DateTime.Now;
            string aberto = "08:00", fechado = "18:00";

            //converte para a data as horas
            DateTime Aberto = DateTime.Parse(aberto);
            DateTime Fechado = DateTime.Parse(fechado);

            if (Horas < Aberto || Horas > Fechado) //(Agora < Aberto || Agora > Fechado)
            {
                Console.Clear();
                Console.WriteLine("\t\tLOCAÇÃO");
                Console.WriteLine("FECHADO PARA LOCAÇÃO");
                Console.WriteLine("Horário de funcionamento: 08:00 às 18:00");
                Console.WriteLine("Tecle ENTER para continuar");
                Console.ReadKey();
            }
            else
            {
                //opção para fazer uma nova operação
                char opcao;
                //limpa tela
                Console.Clear();
                //restrição para não locar caso não  haja carros cadastrados
                FileStream tamanho = new FileStream("veiculos.txt", FileMode.OpenOrCreate);
                if (tamanho.Length < 1)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tLOCAÇÃO");
                    Console.WriteLine("Não há veículos registrados!");
                    Console.WriteLine("Aperte ENTER para continuar.");
                    Console.ReadKey();
                    tamanho.Close();
                }
                //se tem veiculos cadastrados ele roda
                else
                {
                    tamanho.Close();
                    do
                    {
                        //variáveis
                        char seguro;
                        string nome, cpf;
                        //data de agora
                        DateTime Atual = DateTime.Today;
                        //string para pegar as datas de locação e prevista para entrega
                        string dataFinal = "", dataInicial;
                        //teste usado nas restrições
                        bool teste;
                        //nome do cliente
                        Console.Clear();
                        Console.WriteLine("\t\tLOCAÇÃO");
                        do
                        {
                            //restrição para impedir um nome que não existe
                            Console.WriteLine("Nome completo do cliente: ");
                            nome = Console.ReadLine().ToUpper();
                            FileStream arq = new FileStream("cliente.txt", FileMode.OpenOrCreate);
                            StreamReader pesq = new StreamReader(arq);
                            teste = false;
                            string linha;
                            do
                            {
                                linha = pesq.ReadLine();
                                if (linha == "Nome: " + nome)
                                {
                                    teste = true;
                                }
                            } while (linha != null);
                            if (teste == false)
                            {
                                Console.Clear();
                                Console.WriteLine("\t\tLOCAÇÃO");
                                Console.WriteLine("Não há clientes com este nome tente novamente!");
                            }
                            pesq.Close();
                        } while (teste == false);
                        //cpf do cliente
                        do
                        {
                            Console.WriteLine("Digite o cpf: ");
                            cpf = Console.ReadLine();
                            //restrição para impedir um cpf que não existe
                            FileStream arq = new FileStream("cliente.txt", FileMode.OpenOrCreate);
                            StreamReader pesq = new StreamReader(arq);
                            teste = false;
                            string linha;
                            do
                            {
                                linha = pesq.ReadLine();
                                if (linha == "CPF: " + cpf)
                                {
                                    teste = true;
                                }
                            } while (linha != null);
                            if (teste == false)
                            {
                                Console.Clear();
                                Console.WriteLine("\t\tLOCAÇÃO");
                                Console.WriteLine("Não há clientes registrados com este CPF! Tente Novamente!");
                            }
                            pesq.Close();
                        } while (teste == false);
                        //ocupantes necessários
                        do
                        {
                            Console.WriteLine("Ocupantes: ");
                            ocupantes = int.Parse(Console.ReadLine());
                            //restrição para impedir um valor menor que zero
                            if (ocupantes < 1)
                            {
                                Console.Clear();
                                Console.WriteLine("\t\tLOCAÇÃO");
                                Console.WriteLine("Valores inválidos! Tente novamente!");
                            }
                        } while (ocupantes < 1);
                        //se encontra um veiculo da a baixa nele e o coloca ocupado
                        if (ProcurarVeiculos() == true)
                        {
                            situacao = "OCUPADO";
                            Console.Clear();
                            Console.WriteLine("\t\tLOCAÇÃO");
                            DarBaixa(situacao);
                            //data da retirada
                            DateTime Inicio;
                            do
                            {

                                teste = true;
                                Console.WriteLine("Digite a data de retirada: (*/*/*)");
                                dataInicial = Console.ReadLine();
                                Inicio = DateTime.Parse(dataInicial);
                                //restrição para impedir datas que já passaram
                                if (Inicio < Atual)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\t\tLOCAÇÃO");
                                    Console.WriteLine("Data inválida! Hoje é dia " + Atual.ToShortDateString());
                                    teste = false;
                                }
                            } while (teste == false);
                            do
                            {
                                teste = true;
                                Console.WriteLine("Digite a data limite para entrega: (*/*/*)"); ;
                                dataFinal = Console.ReadLine();
                                DateTime Fim = DateTime.Parse(dataFinal);
                                //restrição para impedir datas que já passaram
                                if (Fim < Inicio)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\t\tLOCAÇÃO");
                                    Console.WriteLine("Data limite não pode ser menor que a de retirada! (" + Inicio.ToShortDateString() + ")");
                                    teste = false;
                                }
                            } while (teste == false);
                            //calculo para saber os dias de locação
                            int totalDias = (DateTime.Parse(dataFinal).Subtract(DateTime.Parse(dataInicial))).Days;
                            //caso ele retire e devolva o carro no mesmo dia
                            if (totalDias == 0)
                            {
                                totalDias = 1;
                            }
                            Console.WriteLine("Dias de reserva: " + totalDias);
                            Console.WriteLine("---------------------------------------------------");
                            //Atribuir seguro
                            do
                            {
                                Console.WriteLine("Deseja adquirir o seguro? (Acréscimo de R$50 (S-Sim / N-Não)):");
                                seguro = char.Parse(Console.ReadLine().ToUpper());
                                if (!(seguro == 'S' || seguro == 'N'))
                                {
                                    Console.Clear();
                                    Console.WriteLine("\t\tLOCAÇÃO");
                                    Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                                }
                            } while (!(seguro == 'S' || seguro == 'N'));
                            //escreve no arquivo texto a locação
                            FileStream registrar = new FileStream("locações.txt", FileMode.OpenOrCreate);
                            registrar.Close();
                            StreamWriter escrever = new StreamWriter("locações.txt", true);
                            escrever.WriteLine("LOCAÇÃO ATIVA");
                            escrever.WriteLine("Nome do cliente:" + nome);
                            escrever.WriteLine("CPF:" + cpf);
                            escrever.WriteLine("Data de retirada:" + dataInicial);
                            escrever.WriteLine("Data limite:" + dataFinal);
                            escrever.WriteLine("Dias de aluguel: " + totalDias);
                            escrever.WriteLine("Carro: ");
                            for (int i = 0; i < 2; i++)
                            {
                                escrever.WriteLine(" " + alugado[i]);
                            }
                            if (seguro == 'S')
                            {
                                escrever.WriteLine("Seguro: Sim");
                            }
                            else
                            {
                                escrever.WriteLine("Seguro: Não");
                            }
                            escrever.WriteLine("-----------------------------");
                            escrever.Close();
                            Console.Clear();
                            Console.WriteLine("\t\tLOCAÇÃO");
                            Console.WriteLine("Reserva efetuada com sucesso!");
                        }
                        //nova reserva
                        do
                        {
                            Console.WriteLine("Deseja efetuar uma nova reserva?  (S-Sim/N-Não)");
                            opcao = char.Parse(Console.ReadLine().ToUpper());
                            if (!(opcao == 'S' || opcao == 'N'))
                            {
                                Console.Clear();
                                Console.WriteLine("\t\tLOCAÇÃO");
                                Console.WriteLine("Valores errados! Digite 'S' para SIM e 'N' para NÃO");
                            }
                        } while (!(opcao == 'S' || opcao == 'N'));
                        Console.Clear();
                    } while (opcao == 'S');
                }
            }
        }
        public void DarBaixa(string tipo)
        {
            string codigo;
            //abri o arquivo veículo para contar suas linhas e criar um vetor do mesmo tamanho
            FileStream veic = new FileStream("veiculos.txt", FileMode.OpenOrCreate);
            veic.Close();
            //abri o arquivo locações ver se possui registros
            FileStream loc = new FileStream("locações.txt", FileMode.OpenOrCreate);
            long tamanho = loc.Length;
            loc.Close();
            StreamReader ler = new StreamReader("veiculos.txt");
            int contlinhas = 0;
            string temp;
            while (ler.ReadLine() != null)
            {
                contlinhas++;
            }
            ler.Close();
            //confere se o arquivo veículos possui algum valor, se não, faz o alerta de que não possui
            if (contlinhas < 2)
            {
                //quando não tem veiculos registrados

                Console.Clear();
                Console.WriteLine("\t\tDAR BAIXA EM LOCAÇÂO");
                Console.WriteLine("Não há veículos cadastrados!");
                Console.WriteLine("Aperte ENTER para continuar!");
                Console.ReadKey();
            }
            else if (tamanho < 2 && tipo == "DISPONÍVEL")
            {
                //quando não tem locações registradas
                Console.Clear();
                Console.WriteLine("\t\tDAR BAIXA EM LOCAÇÂO");
                Console.WriteLine("Não há registros de locações!");
                Console.WriteLine("Aperte ENTER para continuar!");
                Console.ReadKey();

            }
            else
            {
                char opcao;
                do
                {
                    string linha;
                    //armazena o código do veículo para poder procura-lo e atualizar seus valores
                    bool existe;
                    do
                    {
                        Console.WriteLine("Digite o código do veículo:");
                        codigo = Console.ReadLine();
                        StreamReader pesq = new StreamReader("veiculos.txt");
                        existe = false;
                        //vê se existe um carro com este código
                        do
                        {
                            linha = pesq.ReadLine();
                            if (linha == "Código: " + codigo)
                            {
                                existe = true;
                                break;
                            }
                        } while (linha != null);
                        if (existe == false) //avisa que não possui carros com este código
                        {
                            Console.Clear();
                            if (tipo == "DISPONÍVEL") //Só pra aparecer o título de dar baixa na hora de dar baixa em uma locação
                            {
                                Console.WriteLine("\t\tDAR BAIXA EM LOCAÇÂO");
                            }
                            else
                            {
                                Console.WriteLine("\t\tLOCAÇÃO");
                            }
                            Console.WriteLine("Não possui carros com este código! Tente novamente!");
                        }
                        pesq.Close();
                    } while (existe == false);
                    string[] linhas = new string[contlinhas];
                    //abri novamente o arquivo e armazena seus valores no vetor
                    StreamReader ler2 = new StreamReader("veiculos.txt");
                    for (int x = 0; x < contlinhas; x++)
                    {
                        linhas[x] = ler2.ReadLine();
                    }
                    ler2.Close();
                    //abre o arquivo para alterar o valor do veículo para disponível ou ocupado de acordo com oque possui no parâmetro tipo
                    ///primeiro altera-se no vetor
                    FileStream arq3 = new FileStream("veiculos.txt", FileMode.OpenOrCreate);
                    StreamWriter escrever = new StreamWriter(arq3);
                    for (int x = 0; x < contlinhas; x++)
                    {
                        if (linhas[x] == "Código: " + codigo)
                        {
                            linhas[x - 1] = "Situação: " + tipo;
                        }
                    }
                    ///depois escreve o vetor no arquivo, assim atualizando os seus valores sobrescrevendo o que possuia
                    for (int x = 0; x < contlinhas; x++)
                    {
                        escrever.WriteLine(linhas[x]);
                    }
                    escrever.Close();
                    //processo para armazenar o carro//pega todo o código e preço do carro
                    for (int x = 0; x < contlinhas; x++)
                    {
                        if (linhas[x] == "Código: " + codigo)
                        {
                            alugado[0] = linhas[x];
                            alugado[1] = linhas[x + 4];
                        }
                    }

                    //somente entra neste if se for dar baixa em uma locação, pois só ela tem o tipo = "DISPONÍVEL"
                    //se for dar baixa em uma locação será preciso calcular os valores e mecher com o arquivo locações
                    if (tipo == "DISPONÍVEL")
                    {
                        //zera o contlinhas
                        contlinhas = 0;
                        string dataFinal, dataInicial = "", dataEstipulada = "", nome = "", cpf = "";
                        double valor, total = 0, multa = 0;
                        //Abre o  arquivo para poder contar suas linhas
                        FileStream arq4 = new FileStream("locações.txt", FileMode.OpenOrCreate);
                        StreamReader contarLinhas = new StreamReader(arq4);
                        do
                        {
                            temp = contarLinhas.ReadLine();
                            contlinhas++;
                        } while (temp != null);
                        contarLinhas.Close();
                        string[] linha2 = new string[contlinhas];
                        bool teste = false, tem = false;
                        //armazena todo o texto na linha2(vetor)
                        StreamReader lerveiculos = new StreamReader("locações.txt");
                        for (int x = 0; x < contlinhas; x++)
                        {
                            linha2[x] = lerveiculos.ReadLine();
                        }
                        lerveiculos.Close();
                        //procura primeiro se tem um carro alugado com este código
                        for (int x = 0; x < contlinhas; x++)
                        {
                            if (linha2[x] == "LOCAÇÃO ATIVA")
                            {
                                if (linha2[x + 7] == " Código: " + codigo)
                                {
                                    tem = true;
                                }
                            }
                        }
                        if (tem == true) //se tem executa as funções se não avisa não possuir locação com o mesmo
                        {
                            //pega o preço do carro alugado e zera a temp para usa-la como auxiliar
                            temp = "";
                            foreach (char c in alugado[1])
                            {
                                if (c == '$') //o teste só valida como true quando encontra o "$" ai a temp começa a armazenar o preço
                                {
                                    teste = true;
                                }
                                else if (teste == true)
                                {
                                    temp += c;
                                }
                            }
                            valor = double.Parse(temp);
                            //procurar o dia de estipulada para entrega e o dia de retirada  
                            teste = false;
                            string temp1 = "", temp2 = "";
                            for (int x = 0; x < contlinhas; x++)
                            {
                                //procura locações ativas
                                if (linha2[x] == "LOCAÇÃO ATIVA")
                                {
                                    //as locações ativas devem ser com o carro correspondente
                                    if (linha2[x + 7] == " Código: " + codigo)
                                    {
                                        //pega o nome e o cpf do usuário que locou
                                        nome = linha2[x + 1];
                                        cpf = linha2[x + 2];
                                        linha2[x] = "LOCAÇÃO INATIVA"; //inativa a locação
                                        foreach (char c in linha2[x + 3]) //linha que possue a data de acordo com o padrão desenhado para
                                        {                                 ///o arquivo texto
                                            if (c == ':')
                                            {
                                                teste = true; //valida quando encontra dois pontos, porque ai
                                            }                 //começa a armazenar a data na temp
                                            else if (teste == true)
                                            {
                                                //pega a dataInicial depois dos dois pontos
                                                temp1 += c;
                                            }
                                        }
                                        teste = false;
                                        foreach (char c in linha2[x + 4]) //linha que possue a data de acordo com o padrão desenhado para 
                                        {                                 ///o arquivo texto
                                            if (c == ':')
                                            {
                                                teste = true; //valida quando encontra os dois pontos, porque ai
                                            }                 //começa a armazenar na temp a data
                                            else if (teste == true)
                                            {
                                                //pega a data estipulada depois dos dois pontos
                                                temp2 += c;
                                            }
                                        }
                                        teste = false; //inicia o teste de novo
                                        if (linha2[x + 9] == "Seguro: Sim")
                                        {
                                            //vê se tem seguro
                                            teste = true; //valida se tem seguro ou não para contabilizar + 50
                                        }
                                    }
                                }
                            }
                            //coleta os valores das temps para as datas respectivas
                            dataInicial = temp1;
                            dataEstipulada = temp2;
                            bool datamenorqueentrega;
                            DateTime Inicio = DateTime.Parse(dataInicial), Fim;
                            //coletar a data de entrega
                            do
                            {
                                datamenorqueentrega = false;
                                Console.WriteLine("Digite a data de entrega: (*/*/*)");
                                dataFinal = Console.ReadLine();
                                Fim = DateTime.Parse(dataFinal);
                                if (Fim < Inicio)
                                {
                                    //não permite que a data de entrega seja menor que a de retirada
                                    Console.Clear();
                                    Console.WriteLine("\t\tLOCAÇÃO");
                                    Console.WriteLine("Data de entrega não pode ser menor que a de retirada! (" + Inicio.ToShortDateString() + ")");
                                    datamenorqueentrega = true;
                                }
                            } while (datamenorqueentrega == true);
                            //diferença entre datas prevista (Quantos dias o usuário combinou de ficar)
                            int estipulado = (DateTime.Parse(dataEstipulada).Subtract(DateTime.Parse(dataInicial))).Days;
                            if (estipulado == 0) //para caso dele entregar no mesmo dia
                            {
                                estipulado = 1;
                            }
                            //dias que usou (Quantos dias o usuário usou)
                            int dias = ((DateTime.Parse(dataFinal).Subtract(DateTime.Parse(dataInicial))).Days);
                            if (dias == 0) //para caso dele entregar no mesmo dia
                            {
                                dias = 1;
                            }
                            int sobra = 0;
                            //calculando o valor
                            if (estipulado == dias) //para quando entrega na data estipulada
                            {
                                if (teste == true) //com seguro
                                {
                                    total = (valor * estipulado) + 50; //valor da diária * quantidade de dias utilzados + seguro
                                }
                                else //sem seguro
                                {
                                    total = (valor * estipulado); //valor da diária * quantidade de dias utilizados
                                }
                            }
                            else if (estipulado < dias) //quando entrega com atraso
                            {
                                if (teste == true) //com seguro
                                {
                                    sobra = dias - estipulado; //dias usados - dias combinados
                                    total = (valor * estipulado); //o valor pago pelos dias combinados
                                    multa = (total * 0.05) + (20 * sobra); //5% total + 20 por dia de atraso
                                    total = (total * 1.05) + (20 * sobra); //a multa de 5% + os 20 reais por dias excedidos
                                    total = total + 50;
                                }
                                else //sem seguro
                                {
                                    sobra = dias - estipulado; //dias usados - dias combinados
                                    total = (valor * estipulado); //o valor pago pelos dias combinados
                                    multa = (total * 0.05) + (20 * sobra); //5% do total + 20 por dia de atraso
                                    total = (total * 1.05) + (20 * sobra); //a multa de 5% + os 20 reais por dias excedidos
                                }
                            }
                            else if (estipulado > dias) //quando entrega adiantado
                            {
                                if (teste == true) //com seguro
                                {
                                    total = (valor * dias) + 50; //valor do carro * dias usados
                                }
                                else //sem seguro
                                {
                                    total = (valor * dias); //valor do carro * dias usados
                                }
                            }
                            //escrever no arquivo
                            StreamWriter escreverlocacao = new StreamWriter("locações.txt");
                            for (int x = 0; x < contlinhas; x++)
                            {
                                escreverlocacao.WriteLine(linha2[x]);
                                if (linha2[x] == "LOCAÇÃO INATIVA")
                                {
                                    if (linha2[x + 7] == " Código: " + codigo)
                                    {
                                        escreverlocacao.WriteLine("Valor total: R$" + total);
                                        escreverlocacao.WriteLine("Dias de uso: " + dias);
                                        escreverlocacao.WriteLine("Data de entrega: " + dataFinal);
                                        //mensagem de confirmação na tela
                                        Console.Clear();
                                        Console.WriteLine("\t\tLOCAÇÃO");
                                        Console.WriteLine("Baixa efetuada com sucesso!");
                                        Console.WriteLine("----------------------------------------------");
                                        Console.WriteLine("Valor do carro: R$" + valor);
                                        Console.WriteLine("Dias de uso: " + dias);
                                        Console.WriteLine("Dias combinados: " + estipulado);
                                        Console.WriteLine("Multa por atraso: R$" + multa);
                                        if(teste == true)
                                        {
                                            Console.WriteLine("Seguro: R$50,00");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Seguro: N/A");
                                        }
                                        Console.WriteLine("TOTAL: R$" + total);
                                        Console.WriteLine("----------------------------------------------");
                                        Console.WriteLine("Aperte ENTER para continuar");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            escreverlocacao.Close();
                            //chama a fidelidade para somar os pontos
                            Fidelidade(nome, cpf, dias);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tLOCAÇÃO");
                            Console.WriteLine("Não possui locações ativas com este veículo!");
                            Console.WriteLine("Aperte ENTER para continuar");
                            Console.ReadKey();
                        }
                    }
                    //fim do if de dar baixa em locação
                    if (tipo != "OCUPADO")
                    {   //este if não deixa que a operação possa se repetir caso este locando um carro, pois na locação o darBaixa
                        ///também é chamado para alterar os valores do carro, mas após ele existe outras opções na função locar, por isso o
                        ///darBaixa não pode repetir só repete caso queira dar baixa em outra locação e não para locar um veículo

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tDAR BAIXA EM LOCAÇÂO");
                            Console.WriteLine("Deseja efetuar uma nova operação? (S-Sim/N-Não)");
                            opcao = char.Parse(Console.ReadLine().ToUpper());
                            if (!(opcao == 'S' || opcao == 'N'))
                            {
                                Console.WriteLine("Valores errados!Digite 'S' para SIM e 'N' para NÃO");
                            }
                        } while (!(opcao == 'S' || opcao == 'N'));
                    }
                    else
                    {
                        opcao = 'N';
                    }
                } while (opcao == 'S');
            }
        } //Dar baixa em locação ativa ou dar baixa em um veículo após ser locado
        public void BaixaManutencao(string tipo)
        {
            string codigo;
            tituloManutencao(tipo);
            //abri o arquivo veículo para contar suas linhas e criar um vetor do mesmo tamanho
            FileStream arq = new FileStream("veiculos.txt", FileMode.OpenOrCreate);
            arq.Close();
            StreamReader ler = new StreamReader("veiculos.txt");
            int contlinhas = 0;
            while (ler.ReadLine() != null)
            {
                contlinhas++;
            }
            ler.Close();
            //confere se o arquivo veículos possui algum valor, se não, faz o alerta de que não possui
            if (contlinhas < 2)
            {
                Console.WriteLine("Não há veículos cadastrados!");
                Console.WriteLine("Aperte ENTER para continuar!");
                Console.ReadKey();
            }
            else
            {
                char opcao;
                do
                {
                    string linha;
                    Console.Clear();
                    Console.WriteLine("\t\tMANUTENÇÃO");
                    //armazena o código do veículo para poder procura-lo e atualizar seus valores
                    bool existe;
                    do
                    {
                        Console.WriteLine("Digite o código do veículo:");
                        codigo = Console.ReadLine();
                        StreamReader pesq = new StreamReader("veiculos.txt");
                        existe = false;
                        //vê se existe um carro com este código
                        do
                        {
                            linha = pesq.ReadLine();
                            if (linha == "Código: " + codigo)
                            {
                                existe = true;
                                break;
                            }
                        } while (linha != null);
                        if (existe == false)
                        {
                            Console.Clear();
                            Console.WriteLine("\t\tMANUTENÇÃO");
                            Console.WriteLine("Não possui carros com este código! Tente novamente!");
                        }
                        pesq.Close();
                    } while (existe == false);
                    string[] linhas = new string[contlinhas];
                    //abri novamente o arquivo e armazena seus valores no vetor
                    StreamReader armazenar = new StreamReader("veiculos.txt");
                    for (int x = 0; x < contlinhas; x++)
                    {
                        linhas[x] = armazenar.ReadLine();
                    }
                    armazenar.Close();
                    //abre o arquivo para alterar o valor do veículo para manutenção, disponível ou ocupado de acordo com oque possui no parâmetro tipo
                    ///primeiro altera-se no vetor
                    bool teste = false;
                    for (int x = 0; x < contlinhas; x++)
                    {
                        if (linhas[x] == "Código: " + codigo)
                        {
                            //mas só altera o valor se o carro não estiver ocupado
                            if (linhas[x - 1] == "Situação: MANUTENÇÃO" || linhas[x - 1] == "Situação: DISPONÍVEL")
                            {
                                linhas[x - 1] = "Situação: " + tipo;
                                teste = true;
                            }
                        }
                    }
                    if (teste == true) //caso tenha alterado os valores
                    {
                        FileStream arq3 = new FileStream("veiculos.txt", FileMode.OpenOrCreate);
                        StreamWriter escrever = new StreamWriter(arq3);
                        ///depois escreve o vetor no arquivo, assim atualizando os seus valores sobrescrevendo o que possuia
                        for (int x = 0; x < contlinhas; x++)
                        {
                            escrever.WriteLine(linhas[x]);
                        }
                        escrever.Close();
                        Console.Clear();
                        Console.WriteLine("\t\tMANUTENÇÃO");
                        Console.WriteLine("Valores atualizados com sucesso! O veículo foi atualizado para " + tipo);
                        Console.WriteLine("Aperte ENTER para continuar.");
                        Console.ReadKey();
                    }
                    else //caso o veículo esteja ocupado e não tenha alterado os valores
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tMANUTENÇÃO");
                        Console.WriteLine("Os valores do veículo não podem ser alterados pois está ocupado!");
                        Console.WriteLine("Aperte ENTER para continuar.");
                        Console.ReadKey();
                    }
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("\t\tMANUTENÇÃO");
                        Console.WriteLine("Deseja efetuar uma nova operação? (S-Sim/N-Não)");
                        opcao = char.Parse(Console.ReadLine().ToUpper());
                        if (!(opcao == 'S' || opcao == 'N'))
                        {
                            Console.WriteLine("Valores errados!Digite 'S' para SIM e 'N' para NÃO");
                        }
                    } while (!(opcao == 'S' || opcao == 'N'));
                } while (opcao == 'S');
            }
        }

        //Funções complementares
        private void tituloManutencao(string tipo) //esta função é privada e exibi somente um titulo para organizar a BaixaManutenção
        {
            Console.Clear();
            if (tipo == "DISPONÍVEL")
            {
                Console.WriteLine("\t\tDAR BAIXA EM MANUTENÇÃO");
            }
            else
            {
                Console.WriteLine("\t\tDECLARAR MANUTENÇÃO");
            }
        }
        private static bool ProcurarVeiculos() //esta função só existe para a locar() 
        {
            Console.Clear();
            Console.WriteLine("\t\tLOCAÇÃO");
            FileStream arq = new FileStream("veiculos.txt", FileMode.Open);
            StreamReader ler = new StreamReader(arq);
            //linhas para armazenar valores
            string linha, linhaocupante, linhadisponivel, situacao = "DISPONÍVEL";
            bool teste = false;
            do
            {
                //procura no arquivo veículos se tem algum carro correspondente ao desejo do usuário de ocupantes
                linhaocupante = ler.ReadLine();
                if (linhaocupante == "Quantidade de Ocupantes: " + ocupantes)
                {
                    linhadisponivel = ler.ReadLine();
                    if (linhadisponivel == "Situação: " + situacao)
                    {
                        Console.WriteLine(linhaocupante);
                        Console.WriteLine(linhadisponivel);
                        for (int i = 0; i < 7; i++)
                        {

                            linha = ler.ReadLine();
                            Console.WriteLine(linha);
                        }
                        teste = true; //quando encontrar escreve os valores deste na tela e além disso valida o teste como verdadeiro
                    }
                }
            } while (linhaocupante != null);
            ler.Close();
            //se não possuir veículos diponíveis
            if (teste == false)
            {
                Console.Clear();
                Console.WriteLine("\t\tLOCAÇÃO");
                Console.WriteLine("Não há veículos disponíveis!");
                Console.WriteLine("Aperte ENTER para continuar!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Aperte ENTER para escolher um dos veículos através do código!");
                Console.ReadKey();
            }
            return teste; //retorna se encontrou ou não por meio do teste
        }
        private static void Fidelidade(string nome, string cpf, int dias)
        {
            int acumulado, pontos = dias * 10, cont = 0;
            FileStream tamanho = new FileStream("fidelidade.txt", FileMode.OpenOrCreate);
            if (tamanho.Length < 1)
            {
                tamanho.Close();
                StreamWriter escrever = new StreamWriter("fidelidade.txt");
                escrever.WriteLine("PONTOS");
                escrever.WriteLine("Pontos:" + pontos);
                escrever.WriteLine(cpf);
                escrever.WriteLine(nome);
                escrever.WriteLine("---------------------------");
                escrever.Close();
            }
            else
            {
                tamanho.Close();
                FileStream arq = new FileStream("fidelidade.txt", FileMode.OpenOrCreate);
                //conta as linhas do arquivo texto
                StreamReader contar = new StreamReader(arq);
                while (contar.ReadLine() != null)
                {
                    cont++;
                }
                contar.Close();
                //recebe o valor do arquivo
                string[] fiel = new string[cont];
                StreamReader ler = new StreamReader("fidelidade.txt");
                for (int x = 0; x < cont; x++)
                {
                    fiel[x] = ler.ReadLine();
                }
                ler.Close();
                //pega os pontos que o cliente tinha
                bool teste = false, existe = false;
                string temp = "";
                for (int i = 0; i < cont; i++)
                {
                    if (fiel[i] == cpf)
                    {
                        existe = true;
                        foreach (char c in fiel[i - 1])
                        {
                            if (c == ':')
                            {
                                teste = true;
                            }
                            else if (teste == true)
                            {
                                temp += c;
                            }
                        }
                        acumulado = int.Parse(temp);
                        acumulado = acumulado + pontos;
                        fiel[i - 1] = "Pontos:" + acumulado;
                    }
                }
                //escreve no arquivo texto caso já possua o valor do cliente
                if (existe == true)
                {
                    StreamWriter registrar = new StreamWriter("fidelidade.txt");
                    for (int i = 0; i < cont; i++)
                    {
                        registrar.WriteLine(fiel[i]);
                    }
                    registrar.Close();
                }
                //caso ainda não possua locações desse cliente
                else
                {
                    StreamWriter escrever2 = new StreamWriter("fidelidade.txt", true);
                    escrever2.WriteLine("PONTOS");
                    escrever2.WriteLine("Pontos:" + pontos);
                    escrever2.WriteLine(cpf);
                    escrever2.WriteLine(nome);
                    escrever2.WriteLine("---------------------------");
                    escrever2.Close();
                }
            }
        } //esta função só existi para quando darBaixa em locação 
          ///calcula a fidelidade direto
    }
}
