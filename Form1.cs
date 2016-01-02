using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.IO;

namespace CreateWebPage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int year = int.Parse(textBox1.Text);
            string calendar = System.String.Empty;
            string ForwardString;
            string BackString;
            int YearNumber = year;
            
            // Create Array to hold Months
            string[] MonthArray = new string[12]{"January","February","March","April","May","June","July","August","September","October","November","December"};

            string TopOfFile = "<html>\n\n<head>\n<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>\n<title>New Page 1</title>\n</head>\n\n<body>\n\n";
            string BottomOfFile = "\n</body>\n\n</html>";

            // Start writing to the calendar string
            calendar = TopOfFile;

	        string BackArrow = "<img border='0' src='images/left_arrow.jpg' width='27' height='27'></a>";
	        string ForwardArrow = "<img border='0' src='images/right_arrow.jpg' width='27' height='27'></a>";
            string StartStyle = "<p align='center'><b><font face='Monotype Corsiva' size='6' color='#6666FF'>";
            string EndStyle = "</font></b></p>" + "\n" + "<p align='center'><img border='0' src='images/IMGPtbd.jpg'></p>" + "\n" + "<p align='center'>TBD</p>" + "\n" + "<p align='center'><font face='Bradley Hand ITC' size = '3' color='#FF0066'><b>John 3:16 - For God so loved the world that he gave his one and only Son, <br>that whoever believes in him shall not perish but have eternal life.</b></font></p>" + "\n";
            
            for (int i = 0; i < 12; i++)
            {
                if (i == 0)
                {
                    ForwardString = "<a href='" + MonthArray[i+1] + ".html'>";
                    calendar += StartStyle + "  " + MonthArray[i] + " " + YearNumber + "  " + ForwardString + ForwardArrow + EndStyle;
                }
                else if (i == 11)
                {
                    BackString = "<a href='" + MonthArray[i-1] + ".html'>";
                    calendar += StartStyle + BackString + BackArrow + "  " + MonthArray[i] + " " + YearNumber +  "  " + EndStyle;
                }
                else
                {
                    ForwardString = "<a href='" + MonthArray[i + 1] + ".html'>";
                    BackString = "<a href='" + MonthArray[i - 1] + ".html'>";
                    calendar += StartStyle + BackString + BackArrow + "  " + MonthArray[i] + " " + YearNumber + "  " + ForwardString + ForwardArrow + EndStyle;
                }

                calendar += html_month_calendar(i+1, year);
                calendar += BottomOfFile;

                // open file
                TextWriter tw = new StreamWriter("c:\\temp\\" + MonthArray[i] + ".html");
                tw.WriteLine(calendar);
                tw.Close();

                calendar = System.String.Empty;
            }
            MessageBox.Show("Done");
        }
    

        // Subroutine that takes a month and year, creates the calendar, and returns it in a string
        string html_month_calendar(int month, int year)
        {
            int DaysInAMonth = DateTime.DaysInMonth(year, month);
            
            DateTime baseDate = new DateTime(year, month, 1);
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            int FirstDay = (int)thisMonthStart.DayOfWeek;
            int extraDay=0;
            if (((DaysInAMonth+FirstDay)%7)!=0) 
                extraDay++;
           
            int NumberOfWeeks = ((DaysInAMonth+FirstDay) / 7) + extraDay ;
                                                
            string[] WeekArray = new string[7]{"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"};

            string calendar = "<table border='2' cellspacing='1' width='100%' height='316'>\n<tr height='47' align=center>\n";
            for (int i = 0; i < 7; i++)
            {
                calendar += "  <td width='14%'><b>" + WeekArray[i] + "</b></td>\n";
            }
            calendar += "</tr>\n";

            int Days = 0;
            String StyleStart = "<td><p style='margin-top:0; margin-bottom:0'><b>";
            String StyleEnd = "</b></td>";

            for (int i = 0; i < NumberOfWeeks; i++) 
            {
                calendar += "<tr height='98' align=left valign=top>\n";
                for (int j = 0; j < 7; j++)
                {
                    if (Days == 0 && FirstDay == j){
                        Days++;
                        calendar += "  " + StyleStart + Days + StyleEnd + "\n";
                    } else if (Days == 0) {
                        calendar += "  " + StyleStart + "." + StyleEnd + "\n";
                    } else {
                        Days++;
                        if (Days <= DaysInAMonth)
                            calendar += "  " + StyleStart + Days + StyleEnd + "\n";
                        else 
                            calendar += "  " + StyleStart + "." + StyleEnd + "\n";
                    }
                }
                calendar += "</tr>\n";
            }
            calendar += "</table>\n";
            return calendar;
        }
    }
}
