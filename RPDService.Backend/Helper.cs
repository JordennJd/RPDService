public static class Helper
{
	static string[] Names = new[]
	{
		"MacBook-Air-Danil", "JORDENN-PC"
	};
	 
	 public static string GetMachineName()
	 {
		if (Names.Contains(Environment.MachineName)) return Environment.MachineName;
		else return "default";
	 }
}
