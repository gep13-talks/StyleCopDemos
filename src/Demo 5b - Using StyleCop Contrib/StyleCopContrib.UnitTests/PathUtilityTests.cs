using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using StyleCopContrib.Runner;

namespace StyleCopContrib.UnitTests
{
    [TestClass]
    [SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", Justification = "For test")]
    public sealed class PathUtilityTests
    {
        #region Tests Methods

        [TestMethod]
        public void TestRelativePaths()
        {
            PathUtilityTests.TestRelativePath(@"C:\DotNet\SVN\Trunk\test\IntegrationsTests",
                @"C:\DotNet\SVN\Trunk\src\CommonAssemblyInfo.cs", @"..\..\src\CommonAssemblyInfo.cs");

            PathUtilityTests.TestRelativePath(@"C:\toto", @"C:\tata\titi.txt", @"..\tata\titi.txt");
            PathUtilityTests.TestRelativePath(@"C:\toto", @"C:\toto\titi.txt", @"titi.txt");
            PathUtilityTests.TestRelativePath(@"c:\TOTO", @"C:\toto\titi.txt", @"titi.txt");
            PathUtilityTests.TestRelativePath(@"C:\toto", @"C:\toto\lulu\titi.txt", @"lulu\titi.txt");
            PathUtilityTests.TestRelativePath(@"C:\toto", @"C:\titi.txt", @"..\titi.txt");
            PathUtilityTests.TestRelativePath(@"C:\toto\lolo", @"C:\tata\titi.txt", @"..\..\tata\titi.txt");
            PathUtilityTests.TestRelativePath(@"C:\toto", @"D:\titi.txt", @"D:\titi.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUnrelatedPaths()
        {
            PathUtilityTests.TestRelativePath(@"..\toto", @"C:\titi.txt", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestUnrelatedPaths2()
        {
            PathUtilityTests.TestRelativePath(@"C:\titi.txt", @"..\toto", string.Empty);
        }

        private static void TestRelativePath(string fromPath, string toPath, string expectedPath)
        {
            string actualPath = PathUtility.GetRelativePath(fromPath, toPath);

            Assert.AreEqual(expectedPath, actualPath);
        }

        [TestMethod]
        public void TestCommonRootPaths()
        {
            PathUtilityTests.TestCommonRootPath(@"C:\", @"C:\toto", @"C:\tata\titi.txt");
            PathUtilityTests.TestCommonRootPath(@"C:\toto", @"C:\toto", @"C:\toto\titi.txt");
            PathUtilityTests.TestCommonRootPath(@"C:\toto", @"C:\toto\lulu.txt", @"C:\toto\titi.txt");
            PathUtilityTests.TestCommonRootPath(string.Empty, @"C:\toto\lulu.txt", @"D:\toto\titi.txt");
            PathUtilityTests.TestCommonRootPath(@"C:\toto\lulu.txt", @"C:\toto\lulu.txt", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRelativePathAndNull()
        {
            PathUtilityTests.TestCommonRootPath(string.Empty, @"..\toto", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRelativePathAndNull2()
        {
            PathUtilityTests.TestCommonRootPath(string.Empty, string.Empty, null);
        }

        private static void TestCommonRootPath(string expectedPath, string path1, string path2)
        {
            string[] paths = string.IsNullOrEmpty(path2) ? new[] { path1 } : new[] { path1, path2 };

            string actualPath = PathUtility.GetCommonRootPath(paths);

            Assert.AreEqual(expectedPath, actualPath);
        }

        #endregion
    }
}