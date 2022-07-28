namespace PentagonHMI
{
    partial class CvXView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CvXView));
            this.axCVX1 = new AxCVXLib.AxCVX();
            ((System.ComponentModel.ISupportInitialize)(this.axCVX1)).BeginInit();
            this.SuspendLayout();
            // 
            // axCVX1
            // 
            this.axCVX1.Enabled = true;
            this.axCVX1.Location = new System.Drawing.Point(0, 0);
            this.axCVX1.Name = "axCVX1";
            this.axCVX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVX1.OcxState")));
            this.axCVX1.Size = new System.Drawing.Size(800, 550); // !! 949, 712
            this.axCVX1.TabIndex = 0;
            // 
            // CvXView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axCVX1);
            this.Name = "CvXView";
            ((System.ComponentModel.ISupportInitialize)(this.axCVX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxCVXLib.AxCVX axCVX1;
    }
}
