namespace Blinkay.WebApi
{
    public static class Constants
    {
        public static class Settings
        {
            public static string AllowedOrigins => "allowed-origins";

            //should be defined in web.config
            public static string MySqlConn => "MysqlConnection" ;
            public static string PgConn => "PostgresConnection";
            public static class Auth
            {
                public static string Issuer => "oauth2:issuer";
                public static string Audience => "oauth2:audience";
                public static string CertThumbprint => "oauth2:cert-thumbprint";
            }
        }
    }
}
