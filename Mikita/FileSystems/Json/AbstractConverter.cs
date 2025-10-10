using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mikita.FileSystems.Json;

public class AbstractConverter<TAbstraction, TImplementation>
	: JsonConverter<TAbstraction>
	where TImplementation: TAbstraction
	{
		public override TAbstraction Read
			(
				ref Utf8JsonReader reader,
				Type typeToConvert,
				JsonSerializerOptions options
			)
			=> JsonSerializer.Deserialize<TImplementation>(ref reader, options)!;

		public override void Write
			(
				Utf8JsonWriter writer,
				TAbstraction value,
				JsonSerializerOptions options
			)
			=> JsonSerializer.Serialize(writer, (TImplementation?) value, options);
	}