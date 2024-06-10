using BenchmarkDotNet.Attributes;
using System.Buffers;
using System.Xml;

namespace Samples;

[MemoryDiagnoser]
public class Benchmark
{
    private ArrayBufferWriter<byte> buffer;
    private MemoryStream stream;
    private XmlWriterSettings settings;

    [GlobalSetup]
    public void GlobalSetup()
    {
        buffer = new(1024);
        stream = new(1024);
        settings = new();
        settings.OmitXmlDeclaration = true;
    }

    [IterationSetup]
    public void IterationSetup()
    {
        buffer.Clear();
        stream.Position = 0;
    }

    [Benchmark]
    public ArrayBufferWriter<byte> FastXmlWriter()
    {
        var xmlWriter = new FastXmlWriter.XmlWriter(buffer);
        xmlWriter.WriteStartElement("svg"u8);
        xmlWriter.WriteAttributeName("xmlns"u8);
        xmlWriter.WriteAttributeValue("http://www.w3.org/2000/svg"u8);
        xmlWriter.WriteAttributeName("width"u8);
        xmlWriter.WriteAttributeValue("600"u8);
        xmlWriter.WriteAttributeName("height"u8);
        xmlWriter.WriteAttributeValue("400"u8);
        xmlWriter.WriteAttributeName("viewBox"u8);
        xmlWriter.WriteAttributeValue("-60 -340 600 400"u8);

        xmlWriter.WriteStartElement("g"u8);
        xmlWriter.WriteAttributeName("transform"u8);
        xmlWriter.WriteAttributeValue("translate(0.5 -0.5) scale(17.3333 -2)"u8);

        xmlWriter.WriteEmptyElement("path"u8);
        xmlWriter.WriteAttributeName("d"u8);
        xmlWriter.WriteAttributeValue("M0 0L0 86L1 88L2 97L5 97L6 99L7 99L8 101L9 102L12 102L13 103L14 106L16 118L19 118L20 118L21 128L22 129L23 129L27 132L28 132L29 133L30 151L30 0"u8);
        xmlWriter.WriteAttributeName("fill"u8);
        xmlWriter.WriteAttributeValue("#CC0000"u8);

        xmlWriter.WriteEmptyElement("path"u8);
        xmlWriter.WriteAttributeName("d"u8);
        xmlWriter.WriteAttributeValue("M0 0L0 28L1 30L2 34L5 41L6 43L7 48L8 53L9 54L12 63L13 67L14 68L16 73L19 77L20 77L21 77L22 80L23 82L27 83L28 85L29 89L30 93L30 0"u8);
        xmlWriter.WriteAttributeName("fill"u8);
        xmlWriter.WriteAttributeValue("#0000DD"u8);

        xmlWriter.WriteEmptyElement("path"u8);
        xmlWriter.WriteAttributeName("d"u8);
        xmlWriter.WriteAttributeValue("M0 0L1 5L2 5L5 14L6 14L7 24L8 26L9 28L12 36L13 45L14 45L16 45L19 48L20 50L21 50L22 51L23 54L27 56L28 56L29 63L30 74L30 0"u8);
        xmlWriter.WriteAttributeName("fill"u8);
        xmlWriter.WriteAttributeValue("green"u8);

        xmlWriter.WriteEndElement("g"u8);

        xmlWriter.WriteStartElement("g"u8);
        xmlWriter.WriteAttributeName("stroke"u8);
        xmlWriter.WriteAttributeValue("black"u8);

        xmlWriter.WriteEmptyElement("path"u8);
        xmlWriter.WriteAttributeName("d"u8);
        xmlWriter.WriteAttributeValue("M0 0h520.5M0 0v-320.5"u8);

        xmlWriter.WriteEndElement("g"u8);

        xmlWriter.WriteEndElement("svg"u8);

        return buffer;
    }

    [Benchmark]
    public MemoryStream XmlWriter()
    {
        var xmlWriter = System.Xml.XmlWriter.Create(stream, settings);
        xmlWriter.WriteStartElement("svg", "http://www.w3.org/2000/svg");
        xmlWriter.WriteAttributeString("width", "600");
        xmlWriter.WriteAttributeString("height", "400");
        xmlWriter.WriteAttributeString("viewBox", "-60 -340 600 400");

        xmlWriter.WriteStartElement("g");
        xmlWriter.WriteAttributeString("transform", "translate(0.5 -0.5) scale(17.3333 -2)");

        xmlWriter.WriteStartElement("path");
        xmlWriter.WriteAttributeString("d", "M0 0L0 86L1 88L2 97L5 97L6 99L7 99L8 101L9 102L12 102L13 103L14 106L16 118L19 118L20 118L21 128L22 129L23 129L27 132L28 132L29 133L30 151L30 0");
        xmlWriter.WriteAttributeString("fill", "#CC0000");
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("path");
        xmlWriter.WriteAttributeString("d", "M0 0L0 28L1 30L2 34L5 41L6 43L7 48L8 53L9 54L12 63L13 67L14 68L16 73L19 77L20 77L21 77L22 80L23 82L27 83L28 85L29 89L30 93L30 0");
        xmlWriter.WriteAttributeString("fill", "#0000DD");
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("path");
        xmlWriter.WriteAttributeString("d", "M0 0L1 5L2 5L5 14L6 14L7 24L8 26L9 28L12 36L13 45L14 45L16 45L19 48L20 50L21 50L22 51L23 54L27 56L28 56L29 63L30 74L30 0");
        xmlWriter.WriteAttributeString("fill", "green");
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("g");
        xmlWriter.WriteAttributeString("stroke", "black");

        xmlWriter.WriteStartElement("path");
        xmlWriter.WriteAttributeString("d", "M0 0h520.5M0 0v-320.5");
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();
        xmlWriter.Flush();

        return stream;
    }
}
