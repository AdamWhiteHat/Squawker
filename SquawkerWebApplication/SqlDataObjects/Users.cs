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

	[Table(Name = "dbo.Users")]
	public partial class Users : INotifyPropertyChanging, INotifyPropertyChanged
	{

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

		private int _Id;

		private string _AspNetUserId;

		private DateTime _CreationDate;

		private DateTimeOffset _TimezoneOffset;

		private string _UserName;

		private string _Email;

		private string _FirstName;

		private string _Surname;

		private EntitySet<Squawks> _Squawks;

		//private EntityRef<AspNetUsers> _AspNetUsers;

		#region Extensibility Method Definitions
		partial void OnLoaded();
		partial void OnValidate(ChangeAction action);
		partial void OnCreated();
		partial void OnIdChanging(int value);
		partial void OnIdChanged();
		partial void OnAspNetUserIdChanging(string value);
		partial void OnAspNetUserIdChanged();
		partial void OnCreationDateChanging(DateTime value);
		partial void OnCreationDateChanged();
		partial void OnTimezoneOffsetChanging(DateTimeOffset value);
		partial void OnTimezoneOffsetChanged();
		partial void OnUserNameChanging(string value);
		partial void OnUserNameChanged();
		partial void OnEmailChanging(string value);
		partial void OnEmailChanged();
		partial void OnFirstNameChanging(string value);
		partial void OnFirstNameChanged();
		partial void OnSurnameChanging(string value);
		partial void OnSurnameChanged();
		#endregion

		public Users()
		{
			this._Squawks = new EntitySet<Squawks>(new Action<Squawks>(this.attach_Squawks), new Action<Squawks>(this.detach_Squawks));
			//this._AspNetUsers = default(EntityRef<AspNetUsers>);
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

		[Column(Storage = "_AspNetUserId", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string AspNetUserId
		{
			get
			{
				return this._AspNetUserId;
			}
			set
			{
				if ((this._AspNetUserId != value))
				{
					//if (this._AspNetUsers.HasLoadedOrAssignedValue)
					//{
					//	throw new ForeignKeyReferenceAlreadyHasValueException();
					//}
					this.OnAspNetUserIdChanging(value);
					this.SendPropertyChanging();
					this._AspNetUserId = value;
					this.SendPropertyChanged("AspNetUserId");
					this.OnAspNetUserIdChanged();
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

		[Column(Storage = "_TimezoneOffset", DbType = "DateTimeOffset(0) NOT NULL")]
		public DateTimeOffset TimezoneOffset
		{
			get
			{
				return this._TimezoneOffset;
			}
			set
			{
				if ((this._TimezoneOffset != value))
				{
					this.OnTimezoneOffsetChanging(value);
					this.SendPropertyChanging();
					this._TimezoneOffset = value;
					this.SendPropertyChanged("TimezoneOffset");
					this.OnTimezoneOffsetChanged();
				}
			}
		}

		[Column(Storage = "_UserName", DbType = "NVarChar(15) NOT NULL", CanBeNull = false)]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this.OnUserNameChanging(value);
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
					this.OnUserNameChanged();
				}
			}
		}

		[Column(Storage = "_Email", DbType = "NVarChar(254) NOT NULL", CanBeNull = false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}

		[Column(Storage = "_FirstName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}

		[Column(Storage = "_Surname", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Surname
		{
			get
			{
				return this._Surname;
			}
			set
			{
				if ((this._Surname != value))
				{
					this.OnSurnameChanging(value);
					this.SendPropertyChanging();
					this._Surname = value;
					this.SendPropertyChanged("Surname");
					this.OnSurnameChanged();
				}
			}
		}

		[Association(Name = "FK_Squawks_UserIdToUsersId", Storage = "_Squawks", ThisKey = "Id", OtherKey = "UserId", DeleteRule = "NO ACTION")]
		public EntitySet<Squawks> Squawks
		{
			get
			{
				return this._Squawks;
			}
			set
			{
				this._Squawks.Assign(value);
			}
		}

		/*
		[Association(Name="FK_Users_AspNetUserIdToAspNetUsersId", Storage="_AspNetUsers", ThisKey="AspNetUserId", OtherKey="Id", IsForeignKey=true)]
		public AspNetUsers AspNetUsers
		{
			get
			{
				return this._AspNetUsers.Entity;
			}
			set
			{
				AspNetUsers previousValue = this._AspNetUsers.Entity;
				if (((previousValue != value) 
							|| (this._AspNetUsers.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._AspNetUsers.Entity = null;
						previousValue.Users.Remove(this);
					}
					this._AspNetUsers.Entity = value;
					if ((value != null))
					{
						value.Users.Add(this);
						this._AspNetUserId = value.Id;
					}
					else
					{
						this._AspNetUserId = default(string);
					}
					this.SendPropertyChanged("AspNetUsers");
				}
			}
		}
		*/

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

		private void attach_Squawks(Squawks entity)
		{
			this.SendPropertyChanging();
			entity.Users = this;
		}

		private void detach_Squawks(Squawks entity)
		{
			this.SendPropertyChanging();
			entity.Users = null;
		}
	}
}
