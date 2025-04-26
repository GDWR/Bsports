using System.Text.Json.Serialization;
using Poker.Models;

namespace Poker;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    IncludeFields = true,
    WriteIndented = false)]
[JsonSerializable(typeof(PersistentState))]
public partial class JsonContext : JsonSerializerContext
{
    
}