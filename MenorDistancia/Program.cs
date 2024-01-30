
// Exemplo de arrays
int[] arr1 = { -1, 2, 8, 12, 20, 25, 30, 35, 40, 45, 55 };
int[] arr2 = { 26, 6, 10, 15, 18, 32, 38, 42, 50, 80 };

var menorDistanciaInfo = EncontrarMenorDistancia(arr1, arr2);

Console.WriteLine($"Os números são:  {menorDistanciaInfo.Numeroarr1} do arr1 e { menorDistanciaInfo.Numeroarr2} do arr2. Sendo a menor distancia: {menorDistanciaInfo.Distancia}");


// a função que calcula a menor distancia.
static DistanciaInfo EncontrarMenorDistancia(int[] arr1, int[] arr2)
{
    // Ordenando os arrays
    Array.Sort(arr1);
    Array.Sort(arr2);

    int menorDistancia = int.MaxValue;
    int i = 0, j = 0;
    DistanciaInfo menorDistanciaInfo = new DistanciaInfo();

    // encontrando a menor diferença dos arrays, só para diferenciar eu tentei evitar aqueles nested loops, que foi a solução mais simples q me veio a mente. Fazendo assim fica mais eficiente, mas em um exercicio simples acredito que não faça tanta diferença.
    while (i < arr1.Length && j < arr2.Length)
    {
        int diff = Math.Abs(arr1[i] - arr2[j]);

        // atualizando a menor distancia e as informações
        if (diff < menorDistancia)
        {
            menorDistancia = diff;
            menorDistanciaInfo.Distancia = diff;
            menorDistanciaInfo.Numeroarr1 = arr1[i];
            menorDistanciaInfo.Numeroarr2 = arr2[j];
        }

        // movendo para o proximo elemento no array com o valor menor
        if (arr1[i] < arr2[j])
            i++;
        else
            j++;
    }

    return menorDistanciaInfo;
}

class DistanciaInfo
{
    public int Distancia { get; set; }
    public int Numeroarr1 { get; set; }
    public int Numeroarr2 { get; set; }
}