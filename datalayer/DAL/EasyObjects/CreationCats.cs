/*
'===============================================================================
'  Generated From - CSharp_EasyObject_BusinessEntity.vbgen
' 
'  ** IMPORTANT  ** 
'  How to Generate your stored procedures:
' 
'  SQL      = SQL_DAAB_StoredProcs.vbgen
'  
'  This object is 'abstract' which means you need to inherit from it to be able
'  to instantiate it.  This is very easily done. You can override properties and
'  methods in your derived class, this allows you to regenerate this class at any
'  time and not worry about overwriting custom code. 
'
'  NEVER EDIT THIS FILE.
'
'  public class YourObject :  _YourObject
'  {
'
'  }
'
'===============================================================================
*/

// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;

using Microsoft.Practices.EnterpriseLibrary.Data;
using NCI.EasyObjects;

namespace BWA.bigWebDesk.DAL
{

	#region Schema

	public class CreationCatsSchema : NCI.EasyObjects.Schema
	{
		private static ArrayList _entries;
		public static SchemaItem DId = new SchemaItem("DId", DbType.Int32, false, false, false, true, true, false);
		public static SchemaItem Id = new SchemaItem("Id", DbType.Int32, true, false, false, true, true, false);
		public static SchemaItem BtInactive = new SchemaItem("btInactive", DbType.Boolean, false, false, false, false, false, true);
		public static SchemaItem IntCreated = new SchemaItem("intCreated", DbType.Int32, false, false, false, false, true, false);
		public static SchemaItem DtCreated = new SchemaItem("dtCreated", DbType.DateTime, false, false, false, false, false, true);
		public static SchemaItem IntUpdatedBy = new SchemaItem("intUpdatedBy", DbType.Int32, false, true, false, false, true, false);
		public static SchemaItem DtUpdated = new SchemaItem("dtUpdated", DbType.DateTime, false, true, false, false, false, false);
		public static SchemaItem VchName = new SchemaItem("vchName", DbType.AnsiString, SchemaItemJustify.None, 50, false, false, false, false);

		public override ArrayList SchemaEntries
		{
			get
			{
				if (_entries == null )
				{
					_entries = new ArrayList();
					_entries.Add(CreationCatsSchema.DId);
					_entries.Add(CreationCatsSchema.Id);
					_entries.Add(CreationCatsSchema.BtInactive);
					_entries.Add(CreationCatsSchema.IntCreated);
					_entries.Add(CreationCatsSchema.DtCreated);
					_entries.Add(CreationCatsSchema.IntUpdatedBy);
					_entries.Add(CreationCatsSchema.DtUpdated);
					_entries.Add(CreationCatsSchema.VchName);
				}
				return _entries;
			}
		}
	}
	#endregion

	public abstract class CreationCats : EasyObject
	{

		public CreationCats()
		{
			CreationCatsSchema _schema = new CreationCatsSchema();
			this.SchemaEntries = _schema.SchemaEntries;
			this.SchemaGlobal = "dbo";
		}
		
		public override void FlushData() 	 
		{ 	 
			this._whereClause = null; 	 
			this._aggregateClause = null; 	 
			base.FlushData(); 	 
		}
			   
