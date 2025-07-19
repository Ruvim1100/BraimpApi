using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Users.Command.UpsertExternalUser;
public class UpsertExternalUserCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpsertExternalUserCommand>
{
    public async Task Handle(UpsertExternalUserCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == request.Id,
            cancellationToken);

        if (user is null)
        {
            user = new User
            {
                Id = request.Id,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname,
                GivenName = request.GivenName,
                Country = request.Country,
            };

            dbContext.Users.Add(user);
        }

        else
        {
            user.Email = request.Email;
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.GivenName = request.GivenName;
            user.Country = request.Country;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
