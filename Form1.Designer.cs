namespace Pouzdanost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Graf = new System.Windows.Forms.Panel();
            this.UnFocus = new System.Windows.Forms.Label();
            this.Back = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.otvoriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spremiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otvoriKonzoluToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Dodaj = new System.Windows.Forms.Button();
            this.Spremi = new System.Windows.Forms.SaveFileDialog();
            this.Otvori = new System.Windows.Forms.OpenFileDialog();
            this.noviToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Graf.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Graf
            // 
            this.Graf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Graf.BackColor = System.Drawing.Color.Wheat;
            this.Graf.Controls.Add(this.UnFocus);
            this.Graf.Location = new System.Drawing.Point(29, 102);
            this.Graf.Name = "Graf";
            this.Graf.Size = new System.Drawing.Size(819, 318);
            this.Graf.TabIndex = 0;
            this.Graf.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // UnFocus
            // 
            this.UnFocus.Location = new System.Drawing.Point(0, 0);
            this.UnFocus.Name = "UnFocus";
            this.UnFocus.Size = new System.Drawing.Size(0, 0);
            this.UnFocus.TabIndex = 0;
            // 
            // Back
            // 
            this.Back.Location = new System.Drawing.Point(29, 73);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(75, 23);
            this.Back.TabIndex = 1;
            this.Back.Text = "<-";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Next
            // 
            this.Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Next.Location = new System.Drawing.Point(773, 73);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 23);
            this.Next.TabIndex = 2;
            this.Next.Text = "->";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(869, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noviToolStripMenuItem,
            this.otvoriToolStripMenuItem,
            this.spremiToolStripMenuItem,
            this.otvoriKonzoluToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(84, 24);
            this.toolStripDropDownButton1.Text = "Datoteke";
            // 
            // otvoriToolStripMenuItem
            // 
            this.otvoriToolStripMenuItem.Name = "otvoriToolStripMenuItem";
            this.otvoriToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.otvoriToolStripMenuItem.Text = "Otvori";
            this.otvoriToolStripMenuItem.Click += new System.EventHandler(this.otvoriToolStripMenuItem_Click);
            // 
            // spremiToolStripMenuItem
            // 
            this.spremiToolStripMenuItem.Name = "spremiToolStripMenuItem";
            this.spremiToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.spremiToolStripMenuItem.Text = "Spremi";
            this.spremiToolStripMenuItem.Click += new System.EventHandler(this.spremiToolStripMenuItem_Click);
            // 
            // otvoriKonzoluToolStripMenuItem
            // 
            this.otvoriKonzoluToolStripMenuItem.Name = "otvoriKonzoluToolStripMenuItem";
            this.otvoriKonzoluToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.otvoriKonzoluToolStripMenuItem.Text = "Otvori konzolu";
            this.otvoriKonzoluToolStripMenuItem.Click += new System.EventHandler(this.otvoriKonzoluToolStripMenuItem_Click);
            // 
            // Dodaj
            // 
            this.Dodaj.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Dodaj.Location = new System.Drawing.Point(409, 73);
            this.Dodaj.Name = "Dodaj";
            this.Dodaj.Size = new System.Drawing.Size(26, 25);
            this.Dodaj.TabIndex = 4;
            this.Dodaj.Text = "+";
            this.Dodaj.UseVisualStyleBackColor = true;
            this.Dodaj.Click += new System.EventHandler(this.OnClickDodaj);
            // 
            // Spremi
            // 
            this.Spremi.DefaultExt = "dijagnostika";
            this.Spremi.Title = "Spremi";
            // 
            // Otvori
            // 
            this.Otvori.FileName = "Otvori";
            this.Otvori.Filter = "Dijagnostika|*.dijagnostika";
            // 
            // noviToolStripMenuItem
            // 
            this.noviToolStripMenuItem.Name = "noviToolStripMenuItem";
            this.noviToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.noviToolStripMenuItem.Text = "Novo";
            this.noviToolStripMenuItem.Click += new System.EventHandler(this.noviToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 450);
            this.Controls.Add(this.Dodaj);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Graf);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Izracunavanje pouzdanosti mješovito spojenog sustava s mogučnošću računanja korak" +
    " po korak radi lakšeg razumjevanja računice mješovitog spoja";
            this.Graf.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Graf;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem otvoriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spremiToolStripMenuItem;
        private System.Windows.Forms.Button Dodaj;
        private System.Windows.Forms.Label UnFocus;
        private System.Windows.Forms.SaveFileDialog Spremi;
        private System.Windows.Forms.OpenFileDialog Otvori;
        private System.Windows.Forms.ToolStripMenuItem otvoriKonzoluToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noviToolStripMenuItem;
    }
}

