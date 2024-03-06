using RPDSerice.Models;
namespace RPDSerice.RPDGenerator.Interfaces;

public interface IRPDGenerator
{
	Byte[] GetRPDPdfBytes(RPD rpd);
}
