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

namespace LifeTracker
{
    /// <summary>
    /// Interaction logic for ReminderWindow.xaml
    /// </summary>
    public partial class ReminderWindow : Window
    {
        public ReminderWindow()
        {
            InitializeComponent();

            // Play sound
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"Meeting Reminder.mp3");
            player.Play();
        }
    }
}
