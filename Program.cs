using System;
using System.Buffers;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.IO;

namespace TextEditor
{

    class Program
    {

        static void Main(String[] args)
        {

            Menu();

        }

        static void Menu()
        {

            Console.Clear();
            Console.WriteLine("Escolha uma opção: \n");

            Console.WriteLine("1 - Abrir um arquivo");
            Console.WriteLine("2 - Criar um novo arquivo");
            Console.WriteLine("0 - Sair");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {

                case 1: Abrir(); break;
                case 2: Editar(); break;
                default: Environment.Exit(0); break;

            }
        }

        static void Abrir()
        {

            Console.Clear();
            Console.WriteLine("Digite o caminho do arquivo: ");

            string path = string.Empty;
            bool caminhoInvalido = true;


            try //Verifica se o diretório existe antes de abri-lo
            {
                while (caminhoInvalido)
                {
                    path = Console.ReadLine();


                    if (string.IsNullOrWhiteSpace(path))
                    {

                        Console.WriteLine("O diretório precisa estar preenchido. Digite o caminho do arquivo: ");

                    }

                    else
                    {
                        path = VerificaValidadeDiretorio(path, true);
                        string pathDirectory = Path.GetDirectoryName(path);

                        string fileName = Path.GetFileName(path);

                        Console.WriteLine(path);
                        if (Directory.Exists(pathDirectory) && !string.IsNullOrWhiteSpace(fileName))
                        {

                            caminhoInvalido = false;

                        }

                        else
                        {
                            Console.WriteLine("Caminho inválido.\nExemplo: C:\\nomedapasta\\nomedoarquivo.txt\n Tente novamente: ");
                        }
                    }

                }
            }
            catch (System.Exception)
            {

                throw;
            }

            using (var file = new StreamReader(path))
            {

                string text = file.ReadToEnd();
                Console.WriteLine(text);

            }

            Console.WriteLine("");
            Console.ReadLine();
            Menu();
        }

        static void Editar()
        {

            Console.Clear();
            Console.WriteLine("Digite seu texto. (Aperte ESC para sair)");

            string text = "";

            do
            {

                text += Console.ReadLine();
                text += Environment.NewLine;

            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Salvar(text);

        }

        static void Salvar(string text)
        {

            Console.Clear();
            Console.WriteLine("Digite o caminho que você deseja salvar");


            bool caminhoInvalido = true;
            string path = string.Empty;

            try
            {
                while (caminhoInvalido)
                {

                    path = Console.ReadLine();
                    path = VerificaValidadeDiretorio(path, true);

                    if (string.IsNullOrWhiteSpace(path))
                    {
                        Console.Clear();
                        Console.WriteLine("O caminho precisa ser preenchido");

                    }
                    else
                    {

                        string pathDirectory = Path.GetDirectoryName(path);
                        string fileName = Path.GetFileName(path);

                        string[] pathFiles = Directory.GetFiles(pathDirectory);
                        bool pathValidity = true;

                        for (int i = 0; i <= pathFiles.Length - 1; i++)
                        {
                            if (pathFiles[i].Contains(fileName))
                            {
                                Console.WriteLine(pathFiles[i]);
                                pathValidity = false;
                                break;
                            }

                        }

                        if (Directory.Exists(pathDirectory) && !string.IsNullOrWhiteSpace(fileName) && pathValidity == true)
                        {
                            path = path.TrimEnd(Path.DirectorySeparatorChar);
                            Console.WriteLine(path);
                            caminhoInvalido = false;

                        }

                        else
                        {

                            Console.WriteLine("O caminho está inválido, tente novamente: ");

                        }
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }


            using (var file = new StreamWriter(path))
            {

                file.Write(text);

            }

            Console.Write($"O arquivo {path} foi salvo com sucesso");
            Console.ReadKey();
            Menu();

        }

        static string VerificaValidadeDiretorio(string path, bool metodoAbrir = false, bool metodoFechar = false)
        {
            if (path.EndsWith(".txt") && metodoAbrir == true)
            {
                return path;
            }
            // Adiciona a barra (\) ao final da string se ela nao existir para criar um diretório valido
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar;
            }
            return path;
        }



    }
}