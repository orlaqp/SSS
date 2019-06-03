using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using SSS.Domain.Seedwork.Attribute;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.UserInfo.Dto;
using SSS.Infrastructure.Repository.UserInfo;
using System;

namespace SSS.Application.UserInfo
{
    [DIService(ServiceLifetime.Scoped, typeof(IUserInfoService))]
    public class UserInfoService : IUserInfoService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        private readonly IUserInfoRepository _repository;
        public UserInfoService(IMapper mapper, IMediatorHandler bus, IUserInfoRepository repository)
        {
            _mapper = mapper;
            _bus = bus;
            _repository = repository;
        }
        public void AddUserInfo(UserInfoInputDto input)
        {
            input.id = Guid.NewGuid().ToString();
            var cmd = _mapper.Map<UserInfoAddCommand>(input);
            _bus.SendCommand(cmd);
        }
    }
}