using System.Diagnostics;

namespace Core;

public static class Benchmark
{
    private static List<long> _times = new();
    private static Stopwatch _timer = new();

    public static void BenchKeyGeneration(int iterations, int bitSize, string measure = "ms")
    {
        float k = 1f;
        if (measure == "sec") k = 1000f;

        _times.Clear();
        for (int i = 0; i < iterations; i++)
        {
            _timer.Restart();

            new RSA(bitSize);

            Console.WriteLine($"{bitSize} bits key pair generation took {_timer.ElapsedMilliseconds/k} {measure}");
            _times.Add(_timer.ElapsedMilliseconds);
        }

        Console.WriteLine($"total time: {_times.Sum() / k} {measure}");
        Console.WriteLine($"average time: {_times.Sum() / (_times.Count * k)} {measure}");
        Console.WriteLine();
    }

    public static void BenchEncryptionDecryption(int iterations, int bitSize, int input, string measure = "ms")
    {
        float k = 1f;
        if (measure == "sec") k = 1000f;
        
        _times.Clear();
        var rsa = new RSA(bitSize);
        for (int i = 0; i < iterations; i++)
        {
            _timer.Restart();
            rsa.Decrypt(rsa.Encrypt(input));

            Console.WriteLine($"RSA with {bitSize} bits key encryption/decryption of [{input}] took {_timer.ElapsedMilliseconds/k} {measure}");
            _times.Add(_timer.ElapsedMilliseconds);
        }
        Console.WriteLine($"total time: {_times.Sum()/k} {measure}");
        Console.WriteLine($"average time: {_times.Sum()/(_times.Count*k)} {measure}");
        Console.WriteLine();

    }
}