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

}
