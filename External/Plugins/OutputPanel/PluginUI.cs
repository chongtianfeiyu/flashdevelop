using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;
using WeifenLuo.WinFormsUI;
using PluginCore.Managers;
using PluginCore.Localization;
using PluginCore.Controls;
using PluginCore.Helpers;
using PluginCore;

namespace OutputPanel
{
    public class PluginUI : DockPanelControl
    {
        private Int32 logCount;
        private RichTextBox textLog;
        private PluginMain pluginMain;
        private String searchInvitation;
        private System.Timers.Timer scrollTimer;
        private ToolStripMenuItem wrapTextItem;
        private ToolStripSpringTextBox findTextBox;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toggleButton;
        private ToolStripButton clearButton;
        private ToolStrip toolStrip;
        private ImageList imageList;
        private Timer typingTimer;
        private Boolean scrolling;
        private Timer autoShow;
        private Boolean muted;

        public PluginUI(PluginMain pluginMain)
        {
            this.InitializeTimers();
            this.scrolling = false;
            this.pluginMain = pluginMain;
            this.logCount = TraceManager.TraceLog.Count;
            this.InitializeComponent();
            this.InitializeContextMenu();
            this.InitializeLayout();
            this.imageList = new ImageList();
            this.imageList.ColorDepth = ColorDepth.Depth32Bit;
            this.imageList.TransparentColor = Color.Transparent;
            this.imageList.ImageSize = ScaleHelper.Scale(new Size(16, 16));
            this.imageList.Images.Add(PluginBase.MainForm.FindImage("146"));
			this.imageList.Images.Add(PluginBase.MainForm.FindImage("147"));
			this.imageList.Images.Add(PluginBase.MainForm.FindImage("147|17|5|4"));
            this.ToggleButtonClick(this, new EventArgs());
        }

