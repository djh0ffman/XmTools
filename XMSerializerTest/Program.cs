namespace XMSerializerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var ser = new XMSerializer.XMSerializer();
            var xm = ser.DeSerialize(@"C:\Users\h0ffman\Google Drive\XMTools\XMSerializerTest\test\test.xm");
            ser.Serialize(xm, @"C:\test\output.xm");
        }
    }
}
