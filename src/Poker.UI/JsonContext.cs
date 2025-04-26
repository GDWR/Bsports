using System.Text.Json.Serialization;
using Poker.UI.Models;

namespace Poker.UI;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    IncludeFields = true,
    WriteIndented = false)]
[JsonSerializable(typeof(PersistentState))]
public partial class JsonContext : JsonSerializerContext
{
    
}