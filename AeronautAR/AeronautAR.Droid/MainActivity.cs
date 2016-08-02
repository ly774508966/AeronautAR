using System;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Hardware; // Use Camera2 later.
using Android.Content.PM;

namespace AeronautAR.Droid
{
    [Activity(Label = "AeronautAR", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, TextureView.ISurfaceTextureListener
    {
        Camera _camera; // Use CameraDevice later.
        TextureView _textureView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _textureView = new TextureView(this);
            _textureView.SurfaceTextureListener = this;

            SetContentView(_textureView);

            // Loads all the shared code.
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public void OnSurfaceTextureAvailable(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
            _camera = Camera.Open();
            _textureView.LayoutParameters = new FrameLayout.LayoutParams(width, height);

            try
            {
                _camera.SetPreviewTexture(surface);
                _camera.StartPreview();
            }
            catch (Java.IO.IOException ioe)
            {
                Console.WriteLine(ioe.Message);
            }
        }

        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            _camera.StopPreview();
            _camera.Release();

            return true;
        }

        public void OnSurfaceTextureSizeChanged(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
            throw new NotImplementedException();
        }

        public void OnSurfaceTextureUpdated(Android.Graphics.SurfaceTexture surface)
        {
            throw new NotImplementedException();
        }
    }
}

