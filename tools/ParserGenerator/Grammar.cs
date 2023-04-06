using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace PipeDream.Tools.ParserGenerator;

public sealed record Grammar(
    IReadOnlyDictionary<string, Kind> Kinds,
    IReadOnlyDictionary<string, GrammarItem> Items,
    GrammarItem RootItem)
{
    public static Grammar BuildGrammar(XmlGrammar grammar)
    {
        var kinds = ImmutableDictionary.CreateBuilder<string, Kind>();
        var items = ImmutableDictionary.CreateBuilder<string, GrammarItem>();

        foreach (var kind in grammar.Elements.OfType<XmlKindDefinition>())
        {
            if (kinds.TryGetValue(kind.Kind, out var _))
                throw new InvalidOperationException(
                    $"Duplicate kind '{kind.Kind}'");

            var builtKind = new Kind(
                DocumentationComment: kind.Comment,
                Group: kind.Group,
                Name: kind.Kind);
            kinds.Add(kind.Kind, builtKind);
        }

        RootGrammarItem? root = null;
        foreach (var item in grammar.Elements.OfType<XmlGrammarItem>())
        {
            if (items.TryGetValue(item.Type, out var _))
                throw new InvalidOperationException(
                    $"Duplicate item '{item.Type}'");

            var builtItem = BuildItem(grammar, item, items, kinds);
            items.Add(item.Type, builtItem);
            if (builtItem is RootGrammarItem rootItem)
                root = rootItem;
        }

        return root == null
            ? throw new InvalidOperationException(
                "Grammar is missing root item")
            : new(
                Kinds: kinds.ToImmutable(),
                Items: items.ToImmutable(),
                RootItem: root);

        static ImmutableHashSet<Kind> BuildKinds(
            List<XmlKind> kinds,
            ImmutableDictionary<string, Kind>.Builder builtKinds)
        {
            var builder = ImmutableHashSet.CreateBuilder<Kind>();
            foreach (var kind in kinds)
            {
                if (!builtKinds.TryGetValue(kind.Kind, out var found))
                    throw new InvalidOperationException(
                        $"Unknown kind '{kind.Kind}'");

                if (!builder.Add(found))
                    throw new InvalidOperationException(
                        $"Duplicate kind '{kind.Kind}");
            }

            return builder.ToImmutable();
        }

        static GrammarItem GetOrBuildItem(XmlGrammar grammar, string name,
            ImmutableDictionary<string, GrammarItem>.Builder builtItems,
            ImmutableDictionary<string, Kind>.Builder builtKinds)
        {
            var builtItem = builtItems.GetValueOrDefault(name);
            if (builtItem == null)
            {
                var item = grammar.Elements
                    .OfType<XmlGrammarItem>()
                    .FirstOrDefault(x => x.Type == name)
                    ?? throw new ArgumentException(
                        $"Unknown item with type '{name}'",
                        nameof(name));

                builtItem = BuildItem(grammar, item, builtItems, builtKinds);
                builtItems.Add(name, builtItem);
            }

            return builtItem;
        }

        static GrammarItem BuildItem(
            XmlGrammar grammar,
            XmlGrammarItem item,
            ImmutableDictionary<string, GrammarItem>.Builder builtItems,
            ImmutableDictionary<string, Kind>.Builder builtKinds)
        {
            return item switch
            {
                XmlRootGrammarItem root
                    => new RootGrammarItem(
                        DocumentationComment: root.Comment,
                        Members: root.Members.Select(
                            m => BuildMember(grammar, m, builtItems, builtKinds))
                            .ToImmutableArray(),
                        Type: root.Type),
                XmlAbstractGrammarItem @abstract
                    => new AbstractGrammarItem(
                        BaseType: GetOrBuildItem(grammar, @abstract.Base!,
                            builtItems, builtKinds),
                        DocumentationComment: @abstract.Comment,
                        Kinds: BuildKinds(@abstract.Kinds, builtKinds),
                        Members: @abstract.Members.Select(
                            m => BuildMember(grammar, m, builtItems, builtKinds))
                            .ToImmutableArray(),
                        Type: @abstract.Type),
                XmlSealedGrammarItem @sealed
                    => new AbstractGrammarItem(
                        BaseType: GetOrBuildItem(grammar, @sealed.Base!,
                            builtItems, builtKinds),
                        DocumentationComment: @sealed.Comment,
                        Kinds: BuildKinds(@sealed.Kinds, builtKinds),
                        Members: @sealed.Members.Select(
                            m => BuildMember(grammar, m, builtItems, builtKinds))
                            .ToImmutableArray(),
                        Type: @sealed.Type),
                _ => throw new InvalidOperationException(
                    "Unknown XML grammar item")
            };
        }

        static Member BuildMember(XmlGrammar grammar,
            XmlMember member,
            ImmutableDictionary<string, GrammarItem>.Builder builtItems,
            ImmutableDictionary<string, Kind>.Builder builtKinds)
        {
            return new(
                DocumentationComment: member.Comment,
                Kinds: BuildKinds(member.Kinds, builtKinds),
                Name: member.Name,
                Type: member.Type);
        }
    }
}

public sealed record Kind(
    XElement? DocumentationComment,
    string Group,
    string Name);

public abstract record GrammarItem(
    GrammarItem? BaseType,
    XElement? DocumentationComment,
    IReadOnlyCollection<Kind> Kinds,
    ImmutableArray<Member> Members,
    string Type);

public sealed record RootGrammarItem(
    XElement? DocumentationComment,
    ImmutableArray<Member> Members,
    string Type)
    : GrammarItem(
        BaseType: null,
        DocumentationComment: DocumentationComment,
        Kinds: ImmutableHashSet.Create<Kind>(),
        Members: Members,
        Type: Type);

public sealed record AbstractGrammarItem(
    GrammarItem? BaseType,
    XElement? DocumentationComment,
    IReadOnlyCollection<Kind> Kinds,
    ImmutableArray<Member> Members,
    string Type)
    : GrammarItem(
        BaseType: BaseType,
        DocumentationComment: DocumentationComment,
        Kinds: Kinds,
        Members: Members,
        Type: Type);

public sealed record SealedGrammarItem(
    GrammarItem? BaseType,
    XElement? DocumentationComment,
    IReadOnlyCollection<Kind> Kinds,
    ImmutableArray<Member> Members,
    string Type)
    : GrammarItem(
        BaseType: BaseType,
        DocumentationComment: DocumentationComment,
        Kinds: Kinds,
        Members: Members,
        Type: Type);

public sealed record Member(
    XElement? DocumentationComment,
    IReadOnlyCollection<Kind> Kinds,
    string Name,
    string Type);