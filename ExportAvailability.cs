public void ExportAvailability(Week week)
{

    // create a file to write to 
    string fileName = @"C:\Documents\Availability.txt";
    if (File.Exists(fileName))
    {
        File.Delete(fileName);
    }


   

    using (StreamWriter sw = File.CreateText(fileName))
    {

        for (int i = 0; i <= 6; i++)
        {
            List<string> days = new List<string>
            { "Monday\n", "Tuesday\n", "Wednesday\n", "Thursday\n", "Friday\n", "Saturday\n", "Sunday\n" };

            sw.WriteLine(days[i])

            for (int j = 0; j < week[i].Count; j++)
            {
                int epochSeconds = week[i][j].GetDate_Time();
                int epochDuration = week[i][j].GetDuration();


                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epochSeconds);
                DateTime dateTime = dateTimeOffset.DateTime;
                string startTime = dateTime.ToString("HH:mm");

                int endtimeepoch = epochSeconds + epochDuration;
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(endtimeepoch);
                DateTime dateTime = dateTimeOffset.DateTime;
                string endTime = dateTime.ToString("HH:mm");


                sw.WriteLine(startTime + " to " + endTime + ",");
    

            }
        }


    }

}
