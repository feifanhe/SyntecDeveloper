namespace Syntec_Developer.Forms
{
	partial class DCProperties
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cboItems = new System.Windows.Forms.ComboBox();
			this.prgItemProperties = new Azuria.Common.Controls.FilteredPropertyGrid();
			this.SuspendLayout();
			// 
			// cboItems
			// 
			this.cboItems.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboItems.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cboItems.FormattingEnabled = true;
			this.cboItems.Location = new System.Drawing.Point( 0, 0 );
			this.cboItems.Name = "cboItems";
			this.cboItems.Size = new System.Drawing.Size( 284, 20 );
			this.cboItems.TabIndex = 0;
			this.cboItems.SelectedIndexChanged += new System.EventHandler( this.cboItems_SelectedIndexChanged );
			// 
			// prgItemProperties
			// 
			this.prgItemProperties.CommandsActiveLinkColor = System.Drawing.Color.PapayaWhip;
			this.prgItemProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.prgItemProperties.Location = new System.Drawing.Point( 0, 20 );
			this.prgItemProperties.Name = "prgItemProperties";
			this.prgItemProperties.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.prgItemProperties.Size = new System.Drawing.Size( 284, 242 );
			this.prgItemProperties.TabIndex = 1;
			this.prgItemProperties.ToolbarVisible = false;
			this.prgItemProperties.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler( this.prgItemProperties_PropertyValueChanged );
			// 
			// DCProperties
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 12F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 284, 262 );
			this.Controls.Add( this.prgItemProperties );
			this.Controls.Add( this.cboItems );
			this.Font = new System.Drawing.Font( "PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 136 ) ) );
			this.Name = "DCProperties";
			this.Text = "ÄÝ©Ê";
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.ComboBox cboItems;
		private Azuria.Common.Controls.FilteredPropertyGrid prgItemProperties;
	}
}