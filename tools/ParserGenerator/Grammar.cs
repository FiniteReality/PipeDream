using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace PipeDream.Tools.ParserGenerator;

public sealed record Grammar(
    IReadOnlyCollection<Kind> Kinds,
    IReadOnlyCollection<GrammarItem> Items,
    GrammarItem RootItem)
{
    private static readonly Comparer<Kind> KindNameComparer
        = Comparer<Kind>.Create(
            (l, r) => string.Compare(l.Name, r.Name,
                StringComparison.Ordinal));

    private static readonly Comparer<GrammarItem> ItemTypeComparer
        = Comparer<GrammarItem>.Create(
            (l, r) => string.Compare(l.Type, r.Type,
                StringComparison.Ordinal));

    public static Grammar BuildGrammar(XmlGrammar grammar)
    {
        var kinds = new Dictionary<string, Kind>();
        var items = new Dictionary<string, GrammarItem>();

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
                Kinds: kinds.Values.ToImmutableSortedSet(KindNameComparer),
                Items: items.Values.ToImmutableSortedSet(ItemTypeComparer),
                RootItem: root);

        static ImmutableSortedSet<Kind> BuildKinds(
            string name,
            XmlKind[]? kinds,
            Dictionary<string, Kind> builtKinds)
        {
            var builder = ImmutableSortedSet.CreateBuilder(KindNameComparer);
            foreach (var kind in kinds ?? Array.Empty<XmlKind>())
            {
                if (!builtKinds.TryGetValue(kind.Kind, out var found))
                    throw new InvalidOperationException(
                        $"Unknown kind '{kind.Kind}' when building '{name}'");

                if (!builder.Add(found))
                    throw new InvalidOperationException(
                        $"Duplicate kind '{kind.Kind}' when building '{name}'");
            }

            return builder.ToImmutable();
        }

        static GrammarItem GetOrBuildItem(XmlGrammar grammar, string name,
            Dictionary<string, GrammarItem> builtItems,
            Dictionary<string, Kind> builtKinds)
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
            Dictionary<string, GrammarItem> builtItems,
            Dictionary<string, Kind> builtKinds)
            => item switch
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
                        Kinds: BuildKinds(item.Type, item.Kinds, builtKinds),
                        Members:
                            (@abstract.Members ?? Array.Empty<XmlMember>())
                                .Select(m => BuildMember(
                                    grammar,
                                    m,
                                    builtItems,
                                    builtKinds))
                                .ToImmutableArray(),
                        Type: @abstract.Type),
                XmlSealedGrammarItem @sealed
                    => new SealedGrammarItem(
                        BaseType: GetOrBuildItem(grammar, @sealed.Base!,
                            builtItems, builtKinds),
                        DocumentationComment: @sealed.Comment,
                        Kinds: BuildKinds(item.Type, item.Kinds, builtKinds),
                        Members:
                            (@sealed.Members ?? Array.Empty<XmlMember>())
                                .Select(m => BuildMember(
                                    grammar,
                                    m,
                                    builtItems,
                                    builtKinds))
                                .ToImmutableArray(),
                        Type: @sealed.Type),
                _ => throw new InvalidOperationException(
                    "Unknown XML grammar item")
            };

        static Member BuildMember(XmlGrammar grammar,
            XmlMember member,
            Dictionary<string, GrammarItem> builtItems,
            Dictionary<string, Kind> builtKinds)
        {
            if (member.Override && member.Kinds?.Length == 0)
                throw new InvalidOperationException(
                    "Cannot override if not specifying kinds");
            if (member.Virtual && member.Override)
                throw new InvalidOperationException(
                    "Cannot specify both Virtual and Override");

            return new(
                DocumentationComment: member.Comment,
                Kinds: BuildKinds(member.Name, member.Kinds, builtKinds),
                Name: member.Name,
                Override: member.Override,
                Type: member.Type,
                Virtual: member.Virtual);
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
    bool Override,
    string Type,
    bool Virtual);