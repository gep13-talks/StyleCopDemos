using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using StyleCop;

namespace StyleCopContrib.Runner
{
    /// <summary>
    /// Utilities for working with CodeFile and CodeProject
    /// </summary>
    public static class ProjectUtility
    {
        #region Methods

        /// <summary>
        /// Creates a code project using only one file.
        /// </summary>
        /// <param name="codeFile">The code file.</param>
        /// <param name="environment">The StyleCop environment.</param>
        /// <returns>The code project.</returns>
        public static CodeProject CreateOneFileProject(string codeFile, StyleCopEnvironment environment)
        {
            string filePath = Path.GetDirectoryName(codeFile);

            return ProjectUtility.CreateCodeProject(new List<string> { codeFile }, filePath, environment);
        }

        /// <summary>
        /// Creates a code project using only one file.
        /// </summary>
        /// <param name="codeFile">The code file.</param>
        /// <param name="settingsLocation">The StyleCop.settings file location.</param>
        /// <param name="environment">The StyleCop environment.</param>
        /// <returns>The code project.</returns>
        public static CodeProject CreateOneFileProject(string codeFile, string settingsLocation,
                                                       StyleCopEnvironment environment)
        {
            return ProjectUtility.CreateCodeProject(new List<string> { codeFile }, settingsLocation, environment);
        }

        /// <summary>
        /// Creates a code project using a list of code file.
        /// </summary>
        /// <param name="codeFiles">The code file list.</param>
        /// <param name="environment">The StyleCop environment.</param>
        /// <returns>The code project.</returns>
        public static CodeProject CreateCodeProject(IEnumerable<string> codeFiles, StyleCopEnvironment environment)
        {
            return ProjectUtility.CreateCodeProject(codeFiles, PathUtility.GetCommonRootPath(codeFiles), environment);
        }

        /// <summary>
        /// Creates a code project using a project file.
        /// </summary>
        /// <param name="projectFile">The project file path.</param>
        /// <param name="environment">The StyleCop environment.</param>
        /// <returns>The code project.</returns>
        public static CodeProject CreateCodeProject(string projectFile, StyleCopEnvironment environment)
        {
            string projectPath = Path.GetDirectoryName(projectFile);

            IEnumerable<string> codeFiles = ProjectUtility.GetAllCodeFile(projectFile);

            return ProjectUtility.CreateCodeProject(codeFiles, projectPath, environment);
        }

        private static CodeProject CreateCodeProject(IEnumerable<string> codeFiles, string location,
                                                     StyleCopEnvironment environment)
        {
            CodeProject codeProject = new CodeProject(Guid.NewGuid().GetHashCode(), location,
                                                      new Configuration(new string[0]));

            foreach (string codeFile in codeFiles)
            {
                if (!File.Exists(codeFile))
                {
                    throw new FileNotFoundException("File " + codeFile + " not found.", codeFile);
                }

                environment.AddSourceCode(codeProject, Path.GetFullPath(codeFile), null);
            }

            return codeProject;
        }

        private static IEnumerable<string> GetAllCodeFile(string projectFile)
        {
            IList<string> codeFiles = new List<string>();

            XDocument document = XDocument.Load(projectFile);

            if (document.Root == null)
            {
                throw new InvalidOperationException("Project file " + projectFile + " is not valid.");
            }

            string projectLocation = Path.GetDirectoryName(projectFile);

            var itemGroups = from element in document.Root.Elements()
                             where element.Name.LocalName == "ItemGroup"
                             select element.Elements();

            foreach (var itemGroup in itemGroups)
            {
                var includeAttributes = from element in itemGroup
                                        where element.Name.LocalName == "Compile"
                                        select element.Attributes();

                foreach (var includeAttribute in includeAttributes)
                {
                    foreach (XAttribute attribute in includeAttribute)
                    {
                        codeFiles.Add(Path.Combine(projectLocation, attribute.Value));
                    }
                }
            }

            return codeFiles;
        }

        #endregion
    }
}