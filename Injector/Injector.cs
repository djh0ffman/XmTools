using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using XMSerializer;

namespace Injector
{
    public partial class Form1 : Form
    {
        private Project _project;
        private XMSerializer.XMSerializer _ser;

        public Form1()
        {
            _project = new Project();
            _ser = new XMSerializer.XMSerializer();
            InitializeComponent();
        }

        private void _btnNewProject_Click(object sender, EventArgs e)
        {
            if (AreYouSure())
            {
                _project = new Project();
                _project.XM = new ExtendedModule();
                _project.XM.ModuleName = "Please Import XM";
                RefreshData();
            }
        }

        private bool AreYouSure()
        {
            if (MessageBox.Show("Are you sure?", "Generic input question", MessageBoxButtons.OKCancel) == DialogResult.OK)
                return true;

            return false;
        }

        private void _btnImportXM_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "Extended Module|*.xm"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _project.SourceModule = File.ReadAllBytes(ofd.FileName);
                _project.ModuleFileName = Path.GetFileName(ofd.FileName);
                RefreshData();
            }
        }

        private void RefreshData()
        {
            if (_project.SourceModule != null)
                _project.XM = _ser.DeSerialize(_project.SourceModule);

            var title = _project.XM.ModuleName.Replace("\0", "").Trim();
            _lblXMTitle.Text = title == "" ? _project.ModuleFileName : title;
            _nudChannels.Value = _project.Channels;
            _chkCentreSource.Checked = _project.CentreSourceModule;
            
            var bl = new BindingList<InjectionPlan>(_project.Plans);
            var src = new BindingSource(bl, null);
            _dgvPlans.AutoGenerateColumns = false;
            _dgvPlans.DataSource = src;

            var bl2 = new BindingList<InjectionStill>(_project.Stills);
            var src2 = new BindingSource(bl2, null);
            _dgvStills.AutoGenerateColumns = false;
            _dgvStills.DataSource = src2; 
        }

        private void _btnSaveProject_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog()
            {
                Filter = "Injector Project|*.ijp"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var project = JsonConvert.SerializeObject(_project);
                File.WriteAllText(sfd.FileName, project);
            }
        }

        private void _btnLoadProject_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                Filter = "Injector Project|*.ijp"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var project = File.ReadAllText(ofd.FileName);
                _project = JsonConvert.DeserializeObject<Project>(project);
                RefreshData();
            }
        }

        private void _btnAddPlan_Click(object sender, EventArgs e)
        {
            _project.Plans.Add(new InjectionPlan
            {
                Name = "New injection plan"
            });
            RefreshData();
        }

        private void _btnAddStill_Click(object sender, EventArgs e)
        {
            _project.Stills.Add(new InjectionStill
            {
                Name = "New injection stil"
            });
            RefreshData();
        }

        private void _btnDelPlan_Click(object sender, EventArgs e)
        {
            if (_dgvPlans.SelectedRows.Count > 0)
            {
                if (AreYouSure())
                {
                    List<InjectionPlan> deletions = new List<InjectionPlan>();
                    foreach (DataGridViewRow r in _dgvPlans.SelectedRows)
                    {
                        deletions.Add((InjectionPlan)r.DataBoundItem);
                    }

                    foreach (var d in deletions)
                    {
                        _project.Plans.RemoveAt(_project.Plans.IndexOf(d));
                    }

                    RefreshData();
                }
            }
        }

        private void _btnDelStill_Click(object sender, EventArgs e)
        {
            if (_dgvStills.SelectedRows.Count > 0)
            {
                if (AreYouSure())
                {
                    List<InjectionStill> deletions = new List<InjectionStill>();
                    foreach (DataGridViewRow r in _dgvStills.SelectedRows)
                    {
                        deletions.Add((InjectionStill)r.DataBoundItem);
                    }

                    foreach (var d in deletions)
                    {
                        _project.Stills.RemoveAt(_project.Stills.IndexOf(d));
                    }

                    RefreshData();
                }
            }
        }

        private void _btnDupe_Click(object sender, EventArgs e)
        {
            if (_dgvPlans.SelectedRows.Count > 0)
            {
                if (AreYouSure())
                {
                    List<InjectionPlan> dupes = new List<InjectionPlan>();
                    foreach (DataGridViewRow r in _dgvPlans.SelectedRows)
                    {
                        dupes.Add((InjectionPlan)r.DataBoundItem);
                    }

                    foreach (var d in dupes)
                    {
                        var p = new InjectionPlan()
                        {
                            DestinationLine = d.DestinationLine,
                            Images = new List<InjectionImage>(),
                            LoopLineInc = d.LoopLineInc,
                            LoopSize = d.LoopSize,
                            Name = d.Name + " copy",
                            PatternHeight = d.PatternHeight,
                            SongPosition = d.SongPosition,
                            TotalFrames = d.TotalFrames,
                            PixelMode = d.PixelMode,
                            FrameLineInc = d.FrameLineInc,
                            PatternAllFrames = d.PatternAllFrames
                        };

                        foreach (var i in d.Images)
                        {
                            p.Images.Add(new InjectionImage()
                            {
                                FileName = i.FileName,
                                Image = i.Image,
                                PatternHeight = i.PatternHeight
                            });
                        }

                        _project.Plans.Add(p);
                    }

                    RefreshData();
                }
            }
        }



        private void _dgvPlans_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgvPlans.SelectedRows.Count == 0)
                return;

            var ijp = (InjectionPlan)_dgvPlans.SelectedRows[0].DataBoundItem;
            if (ijp == null)
                return;

            var bl = new BindingList<InjectionImage>(ijp.Images);
            var src = new BindingSource(bl, null);
            _dgvImages.AutoGenerateColumns = false;
            _dgvImages.DataSource = src;
        }

        private void _dgvStills_SelectionChanged(object sender, EventArgs e)
        {
            if (_dgvStills.SelectedRows.Count == 0)
                return;

            var ijp = (InjectionStill)_dgvStills.SelectedRows[0].DataBoundItem;
            if (ijp == null)
                return;

            var bl = new BindingList<InjectionImage>(ijp.Images);
            var src = new BindingSource(bl, null);
            _dgvImages.AutoGenerateColumns = false;
            _dgvImages.DataSource = src;
        }

        private void _dgvImages_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList();
                files.Sort();
                foreach (string filePath in files)
                {
                    if (Path.GetExtension(filePath).ToUpper() == ".PNG")
                    {
                        var src = (BindingSource)_dgvImages.DataSource;
                        if (src != null)
                        {
                            src.Add(new InjectionImage() 
                            { 
                                FileName = Path.GetFileNameWithoutExtension(filePath), 
                                Image = File.ReadAllBytes(filePath)
                            });
                        }
                    }
                }
            }
        }

        private void _dgvImages_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void _btnDelImage_Click(object sender, EventArgs e)
        {
            if (_dgvImages.SelectedRows.Count > 0)
            {
                if (AreYouSure())
                {
                    List<InjectionImage> deletions = new List<InjectionImage>();
                    foreach (DataGridViewRow r in _dgvImages.SelectedRows)
                    {
                        deletions.Add((InjectionImage)r.DataBoundItem);
                    }

                    foreach (var d in deletions)
                    {
                        var ijp = (InjectionPlan)_dgvPlans.SelectedRows[0].DataBoundItem;
                        ijp.Images.RemoveAt(ijp.Images.IndexOf(d));
                    }

                    RefreshData();
                }
            }
        }

        private void _btnDumpTheFuck_Click(object sender, EventArgs e)
        {
            DoTheWork();
            MessageBox.Show("M8 do you even compress?");
        }

        private void _btnPlayTheFuck_Click(object sender, EventArgs e)
        {
            DoTheWork();
            foreach (var p in Process.GetProcessesByName("XMPlay")) p.Kill();
            Process.Start(@"XMPlay.exe", "output.xm");
        }

        private void DoTheWork()
        {
            _project.XM = _ser.DeSerialize(_project.SourceModule);
            _project.XM.TrackerName = "Logicoma Rulez!";
            var sourceChannels = _project.XM.ChannelCount;

            _project.XM.ResizeChannels(_project.Channels, _project.CentreSourceModule);

            // run the stills
            foreach (var s in _project.Stills)
            {
                _project.XM.RunInjectionStill(s);
            }

            // run the plans
            var plans = new List<InjectionPlan>();
            foreach (var p in _project.Plans) plans.Add(p);

            plans.Sort(delegate(InjectionPlan x, InjectionPlan y)
            {
                return y.SongPosition.CompareTo(x.SongPosition);
            });

            InjectionPlan prevPlan = null;

            foreach (var plan in plans)
            {
                var followingDestinationLine = 0;

                if (prevPlan != null && prevPlan.SongPosition - 1 == plan.SongPosition)
                {
                    followingDestinationLine = prevPlan.DestinationLine;
                }

                try
                {
                    _project.XM.RunInjectionPlan(plan, sourceChannels, followingDestinationLine);
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format("Error with plan {0}: {1}", plan.Name, e.Message));
                }
                prevPlan = plan;
            }

            _project.XM.CleanPatternOrderList();
            _project.XM.RemoveDuplicatePatterns();
            var modData = _ser.Serialize(_project.XM);

            var max = 2097152;
            if (modData.Length > max)
            {
                _lblSize.Text = "FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUK!";
                _pgbSize.Maximum = max;
                _pgbSize.Value = max;
            }
            else
            {
                _lblSize.Text = string.Format("{0:n0} / {1:n0} bytes", modData.Length, max);
                _pgbSize.Maximum = max;
                _pgbSize.Value = modData.Length;
            }

            _lblSongLength.Text = string.Format("{0} / 256 positions", _project.XM.SongLength);
            _pgbLength.Maximum = 256;
            _pgbLength.Value = _project.XM.SongLength;

            File.WriteAllBytes("output.xm", modData);

            /* pattern debug export
            var patId = 0;
            foreach (var pat in _project.XM.Patterns)
            {
                File.WriteAllBytes(string.Format("C:\\Debug\\{0}.bin", patId), pat.UnpackedPatternData);
                patId++;
            }

            var list = "";
            foreach (var patItemId in _project.XM.PatternOrderList)
            {
                list += patItemId.ToString() + "\n";
            }
            File.WriteAllText("C:\\debug\\patorder.txt", list);
            */
        }

        private void _nudChannels_ValueChanged(object sender, EventArgs e)
        {
            _project.Channels = (int)((NumericUpDown)sender).Value;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var p in Process.GetProcessesByName("XMPlay")) p.Kill();
        }

        private void _chkCentreSource_CheckedChanged(object sender, EventArgs e)
        {
            _project.CentreSourceModule = _chkCentreSource.Checked;
        }

        private void _dgvPlans_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _dgvPlans_SelectionChanged(null, null);
        }

        private void _dgvStills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _dgvStills_SelectionChanged(null, null);
        }


    }
}
