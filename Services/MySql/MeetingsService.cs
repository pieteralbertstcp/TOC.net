using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Repository.MySql;

namespace Services.MySql
{
	public class MeetingsService : wwsaService<meetings>
	{


		public DateTime GetNextMeetingDate()
		{
			var result = GetSecondSaturdayFromProvidedDateTime(DateTime.Now);
			//This month, if second staturday has not yet been hit.
			if (DateTime.Now < result)
			{
				addRecordOfDateToDatabaseIfRequired(result);
				return result;
			}
			//Else return next months date.
			var nextMonthsMeeting = GetSecondSaturdayFromProvidedDateTime(DateTime.Now.AddMonths(1));
			addRecordOfDateToDatabaseIfRequired(nextMonthsMeeting);
			return nextMonthsMeeting;
		}

		public bool IsUserAttendingMeetingForProvidedDate(DateTime meetingDate, string memberId)
		{
			return new MeetingsRsvpService().Where(x => x.member_id == memberId && x.meetings.meeting_date == meetingDate && x.is_attending == true).Any();
		}

		private DateTime GetSecondSaturdayFromProvidedDateTime(DateTime fromThisDate)
		{
			var secondSaturday = new DateTime(fromThisDate.Year, fromThisDate.Month, 1);

			var satudayCount = 0;
			while (satudayCount < 2)
			{
				if (secondSaturday.DayOfWeek == DayOfWeek.Saturday)
					satudayCount++;

				if (satudayCount == 2)
					break;

				secondSaturday = secondSaturday.AddDays(1);
			}
			return secondSaturday.Date;
		}


		private void addRecordOfDateToDatabaseIfRequired(DateTime NextMeetingDate)
		{
			var result = Where(x => x.meeting_date == NextMeetingDate).ToList();
			if (result.Any() == false)
			{
				var record = new meetings
				{
					id = Guid.NewGuid().ToString(),
					meeting_date = NextMeetingDate
				};
				Create(record);
			}
		}
	}
}
