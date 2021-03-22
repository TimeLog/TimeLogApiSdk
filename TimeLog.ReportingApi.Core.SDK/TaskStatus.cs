namespace TimeLog.ReportingApi.Core.SDK
{
    public class TaskStatus
    {
        public static int All
        {
            get
            {
                return -1;
            }
        }

        public static int Active
        {
            get
            {
                return 1;
            }
        }

        public static int Inactive
        {
            get
            {
                return 0;
            }
        }
    }
}
