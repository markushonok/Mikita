using Mikita.Network.Messages;
using Mikita.Network.Messages.Registries;

namespace Mikita.Network.Test;

public static class MessageRegistrySequenceTest
	{
		[Fact]
		public static void PreservesOrder()
			{
				var registry = MessageRegistry.New;
				IMessageLabel[] labels =
					[
						new MessageLabel(TypeName: "query", MessageRole.Request),
						new MessageLabel(TypeName: "answer", MessageRole.Response),
						new MessageLabel(TypeName: "changed", MessageRole.Notice)
					];

				registry.Register(labels);

				registry.LabelOf("query").TypeId.Value.ShouldBe((short) 0);
				registry.LabelOf("answer").TypeId.Value.ShouldBe((short) 1);
				registry.LabelOf("changed").TypeId.Value.ShouldBe((short) 2);
			}
	}