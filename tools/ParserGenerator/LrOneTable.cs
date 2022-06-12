

using System.Diagnostics;

internal sealed class LrOneTable
{
    public enum ActionType { Error, Shift, Reduce, Accept }
    public record struct Action(ActionType Type, int? TargetState, Rule? Rule, Production? Production);

    private readonly LrOneAutomata _automata;
    private readonly Grammar _grammar;

    private readonly Dictionary<(int, string), Action> _actionTable;
    private readonly Dictionary<(int, string), int> _gotoTable;

    public LrOneTable(Grammar grammar)
    {
        _automata = new LrOneAutomata(grammar);
        _grammar = grammar;
        _actionTable = new();
        _gotoTable = new();

        // Based on Algorithm 4.56 in the Dragon Book

        // 1.
        var itemSets = _automata.ComputeItems();

        // 2. and 3.
        for (int i = 0; i < itemSets.Count; i++)
        {
            var itemSet = itemSets[i];
            foreach (var item in itemSet)
            {
                // (a)
                if (item.Dot < item.Production.Symbols.Length)
                {
                    var symbol = item.Production.Symbols[item.Dot];

                    // not a terminal
                    if (grammar.GetRuleByName(symbol) != null)
                        continue;

                    var destination = _automata.Goto(itemSet, symbol);
                    var destinationIndex = itemSets.FindIndex(x =>
                        LrOneItemSetComparer.Instance.Equals(x, destination));

                    var action = new Action(
                        ActionType.Shift, destinationIndex,
                            grammar.GetRuleByProduction(item.Production),
                            item.Production);

                    if (_actionTable.TryGetValue((i, symbol), out var existing))
                    {
                        if (action.Rule != existing.Rule)
                            throw new InvalidOperationException(
                                $"{action.Type}/{existing.Type} Conflict");

                        var actionPrecedence = Array.IndexOf(
                            action.Rule!.Productions, item.Production);
                        var existingPrecedence = Array.IndexOf(
                            action.Rule!.Productions, existing.Production);

                        if (actionPrecedence <= existingPrecedence)
                            continue;
                    }

                    _actionTable[(i, symbol)] = action;
                }

                // (b) and (c)
                else if (item.Dot == item.Production.Symbols.Length)
                {
                    var rule = grammar.GetRuleByProduction(item.Production);

                    // (b)
                    if (rule?.Name != "S'")
                    {
                        var action = new Action(
                            ActionType.Reduce, null, rule, item.Production);

                        if (_actionTable.TryGetValue((i, item.Lookahead),
                            out var existing))
                        {
                            if (action.Rule != existing.Rule)
                                throw new InvalidOperationException(
                                    $"{action.Type}/{existing.Type} Conflict");

                            var actionPrecedence = Array.IndexOf(
                                action.Rule!.Productions, item.Production);
                            var existingPrecedence = Array.IndexOf(
                                action.Rule!.Productions, existing.Production);

                            if (actionPrecedence <= existingPrecedence)
                                continue;
                        }

                        _actionTable[(i, item.Lookahead)] = action;
                    }
                    // (c)
                    else if (rule?.Name == "S'" && item.Lookahead == "$")
                        _actionTable.Add((i, item.Lookahead),
                            new(ActionType.Accept, null, rule, item.Production));
                    else
                        Debug.Fail("Dot on RHS but didn't match rule name");
                }
            }

            foreach (var nonterminal in grammar.Symbols
                .Where(x => grammar.GetRuleByName(x) != null))
            {
                var destination = _automata.Goto(itemSet, nonterminal);
                var destinationIndex = itemSets.FindIndex(x =>
                    LrOneItemSetComparer.Instance.Equals(x, destination));

                _gotoTable.Add((i, nonterminal), destinationIndex);
            }
        }
    }

    public ActionTable Actions
        => new ActionTable(this);
    public GotoTable Gotos
        => new GotoTable(this);

    internal struct ActionTable
    {
        private readonly LrOneTable _table;

        public ActionTable(LrOneTable table)
        {
            _table = table;
        }

        public Action this[int state, string nonterminal]
            => default;
    }

    internal struct GotoTable
    {
        private readonly LrOneTable _table;

        public GotoTable(LrOneTable table)
        {
            _table = table;
        }

        public int this[int state, string nonterminal]
            => 0;
    }
}