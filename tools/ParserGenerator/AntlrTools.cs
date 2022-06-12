using System.Collections.Generic;
using System.Text;

internal static class AntlrTools
{
    private static readonly Dictionary<string, string> Substitutions
        = new()
        {
            ["EndOfFile"] = "EOF",
            ["String"] = "String",
            ["Identifier"] = "Identifier"
        };

    private static readonly Dictionary<string, string> Tokens
        = new()
        {
            ["EndOfLine"] = @"'\r\n' | '\n'",
            ["OpenBrace"] = @"'{'",
            ["CloseBrace"] = @"'}'",
            ["Semicolon"] = @"';'",
            ["Equals"] = @"'='",
            ["PreprocessorInclude"] = @"'#include'",
            ["PreprocessorDefine"] = @"'#define'",
            ["Comma"] = @"','",
            ["Dot"] = @"'.'",
            ["Slash"] = @"'/'",
            ["Exclamation"] = @"'!'",
            ["DoubleAmpersand"] = @"'&&'",
            ["OpenParenthesis"] = @"'('",
            ["CloseParenthesis"] = @"')'",
            ["DoubleDot"] = @"'..'"
        };

    public static string GenerateAntlrGrammar(Grammar grammar, string name)
    {
        var rules = new StringBuilder();

        _ = rules.AppendLine($"grammar {name};");
        _ = rules.AppendLine();
        _ = rules.AppendLine("tokens {\n\tString,\n\tIdentifier\n}");
        _ = rules.AppendLine();

        foreach (var token in Tokens)
        {
            _ = rules.AppendLine($"{token.Key}: {token.Value};");
        }

        _ = rules.AppendLine();

        foreach (var rule in grammar.Rules.Skip(1)) // Skip S' rule
        {
            _ = rules.AppendLine(
                $"{char.ToLower(rule.Name[0])}{rule.Name[1..]}");
            var firstProduction = true;
            foreach (var production in rule.Productions)
            {
                _ = rules.Append($"\t{(firstProduction ? ':' : '|')}");
                foreach (var symbol in production.Symbols)
                {
                    if (grammar.GetRuleByName(symbol) is null)
                    {
                        if (!Substitutions.TryGetValue(symbol,
                            out var mappedSymbol)
                            && !Tokens.ContainsKey(mappedSymbol ?? symbol))
                            throw new InvalidOperationException(
                                $"Add {mappedSymbol ?? symbol} to Tokens dictionary");

                        _ = rules.Append($" {mappedSymbol ?? symbol}");
                    }
                    else
                    {
                        _ = rules.Append(
                            $" {char.ToLower(symbol[0])}{symbol[1..]}");
                    }
                }
                _ = rules.AppendLine();
                firstProduction = false;
            }
            _ = rules.AppendLine("\t;\n");
        }

        return rules.ToString();
    }
}