namespace PentagonHMI
{
    partial class AxXGXView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxXGXView));
            this.axXGX1 = new AxXGXLib.AxXGX();
            ((System.ComponentModel.ISupportInitialize)(this.axXGX1)).BeginInit();
            this.SuspendLayout();
            // 
            // axXGX1
            // 
            this.axXGX1.Enabled = true;
            this.axXGX1.Location = new System.Drawing.Point(0, 0);
            this.axXGX1.Margin = new System.Windows.Forms.Padding(0);
            this.axXGX1.Name = "axXGX1";
            this.axXGX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axXGX1.OcxState")));
            this.axXGX1.Size = new System.Drawing.Size(800, 600);
            //this.axXGX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axXGX1.TabIndex = 0;
            // 
            // AxXGXView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axXGX1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AxXGXView";
            this.Size = new System.Drawing.Size(800, 600);
            //this.Dock = System.Windows.Forms.DockStyle.Fill;
            ((System.ComponentModel.ISupportInitialize)(this.axXGX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxXGXLib.AxXGX axXGX1;
    }
}
