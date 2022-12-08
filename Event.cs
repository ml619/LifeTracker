 // Event Classes
    public class Event // (items are public for sake of serialization)
    {

        public string name; // Name of event
        public string GetName()
        {
            return name;
        }
        public void SetName(string Name)
        {
            name = Name;
        }


        public long date_time; // Date and time in epoch
        public long GetDate_Time()
        {
            return date_time;
        }
        public void SetDate_Time(long Date_Time)
        {
            date_time = Date_Time;
        }


        public int flexibility; // How easilt event can be moved
        public int GetFlexibility()
        {
            return flexibility;
        }
        public void SetFlexibility(int Flexibility)
        {
            flexibility = Flexibility;
        }


        public string color; // Color of event on display
        public string GetColor()
        {
            return color;
        }
        public void SetColor(string Color)
        {
            color = Color;
        }


        public double duration; // In terms of number of hours (NOT epoch)
        public double GetDuration()
        {
            return duration;
        }
        public void SetDuration(double Duration)
        {
            duration = Duration;
        }


        public string priority; // How soon the event should be completed
        public string GetPriority()
        {
            return priority;
        }
        public void SetPriority(string Priority)
        {
            priority = Priority;
        }


        public string description; // Short description of activity 
        public string GetDescription()
        {
            return description;
        }
        public void SetDescription(string Description)
        {
            description = Description;
        }

        public string location; // Where the event takes place
        public string GetLocation()
        {
            return location;
        }
        public void SetLocation(string Location)
        {
            location = Location;
        }
    }
    public class Recurring : Event
    {
        public int numInstances; // How many events there are
        public long GetNumInstances()
        {
            return numInstances;
        }
        public void SetNumInstances(int NumInstances)
        {
            numInstances = NumInstances;
        }

        public int step; // How often events occur 
        public int GetStep()
        {
            return step;
        }
        public void SetStep(int Step)
        {
            step = Step;
        }
    }
