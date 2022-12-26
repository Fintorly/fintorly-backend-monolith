using Fintorly.Domain.Entities;
using Fintorly.Infrastructure.Context;

namespace Fintorly.Infrastructure.Repositories;

public class ProfilePictureRepository:GenericRepository<ProfilePicture>,IProfilePictureRepository
{
    public ProfilePictureRepository(FintorlyContext context):base(context)
    {
    }
}