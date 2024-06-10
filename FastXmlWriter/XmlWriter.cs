using System.Buffers;
using System.Text;

namespace FastXmlWriter;

public sealed class XmlWriter
{
    private readonly IBufferWriter<byte> buffer;
    private OpenTag openTag = OpenTag.None;

    public XmlWriter(IBufferWriter<byte> buffer)
    {
        this.buffer = buffer;
    }

    public void WriteStartElement(ReadOnlySpan<char> name)
    {
        WriteEndOfStartElement();
        buffer.Write("<"u8);
        Encoding.UTF8.GetBytes(name, buffer);
        openTag = OpenTag.Normal;
    }

    public void WriteStartElement(ReadOnlySpan<byte> name)
    {
        WriteEndOfStartElement();
        buffer.Write("<"u8);
        buffer.Write(name);
        openTag = OpenTag.Normal;
    }

    public void WriteEndElement(ReadOnlySpan<char> name)
    {
        WriteEndOfStartElement();
        openTag = OpenTag.None;
        buffer.Write("</"u8);
        Encoding.UTF8.GetBytes(name, buffer);
        buffer.Write(">"u8);
    }

    public void WriteEndElement(ReadOnlySpan<byte> name)
    {
        WriteEndOfStartElement();
        openTag = OpenTag.None;
        buffer.Write("</"u8);
        buffer.Write(name);
        buffer.Write(">"u8);
    }

    public void WriteEmptyElement(ReadOnlySpan<char> name)
    {
        WriteEndOfStartElement();
        buffer.Write("<"u8);
        Encoding.UTF8.GetBytes(name, buffer);
        openTag = OpenTag.Empty;
    }

    public void WriteEmptyElement(ReadOnlySpan<byte> name)
    {
        WriteEndOfStartElement();
        buffer.Write("<"u8);
        buffer.Write(name);
        openTag = OpenTag.Empty;
    }

    public void WriteAttributeName(ReadOnlySpan<char> name)
    {
        buffer.Write(" "u8);
        Encoding.UTF8.GetBytes(name, buffer);
    }

    public void WriteAttributeName(ReadOnlySpan<byte> name)
    {
        buffer.Write(" "u8);
        buffer.Write(name);
    }

    public void WriteAttributeValue(ReadOnlySpan<char> value)
    {
        buffer.Write("=\""u8);
        Encoding.UTF8.GetBytes(value, buffer);
        buffer.Write("\""u8);
    }

    public void WriteAttributeValue(ReadOnlySpan<byte> value)
    {
        buffer.Write("=\""u8);
        buffer.Write(value);
        buffer.Write("\""u8);
    }

    public void WriteText(ReadOnlySpan<char> text)
    {
        WriteEndOfStartElement();
        openTag = OpenTag.None;
        Encoding.UTF8.GetBytes(text, buffer);
    }

    public void WriteText(ReadOnlySpan<byte> text)
    {
        WriteEndOfStartElement();
        openTag = OpenTag.None;
        buffer.Write(text);
    }

    private void WriteEndOfStartElement()
    {
        switch (openTag)
        {
            case OpenTag.Normal:
                buffer.Write(">"u8);
                break;
            case OpenTag.Empty:
                buffer.Write("/>"u8);
                break;
        }
    }

    private enum OpenTag
    {
        None,
        Normal,
        Empty
    }
}
