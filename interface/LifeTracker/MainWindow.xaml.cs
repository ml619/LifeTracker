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
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;


namespace LifeTracker
{
    public partial class MainWindow : Window
    {
        // Public variables
        public Dictionary<TextBlock, Event> textToEvent = new Dictionary<TextBlock, Event>();
        public Dictionary<TextBlock, Rectangle> textToBlock = new Dictionary<TextBlock, Rectangle>();
        public Dictionary<TextBlock, System.Windows.Threading.DispatcherTimer> textToTimer = new Dictionary<TextBlock, System.Windows.Threading.DispatcherTimer>();

        public DateTime displayStartOfWeek = DateTime.Today;
        public Week currentWeek = new Week();
        public Calendar calendar = new Calendar();

        string saveFileName = "Storage.xml";

        bool muteCheck = false;

        bool suggestMode = false;
        Event suggestedEvent = null;
        Event originalEvent = null;

        public TaskCompletionSource<bool> tcs1 = null;
        public TaskCompletionSource<bool> tcs2 = null;


        // MainWindow Initialization
        public MainWindow()
        {
            InitializeComponent();
            SelectDisplayWeek.SelectedDate = DateTime.Today;

            //load JSON data
            calendar.LoadXML(saveFileName);

            //set current week to corresponding data in calendar
            TimeSpan t = FindNearestMonday(DateTime.Today) - new DateTime(1970, 1, 1);
            long curWeekEpoch = (long)t.TotalSeconds;
            currentWeek = calendar.GetWeek(curWeekEpoch);

            // Load relevant week data to display and stored data
            t = displayStartOfWeek - new DateTime(1970, 1, 1);
            currentWeek = calendar.GetWeek((long)t.TotalSeconds);

            for (int i = 0; i < 7; i++)
            {
                List<Event> dayInWeek = currentWeek.GetWeek()[i];
                for (int j = 0; j < dayInWeek.Count; j++)
                {
                    AddEventToDisplay(dayInWeek[j]);
                }
            }

            //set accept/reject buttons to default hidden (off screen)
            AcceptSuggestionButton.Margin = new Thickness(-100, -100, 0, 0);
            RejectSuggestionButton.Margin = new Thickness(-100, -100, 0, 0);
        } 

        // Mute Window
        private void MuteButton_Click(object sender, RoutedEventArgs e)
        {
            //toggle bool
            muteCheck = !muteCheck;
            //update button text
            if (muteCheck) MuteButton.Content = "Unmute";
            else MuteButton.Content = "Mute";
        }
        // Close Window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            calendar.SaveXML(saveFileName);
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
            // Save current week data to calendar & JSON
            TimeSpan t = displayStartOfWeek - new DateTime(1970, 1, 1);
            calendar.RemoveWeek((long)t.TotalSeconds);
            calendar.AddWeek(currentWeek);
            //calendar.SaveXML(saveFileName); DEBUG

            // Set week with Monday at the start.
            displayStartOfWeek = FindNearestMonday(SelectDisplayWeek.SelectedDate.Value);
            UpdateDisplayDates(displayStartOfWeek);
            // (use this to access events from JSON)
            long epochVal = (new DateTimeOffset(displayStartOfWeek)).ToUniversalTime().ToUnixTimeMilliseconds();

            // Clear current displayed and stored events
            ClearDisplayAndStoredEvents();

            // Load relevant week data to display and stored data
            t = displayStartOfWeek - new DateTime(1970, 1, 1);
            currentWeek = calendar.GetWeek((long)t.TotalSeconds);

            for (int i = 0; i < 7; i++)
            {
                List<Event> dayInWeek = currentWeek.GetWeek()[i];
                for (int j = 0; j < dayInWeek.Count; j++)
                {
                    AddEventToDisplay(dayInWeek[j]);
                }
            }
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
            // Save current week data to calendar & JSON
            TimeSpan t = displayStartOfWeek - new DateTime(1970, 1, 1);
            calendar.RemoveWeek((long)t.TotalSeconds);
            calendar.AddWeek(currentWeek);
            //calendar.SaveXML(saveFileName); DEBUG

            // Update display and week being accessed.
            displayStartOfWeek = displayStartOfWeek.AddDays(7);
            UpdateDisplayDates(displayStartOfWeek);

            // Clear current displayed and stored events
            ClearDisplayAndStoredEvents();

            // Load relevant week data to display and stored data
            t = displayStartOfWeek - new DateTime(1970, 1, 1);
            currentWeek = calendar.GetWeek((long)t.TotalSeconds);

