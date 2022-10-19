using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Dapper;
using Microworking.Iot.Telegram.Webhook.Api.Domain;
using Microworking.Iot.Telegram.Webhook.Api.Domain.Entities;
using Microworking.Iot.Telegram.Webhook.Api.Application.Enums;
using Microworking.Iot.Telegram.Webhook.Api.Application.Repositories;

namespace Microworking.Iot.Telegram.Webhook.Api.Data.Repositories
{
    public class TelegramDbRepository : ITelegramDbRepository
    {
        private static readonly string SCHEMA_NAME = "dbo";
        private static readonly string HOMES_TABLE_NAME = "HOMES";
        private static readonly string BOTS_TABLE_NAME = "BOTS";
        private static readonly string ROOMS_TABLE_NAME = "ROOMS";
        private static readonly string DEVICES_TABLE_NAME = "DEVICES";
        private static readonly string PERIPHERALS_TABLE_NAME = "PERIPHERALS";
        private static readonly string SCHEDULES_TABLE_NAME = "SCHEDULES";
        private static readonly string ACTIONS_TABLE_NAME = "ACTIONS";
        private static readonly string ACTIONTYPE_TABLE_NAME = "ACTIONTYPE";

        private readonly IUnitOfWork _unitOfWork;

        public TelegramDbRepository(IUnitOfWork unitOfWork,
                                    ILogger<TelegramDbRepository> logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<string>> GetHomes(string Token)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT");
            query.Append("  H.ADDRESS");
            query.Append(", H.CEP");
            query.Append(", H.DATE");
            query.Append(", B.OWNER");
            query.Append(", B.BOTALIAS");
            query.Append(", B.BOTNAME");
            query.Append(", B.DATE");
            query.Append(" FROM");
            query.Append($"  { SCHEMA_NAME }.{ HOMES_TABLE_NAME } H");
            query.Append($", { SCHEMA_NAME }.{ BOTS_TABLE_NAME } B");
            query.Append(" WHERE B.HOMEID = H.ID");
            query.Append(" AND B.OWNER = ");
            query.Append($" (SELECT OWNER FROM { SCHEMA_NAME }.{ HOMES_TABLE_NAME } WHERE TOKEN = @token)");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@token", Token);

            return await _unitOfWork.Connection.QueryAsync<string>(query.ToString(), parameters);
        }

        public async Task<IEnumerable<string>> GetRooms(string Token)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT");
            query.Append("  H.NAME");
            query.Append(", H.ADDRESS");
            query.Append(", R.NAME");
            query.Append(", R.DATE");
            query.Append(" FROM");
            query.Append($"  { SCHEMA_NAME }.{ HOMES_TABLE_NAME } H");
            query.Append($", { SCHEMA_NAME }.{ BOTS_TABLE_NAME } B");
            query.Append($", { SCHEMA_NAME }.{ ROOMS_TABLE_NAME } R");
            query.Append(" WHERE B.HOMEID = H.ID");
            query.Append(" AND R.HOMEID = H.ID");
            query.Append(" AND B.TOKEN = @token");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@token", Token);

            return await _unitOfWork.Connection.QueryAsync<string>(query.ToString(), parameters);
        }

        //LISTAR PERIFERICOS POR ROOM E HOME E USER
        public async Task<IEnumerable<string>> GetPeripherals(string Token, string Room = null)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT");
            query.Append("  H.NAME");
            query.Append(", H.ADDRESS");
            query.Append(", R.NAME");
            query.Append(", P.NAME");
            query.Append(", P.DATE");
            query.Append(" FROM");
            query.Append($"  { SCHEMA_NAME }.{ HOMES_TABLE_NAME } H");
            query.Append($", { SCHEMA_NAME }.{ BOTS_TABLE_NAME } B");
            query.Append($", { SCHEMA_NAME }.{ ROOMS_TABLE_NAME } R");
            query.Append($", { SCHEMA_NAME }.{ DEVICES_TABLE_NAME } D");
            query.Append($", { SCHEMA_NAME }.{ PERIPHERALS_TABLE_NAME } P");
            query.Append(" WHERE B.HOMEID = H.ID");
            query.Append(" AND R.HOMEID = H.ID");
            query.Append(" AND D.HOMEID = H.ID");
            query.Append(" AND D.ROOMID = R.ID");
            query.Append(" AND P.DEVICEID = D.ID");
            query.Append(" AND B.TOKEN = @token");
            if (!string.IsNullOrEmpty(Room)) query.Append(" AND R.NAME = @room");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@token", Token);
            if (!string.IsNullOrEmpty(Room)) parameters.Add("@room", Room);

