using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Map_Creation_Tool.src.View;
namespace Map_Creation_Tool.src
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.

			//ApplicationConfiguration.Initialize();
			Application.Run(new OpenForm());

		}
	}
}
