using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ProductPrototype.DbAccess.Extraction.Rules;

// ---------------------------------------------------------------------------------------------
// Rule model. Mirrors extraction-rules.json 1:1 so the file is the single source of truth for
// "which tag / class / path means what". Adding a new selector is a JSON edit; adding a new kind
// of value handling is one TransformRegistry.Register(...) call.
// ---------------------------------------------------------------------------------------------

public sealed class RuleSet
{
    public int Version { get; init; }
    public string Site { get; init; } = string.Empty;
    public string? Notes { get; init; }
    public PageRules Page { get; init; } = new();
    public PricingRules Pricing { get; init; } = new();
    public SpecNamingRules SpecNaming { get; init; } = new();
    public ColumnModel ColumnModel { get; init; } = new();
    public GroupProfiles GroupProfiles { get; init; } = new();
    public List<ValidationRule> Validation { get; init; } = [];

    public static RuleSet Load(string path)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };
        options.Converters.Add(new TransformSpecConverter());

        var rules = JsonSerializer.Deserialize<RuleSet>(File.ReadAllText(path), options)
                    ?? throw new InvalidOperationException($"Rule file is empty: {path}");

        // Fail at startup, not on page 14,000, if a rule names a transform that does not exist.
        foreach (var spec in rules.AllTransforms())
        {
            if (!TransformRegistry.Exists(spec.Name))
                throw new InvalidOperationException($"Unknown transform '{spec.Name}' in {path}");
        }

        return rules;
    }

    private IEnumerable<TransformSpec> AllTransforms()
    {
        foreach (var f in Page.Fields)
            foreach (var t in f.Transforms) yield return t;

        foreach (var k in Pricing.TableKinds)
            if (k.Name != null)
                foreach (var t in k.Name.Transforms) yield return t;

        foreach (var profile in GroupProfiles.Profiles.Append(GroupProfiles.Default))
            foreach (var color in profile.Colors)
                foreach (var t in color.ValueTransforms) yield return t;

        foreach (var c in Pricing.ItemCells.Concat(Pricing.AddonCells))
        {
            foreach (var t in c.Transforms) yield return t;

            foreach (var cr in c.ContentRules)
            {
                foreach (var t in cr.Transforms) yield return t;
                if (cr.AttributeName != null) foreach (var t in cr.AttributeName.Transforms) yield return t;
                if (cr.Options != null) foreach (var t in cr.Options.Transforms) yield return t;
            }
        }
    }
}

public sealed class PageRules
{
    public List<FieldRule> Fields { get; init; } = [];
}

/// <summary>A page-level value: title, descriptions, images, SKU cross-check list.</summary>
public sealed class FieldRule
{
    public string Id { get; init; } = string.Empty;
    public string Target { get; init; } = string.Empty;     // property on the parsed model
    public string XPath { get; init; } = string.Empty;
    public string Read { get; init; } = "text";             // text | innerHtml | attr:<name>
    public List<TransformSpec> Transforms { get; init; } = [];
    public bool Multiple { get; init; }
    public bool Unique { get; init; }
    public bool Required { get; init; }
    public string? Note { get; init; }
}

public sealed class PricingRules
{
    public string Container { get; init; } = string.Empty;
    public string Tables { get; init; } = "./table";
    public List<TableKindRule> TableKinds { get; init; } = [];   // evaluated in order, first match wins
    public List<CellRule> ItemCells { get; init; } = [];
    public List<CellRule> AddonCells { get; init; } = [];
}

/// <summary>Decides what a table in the pricing area is: skip | group | item | addon.</summary>
public sealed class TableKindRule
{
    public string Id { get; init; } = string.Empty;
    public string Action { get; init; } = string.Empty;
    public MatchSpec Match { get; init; } = new();
    public ValueSpec? Name { get; init; }                   // for action = group
    public string? Note { get; init; }
    public string? CreateAs { get; init; }                  // addon: "item" = create it as a product first
    public LinkRule? LinkTo { get; init; }                  // addon: then link it to the item it follows
}

/// <summary>Links a created product to another through a NopCommerce table (e.g. RelatedProduct).</summary>
public sealed class LinkRule
{
    public string Table { get; init; } = string.Empty;       // RelatedProduct
    public string From { get; init; } = "previousItem";      // ProductId1 = the variant the table follows
    public string To { get; init; } = "this";                // ProductId2 = the associated product
    public string DedupeBy { get; init; } = "Sku";           // one product per SKU, many links
}

