using System;
using CodeKataOne.Interfaces;

namespace CodeKataOne
{
    public class User
    {
        public bool SendMail(IMailProvider provider, String message)
        {
            return provider.Send(message);
        }
    }
}
