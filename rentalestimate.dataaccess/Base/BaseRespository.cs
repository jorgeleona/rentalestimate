
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using rentalestimate.model.Base;
using System.Reflection;

namespace rentalestimate.dataaccess.Base
{
	public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
	{

		protected IDbConnection Connection
		{
			get
			{
				return new SqlConnection("Server = tcp:jorgeleon.database.windows.net,1433; Initial Catalog = rentalestimate; Persist Security Info = False; User ID = manager; Password = R00t+jorge; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
			}
		}

		public IEnumerable<T> Get()
		{

			IEnumerable<T> entities = new List<T>();

			try
			{

				using (IDbConnection dbConnection = this.Connection)
				{
					dbConnection.Open();


					entities = dbConnection.Query<T>($"SELECT * FROM {typeof(T).Name}");
				}

			}
			catch (Exception exception)
			{

				string exceptionMessage = exception.Message;
			}

			return entities;
		}

		public T Add(T newEntity)
		{         
			try
			{
				using (IDbConnection dbConnection = this.Connection)
				{

					newEntity.CreatedDate = DateTime.UtcNow;
					newEntity.CreatedUser = "System";
                                   

					List<String> discardProperties = new List<string>() {"Id","ModifiedDate","ModifiedUser" };

					List<String> properties =  new List<PropertyInfo>(typeof(T).GetProperties()).
                        Where(propInfo => !discardProperties.Contains(propInfo.Name)).
                        Select(propInfo => propInfo.Name).ToList();
					string propertiesNames = String.Join(',', properties);
					properties = properties.Select(prop => $"@{prop}").ToList();
					string propertiesValues = String.Join(',', properties);

					string sqlInsert = $"INSERT INTO {typeof(T).Name}({propertiesNames}) values({propertiesValues}); SELECT CAST(SCOPE_IDENTITY() as int)";
					int returnId = dbConnection.Query<int>(sqlInsert, newEntity).SingleOrDefault();
					newEntity.Id = returnId;
				}
			}
			catch (Exception ex)
			{
				//TODO: Add log
				return null;
			}
			return newEntity;

		}

		public T Get(int entityId) {

			return null;
		}
	}
}