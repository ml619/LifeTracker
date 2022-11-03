class Calender
{
    private Dictionary<int,Week> weeks;

    Week getWeek(int key)
    {
        return weeks[key];
    }

    // Event getEvent(int key)
    // {
    //     return weeks[key];
    // }

    public void addWeek(Week week)
    {
        weeks.Add(week.getDate(),week);
    }

    public void removeWeek(int key)
    {
        weeks.Remove(key);
    }

    public void setWeek(int key,week)
    {
        weeks[key] = week;
    }    

    public void addEvent(Event event)
    {
        if (weeks.ContainsKey(event.getWeek()))
        {
            weeks[event.getWeek()].addEvent(event);
        }
        else
        {
            weeks.Add(events.getWeek(),week(event))
        }
    }

    public void removeEvent(Event event)
    {
        weeks[event.getWeek()].removeEvent(event);
    }

    public void setEvent(Event event)
    {
        weeks[event.getWeek()].setEvent(event);
    }

}