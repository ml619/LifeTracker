﻿using System;
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
    // ResizeMode="NoResize" WindowStartupLocation="CenterScreen" WindowStyle="None"





    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public Dictionary<Rectangle, Event> blockToEvent = new Dictionary<Rectangle, Event>();
        public Dictionary<Rectangle, TextBlock> blockToText = new Dictionary<Rectangle, TextBlock>();
        public MainWindow()
        {
            InitializeComponent();
            SelectDisplayWeek.SelectedDate = DateTime.Today;
        }

        public DateTime displayStartOfWeek = DateTime.Today;
        //current week loaded (TEMPORARILY A BLANK WEEK - FINAL SHOULD LOAD WEEK WITH CURRENT DATE) - DEBUG
        public static week currentWeek = new week();


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
            // Set week with Monday at the start.
            displayStartOfWeek = FindNearestMonday(SelectDisplayWeek.SelectedDate.Value);
            UpdateDisplayDates(displayStartOfWeek);
            // (use this to access events from JSON)
            long epochVal = (new DateTimeOffset(displayStartOfWeek)).ToUniversalTime().ToUnixTimeMilliseconds();

            // Update displayed events to reflect current week.

            // CLEAR CURRENT EVENTS DISPLAYED (use epoch code below, probably) - DEBUG
            // ACCESS MIKE'S DATA STUFF
            // GO THROUGH AND DISPLAY THAT DATA ON CALENDAR

        }

        private void AddEventToDisplay(Event curEvent)
        {
            //Set x margin to correspond to day of week
            DateTime datetime = DateTimeOffset.FromUnixTimeSeconds(curEvent.GetDate_Time()).DateTime;
            int x_margin = 100 * ((int)datetime.DayOfWeek - 1) + 6;

            //Set y_margin to correspond to time during day
            //each 1/4 hour = 8 units
            int y_margin = 8 * (ConvertTimeToHeightNumber(datetime)) + 11;

            //Create Rectangle
            Rectangle rec = new Rectangle()
            {
                Width = 95, //set width
                Height = 8 * 4 * curEvent.GetDuration(), 
                Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(curEvent.GetColor()),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(x_margin, y_margin, 0, 0),
                Stroke = Brushes.Black,
                VerticalAlignment = VerticalAlignment.Top,
                RadiusX = 10,
                RadiusY = 10,
            };
            rec.MouseDown += new MouseButtonEventHandler(EditEventClick);

            //Create Textblock
            TextBlock txtblk = new TextBlock()
            {
                Text = curEvent.GetName(),
                Margin = new Thickness(x_margin, y_margin, 0, 0),
                TextAlignment = TextAlignment.Center,
                Width = 95, //set width
                Height = 8 * 4 * curEvent.GetDuration(),
            };
            txtblk.MouseDown += new MouseButtonEventHandler(EditEventClick);

            Scroll_Area.Children.Add(rec);
            //Scroll_Area.Children.Add(txtblk); - DEBUG

            // Add rectangle-event pair to dictionary
            blockToEvent.Add(rec, curEvent);
            //blockToText.Add(rec,txtblk); - DEBUG
        }

        private int ConvertTimeToHeightNumber(DateTime inputTime)
        {
            int retNum = 0;
            String timeString = inputTime.ToString("HH:mm");
            String hourString1 = timeString.Substring(0, 2);
            String hourString2 = timeString.Substring(3);
            if (hourString1.Substring(0, 1) == "0") { hourString1 = hourString1.Substring(1); }
            if (hourString2.Substring(0, 1) == "0") { hourString2 = hourString2.Substring(1); }
            retNum = Convert.ToInt32(hourString1) * 4;
            retNum += Convert.ToInt32(hourString2) % 15; //in increments of 15
            return retNum;
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
            weekSpanText.Text = displayStartOfWeek.ToString("MMMM") + " " + displayStartOfWeek.Day + " - " +
                (displayStartOfWeek.AddDays(6)).ToString("MMMM") + " " + (displayStartOfWeek.AddDays(6)).Day;
            yearText.Text = displayStartOfWeek.ToString("yyyy");
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            // Pop up Create Event window.
            CreateEventWindow createWin = new CreateEventWindow();
            createWin.ShowDialog();

            // Create event
            AddEventToDisplay(CreateEvent(ref createWin));
        }
        private Event CreateEvent(ref CreateEventWindow createWin)
        {
            Event retEvent = new Event();

            //duration stored in terms of hours (i.e. 1 hours, 1.25 hours, etc.)
            retEvent.SetName(createWin.TitleInput.Text);
            retEvent.SetDate_Time(EpochTimeConversion(ref createWin));
            retEvent.SetDuration(HoursDifferenceConversion(ref createWin));
            retEvent.SetColor(createWin.ColorList.Text);
            retEvent.SetFlexibility(createWin.FlexibilityList.SelectedIndex + 1);
            retEvent.SetPriority(createWin.PriorityList.Text);
            retEvent.SetDescription(createWin.DescriptionInput.Text);

            return retEvent;
        }
        private Event EditEvent(ref EditEventWindow editWin)
        {
            Event retEvent = new Event();

            retEvent.SetName(editWin.TitleInput.Text);
            retEvent.SetDate_Time(EpochTimeConversion(ref editWin));
            retEvent.SetDuration(HoursDifferenceConversion(ref editWin));
            retEvent.SetColor(editWin.ColorList.Text);
            retEvent.SetFlexibility(editWin.FlexibilityList.SelectedIndex + 1);
            retEvent.SetPriority(editWin.PriorityList.Text);
            retEvent.SetDescription(editWin.DescriptionInput.Text);

            return retEvent;
        }

        private void EditEventClick(object sender, RoutedEventArgs e)
        {
            // Pop up Create Event window.
            EditEventWindow editWin = new EditEventWindow();

            Event selectedEvent = blockToEvent[(Rectangle)sender];



            // Retrieve event data corresponding to block selected
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(selectedEvent.GetDate_Time());
            DateTime dateTime = dateTimeOffset.DateTime;
            String dateTimeHour = dateTime.ToString();

            long epochFromDuration = (long)(selectedEvent.GetDuration() * 3600);
            DateTimeOffset dateTimeOffset2 = DateTimeOffset.FromUnixTimeSeconds(selectedEvent.GetDate_Time()+epochFromDuration);
            DateTime dateTime2 = dateTimeOffset2.DateTime;
            String dateTimeHour2 = dateTime2.ToString();

            // Set window parameters to old values
            editWin.TitleInput.Text = selectedEvent.GetName();
            editWin.TimeList1.Text = dateTime.ToString("hh:mm");
            editWin.TimeList2.Text = dateTime2.ToString("hh:mm");
            editWin.AMPM1.Text = dateTime.ToString("tt");
            editWin.AMPM2.Text = dateTime2.ToString("tt");
            editWin.PriorityList.Text = selectedEvent.GetPriority();
            //editWin.FlexibilityList.SelectedIndex = selectedEvent.GetFlexibility().ToString();
            editWin.MonthList.Text = dateTime.ToString("MMM");
            editWin.DayList.Text = dateTime.ToString("dd");
            editWin.YearList.Text = dateTime.ToString("yyyy");
            editWin.ColorList.Text = selectedEvent.GetColor();
            editWin.DescriptionInput.Text = selectedEvent.GetDescription();

            editWin.ShowDialog();

            // Create new event (unless should be deleted)
            if (editWin.deleteEventBool == false) AddEventToDisplay(EditEvent(ref editWin));

            // Delete old event
            blockToEvent.Remove((Rectangle)sender);
            //blockToText.Remove((Rectangle)sender); - DEBUG
            Scroll_Area.Children.Remove((Rectangle)sender);
        }

        private long EpochTimeConversion(ref EditEventWindow editWin)
        {
            //convert from 12 to 24 hour time
            String time12To24;
            int temp;
            if (editWin.AMPM1.Text == "AM") { time12To24 = editWin.TimeList1.Text; }
            else
            {
                int.TryParse(editWin.TimeList1.Text.Substring(0, 2), out temp);
                time12To24 = ((temp + 12) % 24).ToString() + editWin.TimeList1.Text.Substring(2);
            }

            //convert from datetime to epoch time
            String MonthListString = (editWin.MonthList.SelectedIndex + 1).ToString();
            if ((editWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (editWin.MonthList.SelectedIndex + 1).ToString(); }
            String DayListString = (editWin.DayList.SelectedIndex + 1).ToString();
            if ((editWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (editWin.DayList.SelectedIndex + 1).ToString(); }
            String dateTimeString = editWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";
            DateTime tempDate = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan t = tempDate - new DateTime(1970, 1, 1);

            return (int)t.TotalSeconds;
        }

        private double HoursDifferenceConversion(ref CreateEventWindow createWin) //SIMPLIFY 12to24 CONVERTER - DEBUG
        {
            //---START---
            //convert from 12 to 24 hour time
            String time12To24;
            int temp;
            if (createWin.AMPM1.Text == "AM") { time12To24 = createWin.TimeList1.Text; }
            else
            {
                int.TryParse(createWin.TimeList1.Text.Substring(0, 2), out temp);
                time12To24 = ((temp + 12) % 24).ToString() + createWin.TimeList1.Text.Substring(2);
            }

            //convert from datetime to epoch time
            String MonthListString = (createWin.MonthList.SelectedIndex + 1).ToString();
            if ((createWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (createWin.MonthList.SelectedIndex + 1).ToString(); }
            String DayListString = (createWin.DayList.SelectedIndex + 1).ToString();
            if ((createWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (createWin.DayList.SelectedIndex + 1).ToString(); }
            String dateTimeString = createWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";
            DateTime tempDate1 = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            //---END---
            //convert from 12 to 24 hour time
            if (createWin.AMPM2.Text == "AM") { time12To24 = createWin.TimeList2.Text; }
            else
            {
                int.TryParse(createWin.TimeList2.Text.Substring(0, 2), out temp);
                time12To24 = ((temp + 12) % 24).ToString() + createWin.TimeList2.Text.Substring(2);
            }

            //convert from datetime to epoch time
            MonthListString = (createWin.MonthList.SelectedIndex + 1).ToString();
            if ((createWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (createWin.MonthList.SelectedIndex + 1).ToString(); }
            DayListString = (createWin.DayList.SelectedIndex + 1).ToString();
            if ((createWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (createWin.DayList.SelectedIndex + 1).ToString(); }
            dateTimeString = createWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";
            DateTime tempDate2 = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            //find difference between end and start, convert from seconds to hours
            TimeSpan t = tempDate2 - tempDate1;
            return ((double)t.TotalSeconds) / 3600;
        }

        private long EpochTimeConversion(ref CreateEventWindow createWin)
        {
            //convert from 12 to 24 hour time
            String time12To24;
            int temp;
            if (createWin.AMPM1.Text == "AM") { time12To24 = createWin.TimeList1.Text; }
            else
            {
                int.TryParse(createWin.TimeList1.Text.Substring(0, 2), out temp);
                time12To24 = ((temp + 12) % 24).ToString() + createWin.TimeList1.Text.Substring(2);
            }

            //convert from datetime to epoch time
            String MonthListString = (createWin.MonthList.SelectedIndex + 1).ToString();
            if ((createWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (createWin.MonthList.SelectedIndex + 1).ToString(); }
            String DayListString = (createWin.DayList.SelectedIndex + 1).ToString();
            if ((createWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (createWin.DayList.SelectedIndex + 1).ToString(); }
            String dateTimeString = createWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";
            DateTime tempDate = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan t = tempDate - new DateTime(1970, 1, 1);

            return (int)t.TotalSeconds;
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

        private double HoursDifferenceConversion(ref EditEventWindow editWin) //SIMPLIFY 12to24 CONVERTER - DEBUG
        {
            //---START---
            //convert from 12 to 24 hour time
            String time12To24;
            int temp;
            if (editWin.AMPM1.Text == "AM") { time12To24 = editWin.TimeList1.Text; }
            else
            {
                int.TryParse(editWin.TimeList1.Text.Substring(0, 2), out temp);
                time12To24 = ((temp + 12) % 24).ToString() + editWin.TimeList1.Text.Substring(2);
            }

            //convert from datetime to epoch time
            String MonthListString = (editWin.MonthList.SelectedIndex + 1).ToString();
            if ((editWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (editWin.MonthList.SelectedIndex + 1).ToString(); }
            String DayListString = (editWin.DayList.SelectedIndex + 1).ToString();
            if ((editWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (editWin.DayList.SelectedIndex + 1).ToString(); }
            String dateTimeString = editWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";
            DateTime tempDate1 = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            //---END---
            //convert from 12 to 24 hour time
            if (editWin.AMPM2.Text == "AM") { time12To24 = editWin.TimeList2.Text; }
            else
            {
                int.TryParse(editWin.TimeList2.Text.Substring(0, 2), out temp);
                time12To24 = ((temp + 12) % 24).ToString() + editWin.TimeList2.Text.Substring(2);
            }

            //convert from datetime to epoch time
            MonthListString = (editWin.MonthList.SelectedIndex + 1).ToString();
            if ((editWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (editWin.MonthList.SelectedIndex + 1).ToString(); }
            DayListString = (editWin.DayList.SelectedIndex + 1).ToString();
            if ((editWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (editWin.DayList.SelectedIndex + 1).ToString(); }
            dateTimeString = editWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";
            DateTime tempDate2 = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            //find difference between end and start, convert from seconds to hours
            TimeSpan t = tempDate2 - tempDate1;
            return ((double)t.TotalSeconds) / 3600;
        }
    }
}

public class Event //description, priority, time, color, flexibility
{

    protected private string name; // name of event
    public string GetName()
    {
        return name;
    }
    public void SetName(string Name)
    {
        name = Name;
    }


    protected private long date_time; //date and time in epoch
    public long GetDate_Time()
    {
        return date_time;
    }
    public void SetDate_Time(long Date_Time)
    {
        date_time = Date_Time;
    }


    protected private int flexibility;
    public int GetFlexibility()
    {
        return flexibility;
    }
    public void SetFlexibility(int Flexibility)
    {
        flexibility = Flexibility;
    }


    protected private string color; // color of event
    public string GetColor()
    {
        return color;
    }
    public void SetColor(string Color)
    {
        color = Color;
    }


    protected private double duration; //IN TERMS OF HOURS
    public double GetDuration()
    {
        return duration;
    }
    public void SetDuration(double Duration)
    {
        duration = Duration;
    }


    protected private string priority;
    public string GetPriority()
    {
        return priority;
    }
    public void SetPriority(string Priority)
    {
        priority = Priority;
    }


    protected private string description; //short description of acitiviy 
    public string GetDescription()
    {
        return description;
    }
    public void SetDescription(string Description)
    {
        description = Description;
    }


    /*public void Equality(Event event1, Event event2)
    {
        if (event1.name != event2.name){
            break;
        }
        if (event1.date_time != event2.date_time){
            break;
        }
        if (event1.color != event2.color){
            break;
        }
        if (event1.flexibility != event2.flexibility){
            break;
        }
        if (event1.priority != event2.priority){
            break;
        }
        if (event1.description != event2.description){
            break;
        }
        else {
            return true;
        }
    }*/
}

class location : Event
{
    protected private string eventname; // name of location
    public string GetEventName()
    {
        return eventname;
    }
    public void SetEventName(string EventName)
    {
        eventname = EventName;
    }

}

class recurring : Event
{
    private protected long end_date; // end of recurring 
    public long GetEnd_Date()
    {
        return end_date;
    }
    public void SetEnd_Date(long End_Date)
    {
        end_date = End_Date;
    }


    private protected int step; //how often 
    public int GetStep()
    {
        return step;
    }
    public void Step(int Step)
    {
        step = Step;
    }
}

public class week
{
    protected private static List<Event> mon = new List<Event>(); //lists of events
    protected private static List<Event> tue = new List<Event>();
    protected private static List<Event> wed = new List<Event>();
    protected private static List<Event> thu = new List<Event>();
    protected private static List<Event> fri = new List<Event>();
    protected private static List<Event> sat = new List<Event>();
    protected private static List<Event> sun = new List<Event>();

    protected private List<List<Event>> a_week = new List<List<Event>>() { mon, tue, wed, thu, fri, sat, sun };

    public List<List<Event>> GetWeek()
    {
        return a_week;
    }

    public void Add(Event event1, int day)
    {
        a_week[day].Add(event1);
    }

    public void Delete(Event event1, int day)
    {
        a_week[day].Remove(event1);
    }
}