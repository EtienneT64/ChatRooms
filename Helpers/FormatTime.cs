namespace ChatRooms.Helpers
{
    public class FormatTime
    {
        public static string FormatTimeStamp(DateTime messageTime, DateTime currentTime)
        {
            //DateTime currentTime = DateTime.Now;

            if (messageTime.Date == currentTime.Date)
            {
                return "Today at " + messageTime.ToString("HH:mm");
            }
            else if (messageTime.Date == currentTime.Date.AddDays(-1))
            {
                return "Yesterday at " + messageTime.ToString("HH:mm");
            }
            else
            {
                return messageTime.ToString("MMM dd yyyy, HH:mm");
            }
        }
    }
}
