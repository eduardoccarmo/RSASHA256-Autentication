using ConsoleApp3.Configurations;
using ConsoleApp3.Services;

class Program
{
    static async Task Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        string path = @"Inserir caminho do arquivo";
        var privateKey = File.ReadAllText(path);
        var httpClient = new HttpClient();

        var tokenService = new TokenService();
        var httpClientService = new HttpClientService(httpClient);

        var assertion = tokenService.GenerateJwtTokenWithRsa256Signature(privateKey);

        Console.WriteLine("assertion: " + assertion + "\n");

        var token = await httpClientService.GenerateToken(assertion);

        Console.WriteLine("access_token: " + token.access_token + "\n\n" + $"expires_in: {DateTime.Now.AddSeconds(token.expires_in)}");

        Console.WriteLine("Pressione qualquer tecla para encerrar a execução do programa.");
        Console.ReadKey(); 
    }
}
