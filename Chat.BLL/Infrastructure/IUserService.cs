namespace Chat.BLL.Infrastructure
{
    using System.Collections.Generic;

    using Chat.DTO;
    using Chat.DTO.Response;

    public interface IUserService
    {
        OperationStatusInfo<string> Registration(UserDTO userDto);

        OperationStatusInfo<UserDTO> Login(UserLoginDTO userLoginDto);

        OperationStatusInfo<List<UserDTO>> GetAllUsers();
    }
}
