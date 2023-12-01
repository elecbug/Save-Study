namespace OCC02_Customs.Customs
{
    public class AddForm : Form
    {
        public Pair[] Pairs { get; set; }

        public AddForm(params Pair[] pairs)
        {
            Pairs = new Pair[pairs.Length];
            ClientSize = new Size(310, 10 + 30 * pairs.Length);

            for (int i = 0; i < pairs.Length; i++)
            {
                new Label()
                {
                    Parent = this,
                    Visible = true,
                    Location = new Point(5, 5 + i * 30),
                    Text = pairs[i].LabelText,
                    Size = new Size(100, 30),
                    TextAlign = ContentAlignment.TopRight,
                };

                Pairs[i] = pairs[i];

                pairs[i].Control.Parent = this;
                pairs[i].Control.Visible = true;
                pairs[i].Control.Location = new Point(105, 5 + i * 30);
                pairs[i].Control.Size = new Size(200, 30);
            }
        }

    }

    public class Pair
    {
        public string LabelText { get; set; }
        public Control Control { get; set; }

        public Pair(string name, Control control)
        {
            LabelText = name;
            Control = control;
        }
    }
}

/*
 * using e.g.
 * 
            CustomAddForm form = new CustomAddForm(new Pair[]
            {
                new Pair("Name", new TextBox()),
                new Pair("Address", new TextBox()),
                new Pair("Birthday", new DateTimePicker()),
                new Pair("ID", new TextBox()),
                new Pair("Ok", new CheckBox()),
            });

            form.ShowDialog();

            string name = form.Pairs.Where(x => x.LabelText == "Name").First().Control.Text;

            MessageBox.Show(name);
 *
 */