/// <summary>All specified conditions must hold.</summary>
public sealed class MatchSpec
{
    public string? ClassRegex { get; init; }
    public string? XPath { get; init; }
    public string? NotXPath { get; init; }                   // must NOT match anywhere in the table
    public int? MinCells { get; init; }                      // td count of the table's first row
    public int? MaxCells { get; init; }
}

public sealed class ValueSpec
{
    public string? XPath { get; init; }
    public string Read { get; init; } = "text";
    public string? From { get; init; }
    public List<TransformSpec> Transforms { get; init; } = [];
}

/// <summary>Maps a table cell (found by td class regex) to a value or to a content rule set.</summary>
public sealed class CellRule
{
    public string Id { get; init; } = string.Empty;
    public string? Target { get; init; }
    public string TdClass { get; init; } = string.Empty;    // regex over the td class attribute
    public string? Read { get; init; }
    public string? XPath { get; init; }
    public string? TextRegex { get; init; }
    public string? Emit { get; init; }
    public string? Multi { get; init; }
    public bool Multiple { get; init; }
    public bool Required { get; init; }
    public string? Select { get; init; }
    public string? OpenEndedInput { get; init; }
    public List<TransformSpec> Transforms { get; init; } = [];
    public List<ContentRule> ContentRules { get; init; } = [];
    public List<ImgRule> ImgRules { get; init; } = [];
}

/// <summary>Cells with the same class can hold different kinds of content; first match wins.</summary>
public sealed class ContentRule
{
    public string Id { get; init; } = string.Empty;
    public string? Has { get; init; }                       // xpath that must exist, e.g. .//select
    public string? TextRegex { get; init; }
    public string Emit { get; init; } = string.Empty;       // attribute | qtyRange | textCell | ...
    public ValueSpec? AttributeName { get; init; }
    public ValueSpec? Options { get; init; }
    public List<TransformSpec> Transforms { get; init; } = [];
}

public sealed class ImgRule
{
    public string SrcRegex { get; init; } = string.Empty;
    public string? Flag { get; init; }
    public bool Ignore { get; init; }
}

public sealed class ValidationRule
{
    public string Id { get; init; } = string.Empty;
    public string Check { get; init; } = string.Empty;
    public string? Against { get; init; }
    public string Severity { get; init; } = "warn";
    public int? Min { get; init; }
    public int? Max { get; init; }
}

// ---------------------------------------------------------------------------------------------
// Specification naming: variant tables have no header row, so each text cell gets a meaningful
// name ("Mounting Type", "Finish", ...) from its text and/or the group header it sits under.
// ---------------------------------------------------------------------------------------------

public sealed class SpecNamingRules
{
    public string? Note { get; init; }
    public SpecNameDefault Default { get; init; } = new();
    public List<SpecNameRule> Rules { get; init; } = [];    // first match wins
}

public sealed class SpecNameDefault
{
    public string Name { get; init; } = "Specification";
}

public sealed class SpecNameRule
{
    public string Id { get; init; } = string.Empty;
    public string? TextRegex { get; init; }                  // tested on the cell text
    public string? GroupRegex { get; init; }                 // tested on the group header text
    public string Name { get; init; } = string.Empty;        // the specification name to assign
    public string? Value { get; init; }                      // optional template, e.g. "$1"; default = cell text
    public string? StoreAs { get; init; }                    // e.g. qtyRange: not a spec, feeds Min/Max qty
}

public sealed class SpecNamer
{
    private readonly SpecNamingRules _rules;
    private readonly List<(SpecNameRule Rule, Regex? Text, Regex? Group)> _compiled;

    public SpecNamer(SpecNamingRules rules)
    {
        _rules = rules;
        _compiled = rules.Rules
            .Select(r => (r,
                r.TextRegex == null ? null : new Regex(r.TextRegex, RegexOptions.Compiled),
                r.GroupRegex == null ? null : new Regex(r.GroupRegex, RegexOptions.Compiled)))
            .ToList();
    }

