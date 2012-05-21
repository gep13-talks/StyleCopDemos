// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="gep13">
//   Copyright (c) gep13 2012
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Demo_4___StyleCop_Compliance
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The program.
    /// </summary>
    internal static class Program
    {
        #region Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        #endregion
    }
}