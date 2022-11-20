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
using System.Timers;


namespace LifeTracker
{
    // ResizeMode="NoResize" WindowStartupLocation="CenterScreen" WindowStyle="None"





    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        // Public variables
        public Dictionary<TextBlock, Event> textToEvent = new Dictionary<TextBlock, Event>();
        public Dictionary<TextBlock, Rectangle> textToBlock = new Dictionary<TextBlock, Rectangle>();
        public Dictionary<TextBlock, Timer> textToTimer = new Dictionary<TextBlock, Timer>();

        public DateTime displayStartOfWeek = DateTime.Today;
        public static Week currentWeek = new Week(); //MAKE IT SO IT STARTS WITH CURRENT WEEK INSTEAD OF BLANK - DEBUG

        // MainWindow Initialization
        public MainWindow()
        {
            InitializeComponent();
            SelectDisplayWeek.SelectedDate = DateTime.Today;
        }

        // Close Window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        // Minimize Window
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        //DISPLAY HANDLING

        // Change Date Displayed (Date Picker)
        private void ArrivalDatePicker_DateChanged(object sender, EventArgs e)
        {
            // Set week with Monday at the start.
            displayStartOfWeek = FindNearestMonday(SelectDisplayWeek.SelectedDate.Value);
            UpdateDisplayDates(displayStartOfWeek);
            // (use this to access events from JSON)
            long epochVal = (new DateTimeOffset(displayStartOfWeek)).ToUniversalTime().ToUnixTimeMilliseconds();

            // Update displayed events to reflect current week.

            // Clear current displayed and stored events
            ClearDisplayAndStoredEvents();



            // CLEAR CURRENT EVENTS DISPLAYED (use epoch code below, probably) - DEBUG
            // ACCESS MIKE'S DATA STUFF
            // GO THROUGH AND DISPLAY THAT DATA ON CALENDAR

        }
        // Convert date from date picker to nearest (on left) Monday to identify start of week
        private DateTime FindNearestMonday(DateTime inputDate)
        {
            DateTime retDate = new DateTime();

            // Find nearest Monday (going backwards in time), use result to find corresponding week in stored data.
            // Day of week is found numerically - Monday = 0, Sunday = 6
            // Track backwards until reach Monday
            int curWeekDayNum = (int)(SelectDisplayWeek.SelectedDate.Value.DayOfWeek + 6) % 7;
            retDate = inputDate.AddDays(-curWeekDayNum);
            return retDate;
        }
        // Change Date Displayed (Right Arrow)
        private void MoveWeekForward(object sender, RoutedEventArgs e)
        {
            // Update display and week being accessed.
            displayStartOfWeek = displayStartOfWeek.AddDays(7);
            UpdateDisplayDates(displayStartOfWeek);

            // Clear current displayed and stored events
            ClearDisplayAndStoredEvents();
        }
        // Change Date Displayed (Left Arrow)
        private void MoveWeekBackward(object sender, RoutedEventArgs e)
        {
            // Update display and week being accessed.
            displayStartOfWeek = displayStartOfWeek.AddDays(-7);
            UpdateDisplayDates(displayStartOfWeek);

            // Clear current displayed and stored events
            ClearDisplayAndStoredEvents();
        }
        // Update Dates shown on display from input star- of-week day
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
        // Add an Event to the Display
        private void AddEventToDisplay(Event curEvent)
        {
            //Set x margin to correspond to day of week
            DateTime datetime = DateTimeOffset.FromUnixTimeSeconds(curEvent.GetDate_Time()).DateTime;
            int x_margin = 100 * ((int)datetime.DayOfWeek - 1) + 6;
            if (x_margin < 0) x_margin = 606;

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
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(x_margin, y_margin, 0, 0),
                Stroke = Brushes.Black,
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
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };
            txtblk.MouseDown += new MouseButtonEventHandler(EditEventClick);

            Scroll_Area.Children.Add(rec);
            Scroll_Area.Children.Add(txtblk);

            //Create Timer
            // Calculate ms between current time and start time (account for 30 minutes beforehand)
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(curEvent.GetDate_Time());
            DateTime date2 = (dateTimeOffset.DateTime).AddMinutes(-30);
            DateTime date1 = DateTime.Now;
            TimeSpan ts = date2 - date1;
            int ms = (int)ts.TotalMilliseconds;

