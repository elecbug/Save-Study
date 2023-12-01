using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCC02_Customs.Customs
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
            };
        }
    }
}
