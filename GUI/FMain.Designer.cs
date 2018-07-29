namespace GUI
{
    partial class FMain
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.panModel = new System.Windows.Forms.Panel();
            this.numericUpDownNotes = new System.Windows.Forms.NumericUpDown();
            this.labelNotes = new System.Windows.Forms.Label();
            this.checkBoxParallel = new System.Windows.Forms.CheckBox();
            this.lblParallel = new System.Windows.Forms.Label();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.panPicture = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.bwExecute = new System.ComponentModel.BackgroundWorker();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ukMeans = new GUI.KMeans.UKMeans();
            this.tableLayoutPanel.SuspendLayout();
            this.panModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotes)).BeginInit();
            this.panPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBoxSearch.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.tblGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoScroll = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.ukMeans, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnExecute, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(6, 83);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(199, 310);
            this.tableLayoutPanel.TabIndex = 3;
            // 
            // btnExecute
            // 
            this.btnExecute.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExecute.Location = new System.Drawing.Point(3, 9);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(193, 23);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Executar";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // panModel
            // 
            this.panModel.AutoSize = true;
            this.panModel.Controls.Add(this.numericUpDownNotes);
            this.panModel.Controls.Add(this.labelNotes);
            this.panModel.Controls.Add(this.checkBoxParallel);
            this.panModel.Controls.Add(this.lblParallel);
            this.panModel.Controls.Add(this.comboBoxModel);
            this.panModel.Controls.Add(this.lblModel);
            this.panModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panModel.Location = new System.Drawing.Point(6, 6);
            this.panModel.Name = "panModel";
            this.panModel.Padding = new System.Windows.Forms.Padding(5);
            this.panModel.Size = new System.Drawing.Size(199, 68);
            this.panModel.TabIndex = 2;
            // 
            // numericUpDownNotes
            // 
            this.numericUpDownNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownNotes.Location = new System.Drawing.Point(122, 40);
            this.numericUpDownNotes.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownNotes.Name = "numericUpDownNotes";
            this.numericUpDownNotes.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownNotes.TabIndex = 6;
            // 
            // labelNotes
            // 
            this.labelNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(85, 43);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(35, 13);
            this.labelNotes.TabIndex = 5;
            this.labelNotes.Text = "Notas";
            // 
            // checkBoxParallel
            // 
            this.checkBoxParallel.AutoSize = true;
            this.checkBoxParallel.Location = new System.Drawing.Point(58, 43);
            this.checkBoxParallel.Name = "checkBoxParallel";
            this.checkBoxParallel.Size = new System.Drawing.Size(15, 14);
            this.checkBoxParallel.TabIndex = 4;
            this.checkBoxParallel.UseVisualStyleBackColor = true;
            // 
            // lblParallel
            // 
            this.lblParallel.AutoSize = true;
            this.lblParallel.Location = new System.Drawing.Point(6, 42);
            this.lblParallel.Name = "lblParallel";
            this.lblParallel.Size = new System.Drawing.Size(48, 13);
            this.lblParallel.TabIndex = 3;
            this.lblParallel.Text = "Paralelo:";
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(57, 9);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(134, 21);
            this.comboBoxModel.TabIndex = 2;
            // 
            // lblModel
            // 
            this.lblModel.Location = new System.Drawing.Point(6, 12);
            this.lblModel.Margin = new System.Windows.Forms.Padding(0);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(45, 13);
            this.lblModel.TabIndex = 1;
            this.lblModel.Text = "Modelo:";
            // 
            // panPicture
            // 
            this.panPicture.AutoScroll = true;
            this.panPicture.Controls.Add(this.pictureBox);
            this.panPicture.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panPicture.Location = new System.Drawing.Point(214, 83);
            this.panPicture.Name = "panPicture";
            this.panPicture.Size = new System.Drawing.Size(268, 310);
            this.panPicture.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(4, 4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 50);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.AutoSize = true;
            this.groupBoxSearch.Controls.Add(this.textBoxFile);
            this.groupBoxSearch.Controls.Add(this.btnSearch);
            this.groupBoxSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSearch.Location = new System.Drawing.Point(214, 6);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Padding = new System.Windows.Forms.Padding(0);
            this.groupBoxSearch.Size = new System.Drawing.Size(268, 56);
            this.groupBoxSearch.TabIndex = 0;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Imagem";
            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.Enabled = false;
            this.textBoxFile.Location = new System.Drawing.Point(6, 19);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(169, 20);
            this.textBoxFile.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(185, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(71, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Procurar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // bwExecute
            // 
            this.bwExecute.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwExecute_DoWork);
            this.bwExecute.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BwExecute_ProgressChanged);
            this.bwExecute.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwExecute_RunWorkerCompleted);
            // 
            // tblMain
            // 
            this.tblMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tblGeneral, 0, 0);
            this.tblMain.Controls.Add(this.tabControl, 0, 1);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblMain.Size = new System.Drawing.Size(500, 561);
            this.tblMain.TabIndex = 4;
            // 
            // tblGeneral
            // 
            this.tblGeneral.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblGeneral.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tblGeneral.ColumnCount = 2;
            this.tblGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.tblGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGeneral.Controls.Add(this.groupBoxSearch, 1, 0);
            this.tblGeneral.Controls.Add(this.panPicture, 1, 1);
            this.tblGeneral.Controls.Add(this.panModel, 0, 0);
            this.tblGeneral.Controls.Add(this.tableLayoutPanel, 0, 1);
            this.tblGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGeneral.Location = new System.Drawing.Point(6, 6);
            this.tblGeneral.Name = "tblGeneral";
            this.tblGeneral.RowCount = 2;
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblGeneral.Size = new System.Drawing.Size(488, 380);
            this.tblGeneral.TabIndex = 4;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(6, 395);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(488, 160);
            this.tabControl.TabIndex = 1;
            // 
            // ukMeans
            // 
            this.ukMeans.AutoSize = true;
            this.ukMeans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ukMeans.Location = new System.Drawing.Point(3, 3);
            this.ukMeans.Name = "ukMeans";
            this.ukMeans.Size = new System.Drawing.Size(193, 1);
            this.ukMeans.TabIndex = 0;
            this.ukMeans.Visible = false;
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(500, 561);
            this.Controls.Add(this.tblMain);
            this.DoubleBuffered = true;
            this.Name = "FMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Musicalização de Imagens";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panModel.ResumeLayout(false);
            this.panModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotes)).EndInit();
            this.panPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.tblMain.ResumeLayout(false);
            this.tblGeneral.ResumeLayout(false);
            this.tblGeneral.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KMeans.UKMeans ukMeans;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.Panel panModel;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panPicture;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Label lblParallel;
        private System.Windows.Forms.CheckBox checkBoxParallel;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.NumericUpDown numericUpDownNotes;
        private System.ComponentModel.BackgroundWorker bwExecute;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TableLayoutPanel tblGeneral;

    }
}

