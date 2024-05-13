// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

Console.WriteLine("Benchmark");
var summary = BenchmarkRunner.Run(typeof(Program).Assembly);