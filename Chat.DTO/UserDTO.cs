namespace Chat.DTO
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class UserDTO : BaseEntityDTO
    {
        public UserDTO()
        {
        }

        public UserDTO(
            int id,
            string firstName,
            string middleName,
            string lastName,
            GenderDTO gender,
            DateTime? dateOfBirthday,
            string email,
            string login,
            string password)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.MiddleName = middleName;
            this.LastName = lastName;
            this.Gender = gender;
            this.DateOfBirthday = dateOfBirthday;
            this.Email = email;
            this.Login = login;
            this.Password = password;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public GenderDTO Gender { get; set; }

        [DataMember]
        public DateTime? DateOfBirthday { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        public static bool operator ==(UserDTO userDto, UserLoginDTO userLoginDto)
        {
            return userDto?.Login == userLoginDto?.Login && userDto?.Password == userLoginDto?.Password;
        }

        public static bool operator !=(UserDTO userDto, UserLoginDTO userLoginDto)
        {
            return userDto?.Login != userLoginDto?.Login || userDto?.Password != userLoginDto?.Password;
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
            return Equals((UserDTO)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.Id;
                hashCode = (hashCode * 397) ^ (this.FirstName != null ? this.FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.MiddleName != null ? this.MiddleName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LastName != null ? this.LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)this.Gender;
                hashCode = (hashCode * 397) ^ this.DateOfBirthday.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Login != null ? this.Login.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Password != null ? this.Password.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(UserDTO other)
        {
            return this.Id == other.Id && string.Equals(this.FirstName, other.FirstName)
                                       && string.Equals(this.MiddleName, other.MiddleName)
                                       && string.Equals(this.LastName, other.LastName) && this.Gender == other.Gender
                                       && this.DateOfBirthday.Equals(other.DateOfBirthday)
                                       && string.Equals(this.Email, other.Email)
                                       && string.Equals(this.Login, other.Login) && string.Equals(
                                           this.Password,
                                           other.Password);
        }
    }
}