    /// <summary>
    /// Order: 1) rules with StoreAs (quantity range) always win, 2) the product group's own
    /// specificationName, 3) the first matching text rule, 4) the default name.
    /// RuleId "default" means nothing matched: report it so a rule or profile can be added.
    /// </summary>
    public (string Name, string Value, string RuleId, string? StoreAs) Resolve(
        string text, string group, string? profileSpecificationName = null)
    {
        foreach (var entry in _compiled.Where(c => c.Rule.StoreAs != null))
        {
            if (TryMatch(entry, text, group, out var hit))
                return hit;
        }

        if (!string.IsNullOrWhiteSpace(profileSpecificationName))
            return (profileSpecificationName!, text, "profile", null);

        foreach (var entry in _compiled.Where(c => c.Rule.StoreAs == null))
        {
            if (TryMatch(entry, text, group, out var hit))
                return hit;
        }

        return (_rules.Default.Name, text, "default", null);
    }

    private static bool TryMatch(
        (SpecNameRule Rule, Regex? Text, Regex? Group) entry, string text, string group,
        out (string Name, string Value, string RuleId, string? StoreAs) hit)
    {
        hit = default;
        Match? m = null;
        if (entry.Text != null)
        {
            m = entry.Text.Match(text);
            if (!m.Success) return false;
        }
        if (entry.Group != null && !entry.Group.IsMatch(group)) return false;

        var value = entry.Rule.Value != null && m != null ? m.Result(entry.Rule.Value) : text;
        hit = (entry.Rule.Name, value, entry.Rule.Id, entry.Rule.StoreAs);
        return true;
    }
}

// ---------------------------------------------------------------------------------------------
// Column model + group profiles. Variant rows have no header: the must-have columns are sku,
// specification, weight, price, quantity (+ color where the group has one); other cells (icon,
// old price, a second color, ...) are recognised from the row itself. What a colour cell MEANS
// is defined per product group, because it is not the same across products.
// ---------------------------------------------------------------------------------------------

public sealed class ColumnModel
{
    public string? Note { get; init; }
    public string SpecificationCell { get; init; } = "firstDetailCell";
    public string ColorCells { get; init; } = "otherDetailCellsExceptQuantityRange";
    public List<string> OptionalRoles { get; init; } = [];
    public string PriceCells { get; init; } = "oneIsPrice_twoAreOldAndNew";
    public string? TableTypes { get; init; }
    public string LayoutScope { get; init; } = "group";          // layout is read per group, not per page
    public string? LayoutNote { get; init; }
    public CellCountRange GroupHeaderCellCount { get; init; } = new();
    public CellCountRange VariantCellCount { get; init; } = new();
}

public sealed class CellCountRange
{
    public int Min { get; init; }
    public int Max { get; init; } = int.MaxValue;
}

public sealed class GroupProfiles
{
    public string? Note { get; init; }
    public GroupProfile Default { get; init; } = new();
    public List<GroupProfile> Profiles { get; init; } = [];     // first match wins
}

public sealed class GroupProfile
{
    public string Id { get; init; } = string.Empty;
    public string? GroupRegex { get; init; }                     // tested on the group header text
    public string? TableClassRegex { get; init; }                // optional: the pt* class of the header table
    public string? SpecificationName { get; init; }              // names the specification cell for this group, e.g. "Wood Type"
    public List<string> RequiredRoles { get; init; } = [];
    public List<ColorSlot> Colors { get; init; } = [];           // n-th colour cell of a row -> Colors[n]
}

public sealed class ColorSlot
{
    public string Name { get; init; } = string.Empty;            // e.g. "Frame Color"
    public string? Kind { get; init; }                           // select | text (null = either)
    public string? TextRegex { get; init; }                      // tested on a text cell's text
    public string? PlaceholderRegex { get; init; }               // tested on a select's label ("Seat Color")
    public List<TransformSpec> ValueTransforms { get; init; } = [];
}

public sealed class GroupProfileResolver
{
    private readonly GroupProfiles _profiles;
    private readonly List<(GroupProfile Profile, Regex? Group, Regex? TableClass)> _compiled;

    public GroupProfileResolver(GroupProfiles profiles)
    {
        _profiles = profiles;
        _compiled = profiles.Profiles
            .Select(p => (p,
                p.GroupRegex == null ? null : new Regex(p.GroupRegex, RegexOptions.Compiled),
                p.TableClassRegex == null ? null : new Regex(p.TableClassRegex, RegexOptions.Compiled)))
            .ToList();
    }