            // Create a timer with interval in ms (edge case check if time is negative)
            Timer timer = new Timer();
            if (ms > 0)
            {
                timer = new Timer(ms);
                timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, curEvent);
                timer.Enabled = true;
            }
            else timer = null;

            // Add rectangle-event pair to dictionary
            textToEvent.Add(txtblk, curEvent);
            textToBlock.Add(txtblk, rec);
            textToTimer.Add(txtblk, timer);
        }
        // Open reminder pop-up when timer event occurs
        private void OnTimedEvent(Object source, ElapsedEventArgs e, Event curEvent)
        {
            // Put up pop-up window
            ReminderWindow reminder = new ReminderWindow();
            reminder.EventName.Text = curEvent.GetName();
            reminder.EventStart.Text = curEvent.GetDate_Time().ToString("HH:mm");
            reminder.Show();
        }
        // Clear Events From Display, Clear Events Stored in Temporary Memory
        private void ClearDisplayAndStoredEvents()
        {
            // Remove textboxes and rectangles from display
            foreach (KeyValuePair<TextBlock, Event> entry in textToEvent)
            {
                Scroll_Area.Children.Remove(entry.Key);
                Scroll_Area.Children.Remove(textToBlock[entry.Key]);
                textToEvent.Remove(entry.Key);
                textToBlock.Remove(entry.Key);
                textToTimer.Remove(entry.Key);
            }
            textToEvent = new Dictionary<TextBlock, Event>();
            textToBlock = new Dictionary<TextBlock, Rectangle>();
            textToTimer = new Dictionary<TextBlock, Timer>();
        }
        //Calculate corresponding height value (display attribute) to duration of Event (stored value)
        private int ConvertTimeToHeightNumber(DateTime inputTime)
        {
            int retNum = 0;
            String timeString = inputTime.ToString("HH:mm");
            String hourString1 = timeString.Substring(0, 2);
            String hourString2 = timeString.Substring(3);
            if (hourString1.Substring(0, 1) == "0") { hourString1 = hourString1.Substring(1); }
            if (hourString2.Substring(0, 1) == "0") { hourString2 = hourString2.Substring(1); }
            retNum = Convert.ToInt32(hourString1) * 4;
            retNum += Convert.ToInt32(hourString2) / 15; //in increments of 15

            return retNum;
        }



        //EVENT HANDLING (create, edit, delete)

        // Add Event to Calendar from User Input, Update Display if applicable
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            // Pop up Create Event window.
            CreateEventWindow createWin = new CreateEventWindow();
            createWin.ShowDialog();

            // Create event
            Event tempEvent = CreateEvent(ref createWin);

            // Check if end time is before start time - do not create event if so
            if (tempEvent.GetDuration() < 0) return;

            // Check if event within current week - if so, add to display
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tempEvent.GetDate_Time());
            DateTime inputDate = dateTimeOffset.DateTime;
            DateTime startDate = displayStartOfWeek;
            DateTime endDate = startDate.AddDays(7);
            if (startDate <= inputDate && inputDate < endDate) AddEventToDisplay(tempEvent);
        }
        // Create Event object from User Input
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
        // Edit Event in Calendar from User Input, Update Display
        private void EditEventClick(object sender, RoutedEventArgs e)
        {
            // Pop up Create Event window.
            EditEventWindow editWin = new EditEventWindow();

            // Retrieve event from list of events
            Event selectedEvent = textToEvent[(TextBlock)sender];

            // Retrieve event data corresponding to block selected
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(selectedEvent.GetDate_Time());
            DateTime dateTime = dateTimeOffset.DateTime;
            String dateTimeHour = dateTime.ToString();

            long epochFromDuration = (long)(selectedEvent.GetDuration() * 3600);
            DateTimeOffset dateTimeOffset2 = DateTimeOffset.FromUnixTimeSeconds(selectedEvent.GetDate_Time() + epochFromDuration);
            DateTime dateTime2 = dateTimeOffset2.DateTime;
            String dateTimeHour2 = dateTime2.ToString();

            // Set window parameters to old values
            editWin.TitleInput.Text = selectedEvent.GetName();
            editWin.TimeList1.Text = dateTime.ToString("hh:mm");
            editWin.TimeList2.Text = dateTime2.ToString("hh:mm");
            editWin.AMPM1.Text = dateTime.ToString("tt");
            editWin.AMPM2.Text = dateTime2.ToString("tt");
            editWin.PriorityList.Text = selectedEvent.GetPriority();
            editWin.FlexibilityList.SelectedIndex = selectedEvent.GetFlexibility() - 1;
            editWin.MonthList.Text = dateTime.ToString("MMM");
            editWin.DayList.Text = dateTime.ToString("dd");
            editWin.YearList.Text = dateTime.ToString("yyyy");
            editWin.ColorList.Text = selectedEvent.GetColor();
            editWin.DescriptionInput.Text = selectedEvent.GetDescription();

            editWin.ShowDialog();

            // Create new event (unless should be deleted OR duration is negative)
            if (editWin.deleteEventBool == false) AddEventToDisplay(EditEvent(ref editWin));

            // Delete old event
            Scroll_Area.Children.Remove((TextBlock)sender);
            Scroll_Area.Children.Remove(textToBlock[(TextBlock)sender]);
            textToEvent.Remove((TextBlock)sender);
            textToBlock.Remove((TextBlock)sender);
        }
        // Edit Event object from User Input
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
        // Convert User Input Date to Epoch Format (New Event Created)
        private long EpochTimeConversion(ref CreateEventWindow createWin)
        {
            DateTime tempDate = UserInputToDateTime(ref createWin, 1);

            //convert datetime to epoch
            TimeSpan t = tempDate - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
        // Convert User Input Date to Epoch Format (Edited Pre-existing Event)
        private long EpochTimeConversion(ref EditEventWindow editWin)
        {
            DateTime tempDate = UserInputToDateTime(ref editWin, 1);

            //convert datetime to epoch
            TimeSpan t = tempDate - new DateTime(1970, 1, 1);
            return (int)t.TotalSeconds;
        }
        // Find Duration of Event in terms of hours (New Event Created)
        private double HoursDifferenceConversion(ref CreateEventWindow createWin)
        {
            DateTime tempDate1 = UserInputToDateTime(ref createWin, 1);
            DateTime tempDate2 = UserInputToDateTime(ref createWin, 2);

            //find difference between end and start, convert from seconds to hours
            TimeSpan t = tempDate2 - tempDate1;
            return ((double)t.TotalSeconds) / 3600;
        }
        // Find Duration of Event in terms of hours (Edited Pre-existing Event)
        private double HoursDifferenceConversion(ref EditEventWindow editWin) //SIMPLIFY 12to24 CONVERTER - DEBUG
        {
            DateTime tempDate1 = UserInputToDateTime(ref editWin, 1);
            DateTime tempDate2 = UserInputToDateTime(ref editWin, 2);

            //find difference between end and start, convert from seconds to hours
            TimeSpan t = tempDate2 - tempDate1;
            return ((double)t.TotalSeconds) / 3600;
        }

        private DateTime UserInputToDateTime(ref CreateEventWindow createWin, int startOrEnd)
        {
            {
                //convert from 12 to 24 hour time (based on either start or end time)
                String time12To24;
                int temp;
                if (startOrEnd == 1)
                {
                    if (createWin.AMPM1.Text == "AM")
                    {
                        time12To24 = createWin.TimeList1.Text;
                        if (time12To24.Substring(0, 2) == "12") time12To24 = "00" + createWin.TimeList1.Text.Substring(2);
                    }
                    else
                    {
                        int.TryParse(createWin.TimeList1.Text.Substring(0, 2), out temp);
                        time12To24 = ((temp + 12) % 24).ToString() + createWin.TimeList1.Text.Substring(2);
                    }
                }
                else
                {
                    if (createWin.AMPM2.Text == "AM")
                    {
                        time12To24 = createWin.TimeList2.Text;
                        if (time12To24.Substring(0, 2) == "12") time12To24 = "00" + createWin.TimeList2.Text.Substring(2);
                    }
                    else
                    {
                        int.TryParse(createWin.TimeList2.Text.Substring(0, 2), out temp);
                        time12To24 = ((temp + 12) % 24).ToString() + createWin.TimeList2.Text.Substring(2);
                    }
                }

                //convert 0 to 12 in hour
                if (time12To24.Substring(0, 2) == "0:") time12To24 = "12" + time12To24.Substring(1);

                //convert user input to date
                String MonthListString = (createWin.MonthList.SelectedIndex + 1).ToString();
                if ((createWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (createWin.MonthList.SelectedIndex + 1).ToString(); }
                String DayListString = (createWin.DayList.SelectedIndex + 1).ToString();
                if ((createWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (createWin.DayList.SelectedIndex + 1).ToString(); }
                String dateTimeString = createWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";

                //return final date
                return DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private DateTime UserInputToDateTime(ref EditEventWindow editWin, int startOrEnd)
        {
            //convert from 12 to 24 hour time (based on either start or end time)
            String time12To24;
            int temp;
            if (startOrEnd == 1)
            {
                if (editWin.AMPM1.Text == "AM")
                {
                    time12To24 = editWin.TimeList1.Text;
                    if (time12To24.Substring(0, 2) == "12") time12To24 = "00" + editWin.TimeList1.Text.Substring(2);
                }
                else
                {
                    int.TryParse(editWin.TimeList1.Text.Substring(0, 2), out temp);
                    time12To24 = ((temp + 12) % 24).ToString() + editWin.TimeList1.Text.Substring(2);
                }
            }
            else
            {
                if (editWin.AMPM2.Text == "AM")
                {
                    time12To24 = editWin.TimeList2.Text;
                    if (time12To24.Substring(0, 2) == "12") time12To24 = "00" + editWin.TimeList2.Text.Substring(2);
                }
                else
                {
                    int.TryParse(editWin.TimeList2.Text.Substring(0, 2), out temp);
                    time12To24 = ((temp + 12) % 24).ToString() + editWin.TimeList2.Text.Substring(2);
                }
            }

            //convert 0 to 12 in hour
            if (time12To24.Substring(0, 2) == "0:") time12To24 = "12" + time12To24.Substring(1);

            //convert user input to date
            String MonthListString = (editWin.MonthList.SelectedIndex + 1).ToString();
            if ((editWin.MonthList.SelectedIndex + 1).ToString().Length == 1) { MonthListString = "0" + (editWin.MonthList.SelectedIndex + 1).ToString(); }
            String DayListString = (editWin.DayList.SelectedIndex + 1).ToString();
            if ((editWin.DayList.SelectedIndex + 1).ToString().Length == 1) { DayListString = "0" + (editWin.DayList.SelectedIndex + 1).ToString(); }
            String dateTimeString = editWin.YearList.Text + "-" + MonthListString + "-" + DayListString + " " + time12To24 + ":00";

            //return final date
            return DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }


    //CLASSES

    // Event Classes
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


        protected private double duration; //in terms of hours
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

    // Week Class
    public class Week
    {
        protected private static List<Event> mon = new List<Event>(); //lists of events
        protected private static List<Event> tue = new List<Event>();
        protected private static List<Event> wed = new List<Event>();
        protected private static List<Event> thu = new List<Event>();
        protected private static List<Event> fri = new List<Event>();
        protected private static List<Event> sat = new List<Event>();
        protected private static List<Event> sun = new List<Event>();

        protected private List<List<Event>> a_week = new List<List<Event>>() { mon, tue, wed, thu, fri, sat, sun };
        protected private long date;

        public long GetDate()
        {
            return date;
        }

        public void SetDate(long inputDate)
        {
            date = inputDate;
        }

        public List<List<Event>> GetWeek()
        {
            return a_week;
        }

        public void AddEvent(Event event1, int day)
        {
            a_week[day].Add(event1);
        }

        public void DeleteEvent(Event event1, int day)
        {
            a_week[day].Remove(event1);
        }
    }


    // Calendar Class

    class Calendar
    {
        private Dictionary<long, Week> weeks;

        Week GetWeek(long key)
        {
            return weeks[key];
        }

        public void AddWeek(Week week)
        {
            weeks.Add(week.GetDate(), week); //DEBUG - not an actual function in "Week"
        }

        public void RemoveWeek(long key)
        {
            weeks.Remove(key);
        }

        public void SetWeek(long key, Week week)
        {
            weeks[key] = week;
        }

        public void AddEvent(Event inputEvent, long date)
        {
            if (weeks.ContainsKey(date))
            {
                weeks[date].AddEvent(inputEvent, EpochToWeekday(inputEvent.GetDate_Time()));
            }
            else
            {
                Week newWeek = new Week();
                newWeek.AddEvent(inputEvent, EpochToWeekday(inputEvent.GetDate_Time()));
                weeks[date] = newWeek;
            }
        }

        public void DeleteEvent(Event inputEvent)
        {
            weeks[inputEvent.GetDate_Time()].DeleteEvent(inputEvent, EpochToWeekday(inputEvent.GetDate_Time()));
        }

        public void AddEvent(Event inputEvent)
        {
            weeks[inputEvent.GetDate_Time()].AddEvent(inputEvent, EpochToWeekday(inputEvent.GetDate_Time()));
        }

        private int EpochToWeekday(long inputDate)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(inputDate);
            return (int)(dateTimeOffset.DateTime).DayOfWeek;
        }
        
        public void SaveXML(String filePath)  
        {  
        
            System.Xml.Serialization.XmlSerializer saver = new System.Xml.Serialization.XmlSerializer(typeof(Calender));  
            System.IO.FileStream file = System.IO.File.Create(filePath);  
            saver.Serialize(file, this);  
            file.Close();  
        }  


        public void LoadXML(String filePath)  
        {  

            System.Xml.Serialization.XmlSerializer loader = new System.Xml.Serialization.XmlSerializer(typeof(Calander));  
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);  
            this = (Calander)reader.Deserialize(file);  
            file.Close();  

        } 
    }
}
