namespace Chat.DTO
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UserLoginDTO : BaseEntityDTO
    {
        public UserLoginDTO()
        {
        }

        public UserLoginDTO(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }  

        public static bool operator ==(UserLoginDTO userLoginDto1, UserLoginDTO userLoginDto2)
        {
            return userLoginDto1?.Login == userLoginDto2?.Login && userLoginDto1?.Password == userLoginDto2?.Password;
        }

        public static bool operator !=(UserLoginDTO userLoginDto1, UserLoginDTO userLoginDto2)
        {
            return userLoginDto1?.Login != userLoginDto2?.Login || userLoginDto1?.Password != userLoginDto2?.Password;
        }

        public override string ToString()
        {
            return $"Login: {this.Login}, Password: {this.Password}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserLoginDTO)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.Login != null ? this.Login.GetHashCode() : 0) * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
            }
        }

        protected bool Equals(UserLoginDTO other)
        {
            return string.Equals(this.Login, other.Login) && string.Equals(this.Password, other.Password);
        }
    }
}