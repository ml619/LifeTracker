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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateEventWindow : Window
    {
        public CreateEventWindow()
        {
            InitializeComponent();

            // Initialize Lists
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 0; j <= 45; j+=15)
                {
                    if (i < 10 && j < 10)
                    {
                        TimeList1.Items.Add("0" + i + ":" + "0" + j);
                        TimeList2.Items.Add("0" + i + ":" + "0" + j);
                    }
                    else if (i < 10)
                    {
                        TimeList1.Items.Add("0" + i + ":" + j);
                        TimeList2.Items.Add("0" + i + ":" + j);
                    }
                    else if (j < 10)
                    {
                        TimeList1.Items.Add(i + ":" + "0" + j);
                        TimeList2.Items.Add(i + ":" + "0" + j);
                    }
                    else
                    {
                        TimeList1.Items.Add(i + ":" + j);
                        TimeList2.Items.Add(i + ":" + j);
                    }
                }
            }
            TimeList1.SelectedIndex = 0; TimeList2.SelectedIndex = 4;
            AMPM1.Items.Add("AM"); AMPM1.Items.Add("PM");
            AMPM2.Items.Add("AM"); AMPM2.Items.Add("PM");
            AMPM1.SelectedIndex = 0; AMPM2.SelectedIndex = 0;

            PriorityList.Items.Add("LOW"); FlexibilityList.Items.Add("LOW");
            PriorityList.Items.Add("MED"); FlexibilityList.Items.Add("MED");
            PriorityList.Items.Add("HIGH"); FlexibilityList.Items.Add("HIGH");
            PriorityList.SelectedIndex = 0; FlexibilityList.SelectedIndex = 0;

            MonthList.Items.Add("Jan"); MonthList.Items.Add("Feb");
            MonthList.Items.Add("Mar"); MonthList.Items.Add("Apr");
            MonthList.Items.Add("May"); MonthList.Items.Add("Jun");
            MonthList.Items.Add("Jul"); MonthList.Items.Add("Aug");
            MonthList.Items.Add("Sep"); MonthList.Items.Add("Oct");
            MonthList.Items.Add("Nov"); MonthList.Items.Add("Dec");
            MonthList.SelectedIndex = 0;

            for (int i = 1; i <= 31; i++)
            {
                if (i > 9) DayList.Items.Add(i.ToString());
                else DayList.Items.Add("0" + i.ToString());
            }
            DayList.SelectedIndex = 0;

            for (int i = 2030; i >= 1970; i--)
            {
                YearList.Items.Add(i.ToString());
            }
            YearList.SelectedIndex = 0;

            ColorList.Items.Add("LightBlue"); ColorList.Items.Add("LightPink");
            ColorList.Items.Add("PeachPuff"); ColorList.Items.Add("Silver");
            ColorList.Items.Add("PaleGoldenrod"); ColorList.Items.Add("BurlyWood");
            ColorList.Items.Add("Plum"); ColorList.Items.Add("LightGreen");
            ColorList.Text = "LightBlue";
        }

        private void Okay_Button_Click(object sender, RoutedEventArgs e)
        {
            /*
            //Create Event object.
            Event newEvent = CreateEvent();
            DateTime datetime = DateTimeOffset.FromUnixTimeSeconds(newEvent.GetDate_Time()).DateTime;
            MainWindow.currentWeek.Add(newEvent, (int)datetime.DayOfWeek);



            //Add to display - DEBUG should somehow be called from other file, rn its a repeat)
            //Set x margin to correspond to day of week
            int x_margin = 100 * ((int)datetime.DayOfWeek) + 6;

            //Set y_margin to correspond to time during day
            //each 1/4 hour = 8 units
            int retNum = 0;
            String timeString = datetime.ToString("HH:mm");
            String hourString1 = timeString.Substring(0, 2);
            String hourString2 = timeString.Substring(3);
            if (hourString1.Substring(0, 1) == "0") { hourString1 = hourString1.Substring(1); }
            if (hourString2.Substring(0, 1) == "0") { hourString2 = hourString2.Substring(1); }

            int y_margin = 8 * (retNum) + 11;

            //Create Rectangle
            Rectangle rec = new Rectangle()
            {
                Width = 95, //set width
                Height = 8 * 4, //SHOULD BE UPDATED TO MATCH END TIME IN EVENT (for now set to 1 hour by default)
                Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(newEvent.GetColor()),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(x_margin, y_margin, 0, 0),
                Stroke = Brushes.Black,
                VerticalAlignment = VerticalAlignment.Top,
                RadiusX = 10,
                RadiusY = 10,
            };
            //Scroll_Area.Children.Add(rec);

            //HOW ADD THIS TO THE MAIN WINDOW?????? - DEBUG

            */



            // Close create window.
            this.Close();
        }

        /*private Event CreateEvent()
        {
            Event retEvent = new Event();

            retEvent.SetName(TitleInput.Text);
            retEvent.SetDate_Time(epochTimeConversion());
            retEvent.SetColor(ColorList.Text);
            retEvent.SetFlexibility(FlexibilityList.SelectedIndex+1);
            retEvent.SetPriority(PriorityList.Text);
            retEvent.SetDescription(DescriptionInput.Text);

            return retEvent;
        }

        private long epochTimeConversion()
        {
            //convert from 12 to 24 hour time
            String time12To24;
            int temp;
            if (AMPM1.Text == "AM") { time12To24 = TimeList1.Text; }
            else
            {
                int.TryParse(TimeList1.Text.Substring(0, 2), out temp);
                time12To24 = ((temp + 12) % 24).ToString() + TimeList1.Text.Substring(2);
            }

            //convert from datetime to epoch time
            String MonthListString = (MonthList.SelectedIndex + 1).ToString();
            if ((MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (MonthList.SelectedIndex + 1).ToString(); }
            String DayListString = (DayList.SelectedIndex + 1).ToString();
            if ((DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (DayList.SelectedIndex + 1).ToString(); }
            String dateTimeString = YearList.Text + "-" + MonthListString + "-" + MonthListString + " " + time12To24 + ":00";
            DateTime tempDate = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan t = tempDate - new DateTime(1970, 1, 1);
            
            return (int)t.TotalSeconds;
        }
        */
    }
}
