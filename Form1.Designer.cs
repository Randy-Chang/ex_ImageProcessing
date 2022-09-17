namespace ex_ImageProcessing
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbImageFilePath = new System.Windows.Forms.TextBox();
            this.btnLoadImageFilePath = new System.Windows.Forms.Button();
            this.pbFig1 = new System.Windows.Forms.PictureBox();
            this.pbFig2 = new System.Windows.Forms.PictureBox();
            this.pbFig4 = new System.Windows.Forms.PictureBox();
            this.pbFig5 = new System.Windows.Forms.PictureBox();
            this.pbFig3 = new System.Windows.Forms.PictureBox();
            this.pbFig6 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig6)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image File Path";
            // 
            // tbImageFilePath
            // 
            this.tbImageFilePath.Location = new System.Drawing.Point(169, 12);
            this.tbImageFilePath.Name = "tbImageFilePath";
            this.tbImageFilePath.Size = new System.Drawing.Size(397, 33);
            this.tbImageFilePath.TabIndex = 1;
            // 
            // btnLoadImageFilePath
            // 
            this.btnLoadImageFilePath.Location = new System.Drawing.Point(583, 12);
            this.btnLoadImageFilePath.Name = "btnLoadImageFilePath";
            this.btnLoadImageFilePath.Size = new System.Drawing.Size(38, 33);
            this.btnLoadImageFilePath.TabIndex = 2;
            this.btnLoadImageFilePath.Text = "...";
            this.btnLoadImageFilePath.UseVisualStyleBackColor = true;
            // 
            // pbFig1
            // 
            this.pbFig1.Location = new System.Drawing.Point(8, 58);
            this.pbFig1.Name = "pbFig1";
            this.pbFig1.Size = new System.Drawing.Size(400, 300);
            this.pbFig1.TabIndex = 3;
            this.pbFig1.TabStop = false;
            // 
            // pbFig2
            // 
            this.pbFig2.Location = new System.Drawing.Point(414, 58);
            this.pbFig2.Name = "pbFig2";
            this.pbFig2.Size = new System.Drawing.Size(400, 300);
            this.pbFig2.TabIndex = 4;
            this.pbFig2.TabStop = false;
            // 
            // pbFig4
            // 
            this.pbFig4.Location = new System.Drawing.Point(8, 364);
            this.pbFig4.Name = "pbFig4";
            this.pbFig4.Size = new System.Drawing.Size(400, 300);
            this.pbFig4.TabIndex = 5;
            this.pbFig4.TabStop = false;
            // 
            // pbFig5
            // 
            this.pbFig5.Location = new System.Drawing.Point(414, 364);
            this.pbFig5.Name = "pbFig5";
            this.pbFig5.Size = new System.Drawing.Size(400, 300);
            this.pbFig5.TabIndex = 6;
            this.pbFig5.TabStop = false;
            // 
            // pbFig3
            // 
            this.pbFig3.Location = new System.Drawing.Point(820, 58);
            this.pbFig3.Name = "pbFig3";
            this.pbFig3.Size = new System.Drawing.Size(400, 300);
            this.pbFig3.TabIndex = 7;
            this.pbFig3.TabStop = false;
            // 
            // pbFig6
            // 
            this.pbFig6.Location = new System.Drawing.Point(820, 364);
            this.pbFig6.Name = "pbFig6";
            this.pbFig6.Size = new System.Drawing.Size(400, 300);
            this.pbFig6.TabIndex = 8;
            this.pbFig6.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 761);
            this.Controls.Add(this.pbFig6);
            this.Controls.Add(this.pbFig3);
            this.Controls.Add(this.pbFig5);
            this.Controls.Add(this.pbFig4);
            this.Controls.Add(this.pbFig2);
            this.Controls.Add(this.pbFig1);
            this.Controls.Add(this.btnLoadImageFilePath);
            this.Controls.Add(this.tbImageFilePath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbFig1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFig6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox tbImageFilePath;
        private Button btnLoadImageFilePath;
        private PictureBox pbFig1;
        private PictureBox pbFig2;
        private PictureBox pbFig4;
        private PictureBox pbFig5;
        private PictureBox pbFig3;
        private PictureBox pbFig6;
    }
}