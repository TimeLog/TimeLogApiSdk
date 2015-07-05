namespace TimeLog.ReportingApi.SDK
{
    public class ProjectStatus
    {
        public static int Inactive
        {
            get
            {
                return 0;
            }
        }

        public static int Active
        {
            get
            {
                return 1;
            }
        }

        public static int All
        {
            get
            {
                return -2;
            }
        }
    }
}
