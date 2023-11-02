namespace Leo.Windows.Forms
{
    partial class EchoForm
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
            btnConnect = new Button();
            txtAddress = new TextBox();
            splitContainer1 = new SplitContainer();
            btnSend = new Button();
            lblSeperatorLine = new Label();
            tabRequest = new TabControl();
            tabMessage = new TabPage();
            txtMessage = new TextBox();
            tabParams = new TabPage();
            tabHeaders = new TabPage();
            tabSettings = new TabPage();
            lstvResponse = new ListView();
            colFirstHidden = new ColumnHeader();
            colIcon = new ColumnHeader();
            colMessage = new ColumnHeader();
            colTime = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabRequest.SuspendLayout();
            tabMessage.SuspendLayout();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConnect.BackColor = SystemColors.Highlight;
            btnConnect.ForeColor = SystemColors.Control;
            btnConnect.Location = new Point(621, 2);
            btnConnect.Margin = new Padding(4, 3, 4, 3);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(107, 32);
            btnConnect.TabIndex = 9;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtAddress
            // 
            txtAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtAddress.Location = new Point(6, 8);
            txtAddress.Margin = new Padding(4, 3, 4, 3);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(600, 23);
            txtAddress.TabIndex = 8;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 49);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(btnSend);
            splitContainer1.Panel1.Controls.Add(lblSeperatorLine);
            splitContainer1.Panel1.Controls.Add(tabRequest);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lstvResponse);
            splitContainer1.Size = new Size(732, 423);
            splitContainer1.SplitterDistance = 194;
            splitContainer1.TabIndex = 10;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSend.BackColor = SystemColors.ControlLight;
            btnSend.Enabled = false;
            btnSend.ForeColor = SystemColors.ActiveCaptionText;
            btnSend.Location = new Point(621, 157);
            btnSend.Margin = new Padding(4, 3, 4, 3);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(107, 32);
            btnSend.TabIndex = 10;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // lblSeperatorLine
            // 
            lblSeperatorLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSeperatorLine.BorderStyle = BorderStyle.Fixed3D;
            lblSeperatorLine.Location = new Point(5, 152);
            lblSeperatorLine.Name = "lblSeperatorLine";
            lblSeperatorLine.Size = new Size(720, 2);
            lblSeperatorLine.TabIndex = 1;
            // 
            // tabRequest
            // 
            tabRequest.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tabRequest.Controls.Add(tabMessage);
            tabRequest.Controls.Add(tabParams);
            tabRequest.Controls.Add(tabHeaders);
            tabRequest.Controls.Add(tabSettings);
            tabRequest.Location = new Point(2, 3);
            tabRequest.Name = "tabRequest";
            tabRequest.SelectedIndex = 0;
            tabRequest.Size = new Size(727, 146);
            tabRequest.TabIndex = 0;
            // 
            // tabMessage
            // 
            tabMessage.Controls.Add(txtMessage);
            tabMessage.Location = new Point(4, 24);
            tabMessage.Name = "tabMessage";
            tabMessage.Padding = new Padding(3);
            tabMessage.Size = new Size(719, 118);
            tabMessage.TabIndex = 0;
            tabMessage.Text = "Message";
            tabMessage.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            txtMessage.Dock = DockStyle.Fill;
            txtMessage.Location = new Point(3, 3);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(713, 112);
            txtMessage.TabIndex = 0;
            txtMessage.Text = "Hello, 世界";
            // 
            // tabParams
            // 
            tabParams.Location = new Point(4, 24);
            tabParams.Name = "tabParams";
            tabParams.Padding = new Padding(3);
            tabParams.Size = new Size(719, 118);
            tabParams.TabIndex = 1;
            tabParams.Text = "Params";
            tabParams.UseVisualStyleBackColor = true;
            // 
            // tabHeaders
            // 
            tabHeaders.Location = new Point(4, 24);
            tabHeaders.Name = "tabHeaders";
            tabHeaders.Padding = new Padding(3);
            tabHeaders.Size = new Size(719, 118);
            tabHeaders.TabIndex = 2;
            tabHeaders.Text = "Headers";
            tabHeaders.UseVisualStyleBackColor = true;
            // 
            // tabSettings
            // 
            tabSettings.Location = new Point(4, 24);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(719, 118);
            tabSettings.TabIndex = 3;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // lstvResponse
            // 
            lstvResponse.Columns.AddRange(new ColumnHeader[] { colFirstHidden, colIcon, colMessage, colTime });
            lstvResponse.Dock = DockStyle.Fill;
            lstvResponse.Location = new Point(0, 0);
            lstvResponse.MultiSelect = false;
            lstvResponse.Name = "lstvResponse";
            lstvResponse.Size = new Size(732, 225);
            lstvResponse.TabIndex = 0;
            lstvResponse.UseCompatibleStateImageBehavior = false;
            lstvResponse.View = View.Details;
            // 
            // colFirstHidden
            // 
            colFirstHidden.DisplayIndex = 3;
            colFirstHidden.Width = 0;
            // 
            // colIcon
            // 
            colIcon.DisplayIndex = 0;
            colIcon.Text = "Icon";
            colIcon.TextAlign = HorizontalAlignment.Center;
            colIcon.Width = 120;
            // 
            // colMessage
            // 
            colMessage.DisplayIndex = 1;
            colMessage.Text = "Message";
            colMessage.Width = 500;
            // 
            // colTime
            // 
            colTime.DisplayIndex = 2;
            colTime.Text = "Time";
            colTime.TextAlign = HorizontalAlignment.Center;
            colTime.Width = 120;
            // 
            // EchoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 472);
            Controls.Add(splitContainer1);
            Controls.Add(btnConnect);
            Controls.Add(txtAddress);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2);
            Name = "EchoForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "EchoForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabRequest.ResumeLayout(false);
            tabMessage.ResumeLayout(false);
            tabMessage.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private TextBox txtAddress;
        private SplitContainer splitContainer1;
        private Label lblSeperatorLine;
        private TabControl tabRequest;
        private TabPage tabMessage;
        private TabPage tabParams;
        private TabPage tabHeaders;
        private TabPage tabSettings;
        private TextBox txtMessage;
        private Button btnSend;
        private ListView lstvResponse;
        private ColumnHeader colIcon;
        private ColumnHeader colMessage;
        private ColumnHeader colTime;
        private ColumnHeader colFirstHidden;
    }
}