namespace XMSerializer
{
    public class XMPixel
    {
        public int Column { get; set; }
        public byte Value { get; set; }

        public XMPixel(int column, byte value)
        {
            Column = column;
            Value = value;
        }
    }
}
