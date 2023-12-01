using OCC02_Customs.Customs;

namespace OCC02_Customs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            new HintTextBox()
            {
                Parent = this,
                Visible = true,
                HintText = "Name",
                Location = new Point(5, 5),
                Size = new Size(500, 100),
                Font = new Font(Font.Name, 20, FontStyle.Regular),
            };

            new TextBox()
            {
                Parent = this,
                Visible = true,
                Location = new Point(5, 110),
                Size = new Size(500, 100),
                Multiline = true,
            };

            new UploadBox()
            {
                Parent = this,
                Visible = true,
                Location = new Point(510, 100),
                Size = new Size(200, 200),
            };
        }
    }
}