using System;
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
			//Application.Run(new OpenForm());

			// Load an image
			using (Mat image = CvInvoke.Imread("path_to_your_image.jpg"))
			{
				if (image.IsEmpty)
				{
					Console.WriteLine("Could not load image!");
					return;
				}

				// Display the image
				CvInvoke.Imshow("Image Window", image);
				CvInvoke.WaitKey(0);
				CvInvoke.DestroyAllWindows();



			}
		}
	}
}
