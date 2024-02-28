using RPDSerice.RPDGenerator.Implementation;
using RPDSerice.RPDGenerator.Interfaces;
namespace Benchmark;

class Benchmark
{
	IRPDGenerator RPDGenerator = new RPDGenerator();
	
	public static void Main()
	{
		Console.WriteLine("hello world");
	}

}