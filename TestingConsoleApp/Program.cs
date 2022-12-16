using System.Diagnostics;
using Core;

// benchmark
var timer = new Stopwatch();
int iterations = 50;
timer.Start();

Benchmark.BenchKeyGeneration(iterations,512);
Benchmark.BenchKeyGeneration(iterations,1024);
Benchmark.BenchKeyGeneration(iterations,2048,"sec");

Benchmark.BenchEncryptionDecryption(iterations,512,int.MaxValue);
Benchmark.BenchEncryptionDecryption(iterations,1024,int.MaxValue);
Benchmark.BenchEncryptionDecryption(iterations,2048,int.MaxValue);



