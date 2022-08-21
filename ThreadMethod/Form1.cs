namespace ThreadMethod
{
    public partial class Form1 : Form
    {
        ThreadMethodHelper threadMethodHelper;
        public Form1()
        {
            InitializeComponent();
        }

        delegate void SetTextCallback(string text,TextBox textBox);
        delegate string GetTextCallback(TextBox textBox);

        public string GetText(TextBox textBox)
        {
            if (textBox.InvokeRequired)
            {
                GetTextCallback d = new GetTextCallback(GetText);
               return (string)this.Invoke(d, new object[] {  textBox });
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
                this.Invoke(d, new object[] { text, textBox});
            }
            else
            {
                textBox.Text = text;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(Test));
            th.Start();
        }

        void Test()
        {
            //this.textBox1.Text = "test";
            //SetText("test2" ,textBox1);
            threadMethodHelper.SetText("test2", textBox1);
            Thread.Sleep(1000);
            //SetText(GetText(textBox1),textBox2);
            threadMethodHelper.SetText(threadMethodHelper.GetText( textBox1), textBox2);
            
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
             threadMethodHelper = new ThreadMethodHelper(this);
        }
    }
}