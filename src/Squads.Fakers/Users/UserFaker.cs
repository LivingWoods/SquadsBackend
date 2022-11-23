using Squads.Domain.Users;

namespace Squads.Fakers.Users;

public abstract class UserFaker
{
    public class TraineeFaker : EntityFaker<User>
    {
        public TraineeFaker(string locale = "nl") : base(locale)
        {
            User trainee = new(
                "Michiel",
                "Van Herreweghe",
                new DateTime(2001, 01, 16),
                new Email(
                    "michiel.vh@outlook.com"
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
                false
            );

            trainee.OrderTokens(10, true, 50);

            CustomInstantiator(f => trainee);
        }
    }
    public class TrainerFaker : EntityFaker<User>
    {
        public TrainerFaker(string locale = "nl") : base(locale)
        {
            CustomInstantiator(f => new User(
                "Helleni",
                "De Bruyn",
                new DateTime(1995, 05, 24),
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
            ));
        }
    }
}
