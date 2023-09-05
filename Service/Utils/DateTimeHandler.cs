using Service.Interfaces;

namespace Service.Utils
{
    public class DateTimeHandler : IDateTimeHandler
    {
        public DateTime GetCurrentUtcTime()
        {
            return DateTime.UtcNow;
        }
    }
}
