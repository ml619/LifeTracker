using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LifeTracker
{
    /// <summary>
    /// Interaction logic for EditEventWindow.xaml
    /// </summary>
    public partial class EditEventWindow : Window
    {

        public bool deleteEventBool = false; //return to tell mainwindow whether to delete this event
        public EditEventWindow()
        {
            InitializeComponent();
            // Initialize Lists
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 0; j <= 45; j += 15)
                {
                    if (i < 10 && j < 10)
                    {
                        TimeList1.Items.Add("0" + i + ":" + "0" + j);
                        TimeList2.Items.Add("0" + i + ":" + "0" + j);
                    }
                    else if (i < 10)
                    {
                        TimeList1.Items.Add("0" + i + ":" + j);
                        TimeList2.Items.Add("0" + i + ":" + j);
                    }
                    else if (j < 10)
                    {
                        TimeList1.Items.Add(i + ":" + "0" + j);
                        TimeList2.Items.Add(i + ":" + "0" + j);
                    }
                    else
                    {
                        TimeList1.Items.Add(i + ":" + j);
                        TimeList2.Items.Add(i + ":" + j);
                    }
                }
            }
            TimeList1.SelectedIndex = 0; TimeList2.SelectedIndex = 4;
            AMPM1.Items.Add("AM"); AMPM1.Items.Add("PM");
            AMPM2.Items.Add("AM"); AMPM2.Items.Add("PM");
            AMPM1.SelectedIndex = 0; AMPM2.SelectedIndex = 0;

            PriorityList.Items.Add("LOW"); FlexibilityList.Items.Add("LOW");
            PriorityList.Items.Add("MED"); FlexibilityList.Items.Add("MED");
            PriorityList.Items.Add("HIGH"); FlexibilityList.Items.Add("HIGH");
            PriorityList.SelectedIndex = 0; FlexibilityList.SelectedIndex = 0;

            MonthList.Items.Add("Jan"); MonthList.Items.Add("Feb");
            MonthList.Items.Add("Mar"); MonthList.Items.Add("Apr");
            MonthList.Items.Add("May"); MonthList.Items.Add("Jun");
            MonthList.Items.Add("Jul"); MonthList.Items.Add("Aug");
            MonthList.Items.Add("Sep"); MonthList.Items.Add("Oct");
            MonthList.Items.Add("Nov"); MonthList.Items.Add("Dec");
            MonthList.Text = DateTime.Now.ToString("MMM");

            for (int i = 1; i <= 31; i++)
            {
                if(i>9) DayList.Items.Add(i.ToString());
                else DayList.Items.Add("0"+i.ToString());
            }
            DayList.Text = DateTime.Now.ToString("dd");

            for (int i = 2030; i >= 1970; i--)
            {
                YearList.Items.Add(i.ToString());
            }
            YearList.Text = DateTime.Now.ToString("yyyy");

            ColorList.Items.Add("LightBlue"); ColorList.Items.Add("LightPink");
            ColorList.Items.Add("PeachPuff"); ColorList.Items.Add("Silver");
            ColorList.Items.Add("PaleGoldenrod"); ColorList.Items.Add("BurlyWood");
            ColorList.Items.Add("Plum"); ColorList.Items.Add("LightGreen");
            ColorList.Text = "LightBlue";
        }

        private void Okay_Button_Click(object sender, RoutedEventArgs e)
        {
            // Close create window.
            this.Close();
        }
        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            // Close create window.
            deleteEventBool = true;
            this.Close();
        }
    }
}
