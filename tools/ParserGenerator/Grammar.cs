using System.Collections.Generic;
using System.Linq;

internal sealed record Grammar(IReadOnlyList<Rule> Rules)
{
    public Grammar(params Rule[] rules)
        : this(rules
            .Prepend(new Rule("S'", new Production(rules[0].Name)))
            .ToList())
    { }

    public string[] Symbols { get; }
        = Rules
            .SelectMany(
                x => x.Productions,
                (x, y) => (ruleName: x.Name, production: y))
            .SelectMany(
                x => x.production.Symbols,
                (x, y) => (x.ruleName, symbol: y))
            .SelectMany(x => new[] { x.ruleName, x.symbol })
            .Distinct()
            .ToArray();

    public Production Augment { get; } = Rules[0].Productions[0];

    public Rule? GetRuleByName(string name)
        => Rules.FirstOrDefault(x => x.Name == name);

    public Rule? GetRuleByProduction(Production production)
        => Rules.FirstOrDefault(x => x.Productions.Contains(production));
}

internal sealed record Rule(string Name, params Production[] Productions);

internal sealed record Production(params string[] Symbols)
{
    public string? Name { get; init; }

    public override string ToString()
        => string.Join(" ", Symbols);
}