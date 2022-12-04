using System.Diagnostics;
using Core;

// for (int i = 0; i <= 10; i++)
// {
//     var g = PrimeNumberGenerator.GetRandomPrimaryNumber(10);
//     Console.WriteLine(g);
// }


// var text = 111111;
// Console.WriteLine($"text: {text}");

var timer = new Stopwatch();
timer.Start();

var rsa = new RSA();

//
// var text = "danil cymbal rapper";
// Console.WriteLine($"text: {text}");

var input = 19032002;
Console.WriteLine($"input: {input}");

var encrypted = rsa.Encrypt(input);
Console.WriteLine($"encrypted: {encrypted}");

var decrypted = rsa.Decrypt(encrypted);
Console.WriteLine($"decrypted: {decrypted}");

Console.WriteLine(input == decrypted);

// var bits = 1024;
// var prime = PrimeNumberGenerator.GetRandomPrimaryNumber(bits);
timer.Stop();

//Console.WriteLine(prime);

// var encrypted = rsa.Encrypt(text);
// Console.WriteLine($"encrypted: {encrypted}");


Console.WriteLine($"elapsed time: {timer.ElapsedMilliseconds} ms");