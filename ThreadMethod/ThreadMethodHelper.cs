using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadMethod
{
    public class ThreadMethodHelper
    {
        Form form;
        public ThreadMethodHelper(Form form)
        {
            this.form = form;
        }

        delegate void SetTextCallback(string text, TextBox textBox);
        delegate string GetTextCallback(TextBox textBox);

        public string GetText(TextBox textBox)
        {
            if (textBox.InvokeRequired)
            {
                GetTextCallback d = new GetTextCallback(GetText);
                return (string)form.Invoke(d, new object[] { textBox });
            }
            else
            {
                return textBox.Text;
            }
        }

        public void SetText(string text, TextBox textBox)
        {
            if (textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { text, textBox });
            }
            else
            {
                textBox.Text = text;
            }

        }
    }
}
