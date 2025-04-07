using MediatR;
using My_Movie.IRepository;

namespace My_Movie.Application.Features.User.Commands.CreateRandomIDCommand;

public class CreateRandomIDCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateRandomIDCommand>
{
    public async Task Handle(CreateRandomIDCommand request, CancellationToken cancellationToken)
    {
        await userRepository.InsertShuffleIDAsync();

    }
}

