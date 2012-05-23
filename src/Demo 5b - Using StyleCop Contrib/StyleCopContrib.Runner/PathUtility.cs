using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StyleCopContrib.Runner
{
    /// <summary>
    /// A path utility class.
    /// </summary>
    public static class PathUtility
    {
        #region Methods

        /// <summary>
        /// Gets the relative path from a source to a target path.
        /// </summary>
        /// <param name="fromPath">The from source path.</param>
        /// <param name="toPath">The to target path.</param>
        /// <returns>The relative path.</returns>
        public static string GetRelativePath(string fromPath, string toPath)
        {
            if (!Path.IsPathRooted(fromPath) || !Path.IsPathRooted(toPath))
            {
                throw new InvalidOperationException("Only fully qualified path are supported");
            }

            string relativePath;

            string[] fromPathParts = fromPath.Split(Path.DirectorySeparatorChar);
            string[] toPathParts = toPath.Split(Path.DirectorySeparatorChar);

            int partIndex = 0;
            while (partIndex < fromPathParts.Length)
            {
                if (fromPathParts[partIndex].ToUpperInvariant() != toPathParts[partIndex].ToUpperInvariant()) break;

                partIndex++;
            }

            if (partIndex == 0)
            {
                relativePath = toPath;
            }
            else
            {
                string backPath = string.Join(Path.DirectorySeparatorChar.ToString(),
                                              Enumerable.Repeat("..", fromPathParts.Length - partIndex).ToArray());

                string forewardPath = string.Join(Path.DirectorySeparatorChar.ToString(), toPathParts, partIndex,
                                                  toPathParts.Length - partIndex);

                if (!string.IsNullOrEmpty(backPath))
                {
                    relativePath = string.Concat(backPath, Path.DirectorySeparatorChar, forewardPath);
                }
                else
                {
                    relativePath = forewardPath;
                }
            }

            return relativePath;
        }

        /// <summary>
        /// Gets the common root path of the given path list.
        /// </summary>
        /// <param name="paths">The list of path.</param>
        /// <returns>The common root path.</returns>
        public static string GetCommonRootPath(IEnumerable<string> paths)
        {
            string[] commonPathParts = null;
            int commonPartIndex = int.MaxValue;

            foreach (string path in paths)
            {
                if (!Path.IsPathRooted(path))
                {
                    throw new InvalidOperationException("Only fully qualified path are supported");
                }

                string[] pathParts = path.Split(Path.DirectorySeparatorChar);

                if (commonPathParts == null)
                {
                    commonPathParts = pathParts;
                    commonPartIndex = commonPathParts.Length;
                }
                else
                {
                    int partIndex = 0;
                    while (partIndex < pathParts.Length && partIndex < commonPathParts.Length)
                    {
                        if (commonPathParts[partIndex].ToUpperInvariant() != pathParts[partIndex].ToUpperInvariant()) break;

                        partIndex++;
                    }

                    commonPartIndex = Math.Min(commonPartIndex, partIndex);
                }
            }

            string commonPath;
            if (commonPartIndex == 0)
            {
                commonPath = string.Empty;
            }
            else if (commonPartIndex == 1)
            {
                commonPath = string.Concat(commonPathParts[0], Path.DirectorySeparatorChar);
            }
            else
            {
                commonPath = string.Join(Path.DirectorySeparatorChar.ToString(), commonPathParts, 0, commonPartIndex);
            }

            return commonPath;
        }

        #endregion
    }
}