using System;
using System.Collections.Generic;

using StyleCop;
using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers.UsingDirectives
{
    /// <summary>
    /// Settings reader class for using rules.
    /// </summary>
    internal sealed class UsingSettings
    {
        #region Fields

        private const char GroupSeparator = ';';
        private const char UsingSeparator = ',';

        private readonly SettingsManager settingsManager;

        #endregion

        #region Constructor

        internal UsingSettings(StyleCopAddIn sourceAnalyzer, Settings settings)
        {
            this.settingsManager = ServiceLocator.GetService<SettingsManager>();
            this.AliasShouldBeLast = this.settingsManager.GetSetting<bool>("AliasShouldBeLast", sourceAnalyzer, settings);

            string usingDirectiveGroups = this.settingsManager.GetSetting<string>("UsingDirectiveGroups", sourceAnalyzer, settings);

            this.GroupPrefixes = new List<IList<string>>();

            foreach (string groupValue in usingDirectiveGroups.Split(UsingSettings.GroupSeparator))
            {
                this.GroupPrefixes.Add(groupValue.Split(UsingSettings.UsingSeparator));
            }

            foreach (IList<string> prefixList in this.GroupPrefixes)
            {
                foreach (string prefix in prefixList)
                {
                    if (prefix == null) throw new InvalidOperationException("using group cannot be empty");
                    if (string.IsNullOrEmpty(prefix.Trim())) throw new InvalidOperationException("using group cannot be empty");
                }
            }
        }

        #endregion

        #region Properties

        internal static string UsingWildcard
        {
            get
            {
                return "*";
            }
        }

        internal IList<IList<string>> GroupPrefixes { get; private set; }

        internal bool AliasShouldBeLast { get; private set; }

        #endregion

        #region Methods

        internal IEnumerable<UsingDirectiveGroup> GetUsingDirectiveByGroup(IEnumerable<UsingDirective> usingDirectives)
        {
            UsingDirectiveGroup usingDirectiveGroup = null;

            foreach (UsingDirective usingDirective in usingDirectives)
            {
                int usingGroupIndex = this.GetUsingGroupIndex(usingDirective);

                if (usingDirectiveGroup == null)
                {
                    usingDirectiveGroup = new UsingDirectiveGroup(usingGroupIndex);
                }

                if (usingDirectiveGroup.Index != usingGroupIndex)
                {
                    yield return usingDirectiveGroup;
                    usingDirectiveGroup = new UsingDirectiveGroup(usingGroupIndex);
                }

                usingDirectiveGroup.Add(usingDirective);
            }

            if (usingDirectiveGroup != null)
            {
                yield return usingDirectiveGroup;
            }
        }

        private int GetUsingGroupIndex(UsingDirective usingDirective)
        {
            int result = -1;
            int index = 0;
            int wildcardGroup = -1;

            foreach (IList<string> groupPrefix in this.GroupPrefixes)
            {
                foreach (string prefix in groupPrefix)
                {
                    if (prefix == UsingSettings.UsingWildcard) wildcardGroup = index;

                    if (usingDirective.NamespaceType.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        result = index;
                    }
                }

                index++;
            }

            if ((result == -1) && (wildcardGroup != -1)) result = wildcardGroup;

            return result;
        }

        #endregion
    }
}