    public GroupProfile Resolve(string groupHeader, string tableClass)
    {
        foreach (var (profile, groupRx, classRx) in _compiled)
        {
            if (groupRx != null && !groupRx.IsMatch(groupHeader)) continue;
            if (classRx != null && !classRx.IsMatch(tableClass)) continue;
            return profile;
        }
        return _profiles.Default;     // report groups that land here so a profile can be added
    }

    /// <summary>
    /// Names and cleans one colour cell. Slots are matched by CONTENT, not position: the profile's
    /// slots first, then the shared slots on the default profile, then the select's own label,
    /// then "Color". A select (dropdown) has a placeholder; a text cell has none.
    /// </summary>
    public (string Name, string Value, string Source) ResolveColor(
        GroupProfile profile, bool isSelect, string text, string? placeholder)
    {
        foreach (var slot in profile.Colors.Concat(_profiles.Default.Colors))
        {
            if (slot.Kind == "select" && !isSelect) continue;
            if (slot.Kind == "text" && isSelect) continue;
            if (slot.TextRegex != null && (isSelect || !Regex.IsMatch(text, slot.TextRegex))) continue;
            if (slot.PlaceholderRegex != null
                && (string.IsNullOrEmpty(placeholder) || !Regex.IsMatch(placeholder, slot.PlaceholderRegex))) continue;

            var value = isSelect ? string.Empty : (string)(TransformRegistry.Apply(text, slot.ValueTransforms) ?? text);
            return (slot.Name, value, "slot");
        }

        return !string.IsNullOrWhiteSpace(placeholder)
            ? (placeholder!, isSelect ? string.Empty : text, "label")
            : ("Color", isSelect ? string.Empty : text, "fallback");
    }
}

// ---------------------------------------------------------------------------------------------
// Transforms: "deentitize", or { "t": "regexReplace", "pattern": "...", "with": "..." }
// ---------------------------------------------------------------------------------------------

public sealed record TransformSpec(string Name, string? Pattern = null, string? With = null);

public sealed class TransformSpecConverter : JsonConverter<TransformSpec>
{
    public override TransformSpec Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return new TransformSpec(reader.GetString()!);

        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        return new TransformSpec(
            root.GetProperty("t").GetString()!,
            root.TryGetProperty("pattern", out var p) ? p.GetString() : null,
            root.TryGetProperty("with", out var w) ? w.GetString() : null);
    }

    public override void Write(Utf8JsonWriter writer, TransformSpec value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("t", value.Name);
        if (value.Pattern != null) writer.WriteString("pattern", value.Pattern);
        if (value.With != null) writer.WriteString("with", value.With);
        writer.WriteEndObject();
    }
}

public static class TransformRegistry
{
    private static readonly Dictionary<string, Func<object?, TransformSpec, object?>> Map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["deentitize"] = (v, _) => v is string s ? HtmlEntity.DeEntitize(s) : v,
            ["collapseWs"] = (v, _) => v is string s ? Regex.Replace(s.Replace('\u00A0', ' '), @"\s+", " ") : v,
            ["trim"]       = (v, _) => v is string s ? s.Trim() : v,

            // Hook point: delegate to the existing Cleantext(...) helper when wiring this in.
            ["cleanText"]  = (v, _) => v is string s ? s.Trim() : v,

            ["regexReplace"] = (v, t) => v is string s
                ? Regex.Replace(s,
                    t.Pattern ?? throw new InvalidOperationException("regexReplace needs 'pattern'"),
                    t.With ?? string.Empty)
                : v,

            ["decimal"] = (v, _) => v is string s
                           && decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out var d)
                ? (object)d
                : null,
        };

    public static bool Exists(string name) => Map.ContainsKey(name);

    /// <summary>Add a new kind of value handling without touching the engine.</summary>
    public static void Register(string name, Func<object?, TransformSpec, object?> transform) => Map[name] = transform;

    public static object? Apply(object? value, IEnumerable<TransformSpec>? specs)
    {
        if (specs == null) return value;
        foreach (var spec in specs)
            value = Map[spec.Name](value, spec);
        return value;
    }
}
