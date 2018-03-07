using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace XMSerializer
{
    public class ExtendedModule
    {
        public string IdText { get; set; }
        public string ModuleName { get; set; }
        public byte Tag { get; set; }
        public string TrackerName { get; set; }
        public int HeaderSize { get; set; }
        public byte VersionHigh { get; set; }
        public byte VersionLow { get; set; }

        public UInt16 SongLength { get; set; }
        public UInt16 RestartPosition { get; set; }
        public UInt16 ChannelCount { get; set; }
        public UInt16 PatternCount { get; set; }
        public UInt16 InstrumentCount { get; set; }
        public byte[] Flags { get; set; }
        public UInt16 Tempo { get; set; }
        public UInt16 BPM { get; set; }

        public byte[] PatternOrderList { get; set; }

        public List<Pattern> Patterns { get; set; }
        public List<Instrument> Instruments { get; set; }

        private List<InjectionRemap> Remaps { get; set; } 

        public ExtendedModule()
        {
            Patterns = new List<Pattern>();
            Instruments = new List<Instrument>();
            Remaps = new List<InjectionRemap>();
        }

        public Pattern NewPattern(int lines)
        {
            if (lines > 256 || lines < 1) throw new Exception("Are you mental! max 256 lines");

            return new Pattern()
            {
                PackedPatternData = null,
                PackingType = 0,
                HeaderLength = 9,
                RowCount = (ushort)lines,
                UnpackedPatternData = new byte[ChannelCount * 5 * lines]
            };
        }

        public Pattern InsertNewPattern(int position, int size)
        {
            var pat = NewPattern(size);
            Patterns.Add(pat);
            int patId = Patterns.IndexOf(pat);
            var list = PatternOrderList.ToList();
            list.Insert(position, (byte)patId);
            for (var i = 0; i < 256; i++)
                PatternOrderList[i] = list[i];
            SongLength += 1;
            if (SongLength > 256)
                throw new Exception("You've ran out of song length"); 
            return pat;
        }

        public Pattern DuplicatePattern(int position)
        {
            var source = Patterns[PatternOrderList[position]];
            var pat = NewPattern(source.RowCount);
            for (var i = 0; i < source.UnpackedPatternData.Length; i++)
            {
                pat.UnpackedPatternData[i] = source.UnpackedPatternData[i];
            }
            Patterns.Add(pat);
            int patId = Patterns.IndexOf(pat);
            PatternOrderList[position] = (byte)patId;
            return pat;
        }

        private void DeletePatternPosition(int position)
        {
            var list = PatternOrderList.ToList();
            list.RemoveAt(position);
            list.Add(0);
            for (var i = 0; i < 256; i++)
                PatternOrderList[i] = list[i];
            SongLength--;
        }

        private int PatternOccuranceCount(int patternId)
        {
            int count = 0;
            for (var i = 0; i < SongLength; i++)
            {
                var patId = PatternOrderList[i];
                if (patternId == patId) count++;
            }
            return count;
        }

        private bool[] GetChannelMask(Pattern pat)
        {
            var mask = new bool[ChannelCount];
            var index = 0;
            for (var y = 0; y < pat.RowCount; y++)
            {
                for (var x = 0; x < ChannelCount; x++)
                {
                    var data = pat.UnpackedPatternData;
                    if (data[index] + data[index + 1] + data[index + 2] + data[index + 3] + data[index + 4] > 0)
                    {
                        mask[x] = true;
                    }
                    index += 5;
                }
            }
            return mask;
        }

        public void RunInjectionPlans()
        {
            // TODO: take the code off the form!

        }

        public void RunInjectionStill(InjectionStill still)
        {
            var src = Patterns[PatternOrderList[still.SongPosition]];
            var mask = GetChannelMask(src);

            var pat = NewPattern(src.RowCount);
            
            // inject image to new pattern
            pat.InjectImage(BinToImage(still.Images[0].Image), still.PixelMode);

            // inject pattern to new pattern
            var index = 0;
            for (var row = 0; row < src.RowCount; row++)
            {
                for (var i = 0; i < ChannelCount; i++)
                {
                    if (mask[i])
                    {
                        for (var x = 0; x < 5; x++)
                        {
                            pat.UnpackedPatternData[index + x] = src.UnpackedPatternData[index + x];
                        }
                    }

                    index += 5;                 
                }
            }

            // yep, done :)
            Patterns[PatternOrderList[still.SongPosition]] = pat;
        }

        public void RunInjectionPlan(InjectionPlan plan, int sourceChannels, int folloingDestinationLine)
        {
            var src = Patterns[PatternOrderList[plan.SongPosition]];
            var mask = GetChannelMask(src);

            if (src.RowCount != plan.TotalFrames)
                throw new Exception(string.Format("Frame count {0} doesn't match row count {1}", plan.TotalFrames,
                    src.RowCount));

            if (plan.DestinationLine > 0 && plan.SongPosition > 0)
            {
                var prevPatId = PatternOrderList[plan.SongPosition - 1];
                Pattern previous = null;
                if (PatternOccuranceCount(prevPatId) > 1)
                {
                    // previous pattern occurs more than once, so dupe to prevent breakage
                    previous = DuplicatePattern(plan.SongPosition - 1);
                }
                else
                {
                    previous = Patterns[PatternOrderList[plan.SongPosition - 1]];
                }

                var previousEnd = previous.UnpackedPatternData.Length - 2;
                previous.UnpackedPatternData[previousEnd - 3] = 0x00;
                previous.UnpackedPatternData[previousEnd - 2] = 0x00;
                previous.UnpackedPatternData[previousEnd - 1] = 0x00;
                previous.UnpackedPatternData[previousEnd] = 0x0D;
                previous.UnpackedPatternData[previousEnd + 1] = PatBreak(plan.DestinationLine);
            }

            // remove existing pattern position (so we dont play the same pattern twice)
            DeletePatternPosition(plan.SongPosition);

            var loop = 0;
            var destPos = plan.DestinationLine;
            var width = ChannelCount*5;
            var sourceIndex = 0;
            var songPos = plan.SongPosition;

            var loopList = new List<int>();
            bool loopActive = false;

            Pattern pat = null;

            for (var frame = 0; frame < plan.TotalFrames; frame++)
            {
                var destIndex = destPos*width;

                if (!loopActive)
                {
                    InjectionImage image = null;

                    if (plan.Images.Count > loop)
                        image = plan.Images[loop];
                    
                    // height override
                    var height = image != null && image.PatternHeight > 0 ? image.PatternHeight : plan.PatternHeight;

                    // create new pattern
                    pat = InsertNewPattern(songPos, height);

                    // if image exists, inject it
                    if (image != null)
                        pat.InjectImage(BinToImage(image.Image), plan.PixelMode);

                    loopList.Add(songPos);
                }
                else
                {
                    // first loop done, append to previous patterns instead
                    pat = Patterns[PatternOrderList[loopList[loop]]];
                }

                // copy current line
                if (plan.PatternAllFrames == 0)
                {
                    for (var i = 0; i < ChannelCount; i++)
                    {
                        if (mask[i])
                        {
                            for (var x = 0; x < 5; x++)
                            {
                                pat.UnpackedPatternData[destIndex + x] = src.UnpackedPatternData[sourceIndex + x];
                            }
                        }

                        destIndex += 5;
                        sourceIndex += 5;
                    }
                }
                else  // copy whole pattern to destination
                {
                    var thisIndex = 0;
                    for (var line = 0; line < plan.PatternHeight-1; line++)
                    {
                        for (var i = 0; i < ChannelCount; i++)
                        {
                            if (mask[i])
                            {
                                for (var x = 0; x < 5; x++)
                                {
                                    pat.UnpackedPatternData[thisIndex + x] = src.UnpackedPatternData[thisIndex + x];
                                }
                            }

                            thisIndex += 5;
                        }
                    }

                    destIndex += (5*ChannelCount);
                    sourceIndex += (5*ChannelCount);
                }

                loop++;
                if (loop == plan.LoopSize)
                {
                    loopActive = true;
                    destPos += plan.LoopLineInc;

                    // add song position jump to first loop frame but leave the last frame otherwise we'll loop 4 eva
                    if (frame < plan.TotalFrames - 1)
                    {
                        pat.UnpackedPatternData[destIndex - 10] = 0x00;
                        pat.UnpackedPatternData[destIndex - 9] = 0x00;
                        pat.UnpackedPatternData[destIndex - 8] = 0x00;
                        pat.UnpackedPatternData[destIndex - 7] = 0x0B;
                        pat.UnpackedPatternData[destIndex - 6] = (byte) loopList[0];

                        Remaps.Add(new InjectionRemap()
                        {
                            Pattern = pat,
                            Distance = loop - 1,
                            ValueIndex = destIndex - 6
                        });

                    }

                    loop = 0;
                }
                else
                {
                    destPos += plan.FrameLineInc;       // move position for this frame
                }


                // now position jump if required
                pat.UnpackedPatternData[destIndex - 5] = 0x00;
                pat.UnpackedPatternData[destIndex - 4] = 0x00; 
                pat.UnpackedPatternData[destIndex - 3] = 0x00;
                pat.UnpackedPatternData[destIndex - 2] = 0x0D;
                if (frame < plan.TotalFrames - 1)
                {
                    pat.UnpackedPatternData[destIndex - 1] = PatBreak(destPos);
                }
                else
                {
                    pat.UnpackedPatternData[destIndex - 1] = PatBreak(folloingDestinationLine);
                }

                songPos++;
            }

            Remap();  // stuff will have shifted so remap B commands
            CleanPatternOrderList();
            RemoveUnusedPatterns();
            RemoveDuplicatePatterns();
        }

        private void Remap()
        {
            foreach (var r in Remaps)
            {
                var patId = Patterns.IndexOf(r.Pattern);
                for (var i = 0; i < SongLength; i++)
                {
                    if (PatternOrderList[i] == patId)
                    {
                        // remap jump point
                        if (r.Pattern.UnpackedPatternData[r.ValueIndex-1] != 0x0B)
                        {
                            throw new Exception("Found it");
                        }
                        r.Pattern.UnpackedPatternData[r.ValueIndex] = (byte)(i - r.Distance);
                    }
                }
            }
        }


        private Bitmap BinToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return (Bitmap) returnImage;
            }
        }

        private void RemoveUnusedPatterns()
        {
            // based on song length, mark which patterns are actually used
            var used = new bool[256];
            for (var i = 0; i < SongLength; i++)
            {
                used[PatternOrderList[i]] = true;
            }

            // copy highest used pattern to lowest unused pattern
            for (var current = 0; current < used.Length; current++)
            {
                if (!used[current])
                {
                    var lastUsed = FindLastUsedPattern(true, used);
                    if (lastUsed > current)
                    {
                        Patterns[current] = Patterns[lastUsed];
                        RemapPattern(lastUsed, current);
                        used[current] = true;
                        used[lastUsed] = false;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            SanitizePatterns();
        }

        public void RemoveDuplicatePatterns()
        {
            for (var a = 0; a < Patterns.Count; a++)
            {
                for (var b = a + 1; b < Patterns.Count; b++)
                {
                    var pata = Patterns[a];
                    var patb = Patterns[b];
                    if (PatternCompare(pata, patb))
                    {
                        RemapPattern(b, a);
                    }
                }
            }

            SanitizePatterns();
        }

        private bool PatternCompare(Pattern a, Pattern b)
        {
            if (a.RowCount != b.RowCount)
                return false;

            for (var i = 0; i < a.UnpackedPatternData.Length; i++)
            {
                if (a.UnpackedPatternData[i] != b.UnpackedPatternData[i])
                {
                    return false;
                }
            }

            return true;
        }

        private void SanitizePatterns()
        {
            var newPatterns = new List<Pattern>();
            var maxUsedPattern = PatternOrderList.Max();
            for (var p = 0; p <= maxUsedPattern; p++)
            {
                newPatterns.Add(Patterns[p]);
            }
            Patterns = newPatterns;
            PatternCount = (ushort)Patterns.Count;
        }

        private int FindLastUsedPattern(bool state, bool[] blist)
        {
            for (var i = blist.Length - 1; i >= 0; i--)
            {
                if (blist[i] == state) return i;
            }
            return -1;
        }

        public void CleanPatternOrderList()
        {
            for (var i = 0; i < 256; i++)
            {
                if (i >= SongLength) PatternOrderList[i] = 0;
            }
        }

        private void RemapPattern(int source, int dest)
        {
            for (var p = 0; p < SongLength; p++)
            {
                if (PatternOrderList[p] == source)
                    PatternOrderList[p] = (byte)dest;
            }
        }

        private byte PatBreak(int value)
        {
            var faceValue = "0x" + value;
            return Convert.ToByte(faceValue, 16);
        }

        public void ResizeChannels(int newChannelCount, bool centre)
        {
            if (newChannelCount <= ChannelCount)
                throw new Exception("Don't reduce the channel count this much, you'll ruin the faaarkin tune mate!");

            if ((newChannelCount % 2) == 1)
                throw new Exception("Channel count must be even");
            
            if (newChannelCount > 64)
                throw new Exception("You bloody idiot");
            
            int currentCount = ChannelCount;

            var space = new byte[((newChannelCount*5) - (currentCount*5))/2];

            foreach (var pat in Patterns)
            {
                var reader = new BinaryReader(new MemoryStream(pat.UnpackedPatternData));
                var writer = new BinaryWriter(new MemoryStream());

                for (var row = 0; row < pat.RowCount; row++)
                {
                    if (centre)
                    {
                        writer.Write(space);
                        writer.Write(reader.ReadBytes((int)currentCount * 5));
                        writer.Write(space);
                    }
                    else
                    {
                        writer.Write(reader.ReadBytes((int) currentCount*5));
                        writer.Write(space);
                        writer.Write(space);
                    }
                }
                pat.UnpackedPatternData = ((MemoryStream)writer.BaseStream).ToArray();
            }

            ChannelCount = (ushort)newChannelCount;
        }

        public void CreateTestCard()
        {
            var pat = InsertNewPattern(0, 256);
            int width = ChannelCount*5;
            int index = 0;

            for (var i = 0; i < 256; i++)
            {
                pat.UnpackedPatternData[index + 0] = (byte) i;
                pat.UnpackedPatternData[index + 5 + 1] = (byte)i;
                pat.UnpackedPatternData[index + 10 + 2] = (byte)i;
                pat.UnpackedPatternData[index + 15 + 3] = (byte)i;
                pat.UnpackedPatternData[index + 20 + 4] = (byte)i;

                index += width;
            }
        }

        public void UnpackPatterns()
        {
            byte space = 0x00;

            foreach (var pat in Patterns)
            {
                using (var writer = new BinaryWriter(new MemoryStream()))
                {
                    using (var reader = new BinaryReader(new MemoryStream(pat.PackedPatternData)))
                    {
                        while (reader.BaseStream.Position < pat.PackedPatternData.Length)
                        {
                            var flags = reader.ReadByte();
                            if ((flags & 0x80) == 0)
                            {
                                // not packed
                                writer.Write(flags);
                                writer.Write(reader.ReadBytes(4));
                            }
                            else
                            {
                                // packed, loop and test flags
                                for (var i = 0; i < 5; i++)
                                {
                                    if ((flags & (0x01 << i)) == 0)
                                    {
                                        writer.Write(space);
                                    }
                                    else
                                    {
                                        writer.Write(reader.ReadByte());
                                    }
                                }
                            }
                        }
                    }

                    pat.UnpackedPatternData = ((MemoryStream)writer.BaseStream).ToArray();
                }
            }
        }

        public void PackPatterns()
        {
            foreach (var pat in Patterns)
            {
                using (var writer = new BinaryWriter(new MemoryStream()))
                {
                    using (var reader = new BinaryReader(new MemoryStream(pat.UnpackedPatternData)))
                    {
                        while (reader.BaseStream.Position < pat.UnpackedPatternData.Length)
                        {
                            var item = reader.ReadBytes(5);
                            if (item[0] > 0 && item[1] > 0 && item[2] > 0 && item[3] > 0 && item[4] > 0)
                            {
                                // all used, no packing required
                                writer.Write(item);
                            }
                            else
                            {
                                // pack
                                var flags = 0x80;
                                var packed = new List<byte>();
                                for (var i = 0; i < 5; i++)
                                {
                                    if (item[i] > 0)
                                    {
                                        flags += (0x01<<i);
                                        packed.Add(item[i]);
                                    }
                                }
                                writer.Write((byte)flags);
                                foreach (var p in packed) writer.Write(p);
                            }
                        }
                    }
                    pat.PackedPatternData = ((MemoryStream)writer.BaseStream).ToArray();
                    pat.PackedPatternSize = (ushort)pat.PackedPatternData.Length;
                    pat.RowCount = (ushort)(pat.UnpackedPatternData.Length / 5 / ChannelCount);
                }
            }
            PatternCount = (ushort)Patterns.Count;
        }
    }

    public class Pattern
    {
        public int HeaderLength { get; set; }
        public byte PackingType { get; set; }
        public UInt16 RowCount { get; set; }
        public UInt16 PackedPatternSize { get; set; }
        public byte[] PackedPatternData { get; set; }
        public byte[] UnpackedPatternData { get; set; }

        public void Plot(int x, int y, XMPixel pixel)
        {
            // boundary check
            var channelCount = UnpackedPatternData.Length / 5 / RowCount;
            if (x < 0) return;
            if (y < 0) return;
            if (x > channelCount) return;
            if (y >= RowCount) return;

            UnpackedPatternData[(x * 5) + (y * channelCount * 5) + pixel.Column] = pixel.Value;
        }

        public void InjectImage(Bitmap image, int pixelMode)
        {
            Color sourcePixel = new Color();

            int channelCount = UnpackedPatternData.Length / RowCount / 5;

            var xoff = (image.Width - channelCount)/2;
            var yoff = (image.Height - RowCount)/2;

            for (var y = 0; y < RowCount; y++)
            {
                for (var x = 0; x < channelCount; x++)
                {
                    var imageX = x + xoff;
                    var imageY = y + yoff;
                    if (imageX >= 0 && imageX < image.Width && imageY >= 0 && imageY < image.Height)
                    {
                        sourcePixel = image.GetPixel(imageX, imageY);
                    }
                    else
                    {
                        sourcePixel = new Color();
                    }

                    if (sourcePixel != null)
                    {
                        Plot(x, y, ConvertPixel(sourcePixel, pixelMode));
                    }
                }
            }
        }

        private XMPixel ConvertPixel(Color source, int mode)
        {
            var pixel = new XMPixel(0,0);
            int value = 0;

            switch (mode)
            {
                case 0:
                    value = (source.R + source.G + source.B) / 3;
                    pixel = new XMPixel(1, (byte)value);
                    break;
                case 1:
                    value = (source.R) / 12;
                    pixel = new XMPixel(1, (byte)value);
                    break;
                case 2:
                    value = (source.R) / 12;
                    pixel = new XMPixel(2, (byte)value);
                    break;
                default:
                    throw new Exception(string.Format("Invalid pixel mode: {0}", mode));
            }

            return pixel;
        }
    }

    public class Instrument
    {
        public int HeaderLength { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
        public UInt16 SampleCount { get; set; }
        public List<Sample> Samples { get; set; }

        public Instrument()
        {
            Samples = new List<Sample>();
        }
    }

    public class Sample
    {
        public int SampleHeaderSize { get; set; }
        public byte[] SampleNotes { get; set; }

        public byte[] VolumeEnvelopePoints { get; set; }
        public byte[] PanningEnvelopePoints { get; set; }

        public byte VolumePoints { get; set; }
        public byte PanningPoints { get; set; }
        public byte VolumeSustainPoint { get; set; }
        public byte VolumeLoopStartPoint { get; set; }
        public byte VolumeLoopEndPoint { get; set; }
        public byte PanningSustainPoint { get; set; }
        public byte PanningLoopStartPoint { get; set; }
        public byte PanningLoopEndPoint { get; set; }
        public byte VolumeType { get; set; }
        public byte PanningType { get; set; }
        public byte VibratoType { get; set; }
        public byte VibratoSweep { get; set; }
        public byte VibratoDepth { get; set; }
        public byte VibratoRate { get; set; }
        public UInt16 VolumeFadeout { get; set; }
        public UInt16 Reserved { get; set; }
        public byte[] Padding { get; set; }

        public int SampleLength { get; set; }
        public int SampleLoopStart { get; set; }
        public int SampleLoopLength { get; set; }
        public byte SampleVolume { get; set; }
        public byte SampleFinetune { get; set; }
        public byte SampleFlags { get; set; }
        public byte SamplePanning { get; set; }
        public byte SampleRelativeNote { get; set; }
        public byte SampleReserved { get; set; }
        public string SampleName { get; set; }
        public byte[] SampleData { get; set; }
    }
}