            return await _unitOfWork.Connection.QueryAsync<string>(query.ToString(), parameters);
        }

        public IdentityDTO GetIdentity(string SecretToken)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP 1");
            query.Append("  1 ISAUTHORIZED");
            query.Append(", B.INDENTYTOKEN");
            query.Append(", B.AUTHTOKEN");
            query.Append(", B.CHATID");
            query.Append(", B.BOTALIAS");
            query.Append(", B.BOTNAME");
            query.Append(", B.OWNER");
            query.Append(", H.NAME");
            query.Append(", H.ADDRESS");
            query.Append(", H.NUMBER");
            query.Append(", H.CEP");
            query.Append(", B.DATE");
            query.Append(" FROM");
            query.Append($"  { SCHEMA_NAME }.{ BOTS_TABLE_NAME } B");
            query.Append($", { SCHEMA_NAME }.{ HOMES_TABLE_NAME } H");
            query.Append($" WHERE INDENTYTOKEN = @secretToken");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@secretToken", SecretToken);

            IEnumerable<IdentityDTO> result = _unitOfWork.Connection.Query<IdentityDTO>(query.ToString(), parameters);
            return result.Count() > 0 ? result.AsList().FirstOrDefault() : new IdentityDTO { IsAuthorized = false };
        }

        #region Queries for actions and devices selections

        public ActionCommandDTO GetActionCommand(List<string> Terms)
        {
            string terms = string.Join<string>(",", Terms);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT");
            query.Append("  A.ACTIONNAME");
            query.Append(", A.ACTIONCOMMAND");
            query.Append(", A.ACTIONTYPEID");
            query.Append(", T.ACTIONTYPENAME");
            query.Append(" FROM");
            query.Append($"  { SCHEMA_NAME }.{ ACTIONS_TABLE_NAME } A");
            query.Append($", { SCHEMA_NAME }.{ ACTIONTYPE_TABLE_NAME } T");
            query.Append($" WHERE A.ACTIONTYPEID = T.ID");
            query.Append($" AND UPPER(A.ACTIONNAME) IN ({ terms })");

            IEnumerable<ActionCommandDTO> result = _unitOfWork.Connection.Query<ActionCommandDTO>(query.ToString());
            return result.Count() > 0 ? result.AsList().FirstOrDefault() : new ActionCommandDTO { ActionCommand = string.Empty, ActionTypeId = (int)ActionTypeEnum.ActionNotFound };

        }

        public string GetRoom(string IndetityToken, List<string> Terms)
        {
            object value = null;
            string terms = string.Join<string>(",", Terms);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT");
            query.Append("  R.NAME");
            query.Append(" FROM");
            query.Append($"  { SCHEMA_NAME }.{ HOMES_TABLE_NAME } H");
            query.Append($", { SCHEMA_NAME }.{ BOTS_TABLE_NAME } B");
            query.Append($", { SCHEMA_NAME }.{ ROOMS_TABLE_NAME } R");
            query.Append(" WHERE B.INDENTYTOKEN = @token");
            query.Append(" AND B.HOMEID = H.ID");
            query.Append(" AND R.HOMEID = H.ID");
            query.Append($" AND UPPER(R.NAME) IN ({ terms })");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@token", IndetityToken);

            IDictionary<string, object> result = _unitOfWork.Connection.QuerySingle(query.ToString(), parameters);
            bool ifValue = result.TryGetValue("NAME", out value);
            return value.ToString();
        }

        public DeviceModelDTO GetDeviceProperties(string IndetityToken, string Room, List<string> Terms)
        {
            string terms = string.Join<string>(",", Terms);
            StringBuilder query = new StringBuilder();
            query.Append("SELECT");
            query.Append("  P.NAME");
            query.Append(", D.UID");
            query.Append(", P.GPIO");
            query.Append(", P.TYPE");
            query.Append(", P.ISANALOGIC");
            query.Append(" FROM");
            query.Append($"  { SCHEMA_NAME }.{ HOMES_TABLE_NAME } H");
            query.Append($", { SCHEMA_NAME }.{ BOTS_TABLE_NAME } B");
            query.Append($", { SCHEMA_NAME }.{ ROOMS_TABLE_NAME } R");
            query.Append($", { SCHEMA_NAME }.{ DEVICES_TABLE_NAME } D");
            query.Append($", { SCHEMA_NAME }.{ PERIPHERALS_TABLE_NAME } P");
            query.Append(" WHERE B.INDENTYTOKEN = @token");
            query.Append(" AND B.HOMEID = H.ID");
            query.Append(" AND R.HOMEID = H.ID");
            query.Append(" AND D.HOMEID = H.ID");
            query.Append(" AND D.ROOMID = R.ID");
            query.Append(" AND P.DEVICEID = D.ID");
            query.Append(" AND UPPER(R.NAME) = @room");
            query.Append($" AND UPPER(P.NAME) IN ({ terms })");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@token", IndetityToken);
            parameters.Add("@room", Room);

            try
            {
                return _unitOfWork.Connection.QuerySingle<DeviceModelDTO>(query.ToString(), parameters);
            }
            catch(Exception)
            {
                return null;
            }
        }

        #endregion

        //public async Task Create(TelegramIotDTO Data)
        //{
        //    var query = new StringBuilder();
        //    query.Append($"INSERT INTO {SCHEMA}.{TABLE_NAME}(COLLUMN)");
        //    query.Append("VALUES (@COLLUMN)");

        //    var parametros = new DynamicParameters();
        //    parametros.Add("@COLLUMN", Data.Uid, DbType.Int32);

        //    await _unitOfWork.Connection.ExecuteAsync(query.ToString(), parametros, _unitOfWork.Transaction);
        //}

        //public async Task<IEnumerable<TelegramIotDTO>> ReadAll(string TelegamToken)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@uid", TelegamToken);

        //    var query = new StringBuilder();
        //    query.Append("SELECT * FROM ");
        //    query.Append($" {TABLE_NAME} ");
        //    query.Append("WHERE UID = @uid");
        //    return await _unitOfWork.Connection.QueryAsync<TelegramIotDTO>(query.ToString(), parameters);
        //}

        //public async Task Update(TelegramIotDTO Data)
        //{
        //    var query = new StringBuilder();
        //    query.Append($"INSERT INTO {SCHEMA}.{TABLE_NAME}(COLLUMN)");
        //    query.Append("VALUES (@COLLUMN)");

        //    var parametros = new DynamicParameters();
        //    parametros.Add("@COLLUMN", Data.Uid, DbType.Int32);

        //    await _unitOfWork.Connection.ExecuteAsync(query.ToString(), parametros, _unitOfWork.Transaction);
        //}

        //public async Task Delete(string Uid)
        //{
        //    var query = new StringBuilder();
        //    query.Append($"INSERT INTO {SCHEMA}.{TABLE_NAME}(COLLUMN)");
        //    query.Append("VALUES (@COLLUMN)");

        //    var parametros = new DynamicParameters();
        //    parametros.Add("@COLLUMN", Uid, DbType.Int32);

        //    await _unitOfWork.Connection.ExecuteAsync(query.ToString(), parametros, _unitOfWork.Transaction);
        //}
    }
}