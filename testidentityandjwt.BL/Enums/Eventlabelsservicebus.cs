using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace testidentityandjwt.BL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Eventlabelsservicebus
    {
        [EnumMember(Value ="UserregisteredEvent")]
        UserregisteredEvent
    }
}
