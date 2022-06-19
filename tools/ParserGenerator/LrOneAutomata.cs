using System.Diagnostics;
using System.Text;

internal sealed class LrOneAutomata
{
    public record struct Item(Grammar Grammar, Production Production, int Dot, string Lookahead)
    {
        public override string ToString()
        {
            var rule = Grammar.GetRuleByProduction(Production)?.Name ?? "S'";
            var builder = new StringBuilder();

            _ = builder.Append($"[{rule} ->");

            int i = 0;
            foreach (var symbol in Production.Symbols)
            {
                var position = i++;

                if (Dot == position)
                    _ = builder.Append(" ·");

                _ = builder.Append($" {symbol}");
            }

            if (i == Dot)
                _ = builder.Append($" ·");

            _ = builder.Append($", {Lookahead}]");

            return builder.ToString();
        }
    }

    private readonly Grammar _grammar;

    private readonly Dictionary<string, HashSet<string>> _firstSets;

    public LrOneAutomata(Grammar grammar)
    {
        _grammar = grammar;
        _firstSets = new();

        foreach (var symbol in _grammar.Symbols)
        {
            var set = new HashSet<string>();
            _firstSets[symbol] = set;

            var rule = grammar.GetRuleByName(symbol);
            if (rule == null)
            {
                _ = set.Add(symbol);
            }
            else if (rule.Productions.Any(x => x.Symbols.Length == 0))
            {
                _ = set.Add("");
            }
        }

        bool anyAdded;
        do
        {
            anyAdded = false;
            foreach (var nonterminal in _grammar.Rules)
            {
                var set = _firstSets[nonterminal.Name];

                foreach (var production in nonterminal.Productions)
                {
                    var epsilonCount = 0;
                    foreach (var symbol in production.Symbols)
                    {
                        var hasEpsilon = false;
                        foreach (var first in _firstSets[symbol])
                        {
                            if (first == "")
                            {
                                epsilonCount++;
                                hasEpsilon = true;
                                continue;
                            }

                            anyAdded |= set.Add(first);
                        }

                        if (!hasEpsilon)
                            break;
                    }

                    if (epsilonCount == production.Symbols.Length)
                        anyAdded |= set.Add("");
                }
            }
        }
        while (anyAdded);
    }

    public HashSet<string> First(string symbol)
        => _firstSets[symbol];

    public HashSet<string> First(params string[] symbols)
    {
        var result = new HashSet<string>();
        var epsilonCount = 0;

        foreach (var symbol in symbols)
        {
            var hasEpsilon = false;
            foreach (var first in First(symbol))
            {
                if (first == "")
                {
                    epsilonCount++;
                    hasEpsilon = true;
                    continue;
                }

                _ = result.Add(first);
            }

            if (!hasEpsilon)
                break;
        }

        if (epsilonCount == symbols.Length)
            _ = result.Add("");

        return result;
    }

    public HashSet<Item> Kernel(HashSet<Item> items)
    {
        var result = new HashSet<Item>();

        foreach (var item in items)
            if (item.Dot > 0 || item.Production == _grammar.Augment)
                _ = result.Add(item);

        return result;
    }

    private HashSet<Item> Closure(HashSet<Item> items)
    {
        var added = new HashSet<Item>();

        do
        {
            added.Clear();

            foreach (var item in items)
            {
                // Dot is on the RHS, so there is nothing following it
                if (item.Dot == item.Production.Symbols.Length)
                    continue;

                var symbol = item.Production.Symbols[item.Dot];
                var rule = _grammar.GetRuleByName(symbol);

                // Symbol is a terminal, so we ignore it.
                if (rule is null)
                    continue;

                // compute βa for FIRST(βa)
                var nextSymbols = item.Production.Symbols
                        .Skip(item.Dot + 1) // β
                        .Append(item.Lookahead) // a
                        .Distinct()
                        .ToArray();

                foreach (var production in rule.Productions)
                {
                    foreach (var terminal in First(nextSymbols))
                    {
                        var newItem = new Item(_grammar, production, 0, terminal);
                        if (!items.Contains(newItem))
                            _ = added.Add(newItem);
                    }
                }
            }

            foreach (var add in added)
                _ = items.Add(add);
        }
        while (added.Count > 0);

        return items;
    }

    public HashSet<Item> Goto(HashSet<Item> items, string symbol)
    {
        var result = new HashSet<Item>();

        foreach (var item in items)
        {
            if (item.Dot == item.Production.Symbols.Length)
                continue;

            if (item.Production.Symbols[item.Dot] == symbol)
                _ = result.Add(new Item(_grammar, item.Production, item.Dot + 1, item.Lookahead));
        }

        return Closure(result);
    }

    public List<HashSet<Item>> ComputeItems()
    {
        var result = new List<HashSet<Item>>()
        {
            Closure(new() { new(_grammar, _grammar.Augment, 0, "EndOfFile") })
        };
        var added = new List<HashSet<Item>>();

        do
        {
            added.Clear();

            foreach (var set in result)
            {
                foreach (var symbol in _grammar.Symbols)
                {
                    var gotoSet = Goto(set, symbol);

                    if (gotoSet.Count == 0)
                        continue;

                    if (!result.Contains(gotoSet, LrOneItemSetComparer.Instance)
                       && !added.Contains(gotoSet, LrOneItemSetComparer.Instance))
                       added.Add(gotoSet);
                }
            }

            foreach (var add in added)
                result.Add(add);
        }
        while (added.Count > 0);

        return result;
    }
}

internal sealed class LrOneItemSetComparer
    : EqualityComparer<HashSet<LrOneAutomata.Item>>
{
    private LrOneItemSetComparer()
    { }

    public static LrOneItemSetComparer Instance { get; } = new();

    public override bool Equals(
        HashSet<LrOneAutomata.Item>? x,
        HashSet<LrOneAutomata.Item>? y)
        => ReferenceEquals(x, y)
        || (x != null && y != null && x.SetEquals(y));

    public override int GetHashCode(HashSet<LrOneAutomata.Item> obj)
    {
        var hash = new HashCode();

        foreach (var item in obj)
            hash.Add(item);

        return hash.ToHashCode();
    }
}