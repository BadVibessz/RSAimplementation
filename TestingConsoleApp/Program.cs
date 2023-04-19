using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text;
using Core;

// // benchmark
// var timer = new Stopwatch();
// int iterations = 50;
// timer.Start();
//
// Benchmark.BenchKeyGeneration(iterations,512);
// Benchmark.BenchKeyGeneration(iterations,1024);
// Benchmark.BenchKeyGeneration(iterations,2048,"sec");
//
// Benchmark.BenchEncryptionDecryption(iterations,512,int.MaxValue);
// Benchmark.BenchEncryptionDecryption(iterations,1024,int.MaxValue);
// Benchmark.BenchEncryptionDecryption(iterations,2048,int.MaxValue);


int CalculateMaxLength(RSA rsa)
{
    string str = "a";

    var encrypted = rsa.Encrypt(str);
    var decrypted = rsa.Decrypt(encrypted);

    while (str == decrypted)
    {
        str += "a";

        encrypted = rsa.Encrypt(str);
        decrypted = rsa.Decrypt(encrypted);
    }

    return str.Length;
}


int bitSize = 256;
var rsa = new RSA(bitSize: bitSize, encoding: Encoding.ASCII);

Console.WriteLine($"Input a string with length from 1 to {bitSize / 8}");

while (true)
{
    // we cannot encrypt string that is longer than bitSize / 8
    var plainText = Console.ReadLine();

    var encrypted = rsa.Encrypt(plainText);
    var decrypted = rsa.Decrypt(encrypted);

    Console.WriteLine($"Plain text: {plainText}");
    Console.WriteLine($"Encrypted: {encrypted}");
    Console.WriteLine($"Decrypted: {decrypted}");
    Console.WriteLine();
}