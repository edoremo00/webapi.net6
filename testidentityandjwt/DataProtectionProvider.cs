using Microsoft.AspNetCore.DataProtection;

namespace testidentityandjwt;

public class DataProtectionProvider : IDataProtectionProvider
{
    //fake implementation, im not sure what this is supposed to do.
    //You may be able to find an implementation that MS provides and allows you to add
    //via dependency injection in Program.cs
    public IDataProtector CreateProtector(string purpose)
    {
        throw new NotImplementedException();
    }
}