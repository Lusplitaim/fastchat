using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FastChat
{
    internal static class AuthOptions
    {
        public static string ISSUER = "FastChatServer";
        public static string AUDIENCE = "FastChatServer";
        public static string KEY = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.IntcbiAgIFwiaXNzXCI6IFwibXlfaXNzdXJlclwiLFxuICAgXCJpYXRcIjogMTQwMDA2MjQwMDIyMyxcbiAgIFwidHlwZVwiOiBcIi9vbmxpbmUvc3RhdHVzL3YyXCIsXG4gICBcInJlcXVlc3RcIjoge1xuICAgICBcInRyYW5zYWN0aW9uX2lkXCI6IFwidHJhXzc0MzQ3MDgyXCIsXG4gICAgIFwibWVyY2hhbnRfaWRcIjogXCJtZXJjX2E3MTQxdXRuYTg0XCIsXG4gICAgIFwic3RhdHVzXCI6IFwiU1VDQ0VTU1wiXG4gICB9XG4gfSI.tKWD0_8uYPSJCYWa3a6lqt-bIOtTYQ6GCywNd21tYkE";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
