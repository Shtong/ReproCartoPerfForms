using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

namespace ReproCartoPerfForms
{
	public partial class App : Application
	{
        public const string CITY_FILE = "rome_ntvt.mbtiles";
        public const double CITY_POS_X = 12.4807;
        public const double CITY_POS_Y = 41.8962;
        
        public App ()
		{
			InitializeComponent();
            
            // Extract mbtiles from assets
            var t = PrepareAsset(CITY_FILE);
            t.Wait();
            CityDataFile = t.Result;

            // Start
			MainPage = new Fast();
		}

        public string CityDataFile { get; private set; }
        
        private async Task<string> PrepareAsset(string assetName)
        {

            string destPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, assetName);

            if (!File.Exists(destPath))
            {
                Debug.WriteLine("PLEASE WAIT! Copying mbtiles...");
                StorageFile sourceFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/" + assetName, UriKind.Absolute));
                using (Stream sourceStream = await sourceFile.OpenStreamForReadAsync())
                {
                    using (FileStream destStream = File.Create(destPath))
                    {
                        await sourceStream.CopyToAsync(destStream);
                    }
                }
                Debug.WriteLine("Copying mbtiles done!");
            }

            return destPath;
        }
    }
}
