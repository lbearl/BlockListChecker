using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class SuppressionListCheckService : ISuppressionListCheckService
    {
        public List<SuppressedEmailViewModel> GetAllSuppressedEmails()
        {
            throw new NotImplementedException();
        }

        public List<SuppressedEmailViewModel> GetSuppressedEmail(string address)
        {
            throw new NotImplementedException();
        }

    }
}
