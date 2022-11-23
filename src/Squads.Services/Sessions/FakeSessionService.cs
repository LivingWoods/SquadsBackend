using Squads.Domain.Sessions;
using Squads.Shared.Sessions;
using FluentDateTime;
using Squads.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Squads.Services.Sessions;

public class FakeSessionService : ISessionService
{
    private readonly SquadsDbContext _dbContext;

    public FakeSessionService(SquadsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<SessionReply.IdReply> CreateNewSession(SessionRequest.MutateRequest request)
    {
        //_sessions.Add(new Session(
        //    request.Session.startDate,
        //    request.Session.endDate,
        //    Enum.TryParse(request.Session.sessionType, out SessionType result) ? result : throw new Exception("Enum type doesnt exist"),
        //));
        throw new NotImplementedException();
    }

    public async Task DeleteSessionBySessionId(SessionRequest.IdRequest request)
    {
        Session? session = _dbContext.Sessions.FirstOrDefault(s => s.Id == request.SessionId);

        if (session is null)
        {
            // TODO: implement EntityNotFoundException
            throw new Exception("Entity not found");
        }

        _dbContext.Sessions.Remove(session);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SessionReply.DetailReply> GetNextSession(SessionRequest.IndexRequest request)
    {
        Session? session = await _dbContext.Sessions.Where(s => s.StartDate >= DateTime.UtcNow).OrderBy(s => s.StartDate).FirstOrDefaultAsync();

        if (session is null)
        {
            throw new Exception("Entity not found");
        }

        return new SessionReply.DetailReply
        {
            Session = new SessionDto.Detail
            {
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                SessionType = session.SessionType.ToString()
            }
        };
    }

    public Task<SessionReply.DetailReply> GetSessionBySessionId(SessionRequest.IdRequest request)
    {
        Session? session = _dbContext.Sessions.Where(s => s.StartDate >= DateTime.UtcNow).OrderBy(s => s.StartDate).FirstOrDefault();

        if (session is null)
        {
            throw new Exception("Entity not found");
        }

        return Task.FromResult(new SessionReply.DetailReply
        {
            Session = new SessionDto.Detail
            {
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                SessionType = session.SessionType.ToString()
            }
        });
    }

    public async Task<SessionReply.IndexReply> GetSessionsFromCurrentWeek(SessionRequest.IndexRequest request)
    {
        DateTime startOfWeek = DateTime.UtcNow.BeginningOfWeek().AddDays(1);
        DateTime endOfWeek = DateTime.UtcNow.EndOfWeek().AddDays(1);

        List<Session> sessions = await _dbContext.Sessions.Where(s => s.StartDate >= startOfWeek && s.EndDate <= endOfWeek).Include(s => s.Trainer).ToListAsync();

        return new SessionReply.IndexReply
        {
            Sessions = sessions.Select(s => new SessionDto.Index
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                SessionType = s.SessionType.ToString(),
                Trainer = $"{s.Trainer?.FirstName} {s.Trainer?.LastName}",
                CanBeReserved = s.CanBeReserved,
                ReservationCanBeCanceled = s.ReservationCanBeCanceled,
                AmountOfReservations = 6 - s.AmountOfTotalReservations
            })
        };
    }

    public async Task<SessionReply.IndexReply> GetSessionsFromNextWeek(SessionRequest.IndexRequest request)
    {
        DateTime startOfWeek = DateTime.UtcNow.AddDays(7).BeginningOfWeek().AddDays(1);
        DateTime endOfWeek = DateTime.UtcNow.AddDays(7).EndOfWeek().AddDays(1);

        List<Session> sessions = await _dbContext.Sessions.Where(s => s.StartDate >= startOfWeek && s.EndDate <= endOfWeek).Include(s => s.Trainer).ToListAsync();

        return new SessionReply.IndexReply
        {
            Sessions = sessions.Select(s => new SessionDto.Index
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                SessionType = s.SessionType.ToString(),
                Trainer = $"{s.Trainer.FirstName} {s.Trainer.LastName}",
                CanBeReserved = s.CanBeReserved,
                ReservationCanBeCanceled = s.ReservationCanBeCanceled,
                AmountOfReservations = s.AmountOfTotalReservations
            })
        };
    }

    public async Task<SessionReply.IdReply> UpdateSessionBySessionId(SessionRequest.MutateRequest request)
    {
        Session? session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == request.SessionId);

        if (session is null)
        {
            throw new Exception("Entity not found");
        }

        session.ChangeDate(request.Session.StartDate, request.Session.EndDate);
        session.ChangeSessionType(Enum.TryParse(request.Session.SessionType, out SessionType result) ? result : throw new Exception("Enum type doesnt exist"));
        // TODO: implement ChangeTrainer
        //session.ChangeTrainer(request.Session.Trainer);

        await _dbContext.SaveChangesAsync();

        return new SessionReply.IdReply
        {
            SessionId = session.Id
        };
    }
}
