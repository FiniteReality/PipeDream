using System.Buffers;
using System.Xml.Linq;

namespace PipeDream.Tools.ParserGenerator;

internal static class XElementExtensions
{
    /// <summary>
    /// Trims the whitespace from any text nodes in this element.
    /// </summary>
    /// <remarks>
    /// The algorithm used to detect spaces is fairly primitive. As long as you
    /// don't have trailing whitespace on each line of text, it should work.
    /// </remarks>
    /// <param name="element">
    /// The element to trim whitespace from.
    /// </param>
    public static void TrimWhitespace(this XElement element)
    {
        foreach (var text in element.DescendantNodesAndSelf().OfType<XText>())
        {
            TrimWhitespace(text);
        }
    }

    private static void TrimWhitespace(XText text)
    {
        var maxLength = text.Value.Length;
        var lastWasSpace = false;
        foreach (var c in text.Value)
        {
            if (c == ' ')
            {
                if (lastWasSpace)
                    maxLength--;
                lastWasSpace = true;
            }
            else
            {
                lastWasSpace = c == '\n';
            }
        }

        text.Value = string.Create(maxLength, text.Value, TrimCore);
        static void TrimCore(Span<char> buffer, string original)
        {
            var writeIndex = 0;
            var lastWasSpace = false;
            foreach (var c in original)
            {
                if (c == ' ')
                {
                    if (lastWasSpace)
                        continue;

                    lastWasSpace = true;
                }
                else
                {
                    lastWasSpace = c == '\n';
                }

                buffer[writeIndex++] = c;
            }
        }
    }
}