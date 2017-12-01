namespace GeneticTSPWinForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tourDiagram = new System.Windows.Forms.PictureBox();
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.loadInstanceButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.drawnTourLengthLabel = new System.Windows.Forms.Label();
            this.tourCityListTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.populationSizeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iterationsLabel = new System.Windows.Forms.Label();
            this.iterationsTextBox = new System.Windows.Forms.TextBox();
            this.iterationLabel = new System.Windows.Forms.Label();
            this.mutationLabel = new System.Windows.Forms.Label();
            this.mutationTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mutationSizeTextBox = new System.Windows.Forms.TextBox();
            this.labelLastSolution = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tourDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // tourDiagram
            // 
            this.tourDiagram.BackColor = System.Drawing.Color.White;
            this.tourDiagram.Location = new System.Drawing.Point(16, 15);
            this.tourDiagram.Margin = new System.Windows.Forms.Padding(4);
            this.tourDiagram.Name = "tourDiagram";
            this.tourDiagram.Size = new System.Drawing.Size(667, 615);
            this.tourDiagram.TabIndex = 0;
            this.tourDiagram.TabStop = false;
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Location = new System.Drawing.Point(694, 534);
            this.filenameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.Size = new System.Drawing.Size(132, 22);
            this.filenameTextBox.TabIndex = 1;
            this.filenameTextBox.Text = "tsp.txt";
            // 
            // filenameLabel
            // 
            this.filenameLabel.AutoSize = true;
            this.filenameLabel.Location = new System.Drawing.Point(695, 513);
            this.filenameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(65, 17);
            this.filenameLabel.TabIndex = 2;
            this.filenameLabel.Text = "Filename";
            // 
            // loadInstanceButton
            // 
            this.loadInstanceButton.Location = new System.Drawing.Point(695, 566);
            this.loadInstanceButton.Margin = new System.Windows.Forms.Padding(4);
            this.loadInstanceButton.Name = "loadInstanceButton";
            this.loadInstanceButton.Size = new System.Drawing.Size(133, 28);
            this.loadInstanceButton.TabIndex = 3;
            this.loadInstanceButton.Text = "Load instance";
            this.loadInstanceButton.UseVisualStyleBackColor = true;
            this.loadInstanceButton.Click += new System.EventHandler(this.loadInstanceButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(695, 602);
            this.startButton.Margin = new System.Windows.Forms.Padding(4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(133, 28);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // drawnTourLengthLabel
            // 
            this.drawnTourLengthLabel.AutoSize = true;
            this.drawnTourLengthLabel.Location = new System.Drawing.Point(691, 15);
            this.drawnTourLengthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.drawnTourLengthLabel.Name = "drawnTourLengthLabel";
            this.drawnTourLengthLabel.Size = new System.Drawing.Size(136, 17);
            this.drawnTourLengthLabel.TabIndex = 5;
            this.drawnTourLengthLabel.Text = "Drawn tour length: 0";
            // 
            // tourCityListTextBox
            // 
            this.tourCityListTextBox.Location = new System.Drawing.Point(899, 76);
            this.tourCityListTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.tourCityListTextBox.Multiline = true;
            this.tourCityListTextBox.Name = "tourCityListTextBox";
            this.tourCityListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tourCityListTextBox.Size = new System.Drawing.Size(132, 443);
            this.tourCityListTextBox.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(922, 527);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Show Tour";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // populationSizeTextBox
            // 
            this.populationSizeTextBox.Location = new System.Drawing.Point(694, 484);
            this.populationSizeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.populationSizeTextBox.Name = "populationSizeTextBox";
            this.populationSizeTextBox.Size = new System.Drawing.Size(132, 22);
            this.populationSizeTextBox.TabIndex = 8;
            this.populationSizeTextBox.Text = "10000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(695, 463);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Population size:";
            // 
            // iterationsLabel
            // 
            this.iterationsLabel.AutoSize = true;
            this.iterationsLabel.Location = new System.Drawing.Point(695, 415);
            this.iterationsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.iterationsLabel.Name = "iterationsLabel";
            this.iterationsLabel.Size = new System.Drawing.Size(140, 17);
            this.iterationsLabel.TabIndex = 10;
            this.iterationsLabel.Text = "Number of iterations:";
            // 
            // iterationsTextBox
            // 
            this.iterationsTextBox.Location = new System.Drawing.Point(694, 436);
            this.iterationsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.iterationsTextBox.Name = "iterationsTextBox";
            this.iterationsTextBox.Size = new System.Drawing.Size(132, 22);
            this.iterationsTextBox.TabIndex = 11;
            this.iterationsTextBox.Text = "10000";
            // 
            // iterationLabel
            // 
            this.iterationLabel.AutoSize = true;
            this.iterationLabel.Location = new System.Drawing.Point(691, 31);
            this.iterationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.iterationLabel.Name = "iterationLabel";
            this.iterationLabel.Size = new System.Drawing.Size(75, 17);
            this.iterationLabel.TabIndex = 12;
            this.iterationLabel.Text = "Iteration: 0";
            // 
            // mutationLabel
            // 
            this.mutationLabel.AutoSize = true;
            this.mutationLabel.Location = new System.Drawing.Point(695, 370);
            this.mutationLabel.Name = "mutationLabel";
            this.mutationLabel.Size = new System.Drawing.Size(138, 17);
            this.mutationLabel.TabIndex = 13;
            this.mutationLabel.Text = "Mutation chance (%)";
            this.mutationLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // mutationTextBox
            // 
            this.mutationTextBox.Location = new System.Drawing.Point(695, 390);
            this.mutationTextBox.Name = "mutationTextBox";
            this.mutationTextBox.Size = new System.Drawing.Size(131, 22);
            this.mutationTextBox.TabIndex = 14;
            this.mutationTextBox.Text = "10";
            this.mutationTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(695, 318);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Mutation max size";
            // 
            // mutationSizeTextBox
            // 
            this.mutationSizeTextBox.Location = new System.Drawing.Point(695, 338);
            this.mutationSizeTextBox.Name = "mutationSizeTextBox";
            this.mutationSizeTextBox.Size = new System.Drawing.Size(130, 22);
            this.mutationSizeTextBox.TabIndex = 16;
            this.mutationSizeTextBox.Text = "3";
            // 
            // labelLastSolution
            // 
            this.labelLastSolution.AutoSize = true;
            this.labelLastSolution.Location = new System.Drawing.Point(692, 48);
            this.labelLastSolution.Name = "labelLastSolution";
            this.labelLastSolution.Size = new System.Drawing.Size(163, 17);
            this.labelLastSolution.TabIndex = 17;
            this.labelLastSolution.Text = "Best solution found @: 0";
            this.labelLastSolution.Click += new System.EventHandler(this.label3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 656);
            this.Controls.Add(this.labelLastSolution);
            this.Controls.Add(this.mutationSizeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mutationTextBox);
            this.Controls.Add(this.mutationLabel);
            this.Controls.Add(this.iterationLabel);
            this.Controls.Add(this.iterationsTextBox);
            this.Controls.Add(this.iterationsLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.populationSizeTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tourCityListTextBox);
            this.Controls.Add(this.drawnTourLengthLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.loadInstanceButton);
            this.Controls.Add(this.filenameLabel);
            this.Controls.Add(this.filenameTextBox);
            this.Controls.Add(this.tourDiagram);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "TSP";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tourDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox tourDiagram;
        private System.Windows.Forms.TextBox filenameTextBox;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.Button loadInstanceButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label drawnTourLengthLabel;
        private System.Windows.Forms.TextBox tourCityListTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox populationSizeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label iterationsLabel;
        private System.Windows.Forms.TextBox iterationsTextBox;
        private System.Windows.Forms.Label iterationLabel;
        private System.Windows.Forms.Label mutationLabel;
        private System.Windows.Forms.TextBox mutationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mutationSizeTextBox;
        private System.Windows.Forms.Label labelLastSolution;
    }
}

