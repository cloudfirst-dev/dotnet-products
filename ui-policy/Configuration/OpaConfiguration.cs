namespace ui_policy.Configuration
{
    public class OpaConfiguration {
        private string _policyPath = "v1/data/ui";
        public int Timeout { get; set; } = 1000;
        public string BaseAddress { get; set; } = string.Empty;

        public string PolicyPath
        {
            get => _policyPath;
            set => _policyPath = "v1/data" + value;
        }
    }
}