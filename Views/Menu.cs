using System;
using System.Collections.Generic;
using System.Linq;
using registroPontoConsole.Models;

namespace registroPontoConsole
{
    public class Menu
    {
        public RegistroColaborador colaborador = new RegistroColaborador();
        public RegistroHoras registro = new RegistroHoras();
        public List<RegistroColaborador> colaboradores = new List<RegistroColaborador>();
        List<char> listaStatus = new List<char> { 'e', 'E', 's', 'S' };

        public void MenuInicial()
        {
            int decisao;
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
                    Console.WriteLine("  0 - Sair  ");
                    Console.WriteLine("########################");
                    Console.Write("[Escolha uma opção] ");
                    decisao = int.Parse(Console.ReadLine());

                    switch (decisao)
                    {
                        case 1:
                            Registro();
                            Console.ReadKey();
                            break;
                        case 2:
                            CadastroColaborador();
                            Console.ReadKey();
                            break;
                        case 3:
                            Reports();
                            Console.ReadKey();
                            break;
                        case 0:
                            Console.WriteLine("\n\n[ ATÉ LOGO! ]");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("\n\nComando '{0}' não é válido.\n\nTente novamente.", decisao);
                            Console.ReadKey();
                            break;
                    }
                } while (decisao != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
                Console.ReadKey();
            }
            finally
            {
                MenuInicial();
            }
        }

        public void Registro()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("[REGISTRAR] ");

                Console.Write("\nMatricula: ");
                int matricula = int.Parse(Console.ReadLine());
                Console.Write("Entrada ou Saida (e/s)? ");
                char status = Char.Parse(Console.ReadLine());

                if (!listaStatus.Contains(status))
                {
                    Console.WriteLine("\n[Erro de indicador!]");
                }
                else if (!colaboradores.Exists(x => x.Matricula == matricula))
                {
                    Console.WriteLine("\n[Matrícula não existe ou erro de indicador!]");
                }
                else
                {
                    var colab = colaboradores.FirstOrDefault(x => x.Matricula == matricula);

                    registro = new RegistroHoras(DateTimeOffset.Now, status);

                    colab.AddRegistro(registro);
                    Console.WriteLine("\n- Registro salvo com sucesso! -\n");
                    Console.WriteLine("Colaborador: " + colaborador.Nome);
                    Console.WriteLine(registro);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
                Console.ReadKey();
            }
            finally
            {
                MenuInicial();
            }
        }                    

        public void CadastroColaborador()
        
        {
            Console.Clear();
            try
            {
                Console.WriteLine(" [CADASTRAR NOVO COLABORADOR] ");
                Console.Write("\nMatricula: ");
                int matricula = int.Parse(Console.ReadLine());
                Console.Write("Nome: ");
                string nome = Console.ReadLine();
                if (colaboradores.Any(x => x.Matricula == matricula))
                {
                    Console.WriteLine("\n[Matrícula já existe! Tente novamente!]");
                }
                else
                {
                    var colab = new RegistroColaborador(Guid.NewGuid(), matricula, nome);
                    colaboradores.Add(colab);
                    Console.WriteLine("\n- Registro salvo com sucesso! -\n");
                    Console.WriteLine(colab);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
                Console.ReadKey();
            }
            finally
            {
                MenuInicial();
            }
        }         

        public void Reports()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("[REPORTS]");
                Console.Write("\nMatricula: ");
                int matricula = int.Parse(Console.ReadLine());

                var colab = colaboradores.First(x => x.Matricula == matricula);
                Console.WriteLine("Colaborador: " + colab.Nome);
                foreach (var reg in colab.RegistrosDeHoras)
                {
                    Console.WriteLine(reg);
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[OCORREU ERRO] :  " + ex);
                Console.WriteLine("\n\n[ENTER] PARA RETORNAR AO MENU PRINCIPAL");
                Console.ReadKey();
            }
            finally
            {
                MenuInicial();
            }
        }
    }
}
