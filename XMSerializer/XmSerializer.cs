using System;
using System.IO;

namespace XMSerializer
{
    public class XMSerializer
    {
        public ExtendedModule DeSerialize(string fileName)
        {
            var data = File.ReadAllBytes(fileName);

            return DeSerialize(data);
        }

        public ExtendedModule DeSerialize(byte[] data)
        {
            if (data == null)
                return new ExtendedModule();

            var xm = new ExtendedModule();

            using (var reader = new XMReader(new MemoryStream(data)))
            {
                xm.IdText = reader.ReadAscii(17);
                if (xm.IdText != "Extended Module: ")
                    throw new Exception("Invalid file format");
                
                xm.ModuleName = reader.ReadAscii(20);
                xm.Tag = reader.ReadByte();
                xm.TrackerName = reader.ReadAscii(20);

                xm.VersionHigh = reader.ReadByte();
                if (xm.VersionHigh != 4)
                    throw new Exception("Invalid file version");

                xm.VersionLow = reader.ReadByte();
                if (xm.VersionLow != 1)
                    throw new Exception("Invalid file version");

                xm.HeaderSize = reader.ReadInt32();

                xm.SongLength = reader.ReadUInt16();
                xm.RestartPosition = reader.ReadUInt16();
                xm.ChannelCount = reader.ReadUInt16();
                xm.PatternCount = reader.ReadUInt16();
                xm.InstrumentCount = reader.ReadUInt16();
                xm.Flags = reader.ReadBytes(2);
                xm.Tempo = reader.ReadUInt16();
                xm.BPM = reader.ReadUInt16();
                xm.PatternOrderList = reader.ReadBytes(256);

                for (var i = 0; i < xm.PatternCount; i++)
                {
                    var pat = new Pattern();
                    pat.HeaderLength = reader.ReadInt32();
                    pat.PackingType = reader.ReadByte();
                    pat.RowCount = reader.ReadUInt16();
                    pat.PackedPatternSize = reader.ReadUInt16();
                    pat.PackedPatternData = reader.ReadBytes(pat.PackedPatternSize);

                    xm.Patterns.Add(pat);
                }

                for (var i = 0; i < xm.InstrumentCount; i++)
                {
                    var ins = new Instrument();
                    ins.HeaderLength = reader.ReadInt32();
                    ins.Name = reader.ReadAscii(22);
                    ins.Type = reader.ReadByte();
                    ins.SampleCount = reader.ReadUInt16();

                    if (ins.SampleCount > 0)
                    {
                        var smp = new Sample();
                        smp.SampleHeaderSize = reader.ReadInt32();
                        smp.SampleNotes = reader.ReadBytes(96);
                        smp.VolumeEnvelopePoints = reader.ReadBytes(48);
                        smp.PanningEnvelopePoints = reader.ReadBytes(48);
                        
                        smp.VolumePoints = reader.ReadByte();
                        smp.PanningPoints = reader.ReadByte();
                        smp.VolumeSustainPoint = reader.ReadByte();
                        smp.VolumeLoopStartPoint = reader.ReadByte();
                        smp.VolumeLoopEndPoint = reader.ReadByte();
                        smp.PanningSustainPoint = reader.ReadByte();
                        smp.PanningLoopStartPoint = reader.ReadByte();
                        smp.PanningLoopEndPoint = reader.ReadByte();
                        smp.VolumeType = reader.ReadByte();
                        smp.PanningType = reader.ReadByte();
                        smp.VibratoType = reader.ReadByte();
                        smp.VibratoSweep = reader.ReadByte();
                        smp.VibratoDepth = reader.ReadByte();
                        smp.VibratoRate = reader.ReadByte();

                        smp.VolumeFadeout = reader.ReadUInt16();
                        smp.Reserved = reader.ReadUInt16();

                        var skipSize = ins.HeaderLength - 243;
                        smp.Padding = reader.ReadBytes(skipSize);

                        smp.SampleLength = reader.ReadInt32();
                        smp.SampleLoopStart = reader.ReadInt32();
                        smp.SampleLoopLength = reader.ReadInt32();

                        smp.SampleVolume = reader.ReadByte();
                        smp.SampleFinetune = reader.ReadByte();
                        smp.SampleFlags = reader.ReadByte();
                        smp.SamplePanning = reader.ReadByte();
                        smp.SampleRelativeNote = reader.ReadByte();
                        smp.SampleReserved = reader.ReadByte();
                        smp.SampleName = reader.ReadAscii(22);
                        smp.SampleData = reader.ReadBytes(smp.SampleLength);

                        ins.Samples.Add(smp);
                    }

                    xm.Instruments.Add(ins);
                }
            }

            xm.UnpackPatterns();

            return xm;
        }

