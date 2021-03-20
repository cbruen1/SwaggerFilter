using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerFilterTest
{
    public static class Constants
    {
        // Swagger UI grouping and filtering
        public const string ApiVersion1 = "v1";
        public const string ApiVersion2 = "v2";

        // The full consumer name
        public const string ApiConsumerNameConA = "Consumer A";
        public const string ApiConsumerNameConB = "Consumer B";
        public const string ApiConsumerNameConC = "Consumer C";

        // Specify the group name - this appears in the Swagger UI drop-down
        public const string ApiConsumerGroupNameConA = "v2-conA";
        public const string ApiConsumerGroupNameConB = "v2-conB";
        public const string ApiConsumerGroupNameConC = "v2-conC";

        // Decorate each controller method with the tag names below - this determines 
        // what consumer can access what endpoint, and also how the endpoints are 
        // grouped and named in the Swagger UI

        // Swagger ConA tag names
        public const string ApiConsumerTagNameConAAccount = ApiConsumerNameConA + " - Account";
        public const string ApiConsumerTagNameConANotification = ApiConsumerNameConA + " - Notification";

        // Swagger ConB tag names
        public const string ApiConsumerTagNameConBAccountAdmin = ApiConsumerNameConB + " - Account Admin";

        // Swagger ConC tag names
        public const string ApiConsumerTagNameConCAccountAdmin = ApiConsumerNameConC + " - Account Admin";
        public const string ApiConsumerTagNameConCNotification = ApiConsumerNameConC + " - Notification";

        // Store the schemes belonging to each Path for Swagger so only the relevant ones are shown in the Swagger UI
        public static IReadOnlyDictionary<string, List<string>> ApiPathSchemas;

        static Constants()
        {
            ApiPathSchemas = new Dictionary<string, List<string>>()
            {
                // Consumer A has access to all so only specify those for B and C
                { ApiConsumerNameConB, new List<string>() { "SearchOutcome", "VerificationDetails", "VerifyingPartyDetails", "VerificationStatus", "ChangeMobileNumberRequest", "RepaymentOption" }},
                { ApiConsumerNameConC, new List<string>() { "SearchOutcome", "VerificationDetails", "NotificationType", "SendNotificationRequest", "ProblemDetails" }}
            };
        }
    }
}
