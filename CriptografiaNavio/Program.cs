// See https://aka.ms/new-console-template for more information

using System;
using System.Text;

string byteSegment = "10010110 11110111 01010110 00000001 00010111 00100110 01010111 00000001 00010111 01110110 01010111 00110110 11110111 11010111 01010111 00000011";


Console.WriteLine(byteSegment.DescriptografarBits());


public static class StringExtensions
{
    public static String DescriptografarBits(this String byteSegment)
    {
        if(!byteSegment.Contains(" "))
        {
            return "formato diferente da capacidade do sistema";
        }

        string[] byteArray = byteSegment.Split(' ');

        string novoSegmento = "";

        foreach (string eightBits in byteArray)
        {
            // aqui ele ja pega os ultimos 2 digitos dos 8 Bits e inverte eles;
            string ultimosDigitos = eightBits.Substring(eightBits.Length - 2).InverterBits();
            string primeirosDigitos = eightBits.Substring(0, eightBits.Length - 2);

            // aqui só foi juntar os dígitos e inverter os primeiros 4 pelos outros 4 e colocar no novo segmento

            string digitosNovos = primeirosDigitos + ultimosDigitos;

            string metadeInicial = digitosNovos.Substring(0, digitosNovos.Length / 2);
            string metadeFinal = digitosNovos.Substring(digitosNovos.Length / 2);

            novoSegmento += (metadeFinal + metadeInicial);
        }

        //como é mais facil manipular a string eu deixo para depois de decodificar a conversão para byte[] (um array de byte);

        byte[] segmentoEmBytes = novoSegmento.StringParaBytes();

        //para finalizar só é preciso usar a propria função que vem do System.Text de converter para ASCII, para deixar a mensagem mais limpa eu formato ela tirando o \0 que apenas signifca vazio, ou nesse caso o fim da mensagem;

        string mensagemDescriptografada = Encoding.ASCII.GetString(segmentoEmBytes);

        string mensagemFormatada = mensagemDescriptografada.Replace("\0", "");

        return mensagemFormatada;
    }


    private static String InverterBits(this String s)
    {
        return new string(s.Take(3)
                .Select(c => c == '0' ? '1' : '0')
                .Concat(s.Skip(3))
                .ToArray());
    }

    // no final eu não usei esse conversor de string para Hex mas vou manter ele ja que é um jeito tambem de resolver, eu tentei resolver da forma mais simples para que fique o mais legível o possível.
    private static String ConversorHex(this String s)
    {
        return string.Join(" ",
            Enumerable.Range(0, s.Length / 8)
            .Select(i => Convert.ToByte(s.Substring(i * 8, 8), 2).ToString("X2")));
    }

    private static Byte[] StringParaBytes(this String s)
    {
        var list = new List<Byte>();

        for (int i = 0; i < s.Length; i += 8)
        {
            String t = s.Substring(i, 8);

            list.Add(Convert.ToByte(t, 2));
        }

        return list.ToArray();
    }

}