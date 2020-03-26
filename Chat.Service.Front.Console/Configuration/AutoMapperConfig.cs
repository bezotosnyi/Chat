namespace Chat.Service.Front.Console.Configuration
{
    using AutoMapper;

    using Chat.Domain.Entities;
    using Chat.DTO;

    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(
                x =>
                    {
                        x.CreateMap<User, UserDTO>();
                        x.CreateMap<UserDTO, User>();
                        x.CreateMap<User, UserLoginDTO>();
                        x.CreateMap<UserLoginDTO, User>();
                        x.CreateMap<Message, MessageDTO>();
                        x.CreateMap<MessageDTO, Message>();
                    });
        }
    }
}