namespace LittleWatcher.Service.Services
{
    public static class SystemInformation
    {
        public static string OperationSystem { get; set; } = Environment.OSVersion.ToString();
        public static string ProcessorArchitecture { get; set; } = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
        public static string ProcessorModel { get; set; } = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
        public static string UserDomainName { get; set; } =Environment.UserDomainName;
        public static string UserName { get; set; } =Environment.UserName;
        public static int ProcessorCount { get; set; } = Environment.ProcessorCount;
    }
}
