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
            ((System.ComponentModel.ISupportInitialize)(this.tourDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // tourDiagram
            // 
            this.tourDiagram.BackColor = System.Drawing.Color.White;
            this.tourDiagram.Location = new System.Drawing.Point(12, 12);
            this.tourDiagram.Name = "tourDiagram";
            this.tourDiagram.Size = new System.Drawing.Size(500, 500);
            this.tourDiagram.TabIndex = 0;
            this.tourDiagram.TabStop = false;
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Location = new System.Drawing.Point(521, 434);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.Size = new System.Drawing.Size(100, 20);
            this.filenameTextBox.TabIndex = 1;
            this.filenameTextBox.Text = "D:\\tsp.txt";
            // 
            // filenameLabel
            // 
            this.filenameLabel.AutoSize = true;
            this.filenameLabel.Location = new System.Drawing.Point(518, 418);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(49, 13);
            this.filenameLabel.TabIndex = 2;
            this.filenameLabel.Text = "Filename";
            // 
            // loadInstanceButton
            // 
            this.loadInstanceButton.Location = new System.Drawing.Point(521, 460);
            this.loadInstanceButton.Name = "loadInstanceButton";
            this.loadInstanceButton.Size = new System.Drawing.Size(100, 23);
            this.loadInstanceButton.TabIndex = 3;
            this.loadInstanceButton.Text = "Load instance";
            this.loadInstanceButton.UseVisualStyleBackColor = true;
            this.loadInstanceButton.Click += new System.EventHandler(this.loadInstanceButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(521, 489);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // drawnTourLengthLabel
            // 
            this.drawnTourLengthLabel.AutoSize = true;
            this.drawnTourLengthLabel.Location = new System.Drawing.Point(518, 12);
            this.drawnTourLengthLabel.Name = "drawnTourLengthLabel";
            this.drawnTourLengthLabel.Size = new System.Drawing.Size(103, 13);
            this.drawnTourLengthLabel.TabIndex = 5;
            this.drawnTourLengthLabel.Text = "Drawn tour length: 0";
            // 
            // tourCityListTextBox
            // 
            this.tourCityListTextBox.Location = new System.Drawing.Point(521, 38);
            this.tourCityListTextBox.Multiline = true;
            this.tourCityListTextBox.Name = "tourCityListTextBox";
            this.tourCityListTextBox.Size = new System.Drawing.Size(100, 61);
            this.tourCityListTextBox.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(521, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // populationSizeTextBox
            // 
            this.populationSizeTextBox.Location = new System.Drawing.Point(521, 395);
            this.populationSizeTextBox.Name = "populationSizeTextBox";
            this.populationSizeTextBox.Size = new System.Drawing.Size(100, 20);
            this.populationSizeTextBox.TabIndex = 8;
            this.populationSizeTextBox.Text = "10000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(521, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Population size:";
            // 
            // iterationsLabel
            // 
            this.iterationsLabel.AutoSize = true;
            this.iterationsLabel.Location = new System.Drawing.Point(518, 337);
            this.iterationsLabel.Name = "iterationsLabel";
            this.iterationsLabel.Size = new System.Drawing.Size(104, 13);
            this.iterationsLabel.TabIndex = 10;
            this.iterationsLabel.Text = "Number of iterations:";
            // 
            // iterationsTextBox
            // 
            this.iterationsTextBox.Location = new System.Drawing.Point(521, 353);
            this.iterationsTextBox.Name = "iterationsTextBox";
            this.iterationsTextBox.Size = new System.Drawing.Size(100, 20);
            this.iterationsTextBox.TabIndex = 11;
            this.iterationsTextBox.Text = "10000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 533);
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
            this.Name = "Form1";
            this.Text = "TSP";
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
    }
}