            for (int i = 0; i < 7; i++)
            {
                List<Event> dayInWeek = currentWeek.GetWeek()[i];
                for (int j = 0; j < dayInWeek.Count; j++)
                {
                    AddEventToDisplay(dayInWeek[j]);
                }
            }
        }
        // Change Date Displayed (Left Arrow)
        private void MoveWeekBackward(object sender, RoutedEventArgs e)
        {
            // Save current week data to calendar & JSON
            TimeSpan t = displayStartOfWeek - new DateTime(1970, 1, 1);
            calendar.RemoveWeek((long)t.TotalSeconds);
            calendar.AddWeek(currentWeek);
            //calendar.SaveXML(saveFileName); DEBUG

            // Update display and week being accessed.
            displayStartOfWeek = displayStartOfWeek.AddDays(-7);
            UpdateDisplayDates(displayStartOfWeek);

            // Clear current displayed and stored events
            ClearDisplayAndStoredEvents();

            // Load relevant week data to display and stored data
            t = displayStartOfWeek - new DateTime(1970, 1, 1);
            currentWeek = calendar.GetWeek((long)t.TotalSeconds);

            for (int i = 0; i < 7; i++)
            {
                List<Event> dayInWeek = currentWeek.GetWeek()[i];
                for (int j = 0; j < dayInWeek.Count; j++)
                {
                    AddEventToDisplay(dayInWeek[j]);
                }
            }
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
            //Add event into week object
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(curEvent.GetDate_Time());

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
            dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(curEvent.GetDate_Time());
            DateTime date2 = (dateTimeOffset.DateTime).AddMinutes(-30);
            DateTime date1 = DateTime.Now;
            TimeSpan ts = date2 - date1;
            int ms = (int)ts.TotalMilliseconds;

            // Create a timer with interval in ms (edge case check if time is negative)
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            if (ms > 0)
            {
                timer.Tick += (sender, e) => OnTimedEvent(sender, e, curEvent);
                timer.Interval = new TimeSpan(0, 0, 0, 0, ms);
                timer.Start();
            }
            else timer = null;

            // Add rectangle-event pair to dictionary
            textToEvent.Add(txtblk, curEvent);
            textToBlock.Add(txtblk, rec);
            textToTimer.Add(txtblk, timer);
        }
        // Open reminder pop-up when timer event occurs
        private void OnTimedEvent(Object source, EventArgs e, Event curEvent)
        {
            // Put up pop-up window
            if (!muteCheck)
            {
                ReminderWindow reminder = new ReminderWindow();
                reminder.EventName.Text = curEvent.GetName();

                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(curEvent.GetDate_Time());
                reminder.EventStart.Text = (dateTimeOffset.DateTime).ToString("HH:mm");

                reminder.EventLocation.Text = curEvent.GetLocation().ToString(); //DEBUG
                reminder.Show();
            }
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
            textToTimer = new Dictionary<TextBlock, System.Windows.Threading.DispatcherTimer>();

            currentWeek = new Week();
            currentWeek.ClearWeek();
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
            if (startDate <= inputDate && inputDate < endDate)
            {
                //Add event into week object
                dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tempEvent.GetDate_Time());
                DateTime dateTime = dateTimeOffset.DateTime;
                currentWeek.AddEvent(tempEvent, (int)dateTime.DayOfWeek);

                //Add event to display
                AddEventToDisplay(tempEvent);
            }
            else
            {
                //Look for correct week to add event to, do NOT add to current display
                DateTime dateTime = dateTimeOffset.DateTime;

                int curWeekDayNum = (int)(dateTime.DayOfWeek + 6) % 7;

                int tempDateVal = (int)(tempEvent.GetDate_Time() - (tempEvent.GetDate_Time() % 86400)); //get in terms of just day (no hours, auto start at 12)
                dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tempDateVal);
                DateTime adjustedDate = dateTimeOffset.DateTime;

