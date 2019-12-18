using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe_App.Services
{
    public interface IMessageService
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
