using System;
using System.Linq;
using Repository.MySql;

namespace Services.MySql
{
	public class MeetingsRsvpService : wwsaService<meeting_rsvp>
	{
		public int CountOfMembersRsvpForMeeting(DateTime meetingDate)
		{
			var resultCount = 0;

			var dateToBeComapred = DateTime.Parse(meetingDate.ToString("yyyy MMMM dd"));
			var result = Where(x =>x.meetings.meeting_date == dateToBeComapred).ToList();
			if (result.Count == 0) return resultCount;
			
			foreach (var r in result)
			{
				if (r.is_bringing_friend == true)
					resultCount++;
				resultCount++;
			}
			return resultCount;
		}


		public void RvsPforMeeting(string memberId, DateTime nextMeetingDate, bool isAttending, bool isBriningFriend)
		{
			var result = new MeetingsService().Where(x => x.meeting_date == nextMeetingDate).FirstOrDefault();
			if (result == null) return;
			Create(new meeting_rsvp
			{
				id = Guid.NewGuid().ToString(),
				member_id = memberId,
				is_attending = isAttending,
				is_bringing_friend = isBriningFriend,
				meeting_id = result.id
			});
		}

	}
}
