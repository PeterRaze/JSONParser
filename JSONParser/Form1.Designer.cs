namespace JSONParser
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            jsonInput = new TextBox();
            checkBtn = new Button();
            conclusionLabel = new Label();
            SuspendLayout();
            // 
            // jsonInput
            // 
            jsonInput.BorderStyle = BorderStyle.FixedSingle;
            jsonInput.Location = new Point(12, 12);
            jsonInput.Multiline = true;
            jsonInput.Name = "jsonInput";
            jsonInput.ScrollBars = ScrollBars.Vertical;
            jsonInput.Size = new Size(558, 359);
            jsonInput.TabIndex = 0;
            // 
            // checkBtn
            // 
            checkBtn.Location = new Point(12, 374);
            checkBtn.Name = "checkBtn";
            checkBtn.Size = new Size(105, 67);
            checkBtn.TabIndex = 1;
            checkBtn.Text = "Check";
            checkBtn.UseVisualStyleBackColor = true;
            checkBtn.Click += CheckBtn_Click;
            // 
            // conclusionLabel
            // 
            conclusionLabel.BorderStyle = BorderStyle.FixedSingle;
            conclusionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            conclusionLabel.Location = new Point(123, 374);
            conclusionLabel.Name = "conclusionLabel";
            conclusionLabel.Padding = new Padding(9, 0, 0, 0);
            conclusionLabel.Size = new Size(426, 70);
            conclusionLabel.TabIndex = 2;
            conclusionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(582, 453);
            Controls.Add(conclusionLabel);
            Controls.Add(checkBtn);
            Controls.Add(jsonInput);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form";
            Text = "JSON Parser";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox jsonInput;
        private Button checkBtn;
        private Label conclusionLabel;
    }
}