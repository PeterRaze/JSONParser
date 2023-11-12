namespace JSONParser
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var tokens = Lexer.Lex(jsonInput.Text);
                
                if (tokens.Count == 0)
                {
                    ShowMessage("Insert a JSON", Color.Black);
                }
                else
                {
                    int finalIndex = Parser.Parse(tokens, 0);
                    if (finalIndex < tokens.Count)
                    {
                        ShowMessage($"Expecting EOF Got {tokens[finalIndex]}", Color.Red);
                    }
                    else
                    {
                        ShowMessage("JSON is valid!", Color.Green);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
            }
        }

        private void ShowMessage(string message, Color color)
        {
            conclusionLabel.ForeColor = color;
            conclusionLabel.Text = message;
        }

    }
}