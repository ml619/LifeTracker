using System;

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

    public Event reschedule(Event event, Week week)
    {
        Dictionary<Event,long> scores = new Dictionary<Event,long>();
        foreach(List<Event> day in week)
        {
            Event tryevent = new Event();
            tryevent.SetName(event.GetName());

            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(event.GetDate_Time());
            DateTime dateTime = dateTimeOffset.DateTime;
            dateTime.AddHours(1.0);
            TimeSpan t = dateTime - new DateTime(1970, 1, 1);
            long curWeekEpoch = (long)t.TotalSeconds;
            tryevent.SetDate_Time(curWeekEpoch);

            tryevent.SetFlexibility(event.GetFlexibility());
            tryevent.SetColor(event.GetColor());
            tryevent.SetPriority(event.GetPriority());
            tryevent.SetDescription(event.GetDescription());

            //addhours and days and such
        }
        //return Event with highest Score
    }



    private calculateScore(Event event, List<Event> day)
    {
        //my fancy algorithm
        long score;

        long dateScore = event.GetDate_Time() - Math.Abs(event.GetDate_Time().Hour - 12);
        
        int priorityScore;
        if(event.GetPriority() == "LOW")
            priorityScore = 3;
        else if(event.GetPriority() == "MED")
            priorityScore = 2;
        else
            priorityScore = 1;

        int flexScore = event.GetFlexibility();

        int locationScore = 0;
        //if(event.GetType() == typeof (location))
            foreach(Event anevent in day)
                if(anevent.GetLocation().Equals(event.GetLocation()))
                    locationScore++;

        score = dateScore + priorityScore + flexScore + locationScore;
    }
}
