namespace InnspireWebAPI.Services
{
    public class InternalConfigurationsManager
    {
        private readonly IConfiguration configuration;
        public static InternalConfigurationsManager? Instance { get; private set; }


        private const string fallbackJwtKey = "THIS IS A FALLBACK KEY THAT SHOULD NEVER BE USED";

        private string jwtKey = "";
        public string JwtKey => ValueOrFallback(ref jwtKey, "JWT_SECRET", "JWT:Key", fallbackJwtKey);

        private const string fallbackApiUrl = "http://localhost";
        private string? apiUrl;
        public string? ApiUrl => ValueOrFallback(ref apiUrl, "API_URL", "JWT:Issuer", fallbackApiUrl);

        private const string fallbackApiHostname = "localhost";
        private string? apiHostname;
        public string? ApiHostname => ValueOrFallback(ref apiHostname, "API_HOSTNAME", "JWT:Audience", fallbackApiHostname); 

        private string ValueOrFallback(ref string backingField, string environmentName, string configName, string fallback)
        {
            if (string.IsNullOrEmpty(jwtKey))
            {
                var envKey = Environment.GetEnvironmentVariable("JWT_SECRET");
                if (!string.IsNullOrEmpty(envKey))
                {
                    backingField = envKey;
                    return jwtKey;
                }

                var configJwtKey = configuration["JWT:Key"];
                if (!string.IsNullOrEmpty(configJwtKey))
                {
                    backingField = configJwtKey;
                    return jwtKey;
                }

                return fallback;
            }
            return backingField;
        }

        private InternalConfigurationsManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public static void Create(IConfiguration configuration)
        {
            Instance = new InternalConfigurationsManager(configuration);
        }
    }
}
