using System.Diagnostics;
using Core;

var timer = new Stopwatch();

timer.Start();
var rsa = new RSA();
timer.Stop();

Console.WriteLine($"keys generated for: {timer.ElapsedMilliseconds} ms\n");

timer.Restart();
var input = 19032002;
var encrypted = rsa.Encrypt(input);
var decrypted = rsa.Decrypt(encrypted);
timer.Stop();

Console.WriteLine($"input: {input}");
Console.WriteLine($"encrypted: {encrypted}");
Console.WriteLine($"decrypted: {decrypted}");
Console.WriteLine(input == decrypted);

Console.WriteLine($"encryption/decryption took: {timer.ElapsedMilliseconds} ms");