        public void Serialize(ExtendedModule xm, string fileName)
        {
            var data = Serialize(xm);
            File.WriteAllBytes(fileName, data);
        }

        public byte[] Serialize(ExtendedModule xm)
        {
            xm.PackPatterns();

            using (var writer = new XMWriter(new MemoryStream()))
            {
                writer.WriteAscii(xm.IdText, 17);
                writer.WriteAscii(xm.ModuleName, 20);
                writer.Write(xm.Tag);

                writer.WriteAscii(xm.TrackerName,20);

                writer.Write(xm.VersionHigh);
                writer.Write(xm.VersionLow);
                
                writer.Write(xm.HeaderSize);

                writer.Write(xm.SongLength);
                writer.Write(xm.RestartPosition);
                writer.Write(xm.ChannelCount);
                writer.Write(xm.PatternCount);
                writer.Write(xm.InstrumentCount);
                writer.Write(xm.Flags);
                writer.Write(xm.Tempo);
                writer.Write(xm.BPM);
                writer.Write(xm.PatternOrderList);

                foreach (var pat in xm.Patterns)
                {
                    writer.Write(pat.HeaderLength);
                    writer.Write(pat.PackingType);
                    writer.Write(pat.RowCount);
                    writer.Write(pat.PackedPatternSize);
                    writer.Write(pat.PackedPatternData);
                }

                foreach (var ins in xm.Instruments)
                {
                    writer.Write(ins.HeaderLength);
                    writer.WriteAscii(ins.Name, 22);
                    writer.Write(ins.Type);
                    writer.Write(ins.SampleCount);

                    foreach (var smp in ins.Samples)
                    {
                        writer.Write(smp.SampleHeaderSize);
                        writer.Write(smp.SampleNotes);
                        writer.Write(smp.VolumeEnvelopePoints);
                        writer.Write(smp.PanningEnvelopePoints);

                        writer.Write(smp.VolumePoints);
                        writer.Write(smp.PanningPoints);
                        writer.Write(smp.VolumeSustainPoint);
                        writer.Write(smp.VolumeLoopStartPoint);
                        writer.Write(smp.VolumeLoopEndPoint);
                        writer.Write(smp.PanningSustainPoint);
                        writer.Write(smp.PanningLoopStartPoint);
                        writer.Write(smp.PanningLoopEndPoint);
                        writer.Write(smp.VolumeType);
                        writer.Write(smp.PanningType);
                        writer.Write(smp.VibratoType);
                        writer.Write(smp.VibratoSweep);
                        writer.Write(smp.VibratoDepth);
                        writer.Write(smp.VibratoRate);

                        writer.Write(smp.VolumeFadeout);
                        writer.Write(smp.Reserved);

                        writer.Write(smp.Padding);

                        writer.Write(smp.SampleLength);
                        writer.Write(smp.SampleLoopStart);
                        writer.Write(smp.SampleLoopLength);

                        writer.Write(smp.SampleVolume);
                        writer.Write(smp.SampleFinetune);
                        writer.Write(smp.SampleFlags);
                        writer.Write(smp.SamplePanning);
                        writer.Write(smp.SampleRelativeNote);
                        writer.Write(smp.SampleReserved);
                        writer.WriteAscii(smp.SampleName, 22);
                        writer.Write(smp.SampleData);
                    }
                }

                return ((MemoryStream) writer.BaseStream).ToArray();
            }
        }
    }
}