                long mondayEpoch = (long)(adjustedDate.AddDays(-curWeekDayNum) - new DateTime(1970, 1, 1)).TotalSeconds;
                calendar.AddEvent(tempEvent, mondayEpoch);
            }

            //If recurring event, add to subsequent weeks afterwards
            if (tempEvent.GetType() == typeof(Recurring))
            {
                //increment by step until fulfilled all instances (minus current one)
                for(int i = 0; i < ((Recurring)tempEvent).GetNumInstances() - 1; i++)
                {
                    //Look for correct week to add event to, do NOT add to current display
                    DateTime dateTime = (dateTimeOffset.DateTime).AddDays(7 * ((Recurring)tempEvent).GetStep());

                    int curWeekDayNum = (int)(dateTime.DayOfWeek + 6) % 7;

                    int tempDateVal = (int)(tempEvent.GetDate_Time() - (tempEvent.GetDate_Time() % 86400)); //get in terms of just day (no hours, auto start at 12)
                    dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(tempDateVal);
                    DateTime adjustedDate = dateTimeOffset.DateTime;

                    long mondayEpoch = (long)(adjustedDate.AddDays(-curWeekDayNum) - new DateTime(1970, 1, 1)).TotalSeconds; //DEBUG <----- monday of the first event, not changing for some reason
                    calendar.AddEvent(tempEvent, mondayEpoch);
                }
            }
        }
        // Create Event object from User Input
        private Event CreateEvent(ref CreateEventWindow createWin)
        {
            Event retEvent = new Event();

            //check whether recurring event or not
            if (!(bool)createWin.RecurringCheck.IsChecked) retEvent = new Event();
            else
            {
                Recurring tempEvent = new Recurring();
                tempEvent.SetNumInstances(Int32.Parse(createWin.NumInstancesInput.Text));
                tempEvent.SetStep(Int32.Parse(createWin.StepInput.Text));

                retEvent = tempEvent;
            }

            //duration stored in terms of hours (i.e. 1 hours, 1.25 hours, etc.)
            retEvent.SetName(createWin.TitleInput.Text);
            retEvent.SetDate_Time(EpochTimeConversion(ref createWin));
            retEvent.SetDuration(HoursDifferenceConversion(ref createWin));
            retEvent.SetColor(createWin.ColorList.Text);
            retEvent.SetFlexibility(createWin.FlexibilityList.SelectedIndex + 1);
            retEvent.SetPriority(createWin.PriorityList.Text);
            retEvent.SetDescription(createWin.DescriptionInput.Text);

            retEvent.SetLocation(createWin.LocationInput.Text); //DEBUG

            return retEvent;
        }
        // Edit Event in Calendar from User Input, Update Display
        private void EditEventClick(object sender, RoutedEventArgs e)
        {
            if (suggestMode) //check if in suggest mode
            {
                //set original event
                originalEvent = textToEvent[(TextBlock)sender];

                //mark that an event has been clicked
                tcs1?.TrySetResult(true);

                return; //exit (do not carry out edit event protocol)
            }

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

            editWin.LocationInput.Text = selectedEvent.GetLocation(); //DEBUG

            editWin.ShowDialog();

            // Create new event (unless should be deleted OR duration is negative)
            if (editWin.deleteEventBool == false)
            {
                //Add event into week object
                dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(EditEvent(ref editWin).GetDate_Time());
                dateTime = dateTimeOffset.DateTime;
                currentWeek.AddEvent(EditEvent(ref editWin), (int)dateTime.DayOfWeek);

                //Add event to display
                AddEventToDisplay(EditEvent(ref editWin));
            }

            // Delete old event
            dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(selectedEvent.GetDate_Time());
            dateTime = dateTimeOffset.DateTime;
            currentWeek.DeleteEvent(selectedEvent, (int)dateTime.DayOfWeek);

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

            retEvent.SetLocation(editWin.LocationInput.Text); //DEBUG

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

        private void AvailabilityButtonClick(object sender, RoutedEventArgs e)
        {
            currentWeek.ExportAvailability();
        }

        private async void RescheduleButtonClick(object sender, RoutedEventArgs e)
        {
            if (suggestMode)
            {
                //mark that exiting reschedule mode
                tcs2?.TrySetResult(true);
                suggestMode = false;

                //hide buttons again, clear suggest event, revert display background color
                AcceptSuggestionButton.Margin = new Thickness(-100, -100, 0, 0);
                RejectSuggestionButton.Margin = new Thickness(-100, -100, 0, 0);
                suggestedEvent = null;
                originalEvent = null;
                MainBackground.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8E5FFE4");

                return;
            }

            suggestMode = true;

            //change display background color
            MainBackground.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8126C0F");

            //wait for event to be selected
            tcs1 = new TaskCompletionSource<bool>();
            await tcs1.Task;

            //add suggested event to display (NOT to week object yet)
            suggestedEvent = GetSuggestion(originalEvent, ref calendar); //DEBUG <-- ADD IN THE ACTUAL FUNCTION

            //add accept/reject buttons to display, do not allow any other actions until one is clicked]
            //Set x margin to correspond to day of week
            DateTime datetime = DateTimeOffset.FromUnixTimeSeconds(suggestedEvent.GetDate_Time()).DateTime;
            int x_margin = 100 * ((int)datetime.DayOfWeek - 1) + 6;
            if (x_margin < 0) x_margin = 606;

            //Set y_margin to correspond to time during day
            //each 1/4 hour = 8 units
            int y_margin = 8 * (ConvertTimeToHeightNumber(datetime)) + 11;

            AcceptSuggestionButton.Margin = new Thickness(x_margin + 43, y_margin + 3, 0, 0);
            RejectSuggestionButton.Margin = new Thickness(x_margin + 69, y_margin + 3, 0, 0);

            //wait for accept/reject to be pressed
            tcs2 = new TaskCompletionSource<bool>();
            await tcs2.Task;

            //hide buttons again, clear suggest event, revert display background color
            AcceptSuggestionButton.Margin = new Thickness(-100, -100, 0, 0);
            RejectSuggestionButton.Margin = new Thickness(-100, -100, 0, 0);
            suggestedEvent = null;
            originalEvent = null;
            MainBackground.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#E8E5FFE4");
        }
        private Event GetSuggestion(Event originalEvent, ref Calendar calendar)
        {
            return originalEvent; //DEBUG <---ADD THE ACTUAL FUNCTIONALITY
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            //mark that an accept has been chosen
            tcs2?.TrySetResult(true);

            //add suggested event to week DEBUG

            //remove original event (from display and week)


            suggestMode = false;
        }
        private void Reject_Button_Click(object sender, RoutedEventArgs e)
        {
            //mark that an reject has been chosen
            tcs2?.TrySetResult(true);

            //remove suggested event from display DEBUG


            suggestMode = false;
        }
    }


    //CLASSES

    // Event Classes
    public class Event //description, priority, time, color, flexibility
    {

        public string name; // name of event
        public string GetName()
        {
            return name;
        }
        public void SetName(string Name)
        {
            name = Name;
        }


        public long date_time; //date and time in epoch
        public long GetDate_Time()
        {
            return date_time;
        }
        public void SetDate_Time(long Date_Time)
        {
            date_time = Date_Time;
        }


        public int flexibility;
        public int GetFlexibility()
        {
            return flexibility;
        }
        public void SetFlexibility(int Flexibility)
        {
            flexibility = Flexibility;
        }


        public string color; // color of event
        public string GetColor()
        {
            return color;
        }
        public void SetColor(string Color)
        {
            color = Color;
        }


        public double duration; //in terms of number of hours (NOT epoch)
        public double GetDuration()
        {
            return duration;
        }
        public void SetDuration(double Duration)
        {
            duration = Duration;
        }


        public string priority;
        public string GetPriority()
        {
            return priority;
        }
        public void SetPriority(string Priority)
        {
            priority = Priority;
        }


        public string description; //short description of acitiviy 
        public string GetDescription()
        {
            return description;
        }
        public void SetDescription(string Description)
        {
            description = Description;
        }






        //DEBUG
        public string location;
        public string GetLocation()
        {
            return location;
        }
        public void SetLocation(string Location)
        {
            location = Location;
        }
    }

    /*class location : Event //DEBUG
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

    }*/




    //from week that initial one is inserted on, traverse forwards until the end day, by step, adding to the individual calendars on that week
    //if week doesn't exist, should be added (keep track of the mondays) DEBUG



    class Recurring : Event
    {
        public int numInstances; // how many events there are //DEBUG <---- CHANGED FROM THE CLASS DIAGRAM
        public long GetNumInstances()
        {
            return numInstances;
        }
        public void SetNumInstances(int NumInstances)
        {
            numInstances = NumInstances;
        }

        public int step; //how often 
        public int GetStep()
        {
            return step;
        }
        public void SetStep(int Step)
        {
            step = Step;
        }
    }

    // Week Class
    public class Week
    {
        public List<Event> mon; //lists of events       DEBUG <--- EVERYTHING PUBLIC SO IT CAN BE SERIALIZED
        public List<Event> tue;
        public List<Event> wed;
        public List<Event> thu;
        public List<Event> fri;
        public List<Event> sat;
        public List<Event> sun;

        public List<List<Event>> a_week;
        public long date;

        public Week()
        {
            mon = new List<Event>(); //lists of events       DEBUG <--- EVERYTHING PUBLIC SO IT CAN BE SERIALIZED
            tue = new List<Event>();
            wed = new List<Event>();
            thu = new List<Event>();
            fri = new List<Event>();
            sat = new List<Event>();
            sun = new List<Event>();

            a_week = new List<List<Event>>() { mon, tue, wed, thu, fri, sat, sun };
        }

        public void ClearWeek()
        {
            mon = new List<Event>(); //lists of events
            tue = new List<Event>();
            wed = new List<Event>();
            thu = new List<Event>();
            fri = new List<Event>();
            sat = new List<Event>();
            sun = new List<Event>();
        }

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
        public void ExportAvailability()
        {
            // create a file to write to 
            string fileName = "Availability.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (StreamWriter sw = File.CreateText(fileName))
            {
                for (int i = 0; i <= 6; i++)
                {
                    List<string> days = new List<string>
                    { "Monday\n", "\n\nTuesday\n", "\n\nWednesday\n", "\n\nThursday\n", "\n\nFriday\n", "\n\nSaturday\n", "\n\nSunday\n" };
                    sw.WriteLine(days[i]);
                    for (int j = 0; j < a_week[(i + 1) % 7].Count; j++)
                    {
                        long epochSeconds = a_week[(i + 1) % 7][j].GetDate_Time();
                        long epochDuration = (long)(a_week[(i + 1) % 7][j].GetDuration() * 3600);

                        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);
                        DateTime dateTime = dateTimeOffset.DateTime;
                        string startTime = dateTime.ToString("HH:mm");

                        long endtimeepoch = epochSeconds + epochDuration;
                        dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(endtimeepoch);
                        dateTime = dateTimeOffset.DateTime;
                        string endTime = dateTime.ToString("HH:mm");

                        sw.WriteLine(startTime + " to " + endTime + ",");
                    }
                }
            }

            //open file once written to
            Process.Start(new ProcessStartInfo { FileName = fileName, UseShellExecute = true });
        }
    }


    // Calendar Class

    public class Calendar
    {
        [XmlIgnore]
        private Dictionary<long, Week> weeks = new Dictionary<long, Week>();

        public List<SerializeableKeyValue<long, Week>> weeksSerializeable = new List<SerializeableKeyValue<long, Week>>(); //DEBUG <--- make this match the rest of the code

        public Week GetWeek(long key)
        {
            if (!weeks.ContainsKey(key))
            {
                Week newWeek = new Week();
                newWeek.SetDate(key);
                this.AddWeek(newWeek);
                return newWeek;
            }
            return weeks[key];
        }

        public void AddWeek(Week week)
        {
            if (!weeks.ContainsKey(week.GetDate())) weeks.Add(week.GetDate(), week);
        }

        public void RemoveWeek(long key)
        {
            weeks.Remove(key);
        }

        public void SetWeek(long key, Week week)
        {
            weeks[key] = week;
        }

        public void AddEvent(Event inputEvent, long date) //event, corresponding start-of-week in epoch format
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
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            //convert dictionary to serializable list
            weeksSerializeable = new List<SerializeableKeyValue<long, Week>>();
            foreach (KeyValuePair<long, Week> p in weeks)
            {
                weeksSerializeable.Add(new SerializeableKeyValue<long, Week>(p.Key, p.Value));
            }

            System.Xml.Serialization.XmlSerializer saver = new System.Xml.Serialization.XmlSerializer(typeof(Calendar));
            System.IO.FileStream file = System.IO.File.Create(filePath);
            saver.Serialize(file, this);
            file.Close();
        }
        public Calendar LoadXML(String filePath)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            if (new FileInfo(filePath).Length == 0) return new Calendar();
            else
            {
                System.Xml.Serialization.XmlSerializer loader = new System.Xml.Serialization.XmlSerializer(typeof(Calendar));
                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                var calendar = (Calendar)loader.Deserialize(file);
                file.Close();

                weeksSerializeable = calendar.weeksSerializeable;

                //convert serializable list to dictionary
                weeks = new Dictionary<long, Week>();
                //weeks = weeksSerializeable.ToDictionary(x => x.Key, x => x.Value); //DEBUG
                foreach (SerializeableKeyValue<long, Week> p in weeksSerializeable)
                {
                    if (!weeks.ContainsKey(p.Key)) weeks.Add(p.Key, p.Value);
                }

                return calendar;
            }
        }
    }
    public class SerializeableKeyValue<T1, T2> //DEBUG <--- ADD TO CLASS DIAGRAMS!!!!!
    {
        public T1 Key { get; set; }
        public T2 Value { get; set; }
        public SerializeableKeyValue() { }
        public SerializeableKeyValue(T1 t1, T2 t2)
        {
            Key = t1;
            Value = t2;
        }
    }
}