using RPDSerice.Models;
using RPDSerice.RPDGenerator.Interfaces;
namespace RPDSerice.RPDGenerator.Implementation;
public class RPDGenerator : IRPDGenerator
{
	public Byte[] GetRPDPdfBytes(in RPDBase JsonRPD)
	{
		JsonRPD.a = "Dsa";
		throw new NotImplementedException();
	}
}