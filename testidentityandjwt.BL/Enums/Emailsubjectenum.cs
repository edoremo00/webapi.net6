using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace testidentityandjwt.BL.Enums
{
    //[Serializable]

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Emailsubjects
    {
        [EnumMember(Value ="Resetpassword")]
        Resetpassword,
        [EnumMember(Value = "Welcomeemail")]
        Welcomeemail,
        [EnumMember(Value = "Deletedaccount")]
        Deletedaccount,
        [EnumMember(Value = "password has been changed")]
        password_has_been_changed,
        [EnumMember(Value = "personal info changed")]
        personal_info_had_been_changed,

    }
}
