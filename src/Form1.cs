using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vuforia_Autoadd_Targets
{
	public partial class Form1 : Form
	{
		private enum ButtonToggleFlags : byte
		{
			None = 0,
			Cancel = 1,
			Reset = 2,
			WriteRatings = 4,
			ClearRatings = 8,
			All = byte.MaxValue
		};

		private const float SubmitTimeout = 10f;

		private bool
			readyToSubmit = false,
			submittingAll = false,
			shouldCancel = false,
			selectButtonActive = true,
			userClickedCheckAll = true;

		private int
			fileIndex = 0,
			totalFiles = 0,
			delay = 0,
			ratingCellIndex = 0,
			checkCellIndex = 0,
			nameCellIndex = 0;

		private DateTime processStart;
		private readonly bool[] ratingsChecked = { false, false, false, false, false, false };
		private readonly Dictionary<int, int> selectedRows = new Dictionary<int, int>();
		private readonly Dictionary<string, int> ratingsByName = new Dictionary<string, int>();

		public Form1()
		{
			InitializeComponent();
		}

		private void browseImagesButton_Click(object sender, EventArgs e)
		{
			fileIndex = 0;
			progressBar.Value = 0;

			if(browseImagesDialog.ShowDialog() != DialogResult.OK) {
				readyToSubmit = false;
				progressLabel.Text = "Img 0 of 0";
				return;
			}

			readyToSubmit = true;
			totalFiles = browseImagesDialog.FileNames.Length;
			progressBar.Maximum = totalFiles * 100;
			PopulateImageTextbox();
			progressLabel.Text = $"Img 0 of {totalFiles}";
		}

		private void PopulateImageTextbox()
		{
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < totalFiles; ++i) {
				sb.Append(Path.GetFileName(browseImagesDialog.FileNames[i]) + ", ");
			}
			//:: Remove final comma and space.
			sb.Length -= 2;

			imagesTextbox.Text = sb.ToString();
		}

		private void goButton_Click(object sender, EventArgs e) => HandleNavigate();

		private void urlBox_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter) {
				HandleNavigate();
			}
		}

		private void HandleNavigate()
		{
			if(!string.IsNullOrEmpty(urlBox.Text)) {
				browser.Navigate(urlBox.Text);
			}
		}

		private void submitButton_Click(object sender, EventArgs e) => processNextSubmission();

		private async void SendKeysAfterDelay(string keys, int msDelay, bool sendFinalEnter)
		{
			await Task.Delay(msDelay);
			SendKeys.SendWait(keys);
			if(sendFinalEnter) {
				SendKeys.SendWait("{enter}");
			}
		}

		private async void submitAllButton_Click(object sender, EventArgs e)
		{
			if(!readyToSubmit) {
				return;
			}

			SetControlsEnabled(false, excludeFlags: ButtonToggleFlags.Cancel);
			submittingAll = true;
			SetDelay();

			for(int i = fileIndex; i < totalFiles; ++i) {
				processNextSubmission();
				if(i < totalFiles - 1) {
					await Task.Delay(delay);
				}
				if(shouldCancel) {
					break;
				}
			}

			submittingAll = false;
			SetControlsEnabled(true);
			shouldCancel = false;
		}

		private void GetStartAndStopIndices(HtmlElementCollection entries, out int startIndex, out int stopIndex)
		{
			startIndex = 1;
			stopIndex = entries.Count - 1;

			if(string.IsNullOrWhiteSpace(nameSelectBox.Text)) {
				return;
			}

			bool hasSetStartIndex = false;

			for(int i = 1; i < entries.Count; ++i) {
				HtmlElementCollection tdCells = entries[i].GetElementsByTagName("td");
				string name = tdCells[nameCellIndex].FirstChild.Children[2].InnerText;
				if(name != nameSelectBox.Text && name != nameSelectToBox.Text) {
					continue;
				}

				if(hasSetStartIndex) {
					stopIndex = i;
					return;
				}

				startIndex = i;
				hasSetStartIndex = true;

				if(string.IsNullOrWhiteSpace(nameSelectToBox.Text)) {
					stopIndex = i;
					return;
				}
			}
		}

		private void SetRatingSelectedOnPage(int rating, bool value)
		{
			HtmlElementCollection entries = browser.Document.GetElementById("tableListMain").GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
			HtmlElementCollection tdCells;

			SetCellIndices(entries[1].GetElementsByTagName("td"));

			GetStartAndStopIndices(entries, out int startIndex, out int endIndex);

			for(int i = startIndex; i <= endIndex; ++i) {
				bool selectingUnselectedRow = value && (!selectedRows.ContainsKey(i) || selectedRows[i] != rating);
				bool deselectingSelectedRow = !value && (selectedRows.ContainsKey(i) && selectedRows[i] == rating);

				if(selectingUnselectedRow || deselectingSelectedRow) {
					tdCells = entries[i].GetElementsByTagName("td");

					if(int.Parse(tdCells[ratingCellIndex].FirstChild.FirstChild.InnerText) == rating) {
						tdCells[checkCellIndex].FirstChild.FirstChild.InvokeMember("click");

						if(selectingUnselectedRow) {
							selectedRows.Add(i, rating);
						}
						else {
							selectedRows.Remove(i);
						}
					}
				}
			}
		}

		private void SetAllSelectedOnPage(bool value)
		{
			HtmlElementCollection entries = browser.Document.GetElementById("tableListMain").GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
			HtmlElementCollection tdCells;

			SetCellIndices(entries[1].GetElementsByTagName("td"));

			GetStartAndStopIndices(entries, out int startIndex, out int stopIndex);

			for(int i = startIndex; i <= stopIndex; ++i) {
				tdCells = entries[i].GetElementsByTagName("td");

				if(value && !selectedRows.ContainsKey(i)) {
					tdCells[checkCellIndex].FirstChild.FirstChild.InvokeMember("click");
					selectedRows.Add(i, int.Parse(tdCells[ratingCellIndex].FirstChild.FirstChild.InnerText));
				}
				else if(!value && selectedRows.ContainsKey(i)) {
					tdCells[checkCellIndex].FirstChild.FirstChild.InvokeMember("click");
					selectedRows.Remove(i);
				}
			}
		}

		private void selectButton_Click(object sender, EventArgs e)
		{
			DeselectAll();

			Cursor = Cursors.WaitCursor;
			SetControlsEnabled(false, excludeFlags: ButtonToggleFlags.Cancel);

			if(checkBoxAll.CheckState != CheckState.Indeterminate) {
				SetAllSelectedOnPage(checkBoxAll.Checked);
			}
			else for(int i = 0; i < 6; ++i) {
				SetRatingSelectedOnPage(i, ratingsChecked[i]);
			}

			numFilesSelectedLabel.Text = selectedRows.Count == 1 ? "1 file selected" : $"{selectedRows.Count} files selected";

			SetControlsEnabled(true, excludeFlags: ButtonToggleFlags.ClearRatings | ButtonToggleFlags.WriteRatings);
			Cursor = Cursors.Default;
		}

		private void checkBox0_CheckedChanged(object sender, EventArgs e) => ToggleRatingSelection(0);

		private void checkBox1_CheckedChanged(object sender, EventArgs e) => ToggleRatingSelection(1);

		private void checkBox2_CheckedChanged(object sender, EventArgs e) => ToggleRatingSelection(2);

		private void checkBox3_CheckedChanged(object sender, EventArgs e) => ToggleRatingSelection(3);

		private void checkBox4_CheckedChanged(object sender, EventArgs e) => ToggleRatingSelection(4);

		private void checkBox5_CheckedChanged(object sender, EventArgs e) => ToggleRatingSelection(5);

		private void checkBoxAll_CheckedChanged(object sender, EventArgs e) => ToggleAllRatings();

		private void ToggleRatingSelection(int index)
		{
			if(index < 0 || ratingsChecked.Length <= index) {
				throw new ArgumentException($"Invalid rating {index} passed to {nameof(ToggleRatingSelection)}(). Valid ratings are between 0 and {ratingsChecked.Length - 1}, inclusive.");
			}

			userClickedCheckAll = false;
			ratingsChecked[index] = !ratingsChecked[index];

			int numChecked = 0;
			foreach(bool boxChecked in ratingsChecked) {
				if(boxChecked) {
					++numChecked;
				}
			}

			if(numChecked == ratingsChecked.Length) {
				checkBoxAll.CheckState = CheckState.Checked;
				checkBoxAll.Checked = true;
			}
			else if(numChecked > 0) {
				checkBoxAll.CheckState = CheckState.Indeterminate;
				checkBoxAll.Checked = true;
			}
			else {
				checkBoxAll.CheckState = CheckState.Unchecked;
				checkBoxAll.Checked = false;
			}

			userClickedCheckAll = true;
		}

		private void ToggleAllRatings()
		{
			bool checkBoxAllChecked = checkBoxAll.Checked;

			checkBoxAll.CheckState = checkBoxAllChecked ? CheckState.Checked : CheckState.Unchecked;
			checkBox0.Checked = checkBox1.Checked = checkBox2.Checked =
				checkBox3.Checked = checkBox4.Checked = checkBox5.Checked = checkBoxAllChecked;

			for(int i = 0; i < ratingsChecked.Length; ++i) {
				ratingsChecked[i] = checkBoxAllChecked;
			}
		}

		private void DeselectAll()
		{
			HtmlElement selectAllBox = browser.Document.GetElementById("selectall");
			if(!string.IsNullOrEmpty(selectAllBox.GetAttribute("checked"))) {
				selectAllBox.InvokeMember("click");
			}

			selectAllBox.InvokeMember("click");
			selectedRows.Clear();
		}

		private void button25_Click(object sender, EventArgs e) => ChangeNumberOfListings(0);


		private void button50_Click(object sender, EventArgs e) => ChangeNumberOfListings(1);


		private void button100_Click(object sender, EventArgs e) => ChangeNumberOfListings(2);


		private void button200_Click(object sender, EventArgs e) => ChangeNumberOfListings(3);


		private void resetSelectionButton_Click(object sender, EventArgs e)
		{
			selectedRows.Clear();
			selectButton.Enabled = selectButtonActive = true;
		}

		private void captureRatingsButton_Click(object sender, EventArgs e)
		{
			if(selectedRows.Count == 0) {
				const string Message = "No files have been selected in the application. If you have selected markers in the browser " +
					"window, please deselect them and use the application's selection controls.";

				MessageBox.Show(Message, "No Files Selected", MessageBoxButtons.OK);
				return;
			}

			SetControlsEnabled(false);

			HtmlElementCollection entries = browser.Document.GetElementById("tableListMain").GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");

			SetCellIndices(entries[1].GetElementsByTagName("td"));

			for(int i = 1; i < entries.Count; ++i) {
				HtmlElementCollection tdCells = entries[i].GetElementsByTagName("td");
				if(selectedRows.ContainsKey(i)) {
					ratingsByName.Add(tdCells[nameCellIndex].FirstChild.Children[2].InnerText, selectedRows[i]);
				}
			}

			SetControlsEnabled(true);
		}

		private void writeRatingsButton_Click(object sender, EventArgs e)
		{
			saveRatingsDialog.FileName = ratingsFileTitleBox.Text;
			if(saveRatingsDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveRatingsDialog.FileName)) {
				using(StreamWriter outStream = new StreamWriter(saveRatingsDialog.FileName, append: false)) {
					switch(saveRatingsDialog.FilterIndex) {
						case 1:
							outStream.Write(GetRatingInfoAsText());
							break;
						case 2:
							outStream.Write(GetRatingInfoAsJSON(includeWhitespace: false));
							break;
						case 3:
							outStream.Write(GetRatingInfoAsJSON(includeWhitespace: true));
							break;
						case 4:
							outStream.Write(GetRatingInfoAsCSV());
							break;
					}
				}
			}
		}

		private void clearRatingsButton_Click(object sender, EventArgs e)
		{
			ratingsByName.Clear();
			writeRatingsButton.Enabled = clearRatingsButton.Enabled = false;
		}

		private string GetRatingInfoAsText()
		{
			StringBuilder sb = new StringBuilder(MakeFileHeader());
			int padding = GetMaxNameLength() + 2;
			foreach(KeyValuePair<string, int> entry in ratingsByName) {
				sb.AppendLine(entry.Key.PadRight(padding) + entry.Value);
			}

			return sb.ToString();
		}

		private string GetRatingInfoAsJSON(bool includeWhitespace)
		{
			string indentation = includeWhitespace ? "  " : "";
			string space = includeWhitespace ? " " : "";
			string newline = includeWhitespace ? Environment.NewLine : "";

			StringBuilder sb = new StringBuilder(MakeFileHeader() + "{" + newline);

			const string LineFormat = "{2}{\"name\":{3}\"{0}\",{3}\"rating\":{3}{1}},{4}";

			foreach(KeyValuePair<string, int> entry in ratingsByName) {
				sb.AppendFormat(LineFormat, entry.Key, entry.Value, indentation, space, newline);
			}

			if(ratingsByName.Count > 0) {
				//:: Remove final comma.
				sb.Length -= includeWhitespace ? 2 : 1;
			}

			sb.Append(newline + "}");

			return sb.ToString();
		}

		private string GetRatingInfoAsCSV()
		{
			StringBuilder sb = new StringBuilder(MakeFileHeader());
			sb.AppendLine("Name,Rating");

			foreach(KeyValuePair<string, int> entry in ratingsByName) {
				bool nameHasSpecialChar = entry.Key.Contains(",") || entry.Key.Contains(" ") || entry.Key.Contains("\t");
				string name = nameHasSpecialChar ? $"\"{entry.Key}\"" : entry.Key;
				sb.AppendLine(name + "," + entry.Value);
			}

			return sb.ToString();
		}

		private string MakeFileHeader() => commentSymbolBox.Text + " VUFORIA RATINGS INFORMATION" + Environment.NewLine +
				commentSymbolBox.Text + " " + ratingsFileTitleBox.Text + Environment.NewLine +
				commentSymbolBox.Text + " " + CurrentTimeString() + Environment.NewLine + Environment.NewLine;

		private int GetMaxNameLength() => ratingsByName.Keys.Max((name) => name.Length);

		private string CurrentTimeString() => DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

		private void debugCheckBox_CheckedChanged(object sender, EventArgs e) => testBox.Visible = debugCheckBox.Checked;

		private void ChangeNumberOfListings(int numListingsIndex)
		{
			SetAllSelectedOnPage(false);
			if(numListingsIndex >= 0 && numListingsIndex < 4) {
				HtmlDocument document = browser.Document;
				document.GetElementById("rowsPerPage").InvokeMember("click");
				document.GetElementById("paginate").GetElementsByTagName("li")[numListingsIndex].FirstChild.InvokeMember("click");
			}
		}

		private void SetCellIndices(HtmlElementCollection rowCells)
		{
			bool checkCellIndexSet = false, ratingCellIndexSet = false, nameCellIndexSet = false;

			for(int i = 0; i < rowCells.Count; ++i) {
				if(rowCells[i].FirstChild != null) {
					if(!checkCellIndexSet && rowCells[i].FirstChild.FirstChild != null && rowCells[i].FirstChild.FirstChild.GetAttribute("type") == "checkbox") {
						checkCellIndex = i;
						checkCellIndexSet = true;
					}
					else if(!nameCellIndexSet && rowCells[i].FirstChild.FirstChild != null && rowCells[i].FirstChild.FirstChild.TagName.ToLower() == "img") {
						nameCellIndex = i;
						nameCellIndexSet = true;
					}
					else if(!ratingCellIndexSet && rowCells[i].FirstChild.GetAttribute("className") == "rating") {
						ratingCellIndex = i;
						ratingCellIndexSet = true;
					}
				}

				if(checkCellIndexSet && ratingCellIndexSet) {
					break;
				}
			}
		}

		private void cancelButton_Click(object sender, EventArgs e) => shouldCancel = true;

		private void processNextSubmission()
		{
			if(!readyToSubmit) {
				return;
			}

			if(!submittingAll) {
				SetControlsEnabled(false);
			}

			HtmlDocument document = browser.Document;

			progressLabel.Text = $"Img {fileIndex + 1} of {totalFiles}";
			processStart = DateTime.Now;

			while(true) {
				try {
					document.GetElementById("addDeviceTargetUserView").InvokeMember("click");
					SendKeysAfterDelay(Path.GetFileName(browseImagesDialog.FileNames[fileIndex]), 1000, sendFinalEnter: true);
					document.GetElementById("targetDimension").SetAttribute("value", widthBox.Value.ToString());
					document.GetElementById("targetName").SetAttribute("value", Path.GetFileNameWithoutExtension(browseImagesDialog.FileNames[fileIndex]));
					document.GetElementById("targetImgFile").InvokeMember("click");
				}
				catch {
					if((DateTime.Now - processStart).TotalSeconds > SubmitTimeout) {
						shouldCancel = true;
						break;
					}
					continue;
				}

				System.Threading.Thread.Sleep(1000);
				document.GetElementById("AddDeviceTargetBtn").InvokeMember("click");

				AnimateProgressBar();
				break;
			}

			if(!shouldCancel) {
				++fileIndex;
			}

			if(!submittingAll) {
				SetControlsEnabled(true, excludeFlags: ButtonToggleFlags.ClearRatings | ButtonToggleFlags.WriteRatings);
			}
		}

		private async void AnimateProgressBar()
		{
			for(int i = 0; i < 100; ++i) {
				progressBar.PerformStep();
				await Task.Delay(5);
			}
		}

		private void SetControlsEnabled(bool value, ButtonToggleFlags excludeFlags = ButtonToggleFlags.None)
		{
			browseImagesButton.Enabled = value;
			submitNextButton.Enabled = value;
			submitAllButton.Enabled = value;
			delayBox.Enabled = value;
			checkBox0.Enabled = value;
			checkBox1.Enabled = value;
			checkBox2.Enabled = value;
			checkBox3.Enabled = value;
			checkBox4.Enabled = value;
			checkBox5.Enabled = value;
			checkBoxAll.Enabled = value;
			button25.Enabled = value;
			button50.Enabled = value;
			button100.Enabled = value;
			button200.Enabled = value;
			captureRatingsButton.Enabled = value;
			nameSelectBox.Enabled = value;
			nameSelectToBox.Enabled = value;
			ratingsFileTitleBox.Enabled = value;
			commentSymbolBox.Enabled = value;
			widthBox.Enabled = value;

			if((excludeFlags & ButtonToggleFlags.Cancel) == 0) {
				cancelButton.Enabled = value;
			}
			if((excludeFlags & ButtonToggleFlags.Reset) == 0 && selectButtonActive) {
				selectButton.Enabled = value;
			}
			if((excludeFlags & ButtonToggleFlags.WriteRatings) == 0 && (!value || ratingsByName.Count > 0)) {
				writeRatingsButton.Enabled = value;
			}
			if((excludeFlags & ButtonToggleFlags.ClearRatings) == 0 && (!value || ratingsByName.Count > 0)) {
				clearRatingsButton.Enabled = value;
			}
		}

		private void SetDelay()
		{
			if(delayBox.Text.Length > 0) {
				delay = (int)(delayBox.Value * 1000);
			}
			else {
				delay = 10_000;
				delayBox.Text = (delay / 1000).ToString();
			}
		}
	}
}
