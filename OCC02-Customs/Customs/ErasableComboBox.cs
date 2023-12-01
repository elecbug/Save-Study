namespace OCC02_Customs.Customs
{
    public class ErasableComboBox : Panel
    {
        public ComboBox ComboBox { get; private set; }
        public Label EraseButton { get; private set; }
        public new string Text { get => ComboBox.Text; set => ComboBox.Text = value; }
        public List<byte[]> Data { get; private set; }

        public ErasableComboBox()
        {
            ComboBox = new ComboBox()
            {
                Parent = this,
                Visible = true,
                Location = new Point(0, 0),
            };

            EraseButton = new Label()
            {
                Parent = this,
                Visible = true,
                Text = "X",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.DarkRed,
            };

            Data = new List<byte[]>();

            EraseButton.Click += EraseButtonClick;
            SizeChanged += ErasableComboBoxSizeChanged;
        }

        private void ErasableComboBoxSizeChanged(object? sender, EventArgs e)
        {
            ComboBox.Size = new Size(Width - Height, Height);

            EraseButton.Location = new Point(Width - Height, 0);
            EraseButton.Size = new Size(Height, Height);
            EraseButton.Font = new Font(Font.Name, Height / 3, FontStyle.Bold);
        }

        private void EraseButtonClick(object? sender, EventArgs e)
        {
            if (ComboBox.SelectedIndex != -1)
            {
                Data.RemoveAt(ComboBox.SelectedIndex);

                ComboBox.Items.RemoveAt(ComboBox.SelectedIndex);
                ComboBox.SelectedIndex = -1;
                ComboBox.Text = "";
            }
        }
    }
}