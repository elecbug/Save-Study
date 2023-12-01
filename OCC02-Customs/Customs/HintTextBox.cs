﻿namespace OCC02_Customs.Customs
{
    public class HintTextBox : RichTextBox
    {
        private Label HintLabel { get; set; }
        public string HintText { get => HintLabel.Text; set => HintLabel.Text = value; }

        public HintTextBox()
        {
            HintLabel = new Label()
            {
                Parent = this,
                Visible = true,
                Enabled = false,
                Location = new Point(0, 0),
                Dock = DockStyle.Fill,
            };

            HintLabel.Click += HintLabelClick;
            GotFocus += HintTextBoxGotFocus;
            LostFocus += HintTextBoxLostFocus;
        }

        private void HintTextBoxLostFocus(object? sender, EventArgs e)
        {
            if (Text == "")
            {
                HintLabel.Visible = true;
            }
        }

        private void HintTextBoxGotFocus(object? sender, EventArgs e)
        {
            HintLabel.Visible = false;
        }

        private void HintLabelClick(object? sender, EventArgs e)
        {
            Parent!.Focus();
        }
    }
}
