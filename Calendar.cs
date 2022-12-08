    // Calendar Class
    public class Calendar
    {
        [JsonProperty]
        private Dictionary<long, Week> weeks = new Dictionary<long, Week>();

        public Week GetWeek(long key) // Return week by entering epoch value corresponding to Monday of that week
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

        public void AddWeek(Week week) // Add week (given week data)
        {
            if (!weeks.ContainsKey(week.GetDate())) weeks.Add(week.GetDate(), week);
        }
        public void RemoveWeek(long key) // Remove week from dictionary (given epoch date for Monday)
        {
            weeks.Remove(key);
        }
        public void SetWeek(long key, Week week) // Update week value (given epoch date for Monday & week data)
        {
            weeks[key] = week;
        }

        public void AddEvent(Event inputEvent, long date) // Add event, corresponding start-of-week in epoch format
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
        public void DeleteEvent(Event inputEvent) // Delete a specific event from week
        {
            weeks[inputEvent.GetDate_Time()].DeleteEvent(inputEvent, EpochToWeekday(inputEvent.GetDate_Time()));
        }
        public void AddEvent(Event inputEvent) // Add event (given just event)
        {
            weeks[inputEvent.GetDate_Time()].AddEvent(inputEvent, EpochToWeekday(inputEvent.GetDate_Time()));
        }

        private int EpochToWeekday(long inputDate) // Convert epoch long to integer (representing day of week)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(inputDate);
            return (int)(dateTimeOffset.DateTime).DayOfWeek;
        }

        public void Save(String filePath) // Save calendar class to XML file
        {
            // Check if the file exists, create new one if not
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }


            // Serialize data
            string serializedCalander = JsonConvert.SerializeObject(this);
            File.WriteAllText(filePath, serializedCalander);
        }
        public Calendar Load(String filePath) // Load calendar class from XML file
        {
            // Check if the file exists, create new one if not
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            // Deserialize data
            string serializedCalander = File.ReadAllText(filePath);
            Calendar deserializedCalander = JsonConvert.DeserializeObject<Calendar>(serializedCalander);

            return deserializedCalander;
        }

        public Event GetSuggestion(Event inevent, Week week) // Suggest new event time / date
        {
            Dictionary<Event, long> scores = new Dictionary<Event, long>();
            Event bestEvent = inevent;
            long highestScore = 0;

            // Iterate through all days in current week
            foreach (List<Event> day in week.GetWeek())
            {
                Event tryevent = new Event();
                tryevent.SetName(inevent.GetName());

                // Convert to epoch value for next hour
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(inevent.GetDate_Time());
                DateTime dateTime = dateTimeOffset.DateTime;
                dateTime.AddHours(1.0);
                TimeSpan t = dateTime - new DateTime(1970, 1, 1);
                long curWeekEpoch = (long)t.TotalSeconds;


                // Set new event values
                tryevent.SetDate_Time(curWeekEpoch);
                tryevent.SetFlexibility(inevent.GetFlexibility());
                tryevent.SetColor(inevent.GetColor());
                tryevent.SetPriority(inevent.GetPriority());
                tryevent.SetDescription(inevent.GetDescription());
                tryevent.SetDuration(inevent.GetDuration());
                tryevent.SetLocation(inevent.GetLocation());

                // Calculate score (highest score is suggested timing)
                int currDay = dateTime.Day;
                int test = 0;
                while (dateTime.Day == currDay)
                {
                    long newScore = CalculateScore(tryevent, day);
                    if (scores.ContainsKey(tryevent) == false)
                        scores.Add(tryevent, newScore);

                    if (newScore > highestScore)
                    {
                        highestScore = newScore;
                        bestEvent = tryevent;
                    }

                    test++;
                    dateTime = dateTime.AddHours(1.0);
                    t = dateTime - new DateTime(1970, 1, 1);
                    curWeekEpoch = (long)t.TotalSeconds;
                    tryevent.SetDate_Time(curWeekEpoch);
                }
            }
            return bestEvent;
        }

        // Calculate score for best new event time
        private long CalculateScore(Event inevent, List<Event> day)
        {
            long score;

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(inevent.GetDate_Time());
            DateTime dateTime = dateTimeOffset.DateTime;
            long dateScore = inevent.GetDate_Time() - Math.Abs(dateTime.Hour - 12);

            // Algorithm factoring event data
            int priorityScore;
            if (inevent.GetPriority() == "LOW")
                priorityScore = 3;
            else if (inevent.GetPriority() == "MED")
                priorityScore = 2;
            else
                priorityScore = 1;

            int flexScore = inevent.GetFlexibility();

            int locationScore = 0;
            foreach (Event anevent in day)
                if (anevent.GetLocation().Equals(inevent.GetLocation()))
                    locationScore++;

            score = dateScore + priorityScore + flexScore + locationScore;

            return score;
        }
    }
