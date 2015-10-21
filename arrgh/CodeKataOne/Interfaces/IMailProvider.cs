using System;
using System.Net.Mail;

namespace CodeKataOne.Interfaces
{
    public interface IMailProvider
    {
        bool Send(String message);
    }
}