using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services.Interface
{
    public interface INotificationService
    {
        Task<bool> EnviarEmailAsync(string destinatario, string assunto, string corpo);
    }
}