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
using System.Windows.Threading;

namespace LifeTracker
{
    public partial class ReminderWindow : Window
    {
        private protected System.Media.SoundPlayer player = new System.Media.SoundPlayer("Meeting Reminder.wav");
        public ReminderWindow()
        {
            InitializeComponent();

            // Play sound
            player.Play();
        }
        private void OkayButtonClick(object sender, RoutedEventArgs e)
        {
            // Stop sound
            player.Stop();

            // Close create window.
            this.Close();
        }
    }
}
