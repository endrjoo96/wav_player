using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace WAV_player
{
    public partial class MainWindow:Window
    {
        public enum COMMAND
        {
            NOT_SET,
            FROM_PLAYLIST_WINDOW
        }
        
        public COMMAND state = COMMAND.NOT_SET;
        public ObservableCollection<string> playlist = new ObservableCollection<string>();
        public MediaPlayer player = new MediaPlayer();
        DispatcherTimer timer;
        public int count = -1;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(20)
            };
            timer.Tick += new EventHandler(Timer_Tick);
            player.MediaOpened += new EventHandler(Media_Opened);
            player.MediaEnded += new EventHandler(Media_Ended);
        }

        private void Open_menuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Multiselect = true
            };
            if (dialog.ShowDialog() == true)
            {
                foreach (String path in dialog.FileNames)
                {
                    if (!playlist.Contains<string>(path))
                        playlist.Add(path);
                }
            }
        }

        private void Play_button_Click(object sender, RoutedEventArgs e)
        {
            if (Play_button.Content.ToString() == "Odtwórz" && nowplaying_label.Content.ToString() == "Nie odtwarzam")
            {
                Media_Ended(Play_button, new EventArgs());
            } else if (Play_button.Content.ToString() == "Odtwórz" && nowplaying_label.Content.ToString() == "Wstrzymano")
            {
                Play_button.Content = "Wstrzymaj";
                nowplaying_label.Content = playlist[count];
                player.Play();
            } else
            {
                Play_button.Content = "Odtwórz";
                nowplaying_label.Content = "Wstrzymano";
                player.Pause();
            }
        }

        private void Media_Opened(object sender, EventArgs e)
        {
            if (player.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = player.NaturalDuration.TimeSpan;
                Progress_slider.Maximum = ts.TotalMilliseconds;
                Progress_slider.SmallChange = 1;
                Progress_slider.LargeChange = Math.Min(10, ts.Milliseconds / 10);
            }
            Play_button.Content = "Wstrzymaj";
            nowplaying_label.Content = player.Source;
            timer.Start();
            player.Play();
        }

        public void Media_Ended(object sender, EventArgs e)
        {
            count++;
            if (Loop_button.Content.ToString() == "Zapętl jeden")
            {
                if(count>0)
                    count--;
            }
            if (count < playlist.Count)
            {
                if (System.IO.File.Exists(playlist[count]))
                {
                    player.Open(new Uri(playlist[count]));
                    if (state == COMMAND.FROM_PLAYLIST_WINDOW)
                    {
                        count = playlist.Count;
                        state = COMMAND.NOT_SET;
                    }
                } else
                {
                    count++;
                    Media_Ended(sender, new EventArgs());
                }

            } else if (count >= playlist.Count)
            {
                if (Loop_button.Content.ToString() == "Zapętl wszystkie")
                {
                    count = -1;
                    Media_Ended(sender, new EventArgs());
                } else if (sender is Button)
                {
                    if (">>" == (sender as Button).Content.ToString())
                    {
                        count = -1;
                        Media_Ended(sender, new EventArgs());
                    }
                } else
                {
                    count = -1;
                    Play_button.Content = "Odtwórz";
                    nowplaying_label.Content = "Nie odtwarzam";
                }
            }
        }

        bool isDragging = false;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                Progress_slider.Value = player.Position.TotalMilliseconds;
            }
        }
        private void Progress_slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isDragging = true;
            player.Pause();
        }
        private void Progress_slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            isDragging = false;
            player.Position = TimeSpan.FromMilliseconds(Progress_slider.Value);
            player.Play();
        }
        
        private void Next_button_Click(object sender, RoutedEventArgs e)
        {
            Media_Ended(Next_button, new EventArgs());
        }

        private void Prev_button_Click(object sender, RoutedEventArgs e)
        {
            count-=2;
            if(count < -1)
            {
                count = playlist.Count - 2;
            }
            Media_Ended(Next_button, new EventArgs());
        }

        private void Stop_button_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
            count--;
            Play_button.Content = "Odtwórz";
            nowplaying_label.Content = "Nie odtwarzam";
        }

        private void Playlist_menuItem_Click(object sender, RoutedEventArgs e)
        {
            Playlista window = new Playlista(this);
            window.Show();
        }

        private void Loop_button_Click(object sender, RoutedEventArgs e)
        {
            if (Loop_button.Content.ToString() == "Nie zapętlaj")
                Loop_button.Content = "Zapętl jeden";
            else if (Loop_button.Content.ToString() == "Zapętl jeden")
                Loop_button.Content = "Zapętl wszystkie";
            else if (Loop_button.Content.ToString() == "Zapętl wszystkie")
                Loop_button.Content = "Nie zapętlaj";
        }

        private void OpenCustom_button_Click(object sender, RoutedEventArgs e)
        {
            FileBrowser window = new FileBrowser();
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
    }
}
