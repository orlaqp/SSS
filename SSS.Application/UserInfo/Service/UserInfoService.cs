using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SSS.Application.Seedwork.Service;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using SSS.Domain.Seedwork.Attribute;
using SSS.Domain.Seedwork.EventBus;
using SSS.Domain.Seedwork.Model;
using SSS.Domain.UserInfo.Dto;
using SSS.Infrastructure.Repository.UserInfo;
using System;
using System.Collections.Generic;

namespace SSS.Application.UserInfo.Service
{
    [DIService(ServiceLifetime.Scoped, typeof(IUserInfoService))]
    public class UserInfoService : QueryService<SSS.Domain.UserInfo.UserInfo, UserInfoInputDto, UserInfoOutputDto>, IUserInfoService
    {
        private readonly IMapper _mapper;
        private readonly IEventBus _bus;

        private readonly IUserInfoRepository _repository;

        public UserInfoService(IMapper mapper, IUserInfoRepository repository, IEventBus bus) : base(mapper, repository)
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

        public Pages<List<UserInfoOutputDto>> GetListUser(UserInfoInputDto input)
        {
            return GetList(input);
        }

        public UserInfoOutputDto GetByPhone(UserInfoInputDto input)
        {
            return Get(x => x.Phone.Equals(input.phone));
        }
    }
}