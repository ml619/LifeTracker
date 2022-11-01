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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CreateEventWindow : Window
    {
        public CreateEventWindow()
        {
            InitializeComponent();

            // Initialize Lists
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 0; j <= 45; j+=15)
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
                }
            }
            AMPM1.Items.Add("AM"); AMPM1.Items.Add("PM");
            AMPM2.Items.Add("AM"); AMPM2.Items.Add("PM");

            PriorityList.Items.Add("LOW"); FlexibilityList.Items.Add("LOW");
            PriorityList.Items.Add("MED"); FlexibilityList.Items.Add("MED");
            PriorityList.Items.Add("HIGH"); FlexibilityList.Items.Add("HIGH");

            DateList.Items.Add("Jan"); DateList.Items.Add("Feb");
            DateList.Items.Add("Mar"); DateList.Items.Add("Apr");
            DateList.Items.Add("May"); DateList.Items.Add("Jun");
            DateList.Items.Add("Jul"); DateList.Items.Add("Aug");
            DateList.Items.Add("Sep"); DateList.Items.Add("Oct");
            DateList.Items.Add("Nov"); DateList.Items.Add("Dec");

            for (int i = 1; i <= 31; i++)
            {
                DayList.Items.Add(i.ToString());
            }

            for (int i = 2030; i >= 1970; i--)
            {
                YearList.Items.Add(i.ToString());
            }

            ColorList.Items.Add("Red"); ColorList.Items.Add("Blue");
            ColorList.Items.Add("Green"); ColorList.Items.Add("Yellow");
            ColorList.Items.Add("Purple"); ColorList.Items.Add("White");
            ColorList.Items.Add("Brown"); ColorList.Items.Add("Orange");
        }

        private void Okay_Button_Click(object sender, RoutedEventArgs e)
        {
            //STORE ALL FORM DATA
            //SET TEXT TO BLANK

            // Close create window.
            this.Close();
        }
    }
}
