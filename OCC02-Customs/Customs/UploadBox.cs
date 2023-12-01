using System.Diagnostics;

namespace OCC02_Customs.Customs
{
    public class UploadBox : Panel
    {
        public ErasableComboBox FileComboBox { get; private set; }
        private Label PlusLabel { get; set; }

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
