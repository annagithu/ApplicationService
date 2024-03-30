namespace ApplicationService.App.InternalContracts.Application
{
    public static class ActivityNames
    {
        private const string Report = "Report";
        private const string Tutorial = "Tutorial";
        private const string Discussion = "Discussion";
        public static readonly IReadOnlyList<string> AvailableNames = new List<string> {Report, Tutorial, Discussion };
    }
}
