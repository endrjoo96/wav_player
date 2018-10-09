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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WAV_player
{
    public partial class Playlista:Window
    {
        MainWindow mainWindow;
        public Playlista(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            foreach(String path in mainWindow.playlist)
            {
                ListView1.Items.Add(System.IO.Path.GetFileName(path));
                ListView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            }
            mainWindow.playlist.CollectionChanged += Playlist_CollectionChanged;
        }

        private void Playlist_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (String path in mainWindow.playlist)
            {
                ListView1.Items.Add(System.IO.Path.GetFileName(path));
                ListView1.MouseDoubleClick += ListView1_MouseDoubleClick;
            }
        }

        private void ListView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainWindow.count = ListView1.SelectedIndex-1;
            mainWindow.state = MainWindow.COMMAND.FROM_PLAYLIST_WINDOW;
            mainWindow.Media_Ended(new object(), new EventArgs());
        }
    }
}
