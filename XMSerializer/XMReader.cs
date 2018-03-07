using System.IO;
using System.Text;

namespace XMSerializer
{
    public class XMReader : BinaryReader
    {
        public XMReader(Stream stream) : base(stream) { }

        public string ReadAscii(int length)
        {
            var data = base.ReadBytes(length);
            return Encoding.ASCII.GetString(data);
        }
    }
}
