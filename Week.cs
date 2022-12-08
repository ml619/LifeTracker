    // Week Class
    public class Week
    {
        public List<Event> mon; // Lists of events (public for sake of serialization)
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
            mon = new List<Event>(); // Lists of events
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
            mon = new List<Event>(); // Lists of events
            tue = new List<Event>();
            wed = new List<Event>();
            thu = new List<Event>();
            fri = new List<Event>();
            sat = new List<Event>();
            sun = new List<Event>();
        }

        public long GetDate() // Return epoch date defining the week (Monday of week)
        {
            return date;
        }
        public void SetDate(long inputDate)
        {
            date = inputDate;
        }

        public List<List<Event>> GetWeek() // Return list (week) of lists (days) of events
        {
            return a_week;
        }

        public void AddEvent(Event event1, int day) // Add individual event into week
        {
            a_week[day].Add(event1);
        }
        public void DeleteEvent(Event event1, int day) // Remove individual event from week
        {
            a_week[day].Remove(event1);
        }

        public void ExportAvailability() // Export times of events into text file
        {
            // Create a file to write to (as applicable)
            string fileName = "Availability.txt";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Write to file
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
            // Open file once written to
            Process.Start(new ProcessStartInfo { FileName = fileName, UseShellExecute = true });
        }
    }
