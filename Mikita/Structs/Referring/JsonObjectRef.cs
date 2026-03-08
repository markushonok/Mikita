using System.Text.Json.Nodes;

namespace Mikita.Structs.Referring;

public sealed class JsonObjectRef<T>
	(
		JsonObject root,
		string path
	)
	: IRef<T?>
	{
		public T? Value
			{
				get
					{
						var node = NodeAtPath;

						return node is not null
							? node.GetValue<T>()
							: default;

					}
				set => SetTo(value);
			}

		private JsonNode? NodeAtPath
			{
				get
					{
						var parts = path.Split('.');
						var current = (JsonNode?) root;

						foreach (var part in parts)
							{
								if (current is not JsonObject obj)
									return null;

								current = obj[part];

								if (current == null) return null;
							}

						return current;
					}
			}

		public void SetTo
			(
				T value
			)
			{
				var parts = path.Split('.');
				var current = root;

				foreach (var part in parts)
					{
						var next = current[part];

						if (next == null)
							{
								next = new JsonObject();
								current[part] = next;
							}

						if (next is not JsonObject)
							{
								next = new JsonObject();
								current[part] = next;
							}

						current = next.AsObject();
					}

				current[parts[^1]] = JsonValue.Create(value);
			}
	}