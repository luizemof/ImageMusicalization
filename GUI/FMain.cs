using Common;
using Common.General;
using Musicalization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
	/// <summary>
	/// Form principal da aplicação.
	/// </summary>
	public partial class FMain : Form
	{
		public FMain()
		{
			InitializeComponent();

			_FillModeComboBox();
			_CreateTabPages();

			ukMeans.Initialize();

			comboBoxModel.SelectedIndexChanged += ComboBoxModel_SelectedIndexChanged;
			pictureBox.MouseDown += PictureBox_MouseDown;
			pictureBox.MouseMove += PictureBox_MouseMove;
			General.Instance.NotificationRequested += Instance_NotificationRequested;

			bwExecute.WorkerReportsProgress = true;
		}

		private Point _StartLocation;
		private bool _Executing;

		private void _CreateTabPages()
		{
			foreach (ELogType type in Enum.GetValues(typeof(ELogType)))
			{
				if (type != ELogType.Unkown)
					_CreateTabPages(type);
			}
		}

		private void _CreateTabPages(ELogType type)
		{
			TextBox txb = new TextBox
			{
				Name = type.GetDescription(),
				Multiline = true,
				Dock = DockStyle.Fill,
				ScrollBars = ScrollBars.Both,
				ReadOnly = true
			};

			TabPage page = new TabPage();
			page.Text = page.Name = type.GetDescription();
			page.Tag = type;
			page.AutoScroll = true;
			page.Controls.Add(txb);
			tabControl.TabPages.Add(page);
		}

		private void PictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point changePoint = new Point(e.Location.X - _StartLocation.X, e.Location.Y - _StartLocation.Y);
				panPicture.AutoScrollPosition = new Point(-panPicture.AutoScrollPosition.X - changePoint.X, -panPicture.AutoScrollPosition.Y - changePoint.Y);
			}
		}

		private void PictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				_StartLocation = e.Location;
		}

		private void _FillModeComboBox()
		{
			foreach (EModelType model in Enum.GetValues(typeof(EModelType)))
				if (model != EModelType.Unknow)
					comboBoxModel.Items.Add(model);

			comboBoxModel.SelectedIndex = 1;
		}

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Multiselect = false;
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					textBoxFile.Text = dialog.FileName;
					_LoadImage(dialog.FileName);
				}
			}
		}

		private void _LoadImage(string url)
		{
			Image im = Image.FromFile(url);
			pictureBox.Size = im.Size;
			pictureBox.Image = im;
		}

		private void ComboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxModel.SelectedItem != null)
			{
				if ((EModelType)comboBoxModel.SelectedItem == EModelType.KMeans)
					ukMeans.Visible = true;
				else
					ukMeans.Visible = false;
			}
		}

		private void BtnExecute_Click(object sender, EventArgs e)
		{
			_ClearLog();
			if (!_Executing)
			{
				_Executing = true;
				btnExecute.Enabled = false;
				bwExecute.RunWorkerAsync(_BuildArgs());
			}
		}

		private MusicalizationArgs _BuildArgs()
		{
			EModelType type = (EModelType)comboBoxModel.SelectedItem;
			MusicalizationArgs args = new MusicalizationArgs
			{
				ImageFile = textBoxFile.Text,
				TargetType = type,
				Centers = type == EModelType.KMeans ? ukMeans.GetCenters() : null,
				Parallel = checkBoxParallel.Checked,
				MaxNotes = (int)numericUpDownNotes.Value,
				AnalizeImage = true
			};

			return args;
		}

		private void bwExecute_DoWork(object sender, DoWorkEventArgs e)
		{
			MusicalizationArgs args = e.Argument as MusicalizationArgs;
			if (System.IO.File.Exists(args.ImageFile))
				General.Instance.Execute(args);
		}

		private void BwExecute_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			LogArgs args = e.UserState as LogArgs;
			string description = args.LogType.GetDescription();
			if (tabControl.TabPages.ContainsKey(description))
			{
				Control page = tabControl.TabPages[description];
				if (page.Controls.ContainsKey(args.LogType.GetDescription()))
					page.Controls[description].Text += string.Format("{0}\r\n", args.Message);
			}
		}

		private void BwExecute_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
				MessageBox.Show(this, e.Error.Message, "Ocorreu um erro na exeução", MessageBoxButtons.OK, MessageBoxIcon.Error);
			btnExecute.Enabled = true;
			_Executing = false;
		}

		private void _ClearLog()
		{
			foreach (Control pages in tabControl.TabPages)
				foreach (Control txb in pages.Controls)
					txb.Text = string.Empty;
		}

		private void Instance_NotificationRequested(object sender, LogArgs e)
		{
			bwExecute.ReportProgress(0, e);
		}
	}
}