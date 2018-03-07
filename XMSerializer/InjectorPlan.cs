using System.Collections.Generic;

namespace XMSerializer
{
    public class InjectionImage
    {
        public string FileName { get; set; }
        public byte[] Image { get; set; }
        public int PatternHeight { get; set; }
    }

    public class InjectionPlan
    {
        public string Name { get; set; }
        public int SongPosition { get; set; }
        public int DestinationLine { get; set; }
        public int LoopSize { get; set; }
        public int LoopLineInc { get; set; }
        public int FrameLineInc { get; set; }
        public int TotalFrames { get; set; }
        public int PatternHeight { get; set; }
        public int PixelMode { get; set; }
        public int PatternAllFrames { get; set; }
        public List<InjectionImage> Images { get; set; }

        public InjectionPlan()
        {
            Images = new List<InjectionImage>();
        }
    }

    public class InjectionRemap
    {
        public Pattern Pattern { get; set; }
        public int Distance { get; set; }
        public int ValueIndex { get; set; }
    }

    public class InjectionStill
    {
        public string Name { get; set; }
        public int SongPosition { get; set; }
        public int PixelMode { get; set; }
        public List<InjectionImage> Images { get; set; }

        public InjectionStill()
        {
            Images = new List<InjectionImage>();
        }
    }
}
