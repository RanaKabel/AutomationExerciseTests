namespace AutomationExerciseTests.Utils
{
    public static class TestUtils
    {
        public static string GenerateUniqueEmail(string baseEmail)
        {
            string prefix = baseEmail.Split('@')[0];
            string domain = baseEmail.Contains("@") ? baseEmail.Split('@')[1] : "example.com";
            return $"{prefix}+{Guid.NewGuid().ToString("N").Substring(0, 8)}@{domain}";
        }
    }
}
