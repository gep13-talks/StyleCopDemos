namespace StyleCopContrib.CustomRules
{
	partial class UsingDirectiveGroupControl
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
			this.UsingGroupPrefixesTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// UsingGroupPrefixesTextBox
			// 
			this.UsingGroupPrefixesTextBox.Location = new System.Drawing.Point(3, 3);
			this.UsingGroupPrefixesTextBox.Name = "UsingGroupPrefixesTextBox";
			this.UsingGroupPrefixesTextBox.Size = new System.Drawing.Size(308, 20);
			this.UsingGroupPrefixesTextBox.TabIndex = 0;
			this.UsingGroupPrefixesTextBox.TextChanged += new System.EventHandler(this.UsingGroupPrefixesTextBoxTextChanged);
			// 
			// UsingDirectiveGroupControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.UsingGroupPrefixesTextBox);
			this.Name = "UsingDirectiveGroupControl";
			this.Size = new System.Drawing.Size(314, 27);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox UsingGroupPrefixesTextBox;
	}
}
