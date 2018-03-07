using System.IO;
using System.Text;

namespace XMSerializer
{
    public class XMWriter : BinaryWriter
    {
        public XMWriter(Stream stream) : base(stream) { }

        public void WriteAscii(string text, int length)
        {
            var output = new byte[length];
            var convertedText = Encoding.ASCII.GetBytes(text);
            for (var i = 0; i < length; i++)
            {
                if (i < convertedText.Length)
                    output[i] = convertedText[i];
                else
                    output[i] = 0;
            }

            this.Write(output);
        }
    }
}
