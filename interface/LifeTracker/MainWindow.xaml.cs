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

namespace LifeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SelectDisplayWeek.SelectedDate = DateTime.Today;
        }

        //PUBLIC VARIABLES
            //Monday DateTime for Monday of week currently being displayed
        public DateTime displayStartOfWeek = DateTime.Today; 


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ArrivalDatePicker_DateChanged(object sender, EventArgs e)
        {
            //TuesdayText.Text = (FindNearestMonday(SelectDisplayWeek.SelectedDate.Value)).ToString();


            // Set week with Monday at the start.
            displayStartOfWeek = FindNearestMonday(SelectDisplayWeek.SelectedDate.Value);
            UpdateDisplayDates(displayStartOfWeek);
            // (use this to access events from JSON)
            long epochVal = (new DateTimeOffset(displayStartOfWeek)).ToUniversalTime().ToUnixTimeMilliseconds();

            // Update displayed events to reflect current week.

            // CLEAR CURRENT EVENTS DISPLAYED (use epoch code below, probably)
            // ACCESS MIKE'S DATA STUFF
            // GO THROUGH AND DISPLAY THAT DATA ON CALENDAR

            //TRY THIS OUT BELOW TO CREATE EVENT ON SCREEN?
            //button.Content = rectangle;

        }

        private DateTime FindNearestMonday(DateTime inputDate)
        {
            DateTime retDate = new DateTime();

            // Find nearest Monday (going backwards in time), use result to find
            //  corresponding week in stored data.
            // Day of week is found numerically - Monday = 0, Sunday = 6
            // Track backwards until reach Monday
            int curWeekDayNum = (int)(SelectDisplayWeek.SelectedDate.Value.DayOfWeek + 6) % 7;
            retDate = inputDate.AddDays(-curWeekDayNum);
            return retDate;
        }

        private void UpdateDisplayDates(DateTime inputDate)
        {
            // Update M-F numbers
            TextBlock[] weekdayTexts = { mondayText, tuesdayText, wednesdayText, thursdayText, 
                    fridayText, saturdayText, sundayText };
            for (int i = 0; i <= 6; i++)
            {
                weekdayTexts[i].Text = (displayStartOfWeek.AddDays(i)).Month + "/" +
                    (displayStartOfWeek.AddDays(i)).Day;
            }

            // Update week span text
            weekSpanText.Text = displayStartOfWeek.ToString("MMMM") +" "+ displayStartOfWeek.Day +" - "+
                (displayStartOfWeek.AddDays(6)).ToString("MMMM") +" "+ (displayStartOfWeek.AddDays(6)).Day;
            yearText.Text = displayStartOfWeek.ToString("yyyy");
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            // Pop up Create Event window.
            CreateEventWindow createWin = new CreateEventWindow();
            createWin.Show();
        }
        private void MoveWeekForward(object sender, RoutedEventArgs e)
        {
            // Update display and week being accessed.
            displayStartOfWeek = displayStartOfWeek.AddDays(7);
            UpdateDisplayDates(displayStartOfWeek);
        }
        private void MoveWeekBackward(object sender, RoutedEventArgs e)
        {
            // Update display and week being accessed.
            displayStartOfWeek = displayStartOfWeek.AddDays(-7);
            UpdateDisplayDates(displayStartOfWeek);
        }
    }
}
