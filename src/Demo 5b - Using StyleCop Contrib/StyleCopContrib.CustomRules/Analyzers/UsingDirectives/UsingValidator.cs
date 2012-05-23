using System;
using System.Collections.Generic;
using System.Linq;

using StyleCop;
using StyleCop.CSharp;

namespace StyleCopContrib.CustomRules.Analyzers.UsingDirectives
{
    /// <summary>
    /// Using rules validator.
    /// </summary>
    internal sealed class UsingValidator
    {
        #region Fields

        private readonly StyleCopAddIn sourceAnalyzer;
        private readonly UsingSettings usingSettings;
        private readonly List<UsingDirective> usingDirectives;

        #endregion

        #region Constructor

        internal UsingValidator(StyleCopAddIn sourceAnalyzer, UsingSettings usingSettings, IEnumerable<UsingDirective> usingDirectives)
        {
            this.sourceAnalyzer = sourceAnalyzer;
            this.usingSettings = usingSettings;
            this.usingDirectives = usingDirectives.ToList();
        }

        #endregion

        #region Methods

        internal void Validate(CodeDocument document)
        {
            this.ValidateAtLeastOneUsingDirectiveMustBePresent(document);
            this.ValidateFirstUsingDirectiveMustBeSystem();

            IEnumerable<UsingDirectiveGroup> groups =
                this.usingSettings.GetUsingDirectiveByGroup(this.usingDirectives).ToList();

            this.ValidateUsingGroupOrderMustBeRespected(groups);
            this.ValidateUsingGroupMustBeSeparatedByABlankLine(groups);

            foreach (UsingDirectiveGroup group in groups)
            {
                if (!this.usingSettings.AliasShouldBeLast)
                {
                    this.ValidateUsingDirectiveMustBeSortedAlphabeticallyInsideAGroup(group);
                }
                else
                {
                    this.ValidateAliasUsingDirectiveMustBeLastInsideAGroup(group);

                    this.ValidateUsingDirectiveMustBeSortedAlphabeticallyInsideAGroup(UsingValidator.GetGroupWithoutAlias(group));

                    this.ValidateAliasMustBeSortedAlphabeticallyInsideAGroup(UsingValidator.GetGroupWithOnlyAlias(group));
                }
            }
        }

        private void ValidateAtLeastOneUsingDirectiveMustBePresent(CodeDocument document)
        {
            if (this.usingDirectives.Count() == 0)
            {
                DocumentRoot rootElement = ((CsDocument)document).RootElement;

                this.sourceAnalyzer.AddViolation(rootElement, rootElement.LineNumber,
                    ContribRule.FirstUsingDirectiveMustBeSystem);
            }
        }

        private void ValidateFirstUsingDirectiveMustBeSystem()
        {
            UsingDirective firstUsingDirective = this.usingDirectives.FirstOrDefault();

            if ((firstUsingDirective != null) && (firstUsingDirective.NamespaceType != "System"))
            {
                this.sourceAnalyzer.AddViolation(firstUsingDirective, firstUsingDirective.LineNumber,
                    ContribRule.FirstUsingDirectiveMustBeSystem);
            }
        }

        private void ValidateUsingGroupOrderMustBeRespected(IEnumerable<UsingDirectiveGroup> usingGroups)
        {
            usingGroups.ForEachPair((firstGroup, secondGroup) =>
            {
                if (firstGroup.Index > secondGroup.Index)
                {
                    this.sourceAnalyzer.AddViolation(secondGroup.First(), secondGroup.First().LineNumber,
                        ContribRule.UsingDirectiveGroupMustFollowGivenOrder);
                }
            });
        }

        private void ValidateUsingGroupMustBeSeparatedByABlankLine(IEnumerable<UsingDirectiveGroup> usingGroups)
        {
            usingGroups.ForEachPair((firstGroup, secondGroup) =>
            {
                if (secondGroup.FirstLineNumber - firstGroup.LastLineNumber <= 1)
                {
                    this.sourceAnalyzer.AddViolation(secondGroup.First(), secondGroup.FirstLineNumber,
                        ContribRule.UsingDirectiveGroupMustBeSeparatedByBlankLine);
                }
            });
        }

        private void ValidateUsingDirectiveMustBeSortedAlphabeticallyInsideAGroup(IEnumerable<UsingDirective> usingDirectives)
        {
            usingDirectives.ForEachPair((firstUsing, secondUsing) =>
            {
                if (string.CompareOrdinal(firstUsing.NamespaceType, secondUsing.NamespaceType) > -1)
                {
                    this.sourceAnalyzer.AddViolation(secondUsing, secondUsing.LineNumber,
                        ContribRule.UsingDirectiveMustBeSortedAlphabeticallyByGroup);
                }
            });
        }

        private void ValidateAliasMustBeSortedAlphabeticallyInsideAGroup(IEnumerable<UsingDirective> usingDirectives)
        {
            usingDirectives.ForEachPair((firstUsing, secondUsing) =>
            {
                if (string.CompareOrdinal(firstUsing.Alias, secondUsing.Alias) > -1)
                {
                    this.sourceAnalyzer.AddViolation(secondUsing, secondUsing.LineNumber,
                        ContribRule.UsingDirectiveMustBeSortedAlphabeticallyByGroup);
                }
            });
        }

        private void ValidateAliasUsingDirectiveMustBeLastInsideAGroup(IEnumerable<UsingDirective> usingDirectives)
        {
            usingDirectives.ForEachPair((firstUsing, secondUsing) =>
            {
                if (!string.IsNullOrEmpty(firstUsing.Alias)
                    && string.IsNullOrEmpty(secondUsing.Alias))
                {
                    this.sourceAnalyzer.AddViolation(secondUsing, secondUsing.LineNumber,
                        ContribRule.UsingDirectiveMustBeSortedAlphabeticallyByGroup);
                }
            });
        }

        private static IEnumerable<UsingDirective> GetGroupWithoutAlias(IEnumerable<UsingDirective> usingDirectives)
        {
            return usingDirectives.Where(usingDirective => string.IsNullOrEmpty(usingDirective.Alias));
        }

        private static IEnumerable<UsingDirective> GetGroupWithOnlyAlias(IEnumerable<UsingDirective> usingDirectives)
        {
            return usingDirectives.Where(usingDirective => !string.IsNullOrEmpty(usingDirective.Alias));
        }

        #endregion
    }
}