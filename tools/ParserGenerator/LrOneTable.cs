

using System.Collections;
using System.Diagnostics;

internal sealed class LrOneTable
{
    public enum ActionType { Error, Shift, Reduce, Accept }
    public record struct Action(ActionType Type, int? TargetState, Rule? Rule, Production? Production, LrOneAutomata.Item? Item)
    {
        public override string ToString()
            => Type switch
            {
                ActionType.Error => $"Error ({Item})",
                ActionType.Shift => $"Shift ({TargetState}, {Item})",
                ActionType.Reduce => $"Reduce ({Rule!.Name} -> {Production}, {Item})",
                ActionType.Accept => $"Accept ({Item})",
                _ => ""
            };
    }

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
            // 2.
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
                            item.Production,
                            item);

                    if (_actionTable.TryGetValue((i, symbol), out var existing))
                    {
                        if (action.Rule != existing.Rule)
                            throw new InvalidOperationException(
                                $"{action}/{existing} Conflict");

                        var actionPrecedence = Array.IndexOf(
                            action.Rule!.Productions, item.Production);
                        var existingPrecedence = Array.IndexOf(
                            action.Rule!.Productions, existing.Production);

                        if (actionPrecedence >= existingPrecedence)
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
                            ActionType.Reduce, null, rule, item.Production, item);

                        if (_actionTable.TryGetValue((i, item.Lookahead),
                            out var existing))
                        {
                            if (action.Rule != existing.Rule)
                                throw new InvalidOperationException(
                                    $"{action}/{existing} Conflict");

                            var actionPrecedence = Array.IndexOf(
                                action.Rule!.Productions, item.Production);
                            var existingPrecedence = Array.IndexOf(
                                action.Rule!.Productions, existing.Production);

                            if (actionPrecedence >= existingPrecedence)
                                continue;
                        }

                        _actionTable[(i, item.Lookahead)] = action;
                    }
                    // (c)
                    else if (rule?.Name == "S'" && item.Lookahead == "EndOfFile")
                        _actionTable.Add((i, item.Lookahead),
                            new(ActionType.Accept, null, rule, item.Production, item));
                    else
                        Debug.Fail("Dot on RHS but didn't match rule name");
                }
            }

            // 3.
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

    internal struct ActionTable : IEnumerable<KeyValuePair<(int, string), Action>>
    {
        private readonly LrOneTable _table;

        public ActionTable(LrOneTable table)
        {
            _table = table;
        }

        public Action this[int state, string nonterminal]
            => _table._actionTable.TryGetValue((state, nonterminal),
                out var value)
                ? value
                : new(ActionType.Error, null, null, null, null);

        public IEnumerator<KeyValuePair<(int, string), Action>> GetEnumerator()
            => _table._actionTable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }

    internal struct GotoTable : IEnumerable<KeyValuePair<(int, string), int>>
    {
        private readonly LrOneTable _table;

        public GotoTable(LrOneTable table)
        {
            _table = table;
        }

        public int this[int state, string nonterminal]
            => _table._gotoTable[(state, nonterminal)];

        public IEnumerable<KeyValuePair<int, int>> this[string nonterminal]
            => _table._gotoTable
                .Where(x => x.Key.Item2 == nonterminal && x.Value >= 0)
                .Select(x => KeyValuePair.Create(x.Key.Item1, x.Value));

        public IEnumerator<KeyValuePair<(int, string), int>> GetEnumerator()
            => _table._gotoTable.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}