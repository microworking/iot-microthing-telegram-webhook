using System;
using System.Data;

namespace Microworking.Iot.Telegram.Webhook.Api.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        
        IDbTransaction Transaction { get; }

        void BeginTransaction();

        void Rollback();

        void Commit();
    }
}