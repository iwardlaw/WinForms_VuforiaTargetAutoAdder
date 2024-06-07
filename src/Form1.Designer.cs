namespace Vuforia_Autoadd_Targets {
  partial class Form1 {
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
      if(disposing && (components != null)) {
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
      this.browser = new System.Windows.Forms.WebBrowser();
      this.urlLabel = new System.Windows.Forms.Label();
      this.filesLabel = new System.Windows.Forms.Label();
      this.imagesTextbox = new System.Windows.Forms.TextBox();
      this.browseImagesButton = new System.Windows.Forms.Button();
      this.browseImagesDialog = new System.Windows.Forms.OpenFileDialog();
      this.urlBox = new System.Windows.Forms.TextBox();
      this.goButton = new System.Windows.Forms.Button();
      this.submitNextButton = new System.Windows.Forms.Button();
      this.submitAllButton = new System.Windows.Forms.Button();
      this.progressLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.delayLabel = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.delayBox = new System.Windows.Forms.NumericUpDown();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.checkBox2 = new System.Windows.Forms.CheckBox();
      this.checkBox3 = new System.Windows.Forms.CheckBox();
      this.checkBox4 = new System.Windows.Forms.CheckBox();
      this.checkBox5 = new System.Windows.Forms.CheckBox();
      this.checkBox0 = new System.Windows.Forms.CheckBox();
      this.selectButton = new System.Windows.Forms.Button();
      this.button25 = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.button50 = new System.Windows.Forms.Button();
      this.button100 = new System.Windows.Forms.Button();
      this.button200 = new System.Windows.Forms.Button();
      this.checkBoxAll = new System.Windows.Forms.CheckBox();
      this.captureRatingsButton = new System.Windows.Forms.Button();
      this.writeRatingsButton = new System.Windows.Forms.Button();
      this.clearRatingsButton = new System.Windows.Forms.Button();
      this.saveRatingsDialog = new System.Windows.Forms.SaveFileDialog();
      this.testBox = new System.Windows.Forms.TextBox();
      this.ratingsFileTitleBox = new System.Windows.Forms.TextBox();
      this.fileTitleLabel = new System.Windows.Forms.Label();
      this.commentSymbolLabel = new System.Windows.Forms.Label();
      this.commentSymbolBox = new System.Windows.Forms.TextBox();
      this.debugCheckBox = new System.Windows.Forms.CheckBox();
      this.numFilesSelectedLabel = new System.Windows.Forms.Label();
      this.nameSelectLabel = new System.Windows.Forms.Label();
      this.nameSelectBox = new System.Windows.Forms.TextBox();
      this.nameSelectToLabel = new System.Windows.Forms.Label();
      this.nameSelectToBox = new System.Windows.Forms.TextBox();
      this.widthBox = new System.Windows.Forms.NumericUpDown();
      this.widthLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.delayBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
      this.SuspendLayout();
      // 
      // browser
      // 
      this.browser.Location = new System.Drawing.Point(12, 44);
      this.browser.MinimumSize = new System.Drawing.Size(20, 20);
      this.browser.Name = "browser";
      this.browser.ScriptErrorsSuppressed = true;
      this.browser.Size = new System.Drawing.Size(1126, 641);
      this.browser.TabIndex = 0;
      this.browser.Url = new System.Uri("https://developer.vuforia.com/target-manager", System.UriKind.Absolute);
      // 
      // urlLabel
      // 
      this.urlLabel.AutoSize = true;
      this.urlLabel.Location = new System.Drawing.Point(20, 15);
      this.urlLabel.Name = "urlLabel";
      this.urlLabel.Size = new System.Drawing.Size(32, 13);
      this.urlLabel.TabIndex = 1;
      this.urlLabel.Text = "URL:";
      // 
      // filesLabel
      // 
      this.filesLabel.AutoSize = true;
      this.filesLabel.Location = new System.Drawing.Point(20, 704);
      this.filesLabel.Name = "filesLabel";
      this.filesLabel.Size = new System.Drawing.Size(78, 13);
      this.filesLabel.TabIndex = 2;
      this.filesLabel.Text = "Files to upload:";
      // 
      // imagesTextbox
      // 
      this.imagesTextbox.Enabled = false;
      this.imagesTextbox.Location = new System.Drawing.Point(99, 701);
      this.imagesTextbox.Name = "imagesTextbox";
      this.imagesTextbox.Size = new System.Drawing.Size(270, 20);
      this.imagesTextbox.TabIndex = 4;
      // 
      // browseImagesButton
      // 
      this.browseImagesButton.Location = new System.Drawing.Point(375, 699);
      this.browseImagesButton.Name = "browseImagesButton";
      this.browseImagesButton.Size = new System.Drawing.Size(75, 23);
      this.browseImagesButton.TabIndex = 5;
      this.browseImagesButton.Text = "Browse";
      this.browseImagesButton.UseVisualStyleBackColor = true;
      this.browseImagesButton.Click += new System.EventHandler(this.browseImagesButton_Click);
      // 
      // browseImagesDialog
      // 
      this.browseImagesDialog.FileName = "browseImagesDialog";
      this.browseImagesDialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png";
      this.browseImagesDialog.Multiselect = true;
      // 
      // urlBox
      // 
      this.urlBox.Location = new System.Drawing.Point(54, 12);
      this.urlBox.Name = "urlBox";
      this.urlBox.Size = new System.Drawing.Size(413, 20);
      this.urlBox.TabIndex = 6;
      this.urlBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.urlBox_KeyDown);
      // 
      // goButton
      // 
      this.goButton.Location = new System.Drawing.Point(478, 10);
      this.goButton.Name = "goButton";
      this.goButton.Size = new System.Drawing.Size(75, 23);
      this.goButton.TabIndex = 7;
      this.goButton.Text = "Go";
      this.goButton.UseVisualStyleBackColor = true;
      this.goButton.Click += new System.EventHandler(this.goButton_Click);
      // 
      // submitNextButton
      // 
      this.submitNextButton.Location = new System.Drawing.Point(574, 699);
      this.submitNextButton.Name = "submitNextButton";
      this.submitNextButton.Size = new System.Drawing.Size(75, 23);
      this.submitNextButton.TabIndex = 8;
      this.submitNextButton.Text = "Submit Next";
      this.submitNextButton.UseVisualStyleBackColor = true;
      this.submitNextButton.Click += new System.EventHandler(this.submitButton_Click);
      // 
      // submitAllButton
      // 
      this.submitAllButton.Location = new System.Drawing.Point(660, 699);
      this.submitAllButton.Name = "submitAllButton";
      this.submitAllButton.Size = new System.Drawing.Size(75, 23);
      this.submitAllButton.TabIndex = 9;
      this.submitAllButton.Text = "Submit All";
      this.submitAllButton.UseVisualStyleBackColor = true;
      this.submitAllButton.Click += new System.EventHandler(this.submitAllButton_Click);
      // 
      // progressLabel
      // 
      this.progressLabel.AutoSize = true;
      this.progressLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.progressLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.progressLabel.Location = new System.Drawing.Point(939, 696);
      this.progressLabel.Name = "progressLabel";
      this.progressLabel.Size = new System.Drawing.Size(56, 15);
      this.progressLabel.TabIndex = 10;
      this.progressLabel.Text = "Img 0 of 0";
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(853, 699);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 11;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // delayLabel
      // 
      this.delayLabel.AutoSize = true;
      this.delayLabel.Location = new System.Drawing.Point(738, 704);
      this.delayLabel.Name = "delayLabel";
      this.delayLabel.Size = new System.Drawing.Size(51, 13);
      this.delayLabel.TabIndex = 13;
      this.delayLabel.Text = "Delay (s):";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(228, 740);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(88, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "Select by Rating:";
      // 
      // delayBox
      // 
      this.delayBox.DecimalPlaces = 1;
      this.delayBox.Location = new System.Drawing.Point(789, 702);
      this.delayBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.delayBox.Name = "delayBox";
      this.delayBox.Size = new System.Drawing.Size(53, 20);
      this.delayBox.TabIndex = 16;
      this.delayBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(939, 714);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(100, 16);
      this.progressBar.Step = 1;
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      this.progressBar.TabIndex = 20;
      // 
      // checkBox1
      // 
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new System.Drawing.Point(360, 739);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(32, 17);
      this.checkBox1.TabIndex = 21;
      this.checkBox1.Text = "1";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // checkBox2
      // 
      this.checkBox2.AutoSize = true;
      this.checkBox2.Location = new System.Drawing.Point(398, 739);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new System.Drawing.Size(32, 17);
      this.checkBox2.TabIndex = 22;
      this.checkBox2.Text = "2";
      this.checkBox2.UseVisualStyleBackColor = true;
      this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
      // 
      // checkBox3
      // 
      this.checkBox3.AutoSize = true;
      this.checkBox3.Location = new System.Drawing.Point(436, 739);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new System.Drawing.Size(32, 17);
      this.checkBox3.TabIndex = 23;
      this.checkBox3.Text = "3";
      this.checkBox3.UseVisualStyleBackColor = true;
      this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
      // 
      // checkBox4
      // 
      this.checkBox4.AutoSize = true;
      this.checkBox4.Location = new System.Drawing.Point(474, 739);
      this.checkBox4.Name = "checkBox4";
      this.checkBox4.Size = new System.Drawing.Size(32, 17);
      this.checkBox4.TabIndex = 24;
      this.checkBox4.Text = "4";
      this.checkBox4.UseVisualStyleBackColor = true;
      this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
      // 
      // checkBox5
      // 
      this.checkBox5.AutoSize = true;
      this.checkBox5.Location = new System.Drawing.Point(512, 739);
      this.checkBox5.Name = "checkBox5";
      this.checkBox5.Size = new System.Drawing.Size(32, 17);
      this.checkBox5.TabIndex = 25;
      this.checkBox5.Text = "5";
      this.checkBox5.UseVisualStyleBackColor = true;
      this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
      // 
      // checkBox0
      // 
      this.checkBox0.AutoSize = true;
      this.checkBox0.Location = new System.Drawing.Point(322, 739);
      this.checkBox0.Name = "checkBox0";
      this.checkBox0.Size = new System.Drawing.Size(32, 17);
      this.checkBox0.TabIndex = 26;
      this.checkBox0.Text = "0";
      this.checkBox0.UseVisualStyleBackColor = true;
      this.checkBox0.CheckedChanged += new System.EventHandler(this.checkBox0_CheckedChanged);
      // 
      // selectButton
      // 
      this.selectButton.Location = new System.Drawing.Point(142, 735);
      this.selectButton.Name = "selectButton";
      this.selectButton.Size = new System.Drawing.Size(75, 23);
      this.selectButton.TabIndex = 27;
      this.selectButton.Text = "Select";
      this.selectButton.UseVisualStyleBackColor = true;
      this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
      // 
      // button25
      // 
      this.button25.Location = new System.Drawing.Point(118, 803);
      this.button25.Name = "button25";
      this.button25.Size = new System.Drawing.Size(34, 23);
      this.button25.TabIndex = 29;
      this.button25.Text = "25";
      this.button25.UseVisualStyleBackColor = true;
      this.button25.Click += new System.EventHandler(this.button25_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(20, 808);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(97, 13);
      this.label2.TabIndex = 30;
      this.label2.Text = "Number of Listings:";
      // 
      // button50
      // 
      this.button50.Location = new System.Drawing.Point(158, 803);
      this.button50.Name = "button50";
      this.button50.Size = new System.Drawing.Size(34, 23);
      this.button50.TabIndex = 31;
      this.button50.Text = "50";
      this.button50.UseVisualStyleBackColor = true;
      this.button50.Click += new System.EventHandler(this.button50_Click);
      // 
      // button100
      // 
      this.button100.Location = new System.Drawing.Point(198, 803);
      this.button100.Name = "button100";
      this.button100.Size = new System.Drawing.Size(34, 23);
      this.button100.TabIndex = 32;
      this.button100.Text = "100";
      this.button100.UseVisualStyleBackColor = true;
      this.button100.Click += new System.EventHandler(this.button100_Click);
      // 
      // button200
      // 
      this.button200.Location = new System.Drawing.Point(238, 803);
      this.button200.Name = "button200";
      this.button200.Size = new System.Drawing.Size(34, 23);
      this.button200.TabIndex = 33;
      this.button200.Text = "200";
      this.button200.UseVisualStyleBackColor = true;
      this.button200.Click += new System.EventHandler(this.button200_Click);
      // 
      // checkBoxAll
      // 
      this.checkBoxAll.AutoSize = true;
      this.checkBoxAll.Location = new System.Drawing.Point(550, 739);
      this.checkBoxAll.Name = "checkBoxAll";
      this.checkBoxAll.Size = new System.Drawing.Size(37, 17);
      this.checkBoxAll.TabIndex = 34;
      this.checkBoxAll.Text = "All";
      this.checkBoxAll.UseVisualStyleBackColor = true;
      this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
      // 
      // captureRatingsButton
      // 
      this.captureRatingsButton.Location = new System.Drawing.Point(23, 770);
      this.captureRatingsButton.Name = "captureRatingsButton";
      this.captureRatingsButton.Size = new System.Drawing.Size(151, 23);
      this.captureRatingsButton.TabIndex = 35;
      this.captureRatingsButton.Text = "Capture Ratings on Selected";
      this.captureRatingsButton.UseVisualStyleBackColor = true;
      this.captureRatingsButton.Click += new System.EventHandler(this.captureRatingsButton_Click);
      // 
      // writeRatingsButton
      // 
      this.writeRatingsButton.Enabled = false;
      this.writeRatingsButton.Location = new System.Drawing.Point(295, 770);
      this.writeRatingsButton.Name = "writeRatingsButton";
      this.writeRatingsButton.Size = new System.Drawing.Size(100, 23);
      this.writeRatingsButton.TabIndex = 36;
      this.writeRatingsButton.Text = "Write Ratings Info";
      this.writeRatingsButton.UseVisualStyleBackColor = true;
      this.writeRatingsButton.Click += new System.EventHandler(this.writeRatingsButton_Click);
      // 
      // clearRatingsButton
      // 
      this.clearRatingsButton.Enabled = false;
      this.clearRatingsButton.Location = new System.Drawing.Point(185, 770);
      this.clearRatingsButton.Name = "clearRatingsButton";
      this.clearRatingsButton.Size = new System.Drawing.Size(99, 23);
      this.clearRatingsButton.TabIndex = 37;
      this.clearRatingsButton.Text = "Clear Ratings Info";
      this.clearRatingsButton.UseVisualStyleBackColor = true;
      this.clearRatingsButton.Click += new System.EventHandler(this.clearRatingsButton_Click);
      // 
      // saveRatingsDialog
      // 
      this.saveRatingsDialog.DefaultExt = "txt";
      this.saveRatingsDialog.Filter = "Text file (*.txt)|*.txt|JSON file (*.json)|*.json|JSON file (compact) (*.json)|*." +
    "json|CSV file (*.csv)|*.csv";
      // 
      // testBox
      // 
      this.testBox.Location = new System.Drawing.Point(709, 53);
      this.testBox.Multiline = true;
      this.testBox.Name = "testBox";
      this.testBox.Size = new System.Drawing.Size(381, 622);
      this.testBox.TabIndex = 38;
      this.testBox.Visible = false;
      // 
      // ratingsFileTitleBox
      // 
      this.ratingsFileTitleBox.Location = new System.Drawing.Point(490, 772);
      this.ratingsFileTitleBox.Name = "ratingsFileTitleBox";
      this.ratingsFileTitleBox.Size = new System.Drawing.Size(322, 20);
      this.ratingsFileTitleBox.TabIndex = 39;
      // 
      // fileTitleLabel
      // 
      this.fileTitleLabel.AutoSize = true;
      this.fileTitleLabel.Location = new System.Drawing.Point(401, 775);
      this.fileTitleLabel.Name = "fileTitleLabel";
      this.fileTitleLabel.Size = new System.Drawing.Size(88, 13);
      this.fileTitleLabel.TabIndex = 40;
      this.fileTitleLabel.Text = "Ratings File Title:";
      // 
      // commentSymbolLabel
      // 
      this.commentSymbolLabel.AutoSize = true;
      this.commentSymbolLabel.Location = new System.Drawing.Point(823, 775);
      this.commentSymbolLabel.Name = "commentSymbolLabel";
      this.commentSymbolLabel.Size = new System.Drawing.Size(91, 13);
      this.commentSymbolLabel.TabIndex = 41;
      this.commentSymbolLabel.Text = "Comment Symbol:";
      // 
      // commentSymbolBox
      // 
      this.commentSymbolBox.Location = new System.Drawing.Point(915, 772);
      this.commentSymbolBox.Name = "commentSymbolBox";
      this.commentSymbolBox.Size = new System.Drawing.Size(29, 20);
      this.commentSymbolBox.TabIndex = 42;
      this.commentSymbolBox.Text = "##";
      // 
      // debugCheckBox
      // 
      this.debugCheckBox.AutoSize = true;
      this.debugCheckBox.Location = new System.Drawing.Point(709, 14);
      this.debugCheckBox.Name = "debugCheckBox";
      this.debugCheckBox.Size = new System.Drawing.Size(99, 17);
      this.debugCheckBox.TabIndex = 43;
      this.debugCheckBox.Text = "Debug Textbox";
      this.debugCheckBox.UseVisualStyleBackColor = true;
      this.debugCheckBox.CheckedChanged += new System.EventHandler(this.debugCheckBox_CheckedChanged);
      // 
      // numFilesSelectedLabel
      // 
      this.numFilesSelectedLabel.AutoSize = true;
      this.numFilesSelectedLabel.BackColor = System.Drawing.SystemColors.ControlLightLight;
      this.numFilesSelectedLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.numFilesSelectedLabel.Location = new System.Drawing.Point(23, 740);
      this.numFilesSelectedLabel.Name = "numFilesSelectedLabel";
      this.numFilesSelectedLabel.Size = new System.Drawing.Size(98, 15);
      this.numFilesSelectedLabel.TabIndex = 44;
      this.numFilesSelectedLabel.Text = "0 markers selected";
      // 
      // nameSelectLabel
      // 
      this.nameSelectLabel.AutoSize = true;
      this.nameSelectLabel.Location = new System.Drawing.Point(598, 740);
      this.nameSelectLabel.Name = "nameSelectLabel";
      this.nameSelectLabel.Size = new System.Drawing.Size(63, 13);
      this.nameSelectLabel.TabIndex = 45;
      this.nameSelectLabel.Text = "With Name:";
      // 
      // nameSelectBox
      // 
      this.nameSelectBox.Location = new System.Drawing.Point(662, 737);
      this.nameSelectBox.Name = "nameSelectBox";
      this.nameSelectBox.Size = new System.Drawing.Size(147, 20);
      this.nameSelectBox.TabIndex = 46;
      // 
      // nameSelectToLabel
      // 
      this.nameSelectToLabel.AutoSize = true;
      this.nameSelectToLabel.Location = new System.Drawing.Point(817, 740);
      this.nameSelectToLabel.Name = "nameSelectToLabel";
      this.nameSelectToLabel.Size = new System.Drawing.Size(158, 13);
      this.nameSelectToLabel.TabIndex = 47;
      this.nameSelectToLabel.Text = "To Marker with Name (optional):";
      // 
      // nameSelectToBox
      // 
      this.nameSelectToBox.Location = new System.Drawing.Point(976, 737);
      this.nameSelectToBox.Name = "nameSelectToBox";
      this.nameSelectToBox.Size = new System.Drawing.Size(147, 20);
      this.nameSelectToBox.TabIndex = 48;
      // 
      // widthBox
      // 
      this.widthBox.DecimalPlaces = 1;
      this.widthBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
      this.widthBox.Location = new System.Drawing.Point(500, 702);
      this.widthBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.widthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
      this.widthBox.Name = "widthBox";
      this.widthBox.Size = new System.Drawing.Size(63, 20);
      this.widthBox.TabIndex = 49;
      this.widthBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // widthLabel
      // 
      this.widthLabel.AutoSize = true;
      this.widthLabel.Location = new System.Drawing.Point(461, 704);
      this.widthLabel.Name = "widthLabel";
      this.widthLabel.Size = new System.Drawing.Size(38, 13);
      this.widthLabel.TabIndex = 50;
      this.widthLabel.Text = "Width:";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1150, 830);
      this.Controls.Add(this.widthLabel);
      this.Controls.Add(this.widthBox);
      this.Controls.Add(this.nameSelectToBox);
      this.Controls.Add(this.nameSelectToLabel);
      this.Controls.Add(this.nameSelectBox);
      this.Controls.Add(this.nameSelectLabel);
      this.Controls.Add(this.numFilesSelectedLabel);
      this.Controls.Add(this.debugCheckBox);
      this.Controls.Add(this.commentSymbolBox);
      this.Controls.Add(this.commentSymbolLabel);
      this.Controls.Add(this.fileTitleLabel);
      this.Controls.Add(this.ratingsFileTitleBox);
      this.Controls.Add(this.testBox);
      this.Controls.Add(this.clearRatingsButton);
      this.Controls.Add(this.writeRatingsButton);
      this.Controls.Add(this.captureRatingsButton);
      this.Controls.Add(this.checkBoxAll);
      this.Controls.Add(this.button200);
      this.Controls.Add(this.button100);
      this.Controls.Add(this.button50);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.button25);
      this.Controls.Add(this.selectButton);
      this.Controls.Add(this.checkBox0);
      this.Controls.Add(this.checkBox5);
      this.Controls.Add(this.checkBox4);
      this.Controls.Add(this.checkBox3);
      this.Controls.Add(this.checkBox2);
      this.Controls.Add(this.checkBox1);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.delayBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.delayLabel);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.progressLabel);
      this.Controls.Add(this.submitAllButton);
      this.Controls.Add(this.submitNextButton);
      this.Controls.Add(this.goButton);
      this.Controls.Add(this.urlBox);
      this.Controls.Add(this.browseImagesButton);
      this.Controls.Add(this.imagesTextbox);
      this.Controls.Add(this.filesLabel);
      this.Controls.Add(this.urlLabel);
      this.Controls.Add(this.browser);
      this.Name = "Form1";
      this.Text = "Vuforia Image Target Manager";
      ((System.ComponentModel.ISupportInitialize)(this.delayBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.WebBrowser browser;
    private System.Windows.Forms.Label urlLabel;
    private System.Windows.Forms.Label filesLabel;
    private System.Windows.Forms.TextBox imagesTextbox;
    private System.Windows.Forms.Button browseImagesButton;
    private System.Windows.Forms.OpenFileDialog browseImagesDialog;
    private System.Windows.Forms.TextBox urlBox;
    private System.Windows.Forms.Button goButton;
    private System.Windows.Forms.Button submitNextButton;
    private System.Windows.Forms.Button submitAllButton;
    private System.Windows.Forms.Label progressLabel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label delayLabel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown delayBox;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.CheckBox checkBox2;
    private System.Windows.Forms.CheckBox checkBox3;
    private System.Windows.Forms.CheckBox checkBox4;
    private System.Windows.Forms.CheckBox checkBox5;
    private System.Windows.Forms.CheckBox checkBox0;
    private System.Windows.Forms.Button selectButton;
    private System.Windows.Forms.Button button25;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button button50;
    private System.Windows.Forms.Button button100;
    private System.Windows.Forms.Button button200;
    private System.Windows.Forms.CheckBox checkBoxAll;
    private System.Windows.Forms.Button captureRatingsButton;
    private System.Windows.Forms.Button writeRatingsButton;
    private System.Windows.Forms.Button clearRatingsButton;
    private System.Windows.Forms.SaveFileDialog saveRatingsDialog;
    private System.Windows.Forms.TextBox testBox;
    private System.Windows.Forms.TextBox ratingsFileTitleBox;
    private System.Windows.Forms.Label fileTitleLabel;
    private System.Windows.Forms.Label commentSymbolLabel;
    private System.Windows.Forms.TextBox commentSymbolBox;
    private System.Windows.Forms.CheckBox debugCheckBox;
    private System.Windows.Forms.Label numFilesSelectedLabel;
    private System.Windows.Forms.Label nameSelectLabel;
    private System.Windows.Forms.TextBox nameSelectBox;
    private System.Windows.Forms.Label nameSelectToLabel;
    private System.Windows.Forms.TextBox nameSelectToBox;
    private System.Windows.Forms.NumericUpDown widthBox;
    private System.Windows.Forms.Label widthLabel;
  }
}

