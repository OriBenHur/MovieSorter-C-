namespace MovieSorter
{
    partial class MovieSorter
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
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Check_All = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Count = new System.Windows.Forms.Label();
            this.Move_button = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.Source_dir = new System.Windows.Forms.TextBox();
            this.Distension_dir = new System.Windows.Forms.TextBox();
            this.Browes_Source = new System.Windows.Forms.Button();
            this.Browes_Destination = new System.Windows.Forms.Button();
            this.Go_button = new System.Windows.Forms.Button();
            this.Source = new System.Windows.Forms.Label();
            this.Destination = new System.Windows.Forms.Label();
            this.Year = new System.Windows.Forms.Label();
            this.Year_TextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(12, 97);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(764, 433);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // Check_All
            // 
            this.Check_All.AutoSize = true;
            this.Check_All.Location = new System.Drawing.Point(18, 105);
            this.Check_All.Name = "Check_All";
            this.Check_All.Size = new System.Drawing.Size(15, 14);
            this.Check_All.TabIndex = 5;
            this.Check_All.UseVisualStyleBackColor = true;
            this.Check_All.CheckedChanged += new System.EventHandler(this.Check_All_CheckedChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 536);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(670, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // Count
            // 
            this.Count.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Count.AutoSize = true;
            this.Count.Location = new System.Drawing.Point(12, 566);
            this.Count.Name = "Count";
            this.Count.Size = new System.Drawing.Size(0, 13);
            this.Count.TabIndex = 8;
            // 
            // Move_button
            // 
            this.Move_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Move_button.Enabled = false;
            this.Move_button.Location = new System.Drawing.Point(701, 536);
            this.Move_button.Name = "Move_button";
            this.Move_button.Size = new System.Drawing.Size(75, 23);
            this.Move_button.TabIndex = 9;
            this.Move_button.Text = "Move";
            this.Move_button.UseVisualStyleBackColor = true;
            this.Move_button.Click += new System.EventHandler(this.Move_button_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Source_dir
            // 
            this.Source_dir.Location = new System.Drawing.Point(104, 12);
            this.Source_dir.Name = "Source_dir";
            this.Source_dir.Size = new System.Drawing.Size(300, 20);
            this.Source_dir.TabIndex = 0;
            this.Source_dir.TextChanged += new System.EventHandler(this.Source_dir_TextChanged);
            // 
            // Distension_dir
            // 
            this.Distension_dir.Location = new System.Drawing.Point(104, 38);
            this.Distension_dir.Name = "Distension_dir";
            this.Distension_dir.Size = new System.Drawing.Size(300, 20);
            this.Distension_dir.TabIndex = 1;
            this.Distension_dir.TextChanged += new System.EventHandler(this.Distension_dir_TextChanged);
            // 
            // Browes_Source
            // 
            this.Browes_Source.Location = new System.Drawing.Point(410, 11);
            this.Browes_Source.Name = "Browes_Source";
            this.Browes_Source.Size = new System.Drawing.Size(109, 23);
            this.Browes_Source.TabIndex = 2;
            this.Browes_Source.Text = "Browes Source";
            this.Browes_Source.UseVisualStyleBackColor = true;
            this.Browes_Source.Click += new System.EventHandler(this.Browes_Source_Click);
            // 
            // Browes_Destination
            // 
            this.Browes_Destination.Location = new System.Drawing.Point(410, 37);
            this.Browes_Destination.Name = "Browes_Destination";
            this.Browes_Destination.Size = new System.Drawing.Size(109, 23);
            this.Browes_Destination.TabIndex = 3;
            this.Browes_Destination.Text = "Browse Destination";
            this.Browes_Destination.UseVisualStyleBackColor = true;
            this.Browes_Destination.Click += new System.EventHandler(this.Browes_Destination_Click);
            // 
            // Go_button
            // 
            this.Go_button.Enabled = false;
            this.Go_button.Location = new System.Drawing.Point(525, 11);
            this.Go_button.Name = "Go_button";
            this.Go_button.Size = new System.Drawing.Size(75, 50);
            this.Go_button.TabIndex = 10;
            this.Go_button.TabStop = false;
            this.Go_button.Text = "Go!";
            this.Go_button.UseVisualStyleBackColor = true;
            this.Go_button.Click += new System.EventHandler(this.Go_button_Click);
            // 
            // Source
            // 
            this.Source.AutoSize = true;
            this.Source.Location = new System.Drawing.Point(12, 19);
            this.Source.Name = "Source";
            this.Source.Size = new System.Drawing.Size(44, 13);
            this.Source.TabIndex = 11;
            this.Source.Text = "Source:";
            // 
            // Destination
            // 
            this.Destination.AutoSize = true;
            this.Destination.Location = new System.Drawing.Point(12, 43);
            this.Destination.Name = "Destination";
            this.Destination.Size = new System.Drawing.Size(63, 13);
            this.Destination.TabIndex = 12;
            this.Destination.Text = "Destination:";
            // 
            // Year
            // 
            this.Year.AutoSize = true;
            this.Year.Location = new System.Drawing.Point(12, 67);
            this.Year.Name = "Year";
            this.Year.Size = new System.Drawing.Size(88, 13);
            this.Year.TabIndex = 14;
            this.Year.Text = "Year To Filter By:";
            // 
            // Year_TextBox
            // 
            this.Year_TextBox.Location = new System.Drawing.Point(104, 64);
            this.Year_TextBox.MaxLength = 4;
            this.Year_TextBox.Name = "Year_TextBox";
            this.Year_TextBox.Size = new System.Drawing.Size(35, 20);
            this.Year_TextBox.TabIndex = 13;
            this.Year_TextBox.TextChanged += new System.EventHandler(this.Year_TextBox_TextChanged);
            // 
            // MovieSorter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 585);
            this.Controls.Add(this.Year);
            this.Controls.Add(this.Year_TextBox);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.Source);
            this.Controls.Add(this.Go_button);
            this.Controls.Add(this.Move_button);
            this.Controls.Add(this.Count);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Check_All);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Browes_Destination);
            this.Controls.Add(this.Browes_Source);
            this.Controls.Add(this.Distension_dir);
            this.Controls.Add(this.Source_dir);
            this.Name = "MovieSorter";
            this.Text = "MovieSorter";
            this.Load += new System.EventHandler(this.MovieSorter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.CheckBox Check_All;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label Count;
        private System.Windows.Forms.Button Move_button;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button Go_button;
        private System.Windows.Forms.Button Browes_Destination;
        private System.Windows.Forms.Button Browes_Source;
        private System.Windows.Forms.TextBox Distension_dir;
        private System.Windows.Forms.TextBox Source_dir;
        private System.Windows.Forms.Label Year;
        private System.Windows.Forms.TextBox Year_TextBox;
        private System.Windows.Forms.Label Destination;
        private System.Windows.Forms.Label Source;
    }
}

