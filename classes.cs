// classes 

class Event //description, priority, time, color, flexibility
{
    public string name; // name of event
    public int date; //date of event (epoch)
    public int time; //How should this be represented (military)
    public string color; // color of event

    public int flexibility; // how is this represented?
    public int priority; // how is this represented?

}

class location
{
    public string name; // name of location

}

class week
{
    public List<string> mon = new List<string> (); //lists of events
    public List<string> tue = new List<string> ();
    public List<string> wed = new List<string> ();
    public List<string> thu = new List<string> ();
    public List<string> fri = new List<string> ();
    public List<string> sat = new List<string> ();
    public List<string> sun = new List<string> ();

}

class recurring
{
    public int end_date; // end of recurring 
    public int step; //how often 

}

class sugested
{
    public bool accept; // TRUE = Accepted  FALSE = Declined
    public bool move; //Not sure what this is?

}

