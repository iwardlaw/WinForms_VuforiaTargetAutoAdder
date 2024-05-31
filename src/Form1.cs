using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vuforia_Autoadd_Targets {
  public partial class Form1 : Form {

    //private enum ButtonToggle { All, Cancel, Reset, WriteRatings, ClearRatings };

    private enum ButtonToggleFlags { None = 0, Cancel = 1, Reset = 2, WriteRatings = 4, ClearRatings = 8, All = int.MaxValue };

    private bool readyToSubmit = false, submittingAll = false, cancel = false;
    private int fileIndex = 0, totalFiles = 0, delay = 0;
    private int ratingCellIndex = 0, checkCellIndex = 0, nameCellIndex = 0;
    private bool[] ratingsChecked = { false, false, false, false, false, false };
    private Dictionary<int, int> selectedRows;
    private bool selectButtonActive = true, resetSelectionButtonActive = false;
    private bool userClickedCheckAll = true;
    private Dictionary<string, int> ratingsByName;
    private float submitTimeout = 10f;
    private bool processing, timedOut;
    private DateTime processStart;

    public Form1()
    {
      InitializeComponent();

      //browser.ScriptErrorsSuppressed = true;

      //browseImagesDialog.Filter = "JPG files (*.jpg)|*.jpg|PNG files (*.png)|*.png";
      //browseImagesDialog.Multiselect = true;

      selectedRows = new Dictionary<int, int>();
      ratingsByName = new Dictionary<string, int>();
      //resetSelectionButton.Enabled = false;

      //dropdownBox.SelectedIndex = 0;
    }

    private void browseImagesButton_Click(object sender, EventArgs e)
    {
      //SendKeysAfterDelay("FishFloorMedium.jpg", 1500, true);
      if(browseImagesDialog.ShowDialog() == DialogResult.OK) {
        readyToSubmit = true;
        totalFiles = browseImagesDialog.FileNames.Length;
        //progressBar.Maximum = totalFiles;
        progressBar.Maximum = totalFiles * 100;
        imagesTextbox.Clear();
        for(int i = 0; i < totalFiles - 1; ++i)
          imagesTextbox.AppendText(Path.GetFileName(browseImagesDialog.FileNames[i]) + ", ");
        imagesTextbox.AppendText(Path.GetFileName(browseImagesDialog.FileNames[totalFiles - 1]));
        progressLabel.Text = "Img 0 of " + totalFiles;
      }
      else {
        readyToSubmit = false;
        progressLabel.Text = "Img 0 of 0";
      }
      fileIndex = 0;
      progressBar.Value = 0;
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      handleNavigate();
    }

    private void urlBox_TextChanged(object sender, EventArgs e) {}

    private void urlBox_KeyDown(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Enter)
        handleNavigate();
    }

    private void handleNavigate()
    {
      if(!string.IsNullOrEmpty(urlBox.Text))
        browser.Navigate(urlBox.Text);
    }

    private void enterFilename(int index)
    {
      if(readyToSubmit) {
        //SendKeys.SendWait(browseImagesDialog.FileNames[index]);
        //SendKeys.SendWait(@"{Enter}");
        
      }
    }

    private void submitButton_Click(object sender, EventArgs e)
    {
      processNextSubmission();
    }

    // Informed by Silvio N. on StackOverflow.
    // <https://stackoverflow.com/questions/23250224/windows-forms-wait-5-seconds-before-displaying-a-message#23250842>
    private async Task WaitForMs(int ms)
    {
      Cursor = Cursors.WaitCursor;
      Enabled = false;
      await Task.Delay(ms);
      Enabled = true;
      Cursor = Cursors.Default;
    }

    private void WaitForSeconds(int seconds)
    {
      WaitForMs(seconds * 1000);
    }

    private async void SendKeysAfterDelay(string keys, int msDelay, bool enterAfter)
    {
      await Task.Delay(msDelay);
      SendKeys.SendWait(keys);
      if(enterAfter)
        SendKeys.SendWait(@"{enter}");
    }

    private async void submitAllButton_Click(object sender, EventArgs e)
    {
      if(readyToSubmit) {
        //SetControlsEnabled(false, ButtonToggle.All);
        SetControlsEnabled(false, ButtonToggleFlags.Cancel);
        submittingAll = true;
        SetDelay();
        timedOut = false;
        for(int i = fileIndex; i < totalFiles; ++i) {
          processNextSubmission();
          if(i < totalFiles - 1)
            await Task.Delay(delay);
          if(cancel) break;
        }
        submittingAll = false;
        //SetControlsEnabled(true, ButtonToggle.All);
        SetControlsEnabled(true);
        cancel = false;
      }
    }



    //private void cullButton_Click(object sender, EventArgs e)
    //{
    //  if(dropdownBox.SelectedIndex == 0)
    //    SelectSinglePage(true);
    //  else
    //    CullAll();
    //}

    private bool GetStartAndStopIndices(ref HtmlElementCollection entries, out int start, out int end)
    {
      start = 1;
      end = entries.Count - 1;

      if(!string.IsNullOrWhiteSpace(nameSelectBox.Text)) {
        HtmlElementCollection tdCells;
        string name;
        bool startSet = false;

        for(int i = 1; i < entries.Count; ++i) {
          tdCells = entries[i].GetElementsByTagName("td");
          name = tdCells[nameCellIndex].FirstChild.Children[2].InnerText;
          if(name == nameSelectBox.Text || name == nameSelectToBox.Text) {
            if(!startSet) {
              start = i;
              if(string.IsNullOrWhiteSpace(nameSelectToBox.Text)) {
                end = i;
                return true;
              }
              startSet = true;
            }
            else {
              end = i;
              return true;
            }
          }
        }
      }

      return false;
    }

    private void SelectOnPage(int rating, bool select)
    {
      //if(minRatingBox.Value > 0) {
      //if(!ratingsChecked[rating]) {
      HtmlElementCollection entries = browser.Document.GetElementById("tableListMain").GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
      HtmlElementCollection tdCells;

      SetCellIndices(entries[1].GetElementsByTagName("td"));
      //markersRemovedFromPage = 0;

      int startIndex, endIndex;
      GetStartAndStopIndices(ref entries, out startIndex, out endIndex);

      for(int i = startIndex; i <= endIndex; ++i) {
        if((select && (!selectedRows.ContainsKey(i) || selectedRows[i] != rating)) || (!select && (selectedRows.ContainsKey(i) && selectedRows[i] == rating))) {
          tdCells = entries[i].GetElementsByTagName("td");
          //testBox.Text = tdCells[ratingCellIndex].FirstChild.InnerHtml.ToString();
          //if(int.Parse(tdCells[ratingCellIndex].FirstChild.FirstChild.InnerText) < minRatingBox.Value) {
          if(int.Parse(tdCells[ratingCellIndex].FirstChild.FirstChild.InnerText) == rating) {
            tdCells[checkCellIndex].FirstChild.FirstChild.InvokeMember("click");
            if(select)
              selectedRows.Add(i, rating);
            else
              selectedRows.Remove(i);
            //entries[i].SetAttribute("className", entries[i].GetAttribute("className") + " selected");
            //if(select)
            //  tdCells[checkCellIndex].FirstChild.FirstChild.SetAttribute("checked", "checked");
            //else
            //tdCells[checkCellIndex].FirstChild.FirstChild.Attribut
            //++markersRemovedFromPage;
          }
        }
      }

      //if(delete) {
      //  entries[0].Style = "display:table-row;";
      //  entries[0].FirstChild.Style = "display:table-cell;";
      //  //entries[0].FirstChild.Children[1].FirstChild.InvokeMember("click");
      //  //entries[0].FirstChild.Children[1].FirstChild.Focus();
      //  testBox.Text = entries[0].GetElementsByTagName("a")[0].InnerHtml;
      //  //entries[0].GetElementsByTagName("a")[0].Focus();
      //  //SendKeys.SendWait(@"{enter}");
      //  entries[0].GetElementsByTagName("a")[0].InvokeMember("click");
      //  //SendKeys.SendWait(@"{enter}");
      //  //entries[0].FirstChild.Style = "display:none;";
      //  //entries[0].Style = "display:none;";
      //}
      //}
    }

    private void SelectAllOnPage(bool select)
    {
      HtmlElementCollection entries = browser.Document.GetElementById("tableListMain").GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
      HtmlElementCollection tdCells;

      SetCellIndices(entries[1].GetElementsByTagName("td"));

      int startIndex, endIndex;
      GetStartAndStopIndices(ref entries, out startIndex, out endIndex);

      for(int i = startIndex; i <= endIndex; ++i) {
        tdCells = entries[i].GetElementsByTagName("td");
        if(select && !selectedRows.ContainsKey(i)) {
          tdCells[checkCellIndex].FirstChild.FirstChild.InvokeMember("click");
          selectedRows.Add(i, int.Parse(tdCells[ratingCellIndex].FirstChild.FirstChild.InnerText));
        }
        else if(!select && selectedRows.ContainsKey(i)) {
          tdCells[checkCellIndex].FirstChild.FirstChild.InvokeMember("click");
          selectedRows.Remove(i);
        }
      }
    }

    private void selectButton_Click(object sender, EventArgs e)
    {
      DeselectAll();

      Cursor = Cursors.WaitCursor;
      //SetControlsEnabled(false, ButtonToggle.Cancel);
      SetControlsEnabled(false, ButtonToggleFlags.Cancel);

      if(checkBoxAll.CheckState != CheckState.Indeterminate)
        SelectAllOnPage(checkBoxAll.Checked);
      else for(int i = 0; i < 6; ++i)
        SelectOnPage(i, ratingsChecked[i]);

      if(selectedRows.Count == 1)
        numFilesSelectedLabel.Text = "1 file selected";
      else
        numFilesSelectedLabel.Text = selectedRows.Count + " files selected";

      //SetControlsEnabled(true, ButtonToggle.Cancel);
      SetControlsEnabled(true, ButtonToggleFlags.ClearRatings | ButtonToggleFlags.WriteRatings);
      //selectButton.Enabled = selectButtonActive = false;
      //resetSelectionButton.Enabled = resetSelectionButtonActive = true;
      Cursor = Cursors.Default;
    }

    private void checkBox0_CheckedChanged(object sender, EventArgs e)
    {
      //ratingsChecked[0] = !ratingsChecked[0];
      ProcessCheck(0);
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
      //ratingsChecked[1] = !ratingsChecked[1];
      ProcessCheck(1);
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      //ratingsChecked[2] = !ratingsChecked[2];
      ProcessCheck(2);
    }

    private void checkBox3_CheckedChanged(object sender, EventArgs e)
    {
      //ratingsChecked[3] = !ratingsChecked[3];
      ProcessCheck(3);
    }

    private void checkBox4_CheckedChanged(object sender, EventArgs e)
    {
      //ratingsChecked[4] = !ratingsChecked[4];
      ProcessCheck(4);
    }

    private void checkBox5_CheckedChanged(object sender, EventArgs e)
    {
      //ratingsChecked[5] = !ratingsChecked[5];
      ProcessCheck(5);
    }

    private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
    {
      ProcessCheck(-1);
    }

    private void ProcessCheck(int index)
    {
      if(index >= 0 && index < ratingsChecked.Length) {
        userClickedCheckAll = false;
        ratingsChecked[index] = !ratingsChecked[index];

        int numChecked = 0;
        foreach(bool boxChecked in ratingsChecked)
          if(boxChecked)
            ++numChecked;

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
      else if(index == -1 && userClickedCheckAll) {
        if(checkBoxAll.Checked) {
          checkBoxAll.CheckState = CheckState.Checked;
          checkBox0.Checked = checkBox1.Checked = checkBox2.Checked =
            checkBox3.Checked = checkBox4.Checked = checkBox5.Checked = true;
          for(int i = 0; i < ratingsChecked.Length; ++i)
            ratingsChecked[i] = true;
        }
        else {
          checkBoxAll.CheckState = CheckState.Unchecked;
          checkBox0.Checked = checkBox1.Checked = checkBox2.Checked =
            checkBox3.Checked = checkBox4.Checked = checkBox5.Checked = false;
          for(int i = 0; i < ratingsChecked.Length; ++i)
            ratingsChecked[i] = false;
        }
      }
    }

    private void DeselectAll()
    {
      HtmlElement selectAllBox = browser.Document.GetElementById("selectall");
      if(!string.IsNullOrEmpty(selectAllBox.GetAttribute("checked")))
        selectAllBox.InvokeMember("click");
      selectAllBox.InvokeMember("click");
      selectedRows.Clear();
    }

    private void button25_Click(object sender, EventArgs e)
    {
      //browser.Document.GetElementById("rowsPerPage").InvokeMember("click");
      //browser.Document.GetElementById("paginate").GetElementsByTagName("li")[0].InvokeMember("click");
      ChangeNumberOfListings(0);
    }

    private void button50_Click(object sender, EventArgs e)
    {
      //browser.Document.GetElementById("rowsPerPage").InvokeMember("click");
      //browser.Document.GetElementById("paginate").GetElementsByTagName("li")[1].InvokeMember("click");
      ChangeNumberOfListings(1);
    }

    private void button100_Click(object sender, EventArgs e)
    {
      //browser.Document.GetElementById("rowsPerPage").InvokeMember("click");
      //browser.Document.GetElementById("paginate").GetElementsByTagName("li")[2].InvokeMember("click");
      ChangeNumberOfListings(2);
    }

    private void button200_Click(object sender, EventArgs e)
    {
      //browser.Document.GetElementById("rowsPerPage").InvokeMember("click");
      //browser.Document.GetElementById("paginate").GetElementsByTagName("li")[3].InvokeMember("click");
      ChangeNumberOfListings(3);
    }

    private void resetSelectionButton_Click(object sender, EventArgs e)
    {
      selectedRows.Clear();
      selectButton.Enabled = selectButtonActive = true;
      //resetSelectionButton.Enabled = resetSelectionButtonActive = false;
    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void captureRatingsButton_Click(object sender, EventArgs e)
    {
      if(selectedRows.Count > 0) {
        //SetControlsEnabled(false, ButtonToggle.All);
        SetControlsEnabled(false);

        HtmlElementCollection entries = browser.Document.GetElementById("tableListMain").GetElementsByTagName("tbody")[0].GetElementsByTagName("tr");
        HtmlElementCollection tdCells;

        SetCellIndices(entries[1].GetElementsByTagName("td"));

        for(int i = 1; i < entries.Count; ++i) {
          tdCells = entries[i].GetElementsByTagName("td");
          if(selectedRows.ContainsKey(i)) {
            ratingsByName.Add(tdCells[nameCellIndex].FirstChild.Children[2].InnerText, selectedRows[i]);
          }
        }

        //writeRatingsButton.Enabled = true;
        //clearRatingsButton.Enabled = true;
        //SetControlsEnabled(true, ButtonToggle.All);
        SetControlsEnabled(true);
      }
      else {
        string message = "No files have been selected in the application. If you have selected markers in the browser " +
          "window, please deselect them and use the application's selection controls.";
        string caption = "No Files Selected";
        MessageBoxButtons buttons = MessageBoxButtons.OK;

        MessageBox.Show(message, caption, buttons);
      }
    }

    private void writeRatingsButton_Click(object sender, EventArgs e)
    {
      saveRatingsDialog.FileName = ratingsFileTitleBox.Text;
      if(saveRatingsDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveRatingsDialog.FileName)) {
        StreamWriter outStream = new StreamWriter(saveRatingsDialog.FileName, false);
        switch(saveRatingsDialog.FilterIndex) {
          case 1:
            outStream.Write(GetRatingInfoAsText());
            //testBox.Text = GetRatingInfoAsText();
            break;
          case 2:
            outStream.Write(GetRatingInfoAsJSON(false));
            //testBox.Text = GetRatingInfoAsJSON(false);
            break;
          case 3:
            outStream.Write(GetRatingInfoAsJSON(true));
            //testBox.Text = GetRatingInfoAsJSON(true);
            break;
          case 4:
            outStream.Write(GetRatingInfoAsCSV());
            //testBox.Text = GetRatingInfoAsCSV();
            break;
        }
        outStream.Close();
        //writeRatingsButton.Enabled = false;
      }
    }

    private void clearRatingsButton_Click(object sender, EventArgs e)
    {
      ratingsByName.Clear();
      writeRatingsButton.Enabled = false;
      clearRatingsButton.Enabled = false;
    }

    private string GetRatingInfoAsText()
    {
      string ret = MakeFileHeader();
      int padding = GetMaxNameLength() + 2;
      foreach(KeyValuePair<string, int> entry in ratingsByName)
        //ret += string.Format("{0}," + padding + "}: {1}", entry.Key, entry.Value) + Environment.NewLine;
        ret += (entry.Key).PadRight(padding) + entry.Value + Environment.NewLine;
      ret = ret.Substring(0, ret.LastIndexOf(Environment.NewLine));

      return ret;
    }

    private string GetRatingInfoAsJSON(bool compact)
    {
      string ind = compact ? "" : "  ";
      string nl = compact ? "" : Environment.NewLine;
      string spc = compact ? "" : " ";

      string ret = MakeFileHeader() + "{" + nl;
      foreach(KeyValuePair<string, int> entry in ratingsByName)
        ret += ind + "{\"name\":" + spc + "\"" + entry.Key + "\"," + spc + "\"rating\":" + spc + entry.Value + "}," + nl;
      //ret = ret.Substring(0, ret.Length - (compact ? 1 : 2)) + nl + "}";
      ret = ret.Substring(0, ret.LastIndexOf(',')) + nl + "}";

      return ret;
    }

    private string GetRatingInfoAsCSV()
    {
      string ret = MakeFileHeader(), name;
      ret += "Name,Rating" + Environment.NewLine;
      foreach(KeyValuePair<string, int> entry in ratingsByName) {
        if(entry.Key.Contains(",") || entry.Key.Contains(" ") || entry.Key.Contains("\t"))
          name = "\"" + entry.Key + "\"";
        else
          name = entry.Key;

        ret += name + "," + entry.Value + Environment.NewLine;
      }

      return ret;
    }

    private string MakeFileHeader()
    {
      return commentSymbolBox.Text + " " + "VUFORIA RATINGS INFORMATION" + Environment.NewLine +
        commentSymbolBox.Text + " " + ratingsFileTitleBox.Text + Environment.NewLine +
        commentSymbolBox.Text + " " + CurrentTimeString() + Environment.NewLine + Environment.NewLine;
    }

    private int GetMaxNameLength()
    {
      int maxLength = 0;
      foreach(string name in ratingsByName.Keys)
        if(name.Length > maxLength)
          maxLength = name.Length;

      return maxLength;
    }

    private string CurrentTimeString()
    {
      return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
    }

    private void debugCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      testBox.Visible = debugCheckBox.Checked;
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void nameSelectLabel_Click(object sender, EventArgs e)
    {

    }

    private void nameSelectBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void nameSelectToLabel_Click(object sender, EventArgs e)
    {

    }

    private void nameSelectToBox_TextChanged(object sender, EventArgs e)
    {

    }

    private void ChangeNumberOfListings(int numListingsIndex)
    {
      //DeselectAll();
      SelectAllOnPage(false);
      if(numListingsIndex >= 0 && numListingsIndex < 4) {
        browser.Document.GetElementById("rowsPerPage").InvokeMember("click");
        browser.Document.GetElementById("paginate").GetElementsByTagName("li")[numListingsIndex].FirstChild.InvokeMember("click");
      }
    }

    private void SetCellIndices(HtmlElementCollection rowCells)
    {
      bool checkCellIndexSet = false, ratingCellIndexSet = false, nameCellIndexSet = false;

      for(int i = 0; i < rowCells.Count; ++i) {
        if(rowCells[i].FirstChild != null)
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
        if(checkCellIndexSet && ratingCellIndexSet)
          break;
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      cancel = true;
    }

    private void processNextSubmission()
    {
      if(readyToSubmit) {
        if(!submittingAll) {
          //SetControlsEnabled(false, ButtonToggle.All);
          SetControlsEnabled(false);
          //SetDelay();
        }

        progressLabel.Text = "Img " + (fileIndex + 1) + " of " + totalFiles;

        //processing = true;
        processStart = DateTime.Now;

        //while(browser.Document.GetElementById("addDeviceTargetUserView") == null)
        //  /* NOP */;
        while(true) {
          try {
            browser.Document.GetElementById("addDeviceTargetUserView").InvokeMember("click");
            SendKeysAfterDelay(Path.GetFileName(browseImagesDialog.FileNames[fileIndex]), 1000, true);
            browser.Document.GetElementById("targetDimension").SetAttribute("value", widthBox.Value.ToString());
            browser.Document.GetElementById("targetName").SetAttribute("value", Path.GetFileNameWithoutExtension(browseImagesDialog.FileNames[fileIndex]));
            browser.Document.GetElementById("targetImgFile").InvokeMember("click");
          }
          catch(Exception) {
            if((DateTime.Now - processStart).TotalSeconds > submitTimeout) {
              cancel = true;
              break;
            }
            continue;
          }

          //WaitForSeconds(1);
          System.Threading.Thread.Sleep(1000);
          browser.Document.GetElementById("AddDeviceTargetBtn").InvokeMember("click");
          //WaitForSeconds(3);
          //Enabled = false;
          //System.Threading.Thread.Sleep(3000);
          //Enabled = true;
          //SendKeysAfterDelay("", 3000, false);
          //await Task.Delay(5000);
          //await Task.Delay(delay);
          //progressBar.Increment(1);
          AnimateProgressBar();
          break;
        }

        if(!cancel)
          ++fileIndex;

        if(!submittingAll)
          //SetControlsEnabled(true, ButtonToggle.All);
          SetControlsEnabled(true, ButtonToggleFlags.ClearRatings | ButtonToggleFlags.WriteRatings);
      }
    }

    private async void AnimateProgressBar()
    {
      for(int i = 0; i < 100; ++i) {
        progressBar.PerformStep();
        await Task.Delay(5);
      }
    }

    //private void SetControlsEnabled(bool value, ButtonToggle extras)
    private void SetControlsEnabled(bool value, ButtonToggleFlags exclFlags = ButtonToggleFlags.None)
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
      //selectButton.Enabled = value;
      //resetSelectionButton.Enabled = value;
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

      //if(extras == ButtonToggle.All || extras == ButtonToggle.Cancel)
      if((exclFlags & ButtonToggleFlags.Cancel) == 0)
        cancelButton.Enabled = value;
      //if(extras == ButtonToggle.All || extras == ButtonToggle.Reset) {
      if((exclFlags & ButtonToggleFlags.Reset) == 0) {
        if(selectButtonActive)
          selectButton.Enabled = value;
        //resetSelectionButton.Enabled = value;
      }
      //if(extras == ButtonToggle.All || extras == ButtonToggle.WriteRatings)
      if((exclFlags & ButtonToggleFlags.WriteRatings) == 0 && (value == false || ratingsByName.Count > 0))
        writeRatingsButton.Enabled = value;
      //if(extras == ButtonToggle.All || extras == ButtonToggle.ClearRatings)
      if((exclFlags & ButtonToggleFlags.ClearRatings) == 0 && (value == false || ratingsByName.Count > 0))
        clearRatingsButton.Enabled = value;
    }

    private void SetDelay()
    {
      if(delayBox.Text.Length > 0)
        //delay = (int)(float.Parse(delayBox.Text) * 1000f);
        delay = (int)(delayBox.Value * 1000);
      else {
        delay = 10000;
        delayBox.Text = (delay / 1000).ToString();
      }
    }
  }
}
