using System;
using Fintorly.Application.Dtos.UserDtos;
using Fintorly.Domain.Entities;

namespace Fintorly.Application.Features.Commands.UserCommands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByFilterAsync(a => a.EmailAddress == request.EmailAddress || a.PhoneNumber == request.PhoneNumber);
            if (user.Data is not null)
                throw new Exception("Böyle bir kullanıcı zaten kayıtlı");

            var userMapped = _mapper.Map<User>(request);
            userMapped.PasswordHash = new byte[2] {1,2};
            userMapped.PasswordSalt = new byte[2] {2,2};
            userMapped.PaymentChannel = "";
            var result = await _userRepository.AddAsync(userMapped);
            result.Data = _mapper.Map<UserDto>(result.Data);
            return result;
        }
    }
}

