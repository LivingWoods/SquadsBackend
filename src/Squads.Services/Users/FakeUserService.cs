using Squads.Domain.Reservations;
using Squads.Shared.Reservations;
using Squads.Domain.Sessions;
using Squads.Shared.Users;
using Squads.Domain.Users;
using Squads.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentDateTime;

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

    public async Task<UserReply.PlannedReservations> GetPlannedReservations(UserRequest.IdRequest request)
    {
        var plannedReservations = await _dbContext.Reservations.Where(x => x.UserId == request.UserId)
                               .Where(x => x.Session.StartDate.Date >= DateTime.UtcNow.Date)
                               .Select(x => new ReservationDto.Index
                               {
                                   ReservationId = x.Id,
                                   SessionId = x.SessionId,
                               }).ToListAsync();

        return new UserReply.PlannedReservations
        {
            Reservations = plannedReservations
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

    public async Task<UserReply.WeekOverview> GetWeekOverview(UserRequest.WeekOverview request)
    {
        var begginingOfWeek = request.StartDate.FirstDayOfWeek();
        var endOfWeek = request.StartDate.EndOfWeek();



        var sessions = await _dbContext.Sessions
                                          .Include(x => x.Trainer)
                                          .Include(x => x.Reservations)
                                          .ThenInclude(x => x.User).ThenInclude(x => x.Tokens)
                                          .Where(x => x.StartDate.Date >= begginingOfWeek
                                                   && x.EndDate.Date <= endOfWeek)
                                          .AsNoTracking()
                                          .ToListAsync();

        User user = await _dbContext.Users
                            .Include(x => x.Tokens)
                            .Include(x => x.Reservations.Where(x => x.Session.StartDate >= DateTime.UtcNow.Date))
                                .ThenInclude(x => x.Session)
                            .AsNoTracking()
                            .SingleAsync(x => x.Id == request.UserId);

        var reply = new UserReply.WeekOverview();

        foreach (var session in sessions)
        {
            reply.WeekItems.Add(new UserDto.WeekItem
            {
                AmountOfReservations = session.AmountOfTotalReservations,
                Instructor = session.Trainer.FirstName,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                Type = session.SessionType.ToString(),
                CanCancel = user.CanCancelSession(session),
                CanSignUp = user.CanReserveSession(session),
                SessionId = session.Id,
            });
        }

        return reply;
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
