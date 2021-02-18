using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using Avalonia;

namespace AvaloniaApplicationWithControls
{
    public class MainWindow : Window
    {
        //        StackPanel stackPanel = new StackPanel();
        private VideoView VideoView;
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        private VideoView VideoViewSecond;
        private MediaPlayer _mediaPlayersecond;
        //private LibVLC _libVLCsecond;


        private VideoView VideoViewthird;
        private MediaPlayer _mediaPlayerthird;
        //private LibVLC _libVLCthird;


        public MainWindow()
        {
            InitializeComponent();
            
            #if DEBUG
                        this.AttachDevTools(KeyGesture.Parse("Ctrl+F5"));
            #endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            VideoView = this.FindControl<VideoView>("Video1");
            VideoViewSecond = this.FindControl<VideoView>("Video2");
            VideoViewthird = this.FindControl<VideoView>("Video3");
            Core.Initialize();

            _libVLC = new LibVLC();
//            _libVLCsecond = new LibVLC();
//            _libVLCthird = new LibVLC();

            _mediaPlayer = new MediaPlayer(_libVLC);
            _mediaPlayersecond = new MediaPlayer(_libVLC);
            _mediaPlayerthird = new MediaPlayer(_libVLC);

            VideoView.MediaPlayer = _mediaPlayer;
            VideoViewSecond.MediaPlayer = _mediaPlayersecond;
            VideoViewthird.MediaPlayer = _mediaPlayerthird;

            #region NotusefullRegion

            //stackPanel = this.FindControl<StackPanel>("Panel");

            //double TotalWidth = this.Width;

            //double FirstControlWidth = TotalWidth / 2;
            /*
            CustomSkiaControl border1 = new CustomSkiaControl();
            border1.Width = 100;
            border1.Height = this.Height;

            CustomSkiaControl border2 = new CustomSkiaControl();
            border2.Width = 300;
            border2.Height = this.Height;
            */
            //            List<CustomSkiaControl> controls = new List<CustomSkiaControl>();
            //            controls.Add(border1);
            //controls.Add(border2);


            //Button button = new Button();
            //stackPanel.Children.Add(border1);
            //stackPanel.Children.Add(border2);

            //this.Content = stackPanel;


            // Create two buttons
            /*Button myButton1 = new Button();
            myButton1.Content = "Button 1";
            Button myButton2 = new Button();
            myButton2.Content = "Button 2";
            */
            // Create a StackPanel
            /*StackPanel myStackPanel = new StackPanel();
            Avalonia.Controls.Image i = new Avalonia.Controls.Image();
            i.Width = 960;
            i.Height = 500;


            WriteableBitmap writeableBitmap = new WriteableBitmap(PixelSize.FromSize(new Avalonia.Size(960, 500), 2), Vector.One);

            i.Source = writeableBitmap;
//            i.Source = new Avalonia.Media.Imaging.Bitmap(AppContext.BaseDirectory + Path.Combine("HardDrive.png"));
            i.Stretch = Stretch.None;
            i.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            i.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;

            // Add the buttons to the StackPanel
            myStackPanel.Children.Add(i);
//            myStackPanel.Children.Add(border2);

            this.Content = myStackPanel;
            */
            #endregion
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (VideoView.MediaPlayer.IsPlaying)
            {
                VideoView.MediaPlayer.Stop();
            }
        }
        private void StopButtonsecond_Click(object sender, RoutedEventArgs e)
        {
            if (VideoViewSecond.MediaPlayer.IsPlaying)
            {
                VideoViewSecond.MediaPlayer.Stop();
            }
        }
        private void StopButtonthird_Click(object sender, RoutedEventArgs e)
        {
            if (VideoViewthird.MediaPlayer.IsPlaying)
            {
                VideoViewthird.MediaPlayer.Stop();
            }
        }
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VideoView.MediaPlayer.IsPlaying)
            {
                /*VideoView.MediaPlayer.Play(new Media(_libVLC,
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));*/
                VideoView.MediaPlayer.Play(new Media(_libVLC,
                    "http://81.149.56.38:8084/mjpg/video.mjpg", FromType.FromLocation));


            }
        }
        private void PlayButtonthird_Click(object sender, RoutedEventArgs e)
        {
            if (!VideoViewthird.MediaPlayer.IsPlaying)
            {
                /*VideoView.MediaPlayer.Play(new Media(_libVLC,
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));*/
                VideoViewthird.MediaPlayer.Play(new Media(_libVLC,
                    "http://www.supercartoons.net/video/472/pink-pistons.mp4", FromType.FromLocation));


            }
        }
        private void PlayButtonsecond_Click(object sender, RoutedEventArgs e)
        {
            if (!VideoViewSecond.MediaPlayer.IsPlaying)
            {
                VideoViewSecond.MediaPlayer.Play(new Media(_libVLC,
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));
                

            }
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            VideoView.MediaPlayer.Pause();
        }
        private void PauseButtonsecond_Click(object sender, RoutedEventArgs e)
        {
            VideoViewSecond.MediaPlayer.Pause();
        }
        private void PauseButtonthird_Click(object sender, RoutedEventArgs e)
        {
            VideoViewthird.MediaPlayer.Pause();
        }
    }
}
