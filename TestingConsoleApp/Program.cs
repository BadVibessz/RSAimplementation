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


//Console.OutputEncoding = Encoding.UTF8;



// var hash = DataConverter.StringToInteger(plainText, Encoding.ASCII);
// var obratno = DataConverter.IntegerToString(hash, 25, Encoding.ASCII);
//
// var encrypted = rsa.Encrypt(hash);
//
// var bytes = encrypted.ToByteArray();
// var encryptedText = Convert.ToBase64String(bytes);
//
// //var encryptedText = DataConverter.IntegerToString(encrypted, 256, Encoding.ASCII);
//
// Console.WriteLine($"Encrypted: {encryptedText}");
//
// //var encryptedHash = DataConverter.StringToInteger(encryptedText, Encoding.ASCII);
// var encryptedHash = new BigInteger(Convert.FromBase64String(encryptedText), isUnsigned: true);
// Console.WriteLine(encryptedHash == encrypted); // todo: to know why not equals
//
// var decrypted = rsa.Decrypt(encryptedHash);
// var decryptedText = DataConverter.IntegerToString(decrypted, 5, Encoding.ASCII);
// Console.WriteLine($"Decrypted: {decryptedText}");

// var rsa = new RSA(bitSize: 256, encoding: Encoding.ASCII);
//

var rsa = new RSA(bitSize: 256, encoding: Encoding.ASCII);
var plainText = "Secure me!";

var encrypted = rsa.Encrypt(plainText);
var decrypted = rsa.Decrypt(encrypted);

Console.WriteLine($"Plain text: {plainText}");
Console.WriteLine($"Encrypted: {encrypted}");
Console.WriteLine($"Decrypted: {decrypted}");

// var rsa = new RSA(bitSize: 256);
//
// var plainText = "Suka!";
//
// var encryptedText = rsa.Encrypt(plainText);
// Console.WriteLine($"Encrypted: {encryptedText}");
//
// var decryptedText = rsa.Decrypt(encryptedText);
// Console.WriteLine($"Decrypted: {decryptedText}");