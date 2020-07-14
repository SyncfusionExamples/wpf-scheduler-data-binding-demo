using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Shared;
using System.Linq;

namespace DataBinding
{
    /// <summary>
    /// Schedule View Model
    /// </summary>
    public class ScheduleViewModel
    {
        #region Properties

        /// <summary>
        /// current day meetings 
        /// </summary>
        private List<string> currentDayMeetings;

        /// <summary>
        /// minimum time meetings
        /// </summary>
        private List<string> minTimeMeetings;
        /// <summary>
        /// color collection
        /// </summary>
        private List<Brush> colorCollection;

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleViewModel" /> class.
        /// </summary>
        public ScheduleViewModel()
        {
            this.Events = new ObservableCollection<Meeting>();
            this.InitializeDataForBookings();
            this.IntializeAppoitments();
        }

        #endregion Constructor

        /// <summary>
        /// Gets or sets appointments
        /// </summary>
        public ObservableCollection<Meeting> Events
        {
            get;
            set;
        }

        #region Methods

        #region GettingTimeRanges

        /// <summary>
        /// Method for get timing range.
        /// </summary>
        /// <returns>return time collection</returns>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));

            return randomTimeCollection;
        }

        #endregion GettingTimeRanges

        #region InitializeDataForBookings

        /// <summary>
        /// Method for initialize data bookings.
        /// </summary>
        private void InitializeDataForBookings()
        {
            this.currentDayMeetings = new List<string>();
            this.currentDayMeetings.Add("General Meeting");
            this.currentDayMeetings.Add("Plan Execution");

            // MinimumHeight Appointment Subjects
            this.minTimeMeetings = new List<string>();
            this.minTimeMeetings.Add("Client Metting");
            this.minTimeMeetings.Add("Birthday wish alert");

            this.colorCollection = new List<Brush>();
            this.colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF339933")));
            this.colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00ABA9")));
            this.colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE671B8")));
            this.colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1BA1E2")));
            this.colorCollection.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD80073")));
        }

        #endregion InitializeDataForBookings

        #region InitializeAppointments
        /// <summary>
        /// Initialize appointments
        /// </summary>
        /// <param name="count">count value</param>
        private void IntializeAppoitments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = this.GettingTimeRanges();

            DateTime date;
            DateTime dateFrom = DateTime.Now.AddDays(-100);
            DateTime dateTo = DateTime.Now.AddDays(100);
            var random = new Random();
            var dateCount = random.Next(4);
            DateTime dateRangeStart = DateTime.Now.AddDays(0);
            DateTime dateRangeEnd = DateTime.Now.AddDays(1);

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                if (date.Day % 7 != 0)
                {
                    for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 1; additionalAppointmentIndex++)
                    {
                        Meeting meeting = new Meeting();
                        int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                        meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                        meeting.To = meeting.From.AddHours(1);
                        meeting.EventName = this.currentDayMeetings[randomTime.Next(2)];
                        meeting.Color = this.colorCollection[randomTime.Next(2)];
                        meeting.IsAllDay = false;
                        meeting.StartTimeZone = string.Empty;
                        meeting.EndTimeZone = string.Empty;
                        this.Events.Add(meeting);
                    }
                }
                else
                {
                    Meeting meeting = new Meeting();
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                    meeting.To = meeting.From.AddDays(2).AddHours(1);
                    meeting.EventName = this.currentDayMeetings[randomTime.Next(2)];
                    meeting.Color = this.colorCollection[randomTime.Next(2)];
                    meeting.IsAllDay = true;
                    meeting.StartTimeZone = string.Empty;
                    meeting.EndTimeZone = string.Empty;
                    this.Events.Add(meeting);
                }
            }

            // Minimum Height Meetings
            DateTime minDate;
            DateTime minDateFrom = DateTime.Now.AddDays(-2);
            DateTime minDateTo = DateTime.Now.AddDays(2);

            for (minDate = minDateFrom; minDate < minDateTo; minDate = minDate.AddDays(1))
            {
                Meeting meeting = new Meeting();
                meeting.From = new DateTime(minDate.Year, minDate.Month, minDate.Day, randomTime.Next(9, 18), 30, 0);
                meeting.To = meeting.From;
                meeting.EventName = this.minTimeMeetings[randomTime.Next(0, 1)];
                meeting.Color = this.colorCollection[randomTime.Next(0, 2)];
                meeting.StartTimeZone = string.Empty;
                meeting.EndTimeZone = string.Empty;
                
                this.Events.Add(meeting);
            }
        }

        #endregion InitializeAppointments
        #endregion Methods
    }
}