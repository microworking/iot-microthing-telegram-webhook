using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microworking.Iot.Telegram.Webhook.Api.Domain;

namespace Microworking.Iot.Telegram.Webhook.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UnitOfWork> _logger;

        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IConfiguration configuration,
                          Microsoft.AspNetCore.Hosting.IWebHostEnvironment env,
                          ILogger<UnitOfWork> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("TELEGRAM_IOT_DB");
            _logger.LogInformation($"Connection String EnvironmentName: '{env.EnvironmentName}'");
            Connection = new SqlConnection(_connectionString);
            try { Connection.Open(); } catch { }
        }

        public void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            if (Transaction == null)
                throw new Exception("Nenhuma transação inicializada");
            Transaction.Commit();
        }

        public void Dispose()
        {
            if (Transaction != null)
                Transaction.Dispose();
            Connection.Dispose();
        }

        public void Rollback()
        {
            if (Transaction == null)
                throw new Exception("Nenhuma transação inicializada");
            Transaction.Rollback();
        }
    }
}