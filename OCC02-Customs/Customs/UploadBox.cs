using System.Diagnostics;

namespace OCC02_Customs.Customs
{
    public class UploadBox : Panel
    {
        private class ErasableComboBox : Panel
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

        private ErasableComboBox FileComboBox { get; set; }
        private Label PlusLabel { get; set; }

        public ComboBox.ObjectCollection Items { get => FileComboBox.ComboBox.Items; }
        public List<byte[]> Data { get => FileComboBox.Data; }

        public UploadBox()
        {
            BorderStyle = BorderStyle.FixedSingle;
            AllowDrop = true;

            FileComboBox = new ErasableComboBox()
            {
                Parent = this,
                Visible = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Size = new Size(Width, Height / 5),
                Location = new Point(0, Height * 4 / 5),
                Text = "Files",
            };

            PlusLabel = new Label()
            {
                Parent = this,
                Visible = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Enabled = false,
                Text = "+",
            };

            DragEnter += UploadBoxDragEnter;
            DragDrop += UploadBoxDragDrop;
            PlusLabel.SizeChanged += UploadBoxSizeChanged;
        }

        private void UploadBoxDragDrop(object? sender, DragEventArgs e)
        {
            Debug.WriteLine("Drag Drop");

            string[] pathes = (string[])e.Data!.GetData(DataFormats.FileDrop)!;

            foreach (string path in pathes)
            {
                using BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));

                FileComboBox.Data.Add(reader.ReadBytes((int)reader.BaseStream.Length));
                FileComboBox.ComboBox.Items.Add(path.Split('\\').Last());
            }
        }

        private void UploadBoxDragEnter(object? sender, DragEventArgs e)
        {
            Debug.WriteLine("Drag Enter");

            if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void UploadBoxSizeChanged(object? sender, EventArgs e)
        {
            PlusLabel.Font = new Font(Font.Name, Height / 5, FontStyle.Bold);
        }
    }
}
