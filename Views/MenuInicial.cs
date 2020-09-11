using registroPontoConsole.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace registroPontoConsole
{
    public class MenuInicial
    {
        public List<RegistroPonto> colaboradores = new List<RegistroPonto>();
        public List<char> listaIndicadores = new List<char> { 'e', 's' };

        public void MenuInicialView()
        {
            string decisao;
            try
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("########################");
                    Console.WriteLine(" [REGISTRO DE PONTO]  ");
                    Console.WriteLine("  1 - REGISTRAR  ");
                    Console.WriteLine("  2 - CADASTRAR NOVO COLOBORADOR  ");
                    Console.WriteLine("  3 - REPORTS  ");
                    Console.WriteLine("  4 - SALVAR REPORTS  ");
                    Console.WriteLine("  0 - Sair  ");
                    Console.WriteLine("########################");
                    Console.Write("[Escolha uma opção] ");
                    decisao = Console.ReadLine();

                    switch (decisao)
                    {
                        case "1":
                            RegistrosView();
                            Console.ReadKey();
                            break;
                        case "2":
                            CadastroColaboradorView();
                            Console.ReadKey();
                            break;
                        case "3":
                            ReportsView();
                            Console.ReadKey();
                            break;
                        case "4":
                            SalvarReportsView();
                            Console.ReadKey();
                            break;
                        case "5":
                             cool();
                            Console.ReadKey();
                            break;
                        case "0":
                            Console.WriteLine("\n\n[ ATÉ LOGO! ]");
                            break;
                        default:
                            Console.WriteLine("\n\nComando '{0}' não é válido.\n\nTente novamente.", decisao);
                            Console.ReadKey();
                            break;
                    }
                } while (decisao != "0");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
            }
        }

        public void RegistrosView()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("[REGISTRAR] ");

                Console.Write("\nMatricula: ");
                int matricula = int.Parse(Console.ReadLine());

                if (!colaboradores.Exists(x => x.Matricula == matricula))
                {
                    Console.WriteLine("\n[Matrícula não existe!] Cadastre um novo colaborador!");
                }
                else
                {
                    var colab = colaboradores.FirstOrDefault(x => x.Matricula == matricula);

                    if (colab.VerificarStatus() == true)
                    {
                        var registro = new RegistroHora(DateTime.Now, 'E');
                        colab.AddRegistro(registro);
                        colab.AddEntrada(registro.Date);

                        Console.WriteLine("\n- Registro salvo com sucesso! -\n");
                        Console.WriteLine("Colaborador: " + colab.Nome);
                        Console.WriteLine("Entrada : " + registro);
                    }
                    else
                    {
                        var registro = new RegistroHora(DateTime.Now, 'S');
                        colab.AddRegistro(registro);
                        colab.AddSaida(registro.Date);

                        Console.WriteLine("\n- Registro salvo com sucesso! -\n");
                        Console.WriteLine("Colaborador: " + colab.Nome);
                        Console.WriteLine(registro);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
            }
        }

        public void CadastroColaboradorView()
        {
            Console.Clear();
            try
            {
                Console.WriteLine(" [CADASTRAR NOVO COLABORADOR] ");
                Console.Write("\nMatricula (4 Dígitos): ");
                int matricula = int.Parse(Console.ReadLine());
                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                if (colaboradores.Any(x => x.Matricula == matricula))
                {
                    Console.WriteLine("\n[Número de matrícula já existe! Tente outra matrícula!]");
                    Console.WriteLine("[Número de matrícula deve conter 4 dígitos!]");
                }
                else if (String.IsNullOrWhiteSpace(nome))
                {
                    Console.WriteLine("\n[Campo nome não pode ser vazio!]");
                }
                else
                {
                    var colab = new RegistroPonto(Guid.NewGuid(), matricula, nome);
                    colaboradores.Add(colab);
                    Console.WriteLine("\n- Registro salvo com sucesso! -\n");
                    Console.WriteLine(colab);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
            }
        }

        public void ReportsView()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("[REPORTS]");
                Console.Write("\nMatricula: ");
                int matricula = int.Parse(Console.ReadLine());

                var colab = colaboradores.First(x => x.Matricula == matricula);
                Console.WriteLine("Colaborador: " + colab.Nome);
                foreach (var reg in colab.RegistroDeHoras)
                {
                    Console.WriteLine(reg);
                }
                Console.WriteLine("Total de horas trabalhadas: " + colab.HorasTrabalhadas());
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
            }
        }

        public void SalvarReportsView()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("[SALVAR REPORTS] ");
                Console.Write("\nDejesa salvar os reports em arquivo (s/n)? ");
                char status = Char.Parse(Console.ReadLine());
                if (status == 's')
                {
                    string stg = @"C:\git\TestesGX2\registro-ponto-console\controle-de-ponto.txt";

                    var colaborador = colaboradores;
                    using (StreamWriter write = File.AppendText(stg))
                    {
                        write.WriteLine("[REPORTS]");

                        foreach (var c in colaborador)
                        {
                            write.WriteLine("\nColaborador: " + c.Nome);
                            foreach (var reg in c.RegistroDeHoras)
                            {
                                write.WriteLine(reg);
                            }
                            write.WriteLine("Total de horas trabalhadas: " + c.HorasTrabalhadas());
                        }
                    }
                    Console.WriteLine("[Arquivo salvo com sucesso!] Arquivo salvo na pasta principal desse projeto! ");
                }
                else
                {
                    Console.WriteLine("Retornando ao menu inicial!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
            }
        }

        public void cool()
        {
            //USUARIO 01
            colaboradores.Add(new RegistroPonto(Guid.NewGuid(), 1122, "Charlie Brown"));
            DateTimeOffset d1 = new DateTimeOffset(2000, 02, 16, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d2 = new DateTimeOffset(2000, 02, 16, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d3 = new DateTimeOffset(2000, 02, 16, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d4 = new DateTimeOffset(2000, 02, 16, 18, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d5 = new DateTimeOffset(2000, 02, 18, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d6 = new DateTimeOffset(2000, 02, 18, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d7 = new DateTimeOffset(2000, 02, 18, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d8 = new DateTimeOffset(2000, 02, 18, 18, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d9 = new DateTimeOffset(2000, 02, 20, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d10 = new DateTimeOffset(2000, 02, 20, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d11 = new DateTimeOffset(2000, 02, 20, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d12 = new DateTimeOffset(2000, 02, 20, 18, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d13 = new DateTimeOffset(2000, 02, 22, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d14 = new DateTimeOffset(2000, 02, 22, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d15 = new DateTimeOffset(2000, 02, 22, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d16 = new DateTimeOffset(2000, 02, 22, 18, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d17 = new DateTimeOffset(2000, 02, 24, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d18 = new DateTimeOffset(2000, 02, 24, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d19 = new DateTimeOffset(2000, 02, 24, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d20 = new DateTimeOffset(2000, 02, 24, 18, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d21 = new DateTimeOffset(2000, 02, 26, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d22 = new DateTimeOffset(2000, 02, 26, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d23 = new DateTimeOffset(2000, 02, 26, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d24 = new DateTimeOffset(2000, 02, 26, 18, 00, 00, new TimeSpan(-3, 0, 0));

            var reg = colaboradores.FirstOrDefault(x => x.Matricula == 1122);
            reg.AddRegistro(new RegistroHora(d1, 'e'));
            reg.AddRegistro(new RegistroHora(d2, 's'));
            reg.AddRegistro(new RegistroHora(d3, 'e'));
            reg.AddRegistro(new RegistroHora(d4, 's'));
            reg.AddRegistro(new RegistroHora(d5, 'e'));
            reg.AddRegistro(new RegistroHora(d6, 's'));
            reg.AddRegistro(new RegistroHora(d7, 'e'));
            reg.AddRegistro(new RegistroHora(d8, 's'));
            reg.AddRegistro(new RegistroHora(d9, 'e'));
            reg.AddRegistro(new RegistroHora(d10, 's'));
            reg.AddRegistro(new RegistroHora(d11, 'e'));
            reg.AddRegistro(new RegistroHora(d12, 's'));
            reg.AddRegistro(new RegistroHora(d13, 'e'));
            reg.AddRegistro(new RegistroHora(d14, 's'));
            reg.AddRegistro(new RegistroHora(d15, 'e'));
            reg.AddRegistro(new RegistroHora(d16, 's'));
            reg.AddRegistro(new RegistroHora(d17, 'e'));
            reg.AddRegistro(new RegistroHora(d18, 's'));
            reg.AddRegistro(new RegistroHora(d19, 'e'));
            reg.AddRegistro(new RegistroHora(d20, 's'));
            reg.AddRegistro(new RegistroHora(d21, 'e'));
            reg.AddRegistro(new RegistroHora(d22, 's'));
            reg.AddRegistro(new RegistroHora(d23, 'e'));
            reg.AddRegistro(new RegistroHora(d24, 's'));

            reg.AddEntrada(d1);
            reg.AddEntrada(d3);
            reg.AddEntrada(d5);
            reg.AddEntrada(d7);
            reg.AddEntrada(d9);
            reg.AddEntrada(d11);
            reg.AddEntrada(d13);
            reg.AddEntrada(d15);
            reg.AddEntrada(d17);
            reg.AddEntrada(d19);
            reg.AddEntrada(d21);
            reg.AddEntrada(d23);

            reg.AddSaida(d2);
            reg.AddSaida(d4);
            reg.AddSaida(d6);
            reg.AddSaida(d8);
            reg.AddSaida(d10);
            reg.AddSaida(d12);
            reg.AddSaida(d14);
            reg.AddSaida(d16);
            reg.AddSaida(d18);
            reg.AddSaida(d20);
            reg.AddSaida(d22);
            reg.AddSaida(d24);



            //USUARIO 02
            colaboradores.Add(new RegistroPonto(Guid.NewGuid(), 1123, "Joe Satriani"));
            DateTimeOffset d30 = new DateTimeOffset(2000, 02, 16, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d31 = new DateTimeOffset(2000, 02, 16, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d32 = new DateTimeOffset(2000, 02, 16, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d33 = new DateTimeOffset(2000, 02, 16, 18, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d34 = new DateTimeOffset(2000, 02, 18, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d35 = new DateTimeOffset(2000, 02, 18, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d36 = new DateTimeOffset(2000, 02, 18, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d37 = new DateTimeOffset(2000, 02, 18, 18, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d38 = new DateTimeOffset(2000, 02, 20, 08, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d39 = new DateTimeOffset(2000, 02, 20, 12, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d40 = new DateTimeOffset(2000, 02, 20, 14, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d41 = new DateTimeOffset(2000, 02, 20, 18, 00, 00, new TimeSpan(-3, 0, 0));

            var reg2 = colaboradores.FirstOrDefault(x => x.Matricula == 1123);
            reg2.AddRegistro(new RegistroHora(d30, 'e'));
            reg2.AddRegistro(new RegistroHora(d31, 's'));
            reg2.AddRegistro(new RegistroHora(d32, 'e'));
            reg2.AddRegistro(new RegistroHora(d33, 's'));
            reg2.AddRegistro(new RegistroHora(d34, 'e'));
            reg2.AddRegistro(new RegistroHora(d35, 's'));
            reg2.AddRegistro(new RegistroHora(d36, 'e'));
            reg2.AddRegistro(new RegistroHora(d37, 's'));
            reg2.AddRegistro(new RegistroHora(d38, 'e'));
            reg2.AddRegistro(new RegistroHora(d39, 's'));
            reg2.AddRegistro(new RegistroHora(d40, 'e'));
            reg2.AddRegistro(new RegistroHora(d41, 's'));

            reg2.AddEntrada(d30);
            reg2.AddEntrada(d32);
            reg2.AddEntrada(d34);
            reg2.AddEntrada(d36);
            reg2.AddEntrada(d38);
            reg2.AddEntrada(d40);

            reg2.AddSaida(d31);
            reg2.AddSaida(d33);
            reg2.AddSaida(d35);
            reg2.AddSaida(d37);
            reg2.AddSaida(d39);
            reg2.AddSaida(d41);



            //USUARIO 03
            colaboradores.Add(new RegistroPonto(Guid.NewGuid(), 1124, "chuck norris"));
            DateTimeOffset d50 = new DateTimeOffset(2000, 02, 16, 00, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d51 = new DateTimeOffset(2000, 02, 16, 23, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d52 = new DateTimeOffset(2000, 02, 16, 00, 00, 00, new TimeSpan(-3, 0, 0));
            DateTimeOffset d53 = new DateTimeOffset(2000, 02, 16, 23, 00, 00, new TimeSpan(-3, 0, 0));

            var reg3 = colaboradores.FirstOrDefault(x => x.Matricula == 1124);
            reg3.AddRegistro(new RegistroHora(d50, 'e'));
            reg3.AddRegistro(new RegistroHora(d51, 's'));
            reg3.AddRegistro(new RegistroHora(d52, 'e'));
            reg3.AddRegistro(new RegistroHora(d53, 's'));


            reg3.AddEntrada(d50);
            reg3.AddEntrada(d52);


            reg3.AddSaida(d51);
            reg3.AddSaida(d53);

            Console.WriteLine("\n\n[ TESTAR REPORTS ]");
        }
    }
}
