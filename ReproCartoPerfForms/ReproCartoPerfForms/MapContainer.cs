using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Carto.Ui;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using System.Diagnostics;
using Carto.DataSources;
using Carto.Styles;
using Carto.Utils;
using Carto.VectorTiles;
using Carto.Layers;
using Carto.Core;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml;

namespace ReproCartoPerfForms
{
    public class MapContainer : ContentView
    {
        public MapContainer()
        {
            Map = new MapView();
            var timer = new Stopwatch();
            timer.Start();

            // NOTE: Uncomment the timer creation and BeginInvokeOnMainThread to apply a workaround
            //var startTimer = new System.Threading.Timer((_) =>
            //{
            //    Device.BeginInvokeOnMainThread(() =>
            //    {
            MBTilesTileDataSource testDataSource = new MBTilesTileDataSource(((App)App.Current).CityDataFile);
                    CompiledStyleSet styleSet = new CompiledStyleSet(new ZippedAssetPackage(AssetUtils.LoadAsset("nutibright-v3.zip")));
                    MBVectorTileDecoder decoder = new MBVectorTileDecoder(styleSet);
                    VectorTileLayer testLayer = new VectorTileLayer(testDataSource, decoder);
                    Map.Layers.Add(testLayer);
            //    });
            //}, null, 10, System.Threading.Timeout.Infinite);

            timer.Stop();
            Debug.WriteLine($"Done map initialization in {timer.ElapsedMilliseconds}ms");

            MapPos pos = Map.Options.BaseProjection.FromWgs84(new MapPos(App.CITY_POS_X, App.CITY_POS_Y));
            Map.SetFocusPos(pos, 0);
            Map.SetZoom(15, 0);


            Content = Map.ToView();
        }

        public MapView Map { get; private set; }
        
    }
}