using System;
using System.Windows.Forms;

using StyleCop;

using StyleCopContrib.CustomRules.Analyzers;

namespace StyleCopContrib.CustomRules
{
    /// <summary>
    /// UserControl for custom analyzer rules configuration inside the StyleCop project settings window.
    /// </summary>
    public partial class UsingDirectiveGroupControl : UserControl, IPropertyControlPage
    {
        #region Fields

        private readonly CustomAnalyzer analyzer;
        private PropertyControl tabControl;
        private bool dirty;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UsingDirectiveGroupControl"/> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        public UsingDirectiveGroupControl(CustomAnalyzer analyzer)
        {
            this.InitializeComponent();

            this.analyzer = analyzer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UsingDirectiveGroupControl"/> is dirty.
        /// </summary>
        /// <value><c>true</c> if dirty; otherwise, <c>false</c>.</value>
        public bool Dirty
        {
            get
            {
                return this.dirty;
            }

            set
            {
                if (this.dirty != value)
                {
                    this.dirty = value;
                    this.tabControl.DirtyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the name of the tab.
        /// </summary>
        /// <value>The name of the tab.</value>
        public string TabName
        {
            get
            {
                return "UsingDirectiveGroup";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the specified property control with the StyleCop settings file.
        /// </summary>
        /// <param name="propertyControl">The property control.</param>
        public void Initialize(PropertyControl propertyControl)
        {
            this.tabControl = propertyControl;

            StringProperty customSettingProperty =
                (StringProperty)this.analyzer.GetSetting(this.tabControl.MergedSettings, "UsingDirectiveGroups");

            if (customSettingProperty != null)
            {
                this.UsingGroupPrefixesTextBox.Text = customSettingProperty.Value;
            }

            this.Dirty = false;
            this.tabControl.DirtyChanged();
        }

        /// <summary>
        /// Apply the modifications to the StyleCop settings file.
        /// </summary>
        /// <returns>True if the operation was succesfull.</returns>
        public bool Apply()
        {
            if (this.analyzer != null)
            {
                if (this.UsingGroupPrefixesTextBox.Text.Length == 0)
                {
                    this.analyzer.ClearSetting(this.tabControl.LocalSettings, "UsingDirectiveGroups");
                }
                else
                {
                    this.analyzer.SetSetting(this.tabControl.LocalSettings,
                        new StringProperty(this.analyzer, "UsingDirectiveGroups", this.UsingGroupPrefixesTextBox.Text));
                }
            }

            this.Dirty = false;
            this.tabControl.DirtyChanged();

            return true;
        }

        /// <summary>
        /// Pre apply the changes.
        /// </summary>
        /// <returns>True if the operation was succesfull.</returns>
        public bool PreApply()
        {
            return true;
        }

        /// <summary>
        /// Post apply the changes.
        /// </summary>
        /// <param name="wasDirty">if set to <c>true</c> [was dirty].</param>
        public void PostApply(bool wasDirty)
        {
        }

        /// <summary>
        /// Activates the control.
        /// </summary>
        /// <param name="activated">if set to <c>true</c> [activated].</param>
        public void Activate(bool activated)
        {
        }

        /// <summary>
        /// Refreshes the state of the settings override.
        /// </summary>
        public void RefreshSettingsOverrideState()
        {
        }

        private void UsingGroupPrefixesTextBoxTextChanged(object sender, EventArgs e)
        {
            this.Dirty = true;
            this.tabControl.DirtyChanged();
        }

        #endregion
    }
}
