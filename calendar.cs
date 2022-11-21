class Calendar
{
    private Dictionary<long, Week> weeks;

    Week GetWeek(long key)
    {
        return weeks[key];
    }

    // Event getEvent(int key)
    // {
    //     return weeks[key];
    // }

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

    public Event GetSuggestion(Event inevent, Week week)
    {
        Dictionary<long, Event> scores = new Dictionary<long, Event>();
        long highestScore = 0;
        foreach (List<Event> day in week)
        {
            Event tryevent = new Event();
            tryevent.SetName(inevent.GetName());

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(inevent.GetDate_Time());
            DateTime dateTime = dateTimeOffset.DateTime;
            dateTime.AddHours(1.0);
            TimeSpan t = dateTime - new DateTime(1970, 1, 1);
            long curWeekEpoch = (long)t.TotalSeconds;
            tryevent.SetDate_Time(curWeekEpoch);

            tryevent.SetFlexibility(inevent.GetFlexibility());
            tryevent.SetColor(inevent.GetColor());
            tryevent.SetPriority(inevent.GetPriority());
            tryevent.SetDescription(inevent.GetDescription());

            while (dateTime.Hour < 24)
            {
                long newScore = calculateScore(tryevent, day);
                scores.Add(newScore, tryevent);

                if (newScore > highestScore)
                    highestScore = newScore;

                dateTime.AddHours(1.0);
                t = dateTime - new DateTime(1970, 1, 1);
                curWeekEpoch = (long)t.TotalSeconds;
                tryevent.SetDate_Time(curWeekEpoch);
            }
        }
        return scores[highestScore];
    }

    private long calculateScore(Event inevent, List<Event> day)
    {
        //my fancy algorithm
        long score;

        long dateScore = inevent.GetDate_Time() - Math.Abs(inevent.GetDate_Time().Hour - 12);

        int priorityScore;
        if (inevent.GetPriority() == "LOW")
            priorityScore = 3;
        else if (inevent.GetPriority() == "MED")
            priorityScore = 2;
        else
            priorityScore = 1;

        int flexScore = inevent.GetFlexibility();

        int locationScore = 0;
        //if(event.GetType() == typeof (location))
        foreach (Event anevent in day)
            if (anevent.GetLocation().Equals(inevent.GetLocation()))
                locationScore++;

        score = dateScore + priorityScore + flexScore + locationScore;

        return score;
    }
}
