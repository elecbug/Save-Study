namespace TPS09_ChatClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NameBox = new TextBox();
            OutputTextBox = new RichTextBox();
            InputTextBox = new TextBox();
            SendButton = new Button();
            ConnectButton = new Button();
            SuspendLayout();
            // 
            // NameBox
            // 
            NameBox.Location = new Point(12, 12);
            NameBox.Name = "NameBox";
            NameBox.Size = new Size(331, 23);
            NameBox.TabIndex = 0;
            // 
            // OutputTextBox
            // 
            OutputTextBox.Location = new Point(12, 41);
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.ReadOnly = true;
            OutputTextBox.Size = new Size(388, 329);
            OutputTextBox.TabIndex = 1;
            OutputTextBox.Text = "";
            // 
            // InputTextBox
            // 
            InputTextBox.Location = new Point(12, 376);
            InputTextBox.Multiline = true;
            InputTextBox.Name = "InputTextBox";
            InputTextBox.Size = new Size(331, 23);
            InputTextBox.TabIndex = 0;
            // 
            // SendButton
            // 
            SendButton.Location = new Point(349, 376);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(51, 23);
            SendButton.TabIndex = 2;
            SendButton.Text = "Send";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(349, 12);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(51, 23);
            ConnectButton.TabIndex = 2;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(412, 450);
            Controls.Add(ConnectButton);
            Controls.Add(SendButton);
            Controls.Add(OutputTextBox);
            Controls.Add(InputTextBox);
            Controls.Add(NameBox);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox NameBox;
        private RichTextBox OutputTextBox;
        private TextBox InputTextBox;
        private Button SendButton;
        private Button ConnectButton;
    }
}