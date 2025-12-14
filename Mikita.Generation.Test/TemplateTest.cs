namespace Mikita.Generation.Test;

public class TemplateTest
	{
		[Fact]
		public void FindFileByPath()
			=> Template.AtPath("Mikita.Generation.BroadcastGatewayAddition.cs.txt");

		[Fact]
		public void FindFileByName()
			=> Template.WithUniqueName("BroadcastGatewayAddition.cs.txt");
	}