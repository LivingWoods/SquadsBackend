using Squads.Domain.Reservations;
using Squads.Shared.Reservations;
using Squads.Domain.Sessions;
using Squads.Shared.Users;
using Squads.Domain.Users;
using Squads.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Squads.Services.Users;

public class FakeUserService : IUserService
{
    private readonly SquadsDbContext _dbContext;

    public FakeUserService(SquadsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CancelReservationByReservationId(UserRequest.ReservationRequest request)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user is null)
        {
            // TODO: implement EntityNotFoundException
            throw new Exception("Entity not found");
        }

        Reservation? reservation = await _dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == request.ReservationId);

        if (reservation is null)
        {
            // TODO: implement EntityNotFoundException
            throw new Exception("Entity not found");
        }

        if (reservation.Session.ReservationCanBeCanceled)
        {
            reservation.CancelReservation();
        }

        await _dbContext.SaveChangesAsync();
    }

    public Task<UserReply.IdReply> CreateUser(UserRequest.MutateRequest request)
    {
        //_users.Add(new User(
        //    request.User.FirstName,
        //    request.User.LastName,
        //    request.User.BirthDate,
        //    new Email(request.User.Email),
        //    new PhoneNumber(request.User.PhoneNumber),
        //    new Address(
        //        request.User.Address.AddressLine1,
        //        request.User.Address.AddressLine2,
        //        request.User.Address.ZipCode,
        //        request.User.Address.City
        //    ),
        //    request.User.PhysicalIssues,
        //    request.User.DrugsUsed,
        //    request.User.Length,
        //    request.User.OptedInOnNewsletter,
        //    request.User.OptedInOnWhatsapp,
        //    request.User.HasSignedHouseRules,
        //    request.User.IsTrainer
        //));

        //return Task.FromResult(new UserReply.IdReply
        //{
        //    UserId = 
        //})

        throw new NotImplementedException();
    }

    public async Task DeleteUserByUserId(UserRequest.IdRequest request)
    {
        User? user = _dbContext.Users.FirstOrDefault(u => u.Id == request.UserId);

        if (user is null)
        {
            // TODO: implement EntityNotFoundException
            throw new Exception("Entity not found");
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserReply.IndexReply> GetAllUsers(UserRequest.IndexRequest request)
    {
        return new UserReply.IndexReply
        {
            Users = await _dbContext.Users.Select(u => new UserDto.Index
            {
                FirstName = u.FirstName,
                LastName = u.LastName
            }).ToListAsync()
        };
    }

    public async Task<UserReply.DetailReply> GetUserByUserId(UserRequest.IdRequest request)
    {
        var query = await _dbContext.Users.Include(u => u.Reservations).ThenInclude(r => r.Session).AsQueryable().ToListAsync();
        User? user = query.FirstOrDefault(u => u.Id == request.UserId);

        if (user is null)
        {
            // TODO: implement EntityNotFoundException
            throw new Exception("Entity not found");
        }

        return new UserReply.DetailReply
        {
            User = new UserDto.Detail
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email.Value,
                PhoneNumber = user.PhoneNumber.Value,
                Address = new AddressDto.Index
                {
                    AddressLine1 = user.Address.AddressLine1,
                    AddressLine2 = user.Address.AddressLine2,
                    ZipCode = user.Address.ZipCode,
                    City = user.Address.City
                },
                HasSubscription = user.HasActiveSubscription,
                PhysicalIssues = user.PhysicalIssues,
                DrugsUsed = user.DrugsUsed,
                Length = user.Length,
                OptedInOnNewsletter = user.OptedInOnNewsletter,
                OptedInOnWhatsapp = user.OptedInOnWhatsapp,
                HasSignedHouseRules = user.HasSignedHouseRules,
                IsTrainer = user.IsTrainer,
                AmountOfPlannedReservation = user.AmountOfPlannedReservations,
                Reservations = user.PlannedReservations.Select(pr => new ReservationDto.Index
                {
                    ReservationId = pr.Id,
                    SessionId = pr.SessionId,
                    UserId = pr.UserId
                })
            }
        };
    }

    public async Task<UserReply.IdReply> ReserveSession(UserRequest.ReservationRequest request)
    {
        var query = _dbContext.Users.Include(u => u.Reservations).ThenInclude(r => r.Session).Include(u => u.Tokens).AsQueryable();
        User? user = await query.FirstOrDefaultAsync(u => u.Id == request.UserId);
        Session? session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == request.SessionId);

        if (user is null || session is null)
        {
            // TODO: implement EntityNotFoundException
            throw new Exception("Entity not found");
        }

        user.ReserveSession(session);
        
        await _dbContext.SaveChangesAsync();

        return new UserReply.IdReply
        {
            UserId = user.Id
        };
    }

    public Task<UserReply.IdReply> UpdateUserByUserId(UserRequest.MutateRequest request)
    {
        throw new NotImplementedException();
    }
}
