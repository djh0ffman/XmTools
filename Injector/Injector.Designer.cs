namespace Injector
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._btnLoadProject = new System.Windows.Forms.Button();
            this._btnSaveProject = new System.Windows.Forms.Button();
            this._btnNewProject = new System.Windows.Forms.Button();
            this._btnImportXM = new System.Windows.Forms.Button();
            this._lblXMTitle = new System.Windows.Forms.Label();
            this._btnAddPlan = new System.Windows.Forms.Button();
            this._dgvPlans = new System.Windows.Forms.DataGridView();
            this.PlanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SongPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoopSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoopLineInc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrameLineInc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFrames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatternHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PixelMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatternAllFrames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._btnDelPlan = new System.Windows.Forms.Button();
            this._btnDelImage = new System.Windows.Forms.Button();
            this._dgvImages = new System.Windows.Forms.DataGridView();
            this.Image = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatternHeightImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._btnDumpTheFuck = new System.Windows.Forms.Button();
            this._nudChannels = new System.Windows.Forms.NumericUpDown();
            this._lblChannels = new System.Windows.Forms.Label();
            this._btnPlayTheFuck = new System.Windows.Forms.Button();
            this._lblSize = new System.Windows.Forms.Label();
            this._pgbSize = new System.Windows.Forms.ProgressBar();
            this._chkCentreSource = new System.Windows.Forms.CheckBox();
            this._lblSongLength = new System.Windows.Forms.Label();
            this._pgbLength = new System.Windows.Forms.ProgressBar();
            this._btnDupe = new System.Windows.Forms.Button();
            this._dgvStills = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.StillName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StillSongPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StillPixelMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPlans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvStills)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnLoadProject
            // 
            this._btnLoadProject.Location = new System.Drawing.Point(116, 12);
            this._btnLoadProject.Name = "_btnLoadProject";
            this._btnLoadProject.Size = new System.Drawing.Size(98, 23);
            this._btnLoadProject.TabIndex = 0;
            this._btnLoadProject.Text = "Load Project";
            this._btnLoadProject.UseVisualStyleBackColor = true;
            this._btnLoadProject.Click += new System.EventHandler(this._btnLoadProject_Click);
            // 
            // _btnSaveProject
            // 
            this._btnSaveProject.Location = new System.Drawing.Point(220, 12);
            this._btnSaveProject.Name = "_btnSaveProject";
            this._btnSaveProject.Size = new System.Drawing.Size(98, 23);
            this._btnSaveProject.TabIndex = 0;
            this._btnSaveProject.Text = "Save Project";
            this._btnSaveProject.UseVisualStyleBackColor = true;
            this._btnSaveProject.Click += new System.EventHandler(this._btnSaveProject_Click);
            // 
            // _btnNewProject
            // 
            this._btnNewProject.Location = new System.Drawing.Point(12, 12);
            this._btnNewProject.Name = "_btnNewProject";
            this._btnNewProject.Size = new System.Drawing.Size(98, 23);
            this._btnNewProject.TabIndex = 0;
            this._btnNewProject.Text = "New Project";
            this._btnNewProject.UseVisualStyleBackColor = true;
            this._btnNewProject.Click += new System.EventHandler(this._btnNewProject_Click);
            // 
            // _btnImportXM
            // 
            this._btnImportXM.Location = new System.Drawing.Point(12, 57);
            this._btnImportXM.Name = "_btnImportXM";
            this._btnImportXM.Size = new System.Drawing.Size(98, 23);
            this._btnImportXM.TabIndex = 0;
            this._btnImportXM.Text = "Import XM";
            this._btnImportXM.UseVisualStyleBackColor = true;
            this._btnImportXM.Click += new System.EventHandler(this._btnImportXM_Click);
            // 
            // _lblXMTitle
            // 
            this._lblXMTitle.AutoSize = true;
            this._lblXMTitle.Location = new System.Drawing.Point(117, 62);
            this._lblXMTitle.Name = "_lblXMTitle";
            this._lblXMTitle.Size = new System.Drawing.Size(90, 13);
            this._lblXMTitle.TabIndex = 1;
            this._lblXMTitle.Text = "Please Import XM";
            // 
            // _btnAddPlan
            // 
            this._btnAddPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnAddPlan.Location = new System.Drawing.Point(916, 62);
            this._btnAddPlan.Name = "_btnAddPlan";
            this._btnAddPlan.Size = new System.Drawing.Size(52, 23);
            this._btnAddPlan.TabIndex = 3;
            this._btnAddPlan.Text = "Add";
            this._btnAddPlan.UseVisualStyleBackColor = true;
            this._btnAddPlan.Click += new System.EventHandler(this._btnAddPlan_Click);
            // 
            // _dgvPlans
            // 
            this._dgvPlans.AllowUserToAddRows = false;
            this._dgvPlans.AllowUserToDeleteRows = false;
            this._dgvPlans.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvPlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvPlans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlanName,
            this.SongPosition,
            this.DestinationLine,
            this.LoopSize,
            this.LoopLineInc,
            this.FrameLineInc,
            this.TotalFrames,
            this.PatternHeight,
            this.PixelMode,
            this.PatternAllFrames});
            this._dgvPlans.Location = new System.Drawing.Point(12, 91);
            this._dgvPlans.Name = "_dgvPlans";
            this._dgvPlans.RowHeadersVisible = false;
            this._dgvPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvPlans.Size = new System.Drawing.Size(956, 218);
            this._dgvPlans.TabIndex = 4;
            this._dgvPlans.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvPlans_CellClick);
            this._dgvPlans.SelectionChanged += new System.EventHandler(this._dgvPlans_SelectionChanged);
            // 
            // PlanName
            // 
            this.PlanName.DataPropertyName = "Name";
            this.PlanName.HeaderText = "Name";
            this.PlanName.Name = "PlanName";
            this.PlanName.Width = 130;
            // 
            // SongPosition
            // 
            this.SongPosition.DataPropertyName = "SongPosition";
            this.SongPosition.HeaderText = "SongPosition";
            this.SongPosition.Name = "SongPosition";
            this.SongPosition.Width = 80;
            // 
            // DestinationLine
            // 
            this.DestinationLine.DataPropertyName = "DestinationLine";
            this.DestinationLine.HeaderText = "DestinationLine";
            this.DestinationLine.Name = "DestinationLine";
            // 
            // LoopSize
            // 
            this.LoopSize.DataPropertyName = "LoopSize";
            this.LoopSize.HeaderText = "LoopSize";
            this.LoopSize.Name = "LoopSize";
            this.LoopSize.Width = 80;
            // 
            // LoopLineInc
            // 
            this.LoopLineInc.DataPropertyName = "LoopLineInc";
            this.LoopLineInc.HeaderText = "LoopLineInc";
            this.LoopLineInc.Name = "LoopLineInc";
            // 
            // FrameLineInc
            // 
            this.FrameLineInc.DataPropertyName = "FrameLineInc";
            this.FrameLineInc.HeaderText = "FrameLineInc";
            this.FrameLineInc.Name = "FrameLineInc";
            // 
            // TotalFrames
            // 
            this.TotalFrames.DataPropertyName = "TotalFrames";
            this.TotalFrames.HeaderText = "TotalFrames";
            this.TotalFrames.Name = "TotalFrames";
            this.TotalFrames.Width = 80;
            // 
            // PatternHeight
            // 
            this.PatternHeight.DataPropertyName = "PatternHeight";
            this.PatternHeight.HeaderText = "PatternHeight";
            this.PatternHeight.Name = "PatternHeight";
            this.PatternHeight.Width = 80;
            // 
            // PixelMode
            // 
            this.PixelMode.DataPropertyName = "PixelMode";
            this.PixelMode.HeaderText = "PixelMode";
            this.PixelMode.Name = "PixelMode";
            // 
            // PatternAllFrames
            // 
            this.PatternAllFrames.DataPropertyName = "PatternAllFrames";
            this.PatternAllFrames.HeaderText = "PatternAllFrames";
            this.PatternAllFrames.Name = "PatternAllFrames";
            // 
            // _btnDelPlan
            // 
            this._btnDelPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDelPlan.Location = new System.Drawing.Point(858, 62);
            this._btnDelPlan.Name = "_btnDelPlan";
            this._btnDelPlan.Size = new System.Drawing.Size(52, 23);
            this._btnDelPlan.TabIndex = 3;
            this._btnDelPlan.Text = "Del";
            this._btnDelPlan.UseVisualStyleBackColor = true;
            this._btnDelPlan.Click += new System.EventHandler(this._btnDelPlan_Click);
            // 
            // _btnDelImage
            // 
            this._btnDelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDelImage.Location = new System.Drawing.Point(1192, 12);
            this._btnDelImage.Name = "_btnDelImage";
            this._btnDelImage.Size = new System.Drawing.Size(48, 23);
            this._btnDelImage.TabIndex = 3;
            this._btnDelImage.Text = "Del";
            this._btnDelImage.UseVisualStyleBackColor = true;
            this._btnDelImage.Click += new System.EventHandler(this._btnDelImage_Click);
            // 
            // _dgvImages
            // 
            this._dgvImages.AllowDrop = true;
            this._dgvImages.AllowUserToAddRows = false;
            this._dgvImages.AllowUserToDeleteRows = false;
            this._dgvImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvImages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Image,
            this.PatternHeightImage});
            this._dgvImages.Location = new System.Drawing.Point(985, 41);
            this._dgvImages.Name = "_dgvImages";
            this._dgvImages.RowHeadersVisible = false;
            this._dgvImages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvImages.Size = new System.Drawing.Size(255, 455);
            this._dgvImages.TabIndex = 5;
            this._dgvImages.DragDrop += new System.Windows.Forms.DragEventHandler(this._dgvImages_DragDrop);
            this._dgvImages.DragEnter += new System.Windows.Forms.DragEventHandler(this._dgvImages_DragEnter);
            // 
            // Image
            // 
            this.Image.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Image.DataPropertyName = "FileName";
            this.Image.HeaderText = "Image";
            this.Image.Name = "Image";
            this.Image.ReadOnly = true;
            // 
            // PatternHeightImage
            // 
            this.PatternHeightImage.DataPropertyName = "PatternHeight";
            this.PatternHeightImage.HeaderText = "Height";
            this.PatternHeightImage.Name = "PatternHeightImage";
            // 
            // _btnDumpTheFuck
            // 
            this._btnDumpTheFuck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDumpTheFuck.Location = new System.Drawing.Point(985, 502);
            this._btnDumpTheFuck.Name = "_btnDumpTheFuck";
            this._btnDumpTheFuck.Size = new System.Drawing.Size(255, 60);
            this._btnDumpTheFuck.TabIndex = 6;
            this._btnDumpTheFuck.Text = "DUMP THE F**K!";
            this._btnDumpTheFuck.UseVisualStyleBackColor = true;
            this._btnDumpTheFuck.Click += new System.EventHandler(this._btnDumpTheFuck_Click);
            // 
            // _nudChannels
            // 
            this._nudChannels.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this._nudChannels.Location = new System.Drawing.Point(423, 15);
            this._nudChannels.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this._nudChannels.Name = "_nudChannels";
            this._nudChannels.Size = new System.Drawing.Size(66, 20);
            this._nudChannels.TabIndex = 7;
            this._nudChannels.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._nudChannels.ValueChanged += new System.EventHandler(this._nudChannels_ValueChanged);
            // 
            // _lblChannels
            // 
            this._lblChannels.AutoSize = true;
            this._lblChannels.Location = new System.Drawing.Point(366, 17);
            this._lblChannels.Name = "_lblChannels";
            this._lblChannels.Size = new System.Drawing.Size(51, 13);
            this._lblChannels.TabIndex = 8;
            this._lblChannels.Text = "Channels";
            // 
            // _btnPlayTheFuck
            // 
            this._btnPlayTheFuck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnPlayTheFuck.Location = new System.Drawing.Point(713, 502);
            this._btnPlayTheFuck.Name = "_btnPlayTheFuck";
            this._btnPlayTheFuck.Size = new System.Drawing.Size(255, 60);
            this._btnPlayTheFuck.TabIndex = 6;
            this._btnPlayTheFuck.Text = "PLAY THE F**K!";
            this._btnPlayTheFuck.UseVisualStyleBackColor = true;
            this._btnPlayTheFuck.Click += new System.EventHandler(this._btnPlayTheFuck_Click);
            // 
            // _lblSize
            // 
            this._lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblSize.AutoSize = true;
            this._lblSize.Location = new System.Drawing.Point(20, 506);
            this._lblSize.Name = "_lblSize";
            this._lblSize.Size = new System.Drawing.Size(69, 13);
            this._lblSize.TabIndex = 9;
            this._lblSize.Text = "Actual Size ;)";
            // 
            // _pgbSize
            // 
            this._pgbSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._pgbSize.Location = new System.Drawing.Point(23, 522);
            this._pgbSize.Name = "_pgbSize";
            this._pgbSize.Size = new System.Drawing.Size(133, 23);
            this._pgbSize.TabIndex = 10;
            // 
            // _chkCentreSource
            // 
            this._chkCentreSource.AutoSize = true;
            this._chkCentreSource.Location = new System.Drawing.Point(423, 41);
            this._chkCentreSource.Name = "_chkCentreSource";
            this._chkCentreSource.Size = new System.Drawing.Size(132, 17);
            this._chkCentreSource.TabIndex = 11;
            this._chkCentreSource.Text = "Centre Source Module";
            this._chkCentreSource.UseVisualStyleBackColor = true;
            this._chkCentreSource.CheckedChanged += new System.EventHandler(this._chkCentreSource_CheckedChanged);
            // 
            // _lblSongLength
            // 
            this._lblSongLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblSongLength.AutoSize = true;
            this._lblSongLength.Location = new System.Drawing.Point(177, 506);
            this._lblSongLength.Name = "_lblSongLength";
            this._lblSongLength.Size = new System.Drawing.Size(82, 13);
            this._lblSongLength.TabIndex = 12;
            this._lblSongLength.Text = "Actual Length ;)";
            // 
            // _pgbLength
            // 
            this._pgbLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._pgbLength.Location = new System.Drawing.Point(180, 522);
            this._pgbLength.Name = "_pgbLength";
            this._pgbLength.Size = new System.Drawing.Size(133, 23);
            this._pgbLength.TabIndex = 13;
            // 
            // _btnDupe
            // 
            this._btnDupe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnDupe.Location = new System.Drawing.Point(800, 62);
            this._btnDupe.Name = "_btnDupe";
            this._btnDupe.Size = new System.Drawing.Size(52, 23);
            this._btnDupe.TabIndex = 14;
            this._btnDupe.Text = "Dupe";
            this._btnDupe.UseVisualStyleBackColor = true;
            this._btnDupe.Click += new System.EventHandler(this._btnDupe_Click);
            // 
            // _dgvStills
            // 
            this._dgvStills.AllowUserToAddRows = false;
            this._dgvStills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvStills.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StillName,
            this.StillSongPos,
            this.StillPixelMode});
            this._dgvStills.Location = new System.Drawing.Point(12, 313);
            this._dgvStills.Name = "_dgvStills";
            this._dgvStills.RowHeadersVisible = false;
            this._dgvStills.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvStills.Size = new System.Drawing.Size(956, 149);
            this._dgvStills.TabIndex = 15;
            this._dgvStills.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvStills_CellClick);
            this._dgvStills.SelectionChanged += new System.EventHandler(this._dgvStills_SelectionChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(916, 468);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this._btnAddStill_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(858, 468);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Del";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this._btnDelStill_Click);
            // 
            // StillName
            // 
            this.StillName.DataPropertyName = "Name";
            this.StillName.HeaderText = "Name";
            this.StillName.Name = "StillName";
            this.StillName.Width = 150;
            // 
            // StillSongPos
            // 
            this.StillSongPos.DataPropertyName = "SongPosition";
            this.StillSongPos.HeaderText = "Song Position";
            this.StillSongPos.Name = "StillSongPos";
            // 
            // StillPixelMode
            // 
            this.StillPixelMode.DataPropertyName = "PixelMode";
            this.StillPixelMode.HeaderText = "PixelMode";
            this.StillPixelMode.Name = "StillPixelMode";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 574);
            this.Controls.Add(this._dgvStills);
            this.Controls.Add(this._btnDupe);
            this.Controls.Add(this._pgbLength);
            this.Controls.Add(this._lblSongLength);
            this.Controls.Add(this._chkCentreSource);
            this.Controls.Add(this._pgbSize);
            this.Controls.Add(this._lblSize);
            this.Controls.Add(this._lblChannels);
            this.Controls.Add(this._nudChannels);
            this.Controls.Add(this._btnPlayTheFuck);
            this.Controls.Add(this._btnDumpTheFuck);
            this.Controls.Add(this._dgvImages);
            this.Controls.Add(this._dgvPlans);
            this.Controls.Add(this._btnDelImage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this._btnDelPlan);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._btnAddPlan);
            this.Controls.Add(this._lblXMTitle);
            this.Controls.Add(this._btnSaveProject);
            this.Controls.Add(this._btnImportXM);
            this.Controls.Add(this._btnNewProject);
            this.Controls.Add(this._btnLoadProject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Injector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._dgvPlans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dgvStills)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnLoadProject;
        private System.Windows.Forms.Button _btnSaveProject;
        private System.Windows.Forms.Button _btnNewProject;
        private System.Windows.Forms.Button _btnImportXM;
        private System.Windows.Forms.Label _lblXMTitle;
        private System.Windows.Forms.Button _btnAddPlan;
        private System.Windows.Forms.DataGridView _dgvPlans;
        private System.Windows.Forms.Button _btnDelPlan;
        private System.Windows.Forms.Button _btnDelImage;
        private System.Windows.Forms.DataGridView _dgvImages;
        private System.Windows.Forms.Button _btnDumpTheFuck;
        private System.Windows.Forms.NumericUpDown _nudChannels;
        private System.Windows.Forms.Label _lblChannels;
        private System.Windows.Forms.Button _btnPlayTheFuck;
        private System.Windows.Forms.Label _lblSize;
        private System.Windows.Forms.ProgressBar _pgbSize;
        private System.Windows.Forms.CheckBox _chkCentreSource;
        private System.Windows.Forms.Label _lblSongLength;
        private System.Windows.Forms.ProgressBar _pgbLength;
        private System.Windows.Forms.Button _btnDupe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Image;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatternHeightImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SongPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoopSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoopLineInc;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrameLineInc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalFrames;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatternHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn PixelMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatternAllFrames;
        private System.Windows.Forms.DataGridView _dgvStills;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn StillName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StillSongPos;
        private System.Windows.Forms.DataGridViewTextBoxColumn StillPixelMode;
    }
}

