using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Collections.Generic;

namespace SquawkerWebApplication.SqlDataObjects
{
	[Table(Name = "dbo.Squawks")]
	public partial class Squawks : INotifyPropertyChanging, INotifyPropertyChanged
	{

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

		private int _Id;

		private int _UserId;

		private DateTime _CreationDate;

		private string _Content;

		private decimal _Latitude;

		private decimal _Longitude;

		private EntityRef<Users> _Users;

		#region Extensibility Method Definitions
		partial void OnLoaded();
		partial void OnValidate(ChangeAction action);
		partial void OnCreated();
		partial void OnIdChanging(int value);
		partial void OnIdChanged();
		partial void OnUserIdChanging(int value);
		partial void OnUserIdChanged();
		partial void OnCreationDateChanging(DateTime value);
		partial void OnCreationDateChanged();
		partial void OnContentChanging(string value);
		partial void OnContentChanged();
		partial void OnLatitudeChanging(decimal value);
		partial void OnLatitudeChanged();
		partial void OnLongitudeChanging(decimal value);
		partial void OnLongitudeChanged();
		#endregion

		public Squawks()
		{
			this._Users = default(EntityRef<Users>);
			OnCreated();
		}

		[Column(Storage = "_Id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}

		[Column(Storage = "_UserId", DbType = "Int NOT NULL")]
		public int UserId
		{
			get
			{
				return this._UserId;
			}
			set
			{
				if ((this._UserId != value))
				{
					if (this._Users.HasLoadedOrAssignedValue)
					{
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIdChanging(value);
					this.SendPropertyChanging();
					this._UserId = value;
					this.SendPropertyChanged("UserId");
					this.OnUserIdChanged();
				}
			}
		}

		[Column(Storage = "_CreationDate", DbType = "DateTime2(7) NOT NULL")]
		public DateTime CreationDate
		{
			get
			{
				return this._CreationDate;
			}
			set
			{
				if ((this._CreationDate != value))
				{
					this.OnCreationDateChanging(value);
					this.SendPropertyChanging();
					this._CreationDate = value;
					this.SendPropertyChanged("CreationDate");
					this.OnCreationDateChanged();
				}
			}
		}

		[Column(Storage = "_Content", DbType = "NVarChar(280) NOT NULL", CanBeNull = false)]
		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				if ((this._Content != value))
				{
					this.OnContentChanging(value);
					this.SendPropertyChanging();
					this._Content = value;
					this.SendPropertyChanged("Content");
					this.OnContentChanged();
				}
			}
		}

		[Column(Storage = "_Latitude", DbType = "Decimal(8,6) NOT NULL")]
		public decimal Latitude
		{
			get
			{
				return this._Latitude;
			}
			set
			{
				if ((this._Latitude != value))
				{
					this.OnLatitudeChanging(value);
					this.SendPropertyChanging();
					this._Latitude = value;
					this.SendPropertyChanged("Latitude");
					this.OnLatitudeChanged();
				}
			}
		}

		[Column(Storage = "_Longitude", DbType = "Decimal(9,6) NOT NULL")]
		public decimal Longitude
		{
			get
			{
				return this._Longitude;
			}
			set
			{
				if ((this._Longitude != value))
				{
					this.OnLongitudeChanging(value);
					this.SendPropertyChanging();
					this._Longitude = value;
					this.SendPropertyChanged("Longitude");
					this.OnLongitudeChanged();
				}
			}
		}

		[Association(Name = "FK_Squawks_UserIdToUsersId", Storage = "_Users", ThisKey = "UserId", OtherKey = "Id", IsForeignKey = true)]
		public Users Users
		{
			get
			{
				return this._Users.Entity;
			}
			set
			{
				Users previousValue = this._Users.Entity;
				if (((previousValue != value)
							|| (this._Users.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Users.Entity = null;
						previousValue.Squawks.Remove(this);
					}
					this._Users.Entity = value;
					if ((value != null))
					{
						value.Squawks.Add(this);
						this._UserId = value.Id;
					}
					else
					{
						this._UserId = default(int);
					}
					this.SendPropertyChanged("Users");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}

		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
