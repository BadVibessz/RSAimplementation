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




int bitSize = 256;
var encoding  = Encoding.UTF8;
var rsa = new RSA(bitSize: bitSize, encoding: encoding);

Console.WriteLine($"Input a string with length from 1 to {bitSize / 8}");

var bytes = new byte[32];

for (int i = 0; i < 32; i++)
    bytes[i] = 1;

var encryptedBytes = rsa.Encrypt(bytes);
var decryptedBytes = rsa.Decrypt(encryptedBytes);


while (true)
{
    // // we cannot encrypt string that is longer than bitSize / 8
    // var plainText = Console.ReadLine();
    //
    // var encrypted = rsa.EncryptToBase64(plainText ?? "");
    // var decrypted = rsa.DecryptFromBase64(encrypted);
    //
    // Console.WriteLine($"Plain text: {plainText}");
    // Console.WriteLine($"Encrypted: {encrypted}");
    // Console.WriteLine($"Decrypted: {decrypted}");
    // Console.WriteLine();
    
    // we cannot encrypt string that is longer than bitSize / 8
    var plainText = Console.ReadLine();

     var encrypted = rsa.Encrypt(encoding.GetBytes(plainText ?? ""));
     var decrypted = rsa.Decrypt(encrypted);
     
     Console.WriteLine($"Plain text: {plainText}");
     Console.WriteLine($"Encrypted: {encoding.GetString(encrypted)}");
     Console.WriteLine($"Decrypted: {encoding.GetString(decrypted)}");
     Console.WriteLine();
}