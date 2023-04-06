using System.Text;

namespace PipeDream.Tools.ParserGenerator;

internal sealed class PrefixedTextWriter : TextWriter
{
    private readonly TextWriter _innerWriter;
    private readonly string _prefix;

    private bool _writtenPrefixThisLine;

    public PrefixedTextWriter(string prefix, TextWriter original)
    {
        _innerWriter = original;
        _prefix = prefix;
        _writtenPrefixThisLine = false;
    }

    public override Encoding Encoding => _innerWriter.Encoding;

    public override void Write(char value)
    {
        if (!_writtenPrefixThisLine)
        {
            _innerWriter.Write(_prefix);
            _writtenPrefixThisLine = true;
        }

        _innerWriter.Write(value);

        if (value == '\n')
        {
            _writtenPrefixThisLine = false;
        }
    }

    public override void Flush() => _innerWriter.Flush();
}