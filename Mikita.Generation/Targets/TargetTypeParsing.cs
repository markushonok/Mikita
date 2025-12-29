using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Mikita.Generation.Targets;

public static class TargetTypeParsing
	{
		extension(TargetType)
			{
				public static IReadOnlyList<TargetType> FromJson(string json)
					{
						if (string.IsNullOrWhiteSpace(json))
							return [];

						var token = JToken.Parse(json);

						if (token is not JArray array)
							return [];

						var result = new List<TargetType>();

						foreach (var item in array)
							{
								if (item is not JObject jObject)
									continue;

								var target = TargetType.FromObject(jObject);
								if (target is not null)
									result.Add(target);
							}

						return result;
					}

				private static TargetType? FromObject(JObject obj)
					{
						var name = obj.TryGetValue
							(
								"name",
								StringComparison.OrdinalIgnoreCase,
								out var nameToken
							)
							? nameToken.ToString()
							: null;

						if (string.IsNullOrWhiteSpace(name))
							return null;

						var arity = 0;
						if (obj.TryGetValue("arity", StringComparison.OrdinalIgnoreCase, out var arityToken))
							{
								if (int.TryParse(arityToken?.ToString(), out var parsed))
									arity = parsed;
							}

						var products = TargetProducts.None;
						if (obj.TryGetValue("products", StringComparison.OrdinalIgnoreCase, out var productsToken)
							&& productsToken is JArray productsArray)
							{
								foreach (var pToken in productsArray)
									{
										var raw = pToken?.ToString();
										if (string.IsNullOrWhiteSpace(raw))
											continue;

										var p = raw?.Trim().ToLowerInvariant();
										if (p == "broadcast")
											products |= TargetProducts.Broadcast;
										else if (p == "live")
											products |= TargetProducts.Live;
										else if (p == "log")
											products |= TargetProducts.Log;
									}
							}

						return new TargetType(name!, arity, products);
					}
			}
	}