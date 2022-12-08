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
