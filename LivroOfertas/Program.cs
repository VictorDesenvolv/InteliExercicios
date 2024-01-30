// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;



string input = "12\r\n1,0,15.4,50\r\n2,0,15.5,50\r\n2,2,0,0\r\n2,0,15.4,10\r\n3,0,15.9,30\r\n3,1,0,20\r\n4,0,16.50,200\r\n5,0,17.00,100\r\n5,0,16.59,20\r\n6,2,0,0\r\n1,2,0,0\r\n2,1,15.6,0";

var item = LivroServices.ProcessarHistoricoLivros(input);

Console.WriteLine(item);

static class LivroServices
{
    public static string ProcessarHistoricoLivros(string input)
    {
        List<Livro> Livros = new List<Livro>();
        List<Livro> LivrosNaoEncontrados = new List<Livro>();

        var deinput = input.Substring(0, input.IndexOf("\r\n"));

        input = input.Replace($"{deinput}\r\n", "");

        string[] inputArray = input.Split("\r\n");


        foreach (string str in inputArray)
        {
            string[] item = str.Split(",");

            Livro livro = new Livro
            {
                Posicao = int.Parse(item[0]),
                Acao = int.Parse(item[1]),
                Valores = double.Parse(item[2].Replace(".",",")),
                Quantidade = int.Parse(item[3])
            };

            livro.AplicarAcao(Livros, LivrosNaoEncontrados);

        }

        Livros.ReorganizarPorValor();

        string mensagemRetorno = "";

        foreach(var item in Livros)
        {
            mensagemRetorno += $"{item.Posicao},{item.Valores.ToString().Replace(",",".")},{item.Quantidade}\r\n";
        }

        return mensagemRetorno;


    }

    private static void ReorganizarPorValor(this List<Livro> Livros)
    {
        Livros.OrderByDescending(t => t.Valores).ToList();

        for (int i = 0; i < Livros.Count; i++)
        {
            Livros[i].Posicao = i + 1;
        }
    }

    private static void AplicarAcao(this Livro livro, List<Livro> Livros, List<Livro> LivrosNaoEncontrados)
    {
        if (livro.Acao == 0)
        {
            Livros.Add(livro);
        }

        if (livro.Acao == 1)
        {
            var livroLista = Livros.FirstOrDefault(l => l.Posicao == livro.Posicao);
            if (livroLista != null)
            {
                livroLista.Valores = livro.Valores;
                livroLista.Quantidade = livro.Quantidade;
            }
            else
            {
                LivrosNaoEncontrados.Add(livro);
            }
        }

        if (livro.Acao == 2)
        {
            var livroLista = Livros.FirstOrDefault(l => l.Posicao == livro.Posicao);
            if (livroLista != null)
            {
                Livros.Remove(livroLista);
            }
            else
            {
                LivrosNaoEncontrados.Add(livro);
            }
        }

    }
}


class Livro{
    public int Posicao { get; set; }
    public int Acao { get; set; }
    public double Valores { get; set; }
    public int Quantidade { get; set; }
}