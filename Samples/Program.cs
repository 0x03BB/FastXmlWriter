using System.IO.Pipelines;
using FastXmlWriter;

namespace Samples;

internal class Program
{
    async static Task Main(string[] _)
    {
        using var stream = File.Create("sample.svg", 0);
        var writer = PipeWriter.Create(stream);

        var xmlWriter = new XmlWriter(writer);
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

        await writer.FlushAsync();
        await writer.CompleteAsync();
    }
}