		/// <summary>
		/// Loads the business object with info from the database, based on the requested primary key.
		/// </summary>
		/// <param name="DId"></param>
		/// <param name="Id"></param>
		/// <returns>A Boolean indicating success or failure of the query</returns>
		public bool LoadByPrimaryKey(int DId, int Id)
		{
			switch(this.DefaultCommandType)
			{
				case CommandType.StoredProcedure:
					ListDictionary parameters = new ListDictionary();

					// Add in parameters
					parameters.Add(CreationCatsSchema.DId.FieldName, DId);
					parameters.Add(CreationCatsSchema.Id.FieldName, Id);

					return base.LoadFromSql(this.SchemaStoredProcedureWithSeparator + "GetCreationCats", parameters, CommandType.StoredProcedure);

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();
					this.Where.DId.Value = DId;
					this.Where.Id.Value = Id;
					return this.Query.Load();

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}
	
		/// <summary>
		/// Loads all records from the table.
		/// </summary>
		/// <returns>A Boolean indicating success or failure of the query</returns>
		public bool LoadAll()
		{
			switch(this.DefaultCommandType)
			{
				case CommandType.StoredProcedure:
					return base.LoadFromSql(this.SchemaStoredProcedureWithSeparator + "GetAllCreationCats", null, CommandType.StoredProcedure);

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();
					return this.Query.Load();

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		/// <summary>
		/// Adds a new record to the internal table.
		/// </summary>
		public override void AddNew()
		{
			base.AddNew();
		}

		protected override DbCommand GetInsertCommand(CommandType commandType)
		{	
			DbCommand dbCommand;

			// Create the Database object, using the default database service. The
			// default database service is determined through configuration.
			Database db = GetDatabase();

			switch(commandType)
			{
				case CommandType.StoredProcedure:
					string sqlCommand = this.SchemaStoredProcedureWithSeparator + "AddCreationCats";
					dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddParameter(dbCommand, "Id", DbType.Int32, 0, ParameterDirection.Output, true, 0, 0, "Id", DataRowVersion.Default, Convert.DBNull);
					CreateParameters(db, dbCommand);
					
					return dbCommand;

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();
					foreach(SchemaItem item in this.SchemaEntries)
					{
						if (!(item.IsAutoKey || item.IsComputed))
						{
							this.Query.AddInsertColumn(item);
						}
					}
					dbCommand = this.Query.GetInsertCommandWrapper();

					dbCommand.Parameters.Clear();
					CreateParameters(db, dbCommand);
					db.AddParameter(dbCommand, "Id", DbType.Int32, 0, ParameterDirection.Output, true, 0, 0, "Id", DataRowVersion.Default, Convert.DBNull);
					
					return dbCommand;

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		protected override DbCommand GetUpdateCommand(CommandType commandType)
		{
            DbCommand dbCommand;

			// Create the Database object, using the default database service. The
			// default database service is determined through configuration.
			Database db = GetDatabase();

			switch(commandType)
			{
				case CommandType.StoredProcedure:
					string sqlCommand = this.SchemaStoredProcedureWithSeparator + "UpdateCreationCats";
					dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddInParameter(dbCommand, "Id", DbType.Int32, "Id", DataRowVersion.Current);
					CreateParameters(db, dbCommand);
					
					return dbCommand;

				case CommandType.Text:
					this.Query.ClearAll();
					foreach(SchemaItem item in this.SchemaEntries)
					{
						if (!(item.IsAutoKey || item.IsComputed))
						{
							this.Query.AddUpdateColumn(item);
						}
					}

					this.Where.WhereClauseReset();
					this.Where.Id.Operator = WhereParameter.Operand.Equal;
					dbCommand = this.Query.GetUpdateCommandWrapper();

					dbCommand.Parameters.Clear();
					CreateParameters(db, dbCommand);
					db.AddInParameter(dbCommand, "Id", DbType.Int32, "Id", DataRowVersion.Current);
					
					return dbCommand;

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		protected override DbCommand GetDeleteCommand(CommandType commandType)
		{
            DbCommand dbCommand;

			// Create the Database object, using the default database service. The
			// default database service is determined through configuration.
			Database db = GetDatabase();

			switch(commandType)
			{
				case CommandType.StoredProcedure:
					string sqlCommand = this.SchemaStoredProcedureWithSeparator + "DeleteCreationCats";
					dbCommand = db.GetStoredProcCommand(sqlCommand);
					db.AddInParameter(dbCommand, "DId", DbType.Int32, "DId", DataRowVersion.Current);
					db.AddInParameter(dbCommand, "Id", DbType.Int32, "Id", DataRowVersion.Current);
					
					return dbCommand;

				case CommandType.Text:
					this.Query.ClearAll();
					this.Where.WhereClauseReset();
					this.Where.DId.Operator = WhereParameter.Operand.Equal;
					this.Where.Id.Operator = WhereParameter.Operand.Equal;
					dbCommand = this.Query.GetDeleteCommandWrapper();

					dbCommand.Parameters.Clear();
					db.AddInParameter(dbCommand, "DId", DbType.Int32, "DId", DataRowVersion.Current);
					db.AddInParameter(dbCommand, "Id", DbType.Int32, "Id", DataRowVersion.Current);
					
					return dbCommand;

				default:
					throw new ArgumentException("Invalid CommandType", "commandType");
			}
		}

		private void CreateParameters(Database db, DbCommand dbCommand)
		{
			db.AddInParameter(dbCommand, "DId", DbType.Int32, "DId", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "btInactive", DbType.Boolean, "btInactive", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "intCreated", DbType.Int32, "intCreated", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "dtCreated", DbType.DateTime, "dtCreated", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "intUpdatedBy", DbType.Int32, "intUpdatedBy", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "dtUpdated", DbType.DateTime, "dtUpdated", DataRowVersion.Current);
			db.AddInParameter(dbCommand, "vchName", DbType.AnsiString, "vchName", DataRowVersion.Current);
		}
		
		#region Properties
		public virtual int DId
		{
			get
			{
				return this.GetInteger(CreationCatsSchema.DId.FieldName);
	    	}
			set
			{
				this.SetInteger(CreationCatsSchema.DId.FieldName, value);
			}
		}
		public virtual int Id
		{
			get
			{
				return this.GetInteger(CreationCatsSchema.Id.FieldName);
	    	}
			set
			{
				this.SetInteger(CreationCatsSchema.Id.FieldName, value);
			}
		}
		public virtual bool BtInactive
		{
			get
			{
				return this.GetBoolean(CreationCatsSchema.BtInactive.FieldName);
	    	}
			set
			{
				this.SetBoolean(CreationCatsSchema.BtInactive.FieldName, value);
			}
		}
		public virtual int IntCreated
		{
			get
			{
				return this.GetInteger(CreationCatsSchema.IntCreated.FieldName);
	    	}
			set
			{
				this.SetInteger(CreationCatsSchema.IntCreated.FieldName, value);
			}
		}
		public virtual DateTime DtCreated
		{
			get
			{
				return this.GetDateTime(CreationCatsSchema.DtCreated.FieldName);
	    	}
			set
			{
				this.SetDateTime(CreationCatsSchema.DtCreated.FieldName, value);
			}
		}
		public virtual int IntUpdatedBy
		{
			get
			{
				return this.GetInteger(CreationCatsSchema.IntUpdatedBy.FieldName);
	    	}
			set
			{
				this.SetInteger(CreationCatsSchema.IntUpdatedBy.FieldName, value);
			}
		}
		public virtual DateTime DtUpdated
		{
			get
			{
				return this.GetDateTime(CreationCatsSchema.DtUpdated.FieldName);
	    	}
			set
			{
				this.SetDateTime(CreationCatsSchema.DtUpdated.FieldName, value);
			}
		}
		public virtual string VchName
		{
			get
			{
				return this.GetString(CreationCatsSchema.VchName.FieldName);
	    	}
			set
			{
				this.SetString(CreationCatsSchema.VchName.FieldName, value);
			}
		}

		public override string TableName
		{
			get { return "CreationCats"; }
		}
		
		#endregion		
		
		#region String Properties
	
		public virtual string s_DId
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.DId.FieldName) ? string.Empty : base.GetIntegerAsString(CreationCatsSchema.DId.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.DId.FieldName);
				else
					this.DId = base.SetIntegerAsString(CreationCatsSchema.DId.FieldName, value);
			}
		}

		public virtual string s_Id
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.Id.FieldName) ? string.Empty : base.GetIntegerAsString(CreationCatsSchema.Id.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.Id.FieldName);
				else
					this.Id = base.SetIntegerAsString(CreationCatsSchema.Id.FieldName, value);
			}
		}

		public virtual string s_BtInactive
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.BtInactive.FieldName) ? string.Empty : base.GetBooleanAsString(CreationCatsSchema.BtInactive.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.BtInactive.FieldName);
				else
					this.BtInactive = base.SetBooleanAsString(CreationCatsSchema.BtInactive.FieldName, value);
			}
		}

		public virtual string s_IntCreated
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.IntCreated.FieldName) ? string.Empty : base.GetIntegerAsString(CreationCatsSchema.IntCreated.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.IntCreated.FieldName);
				else
					this.IntCreated = base.SetIntegerAsString(CreationCatsSchema.IntCreated.FieldName, value);
			}
		}

		public virtual string s_DtCreated
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.DtCreated.FieldName) ? string.Empty : base.GetDateTimeAsString(CreationCatsSchema.DtCreated.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.DtCreated.FieldName);
				else
					this.DtCreated = base.SetDateTimeAsString(CreationCatsSchema.DtCreated.FieldName, value);
			}
		}

		public virtual string s_IntUpdatedBy
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.IntUpdatedBy.FieldName) ? string.Empty : base.GetIntegerAsString(CreationCatsSchema.IntUpdatedBy.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.IntUpdatedBy.FieldName);
				else
					this.IntUpdatedBy = base.SetIntegerAsString(CreationCatsSchema.IntUpdatedBy.FieldName, value);
			}
		}

		public virtual string s_DtUpdated
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.DtUpdated.FieldName) ? string.Empty : base.GetDateTimeAsString(CreationCatsSchema.DtUpdated.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.DtUpdated.FieldName);
				else
					this.DtUpdated = base.SetDateTimeAsString(CreationCatsSchema.DtUpdated.FieldName, value);
			}
		}

		public virtual string s_VchName
	    {
			get
	        {
				return this.IsColumnNull(CreationCatsSchema.VchName.FieldName) ? string.Empty : base.GetStringAsString(CreationCatsSchema.VchName.FieldName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(CreationCatsSchema.VchName.FieldName);
				else
					this.VchName = base.SetStringAsString(CreationCatsSchema.VchName.FieldName, value);
			}
		}


		#endregion		
	
		#region Where Clause
		public class WhereClause
		{
			public WhereClause(EasyObject entity)
			{
				this._entity = entity;
			}
			
			public TearOffWhereParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffWhereParameter(this);
					}

					return _tearOff;
				}
			}

			#region TearOff's
			public class TearOffWhereParameter
			{
				public TearOffWhereParameter(WhereClause clause)
				{
					this._clause = clause;
				}
				
				
				public WhereParameter DId
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.DId);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}

				public WhereParameter Id
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.Id);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}

				public WhereParameter BtInactive
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.BtInactive);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}

				public WhereParameter IntCreated
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.IntCreated);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}

				public WhereParameter DtCreated
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.DtCreated);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}

				public WhereParameter IntUpdatedBy
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.IntUpdatedBy);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}

				public WhereParameter DtUpdated
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.DtUpdated);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}

				public WhereParameter VchName
				{
					get
					{
							WhereParameter wp = new WhereParameter(CreationCatsSchema.VchName);
							this._clause._entity.Query.AddWhereParameter(wp);
							return wp;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter DId
		    {
				get
		        {
					if(_DId_W == null)
	        	    {
						_DId_W = TearOff.DId;
					}
					return _DId_W;
				}
			}

			public WhereParameter Id
		    {
				get
		        {
					if(_Id_W == null)
	        	    {
						_Id_W = TearOff.Id;
					}
					return _Id_W;
				}
			}

			public WhereParameter BtInactive
		    {
				get
		        {
					if(_BtInactive_W == null)
	        	    {
						_BtInactive_W = TearOff.BtInactive;
					}
					return _BtInactive_W;
				}
			}

			public WhereParameter IntCreated
		    {
				get
		        {
					if(_IntCreated_W == null)
	        	    {
						_IntCreated_W = TearOff.IntCreated;
					}
					return _IntCreated_W;
				}
			}

			public WhereParameter DtCreated
		    {
				get
		        {
					if(_DtCreated_W == null)
	        	    {
						_DtCreated_W = TearOff.DtCreated;
					}
					return _DtCreated_W;
				}
			}

			public WhereParameter IntUpdatedBy
		    {
				get
		        {
					if(_IntUpdatedBy_W == null)
	        	    {
						_IntUpdatedBy_W = TearOff.IntUpdatedBy;
					}
					return _IntUpdatedBy_W;
				}
			}

			public WhereParameter DtUpdated
		    {
				get
		        {
					if(_DtUpdated_W == null)
	        	    {
						_DtUpdated_W = TearOff.DtUpdated;
					}
					return _DtUpdated_W;
				}
			}

			public WhereParameter VchName
		    {
				get
		        {
					if(_VchName_W == null)
	        	    {
						_VchName_W = TearOff.VchName;
					}
					return _VchName_W;
				}
			}

			private WhereParameter _DId_W = null;
			private WhereParameter _Id_W = null;
			private WhereParameter _BtInactive_W = null;
			private WhereParameter _IntCreated_W = null;
			private WhereParameter _DtCreated_W = null;
			private WhereParameter _IntUpdatedBy_W = null;
			private WhereParameter _DtUpdated_W = null;
			private WhereParameter _VchName_W = null;

			public void WhereClauseReset()
			{
				_DId_W = null;
				_Id_W = null;
				_BtInactive_W = null;
				_IntCreated_W = null;
				_DtCreated_W = null;
				_IntUpdatedBy_W = null;
				_DtUpdated_W = null;
				_VchName_W = null;

				this._entity.Query.FlushWhereParameters();

			}
	
			private EasyObject _entity;
			private TearOffWhereParameter _tearOff;
			
		}
	
		public WhereClause Where
		{
			get
			{
				if(_whereClause == null)
				{
					_whereClause = new WhereClause(this);
				}
		
				return _whereClause;
			}
		}
		
		private WhereClause _whereClause = null;	
		#endregion
		
		#region Aggregate Clause
		public class AggregateClause
		{
			public AggregateClause(EasyObject entity)
			{
				this._entity = entity;
			}
			
			public TearOffAggregateParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffAggregateParameter(this);
					}

					return _tearOff;
				}
			}

			#region TearOff's
			public class TearOffAggregateParameter
			{
				public TearOffAggregateParameter(AggregateClause clause)
				{
					this._clause = clause;
				}
				
				
				public AggregateParameter DId
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.DId);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}

				public AggregateParameter Id
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.Id);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}

				public AggregateParameter BtInactive
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.BtInactive);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}

				public AggregateParameter IntCreated
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.IntCreated);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}

				public AggregateParameter DtCreated
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.DtCreated);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}

				public AggregateParameter IntUpdatedBy
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.IntUpdatedBy);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}

				public AggregateParameter DtUpdated
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.DtUpdated);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}

				public AggregateParameter VchName
				{
					get
					{
							AggregateParameter ap = new AggregateParameter(CreationCatsSchema.VchName);
							this._clause._entity.Query.AddAggregateParameter(ap);
							return ap;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter DId
		    {
				get
		        {
					if(_DId_W == null)
	        	    {
						_DId_W = TearOff.DId;
					}
					return _DId_W;
				}
			}

			public AggregateParameter Id
		    {
				get
		        {
					if(_Id_W == null)
	        	    {
						_Id_W = TearOff.Id;
					}
					return _Id_W;
				}
			}

			public AggregateParameter BtInactive
		    {
				get
		        {
					if(_BtInactive_W == null)
	        	    {
						_BtInactive_W = TearOff.BtInactive;
					}
					return _BtInactive_W;
				}
			}

			public AggregateParameter IntCreated
		    {
				get
		        {
					if(_IntCreated_W == null)
	        	    {
						_IntCreated_W = TearOff.IntCreated;
					}
					return _IntCreated_W;
				}
			}

			public AggregateParameter DtCreated
		    {
				get
		        {
					if(_DtCreated_W == null)
	        	    {
						_DtCreated_W = TearOff.DtCreated;
					}
					return _DtCreated_W;
				}
			}

			public AggregateParameter IntUpdatedBy
		    {
				get
		        {
					if(_IntUpdatedBy_W == null)
	        	    {
						_IntUpdatedBy_W = TearOff.IntUpdatedBy;
					}
					return _IntUpdatedBy_W;
				}
			}

			public AggregateParameter DtUpdated
		    {
				get
		        {
					if(_DtUpdated_W == null)
	        	    {
						_DtUpdated_W = TearOff.DtUpdated;
					}
					return _DtUpdated_W;
				}
			}

			public AggregateParameter VchName
		    {
				get
		        {
					if(_VchName_W == null)
	        	    {
						_VchName_W = TearOff.VchName;
					}
					return _VchName_W;
				}
			}

			private AggregateParameter _DId_W = null;
			private AggregateParameter _Id_W = null;
			private AggregateParameter _BtInactive_W = null;
			private AggregateParameter _IntCreated_W = null;
			private AggregateParameter _DtCreated_W = null;
			private AggregateParameter _IntUpdatedBy_W = null;
			private AggregateParameter _DtUpdated_W = null;
			private AggregateParameter _VchName_W = null;

			public void AggregateClauseReset()
			{
				_DId_W = null;
				_Id_W = null;
				_BtInactive_W = null;
				_IntCreated_W = null;
				_DtCreated_W = null;
				_IntUpdatedBy_W = null;
				_DtUpdated_W = null;
				_VchName_W = null;

				this._entity.Query.FlushAggregateParameters();

			}
	
			private EasyObject _entity;
			private TearOffAggregateParameter _tearOff;
			
		}
	
		public AggregateClause Aggregate
		{
			get
			{
				if(_aggregateClause == null)
				{
					_aggregateClause = new AggregateClause(this);
				}
		
				return _aggregateClause;
			}
		}
		
		private AggregateClause _aggregateClause = null;	
		#endregion
	}
}