        #region Windows Forms Designer Generated Code

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginUI));
            this.scrollTimer = new System.Timers.Timer();
            this.textLog = new System.Windows.Forms.RichTextBox();
            this.toolStrip = new PluginCore.Controls.ToolStripEx();
            this.toggleButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.findTextBox = new System.Windows.Forms.ToolStripSpringTextBox();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.scrollTimer)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollTimer
            // 
            this.scrollTimer.Enabled = true;
            this.scrollTimer.Interval = 50;
            this.scrollTimer.SynchronizingObject = this;
            this.scrollTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.ScrollTimerElapsed);
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.SystemColors.Window;
            this.textLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog.Location = new System.Drawing.Point(1, 26);
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.Size = new System.Drawing.Size(278, 326);
            this.textLog.TabIndex = 1;
            this.textLog.Text = "";
            this.textLog.WordWrap = false;
            this.textLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PluginUIKeyDown);
            this.textLog.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextLogMouseUp);
            this.textLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.LinkClicked);
            this.textLog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextLogMouseDown);
            // 
            // toolStrip
            // 
            this.toolStrip.CanOverflow = false;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = ScaleHelper.Scale(new Size(16, 16));
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleButton,
            this.toolStripSeparator1,
            this.findTextBox,
            this.clearButton});
            this.toolStrip.Location = new System.Drawing.Point(1, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(1, 1, 2, 2);
            this.toolStrip.Size = new System.Drawing.Size(278, 26);
            this.toolStrip.Stretch = true;
            this.toolStrip.TabIndex = 1;
            // 
            // toggleButton
            // 
            this.toggleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toggleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(23, 20);
            this.toggleButton.Text = "toolStripButton1";
            this.toggleButton.Click += new System.EventHandler(this.ToggleButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // findTextBox
            //
			this.findTextBox.Name = "FindTextBox";
			this.findTextBox.Size = new System.Drawing.Size(190, 23);
			this.findTextBox.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
			this.findTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
			this.findTextBox.TextChanged += new System.EventHandler(this.FindTextBoxTextChanged);
			this.findTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PluginUIKeyDown);
			this.findTextBox.Leave += new System.EventHandler(this.FindTextBoxLeave);
            this.findTextBox.Enter += new System.EventHandler(this.FindTextBoxEnter);
            // 
            // clearButton
            //
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(23, 21);
            this.clearButton.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
			this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.clearButton.Click += new System.EventHandler(this.ClearButtonClick);
            // 
            // PluginUI
            // 
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.toolStrip);
            this.Name = "PluginUI";
            this.Size = new System.Drawing.Size(280, 352);
            ((System.ComponentModel.ISupportInitialize)(this.scrollTimer)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Methods and Event Handlers

        /// <summary>
        /// Initializes the custom rendering
        /// </summary>
        private void InitializeLayout()
        {
            this.toolStrip.Renderer = new DockPanelStripRenderer();
        }

        /// <summary>
        /// Initializes the timers used in the control.
        /// </summary>
        private void InitializeTimers()
        {
            this.autoShow = new Timer();
            this.autoShow.Interval = 300;
            this.autoShow.Tick += new EventHandler(this.AutoShowPanel);
            this.typingTimer = new Timer();
            this.typingTimer.Tick += new EventHandler(this.TypingTimerTick);
            this.typingTimer.Interval = 250;
        }

        /// <summary>
        /// Initializes the context menu
        /// </summary>
        private void InitializeContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Font = PluginBase.Settings.DefaultFont;
            menu.Renderer = new DockPanelStripRenderer();
            menu.Items.Add(new ToolStripMenuItem(TextHelper.GetString("Label.ClearOutput"), null, new EventHandler(this.ClearOutput)));
            menu.Items.Add(new ToolStripMenuItem(TextHelper.GetString("Label.CopyOutput"), null, new EventHandler(this.CopyOutput)));
            menu.Items.Add(new ToolStripSeparator());
            wrapTextItem = new ToolStripMenuItem(TextHelper.GetString("Label.WrapText"), null, new EventHandler(this.WrapText));
            menu.Items.Add(wrapTextItem);
            this.searchInvitation = TextHelper.GetString("Label.SearchInvitation");
            this.clearButton.ToolTipText = TextHelper.GetString("Label.ClearSearchText");
            this.clearButton.Image = PluginBase.MainForm.FindImage("153");
            this.textLog.Font = PluginBase.Settings.ConsoleFont;
            this.findTextBox.Text = this.searchInvitation; 
            this.textLog.ContextMenuStrip = menu;
            this.ApplyWrapText();
        }

        /// <summary>
        /// Opens the clicked link
        /// </summary>
        private void LinkClicked(Object sender, LinkClickedEventArgs e)
        {
            PluginBase.MainForm.CallCommand("Browse", e.LinkText);
        }

        /// <summary>
        /// Handle the internal key down event
        /// </summary>
        private void PluginUIKeyDown(Object sender, KeyEventArgs e)
        {
            this.OnShortcut(e.KeyData);
        }

        /// <summary>
        /// Changes the wrapping in the control
        /// </summary>
        private void WrapText(Object sender, System.EventArgs e)
        {
            this.pluginMain.PluginSettings.WrapOutput = !this.pluginMain.PluginSettings.WrapOutput;
            this.pluginMain.SaveSettings();
            this.ApplyWrapText();
        }

        /// <summary>
        /// Applies the wrapping in the control
        /// </summary>
        public void ApplyWrapText()
        {
            if (this.pluginMain.PluginPanel == null) return;
            if (this.pluginMain.PluginPanel.InvokeRequired)
            {
                this.pluginMain.PluginPanel.BeginInvoke((MethodInvoker)delegate { this.ApplyWrapText(); });
                return;
            }
            this.wrapTextItem.Checked = this.pluginMain.PluginSettings.WrapOutput;
            this.textLog.WordWrap = this.pluginMain.PluginSettings.WrapOutput;
        }

        /// <summary>
        /// Copies the text to clipboard
        /// </summary>
        private void CopyOutput(Object sender, System.EventArgs e)
        {
            if (this.textLog.SelectedText.Length > 0) this.textLog.Copy();
            else if (!String.IsNullOrEmpty(this.textLog.Text))
            {
                Clipboard.SetText(this.textLog.Text);
                PluginBase.MainForm.RefreshUI();
            }
        }

        /// <summary>
        /// Clears the output
        /// </summary>
        public void ClearOutput(Object sender, System.EventArgs e)
        {
            this.textLog.Clear();
        }

        /// <summary>
        /// Flashes the panel to the user
        /// </summary>
        public void DisplayOutput()
        {
            this.autoShow.Stop();
            this.autoShow.Start();
        }

        /// <summary>
        /// Shows the panel
        /// </summary>
        private void AutoShowPanel(Object sender, System.EventArgs e)
        {
            this.autoShow.Stop();
            if (this.textLog.TextLength > 0)
            {
                DockContent panel = this.Parent as DockContent;
                DockState ds = panel.VisibleState;
                if (!panel.Visible || ds.ToString().EndsWith("AutoHide"))
                {
                    panel.Show();
                    if (ds.ToString().EndsWith("AutoHide")) panel.Activate();
                }
            }
        }

        /// <summary>
        /// Handles the shortcut
        /// </summary>
        public Boolean OnShortcut(Keys keys)
        {
            if (ContainsFocus)
            {
                if (keys == Keys.F3)
                {
                    this.FindNextMatch(true);
                    return true;
                }
                else if (keys == (Keys.Shift | Keys.F3))
                {
                    this.FindNextMatch(false);
                    return true;
                }
                else if (keys == Keys.Escape)
                {
                    ITabbedDocument doc = PluginBase.MainForm.CurrentDocument;
                    if (doc != null && doc.IsEditable) doc.SciControl.Focus();
                }
                else if (keys == (Keys.Control | Keys.F))
                {
                    findTextBox.Focus();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds entries to the output if new entries are found
        /// </summary>
        public void AddTraces()
        {
            if (this.muted) return;
			if (!this.scrolling)
			{
				this.toggleButton.Image = this.imageList.Images[2];
				return;
			}
            IList<TraceItem> log = TraceManager.TraceLog;
            Int32 newCount = log.Count;
            if (newCount <= this.logCount)
            {
                this.logCount = newCount;
                return;
            }
            Int32 state;
            String message;
            TraceItem entry;
            String newText = "";
            Color newColor = Color.Black;
            Color currentColor = Color.Black;
            int oldSelectionStart = this.textLog.SelectionStart;
            int oldSelectionLength = this.textLog.SelectionLength;
            List<HighlightMarker> markers = this.pluginMain.PluginSettings.HighlightMarkers;
			int visibPos = this.textLog.GetCharIndexFromPosition(Point.Empty);
            for (Int32 i = this.logCount; i < newCount; i++)
            {
                entry = log[i];
                state = entry.State;
                if (entry.Message == null) message = "";
                else message = entry.Message;
                // Automatic state from message, legacy format, ie. "2:message" -> state = 2
                if (this.pluginMain.PluginSettings.UseLegacyColoring && state == 1 && message.Length > 2 && message[1] == ':' && Char.IsDigit(message[0]))
                {
                    if (int.TryParse(message[0].ToString(), out state))
                    {
                        message = message.Substring(2);
                    }
                }
                // Automatic state from message: New format with customizable markers
                if (state == 1 && markers != null && markers.Count > 0)
                {
                    foreach (HighlightMarker marker in markers)
                    {
                        if (message.Contains(marker.Marker))
                        {
                            state = (int)marker.Level;
                            break;
                        }
                    }
                }
                switch (state)
                {
                    case 0: // Info
                        newColor = Color.Gray;
                        break;
                    case 1: // Debug
                        newColor = Color.Black;
                        break;
                    case 2: // Warning
                        newColor = Color.Orange;
                        break;
                    case 3: // Error
                        newColor = Color.Red;
                        break;
                    case 4: // Fatal
                        newColor = Color.Magenta;
                        break;
                    case -1: // ProcessStart
                        newColor = Color.Blue;
                        break;
                    case -2: // ProcessEnd
                        newColor = Color.Blue;
                        break;
                    case -3: // ProcessError
                        newColor = (message.IndexOf("Warning") >= 0) ? Color.Orange : Color.Red;
                        break;
                }
                if (newColor != currentColor)
                {
                    if (newText.Length > 0)
                    {
                        this.textLog.Select(this.textLog.TextLength, 0);
                        this.textLog.SelectionColor = currentColor;
                        this.textLog.AppendText(newText);
                        newText = "";
                    }
                    currentColor = newColor;
                }
                newText += message + "\n";
            }
            if (newText.Length > 0)
            {
                this.ClearCurrentSelection();
                this.textLog.Select(this.textLog.TextLength, 0);
                this.textLog.SelectionColor = currentColor;
                this.textLog.AppendText(newText);
            }
			if (oldSelectionLength != 0) this.textLog.Select(oldSelectionStart, oldSelectionLength);
			else if (scrolling) this.textLog.Select(this.textLog.TextLength, 0);
			else this.textLog.Select(visibPos, 0);
            this.logCount = newCount;
            this.scrollTimer.Enabled = true;
            this.TypingTimerTick(null, null);
        }

        /// <summary>
        /// Scrolling fix on RichTextBox
        /// </summary> 
        private void ScrollTimerElapsed(Object sender, System.Timers.ElapsedEventArgs e)
        {
            this.scrollTimer.Enabled = false;
            if (this.pluginMain.PluginSettings.ShowOnProcessEnd)
            {
                this.DisplayOutput();
            }
            try { this.textLog.ScrollToCaret(); }
            catch { /* WineMod: not supported */ }
        }

        /// <summary>
        /// Filters the output by search text
        /// </summary>
        private void FilterOutput(String findText)
        {
            this.textLog.Select(0, this.textLog.TextLength);
            this.textLog.SelectionBackColor = this.textLog.BackColor;
            if (findText.Trim() != "")
            {
                findText = Regex.Escape(findText);
                MatchCollection results = Regex.Matches(this.textLog.Text, findText, RegexOptions.IgnoreCase);
                for (Int32 i = 0; i < results.Count; i++)
                {
                    Match match = results[i];
                    this.textLog.SelectionStart = match.Index;
                    this.textLog.SelectionLength = match.Length;
                    this.textLog.SelectionBackColor = Color.LightSkyBlue;
                }
            }
        }

        /// <summary>
        /// Handles the text change event
        /// </summary>
        private void FindTextBoxTextChanged(Object sender, EventArgs e)
        {
            if (this.textLog.TextLength > 10000)
            {
                this.typingTimer.Stop();
                this.typingTimer.Start();
            }
            else this.TypingTimerTick(null, null);
        }

        /// <summary>
        /// When the typing timer ticks update the search
        /// </summary>
        private void TypingTimerTick(Object sender, EventArgs e)
        {
            this.typingTimer.Stop();
            String searchText = this.findTextBox.Text;
            if (searchText == searchInvitation) searchText = "";
            if (searchText.Trim() != "") this.FilterOutput(searchText);
            else this.ClearCurrentSelection();
            this.clearButton.Enabled = searchText.Length > 0;
        }

        /// <summary>
        /// When user enters control, handle it
        /// </summary>
        private void FindTextBoxEnter(Object sender, System.EventArgs e)
        {
            if (this.findTextBox.Text == searchInvitation)
            {
                this.findTextBox.Text = "";
                this.findTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        /// <summary>
        /// When user leaves control, handle it
        /// </summary>
        private void FindTextBoxLeave(Object sender, System.EventArgs e)
        {
            if (this.findTextBox.Text == "")
            {
                this.clearButton.Enabled = false;
                this.findTextBox.Text = searchInvitation;
                this.findTextBox.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        /// <summary>
        /// Clears the search text from the control
        /// </summary>
        private void ClearButtonClick(Object sender, EventArgs e)
        {
            this.findTextBox.Text = "";
            this.ClearCurrentSelection();
            this.FindTextBoxLeave(null, null);
        }

        /// <summary>
        /// Finds the next match and selects it
        /// </summary>
        private void FindNextMatch(Boolean forward)
        {
            try
            {
                String searchText = this.findTextBox.Text;
                if (searchText == searchInvitation) searchText = "";
                if (searchText.Trim() != "")
                {
                    Int32 curPos = this.textLog.SelectionStart + this.textLog.SelectionLength;
                    MatchCollection results = Regex.Matches(this.textLog.Text, searchText, RegexOptions.IgnoreCase);
                    Match nearestMatch = results[0];
                    for (Int32 i = 0; i < results.Count; i++)
                    {
                        if (forward)
                        {
                            if (curPos > results[results.Count - 1].Index)
                            {
                                nearestMatch = results[0];
                                break;
                            }
                            if (results[i].Index >= curPos)
                            {
                                nearestMatch = results[i];
                                break;
                            }
                        }
                        else
                        {
                            if (this.textLog.SelectedText.Length > 0 && curPos <= results[0].Index + results[0].Length)
                            {
                                nearestMatch = results[results.Count - 1];
                                break;
                            }
                            if (curPos < results[0].Index + results[0].Length)
                            {
                                nearestMatch = results[results.Count - 1];
                                break;
                            }
                            if (this.textLog.SelectedText.Length == 0 && curPos == results[i].Index + results[i].Length)
                            {
                                nearestMatch = results[i];
                                break;
                            }
                            if (results[i].Index > nearestMatch.Index && results[i].Index + results[i].Length < curPos)
                            {
                                nearestMatch = results[i];
                            }
                        }
                    }
                    this.textLog.Focus();
                    this.textLog.Select(nearestMatch.Index, nearestMatch.Length);
                    try { this.textLog.ScrollToCaret(); }
                    catch { /* WineMod: not supported */ }
                }
            }
            catch { }
        }

        /// <summary>
        /// Clears the current selection
        /// </summary>
        private void ClearCurrentSelection()
        {
			int oldSelectionStart = this.textLog.SelectionStart;
			int oldSelectionLength = this.textLog.SelectionLength;
            this.textLog.Select(0, this.textLog.TextLength);
            this.textLog.SelectionBackColor = this.textLog.BackColor;
			this.textLog.Select(oldSelectionStart, oldSelectionLength);
        }

        /// <summary>
        /// Toggle the scrolling enabled
        /// </summary>
        private void ToggleButtonClick(object sender, EventArgs e)
        {
            this.scrolling = !this.scrolling;
            this.toggleButton.Image = this.imageList.Images[(this.scrolling ? 0 : 1)];
            this.toggleButton.ToolTipText = (this.scrolling ? TextHelper.GetString("ToolTip.StopScrolling") : TextHelper.GetString("ToolTip.StartScrolling"));
            if (this.scrolling) this.AddTraces();
        }

        /// <summary>
        /// Handle the muting of the traces
        /// </summary>
        private void TextLogMouseDown(object sender, MouseEventArgs e)
        {
            this.muted = true;
        }

        /// <summary>
        /// Handle the muting of the traces
        /// </summary>
        private void TextLogMouseUp(object sender, MouseEventArgs e)
        {
            this.muted = false;
            this.AddTraces();
        }

        #endregion

    }

}
