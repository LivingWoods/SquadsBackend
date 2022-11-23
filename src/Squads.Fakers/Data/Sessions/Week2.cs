using Squads.Domain.Sessions;
using Squads.Domain.Users;

namespace Squads.Fakers.Data.Sessions;

public class Week2
{
    private List<Session> _sessions = new();

    public IReadOnlyList<Session> Sessions => _sessions.AsReadOnly();

    public Week2()
    {
        // Monday
        Session mondaySession = new Session(
            new DateTime(2022, 11, 21, 19, 0, 0),
            new DateTime(2022, 11, 21, 20, 0, 0),
            SessionType.HeavyWorkoutSession,
            new User(
                "Helleni",
                "De Bruyn",
                new DateTime(2022, 05, 24),
                new Email(
                    "helleni.debruyn@icloud.com"
                ),
                new PhoneNumber(
                    "+32476490970"
                ),
                new Address(
                    "Kruiskouterstraat 22",
                    "",
                    "9420",
                    "Mere"
                ),
                "",
                "",
                190,
                true,
                true,
                true,
                true
            )
        );

        // Tuesday
        Session tuesdaySession = new Session(
            new DateTime(2022, 11, 22, 19, 0, 0),
            new DateTime(2022, 11, 22, 20, 0, 0),
            SessionType.HeavyWorkoutSession,
            new User(
                "Helleni",
                "De Bruyn",
                new DateTime(2022, 05, 24),
                new Email(
                    "helleni.debruyn@icloud.com"
                ),
                new PhoneNumber(
                    "+32476490970"
                ),
                new Address(
                    "Kruiskouterstraat 22",
                    "",
                    "9420",
                    "Mere"
                ),
                "",
                "",
                190,
                true,
                true,
                true,
                true
            )
        );

        // Wednessday
        Session wednessDaySession = new Session(
            new DateTime(2022, 11, 23, 19, 0, 0),
            new DateTime(2022, 11, 23, 20, 0, 0),
            SessionType.HeavyWorkoutSession,
            new User(
                "Helleni",
                "De Bruyn",
                new DateTime(2022, 05, 24),
                new Email(
                    "helleni.debruyn@icloud.com"
                ),
                new PhoneNumber(
                    "+32476490970"
                ),
                new Address(
                    "Kruiskouterstraat 22",
                    "",
                    "9420",
                    "Mere"
                ),
                "",
                "",
                190,
                true,
                true,
                true,
                true
            )
        );

        // Thursday
        Session thursdaySession = new Session(
            new DateTime(2022, 11, 24, 19, 0, 0),
            new DateTime(2022, 11, 24, 20, 0, 0),
            SessionType.HeavyWorkoutSession,
            new User(
                "Helleni",
                "De Bruyn",
                new DateTime(2022, 05, 24),
                new Email(
                    "helleni.debruyn@icloud.com"
                ),
                new PhoneNumber(
                    "+32476490970"
                ),
                new Address(
                    "Kruiskouterstraat 22",
                    "",
                    "9420",
                    "Mere"
                ),
                "",
                "",
                190,
                true,
                true,
                true,
                true
            )
        );

        // Friday

        // Saturday
        Session saturdaySession = new Session(
            new DateTime(2022, 11, 26, 19, 0, 0),
            new DateTime(2022, 11, 26, 20, 0, 0),
            SessionType.HeavyWorkoutSession,
            new User(
                "Helleni",
                "De Bruyn",
                new DateTime(2022, 05, 24),
                new Email(
                    "helleni.debruyn@icloud.com"
                ),
                new PhoneNumber(
                    "+32476490970"
                ),
                new Address(
                    "Kruiskouterstraat 22",
                    "",
                    "9420",
                    "Mere"
                ),
                "",
                "",
                190,
                true,
                true,
                true,
                true
            )
        );

        // Sunday
        Session sundaySession = new Session(
            new DateTime(2022, 11, 27, 19, 0, 0),
            new DateTime(2022, 11, 27, 20, 0, 0),
            SessionType.HeavyWorkoutSession,
            new User(
                "Helleni",
                "De Bruyn",
                new DateTime(2022, 05, 24),
                new Email(
                    "helleni.debruyn@icloud.com"
                ),
                new PhoneNumber(
                    "+32476490970"
                ),
                new Address(
                    "Kruiskouterstraat 22",
                    "",
                    "9420",
                    "Mere"
                ),
                "",
                "",
                190,
                true,
                true,
                true,
                true
            )
        );

        _sessions.Add(mondaySession);
        _sessions.Add(tuesdaySession);
        _sessions.Add(wednessDaySession);
        _sessions.Add(thursdaySession);
        _sessions.Add(saturdaySession);
        _sessions.Add(sundaySession);
    }
}
