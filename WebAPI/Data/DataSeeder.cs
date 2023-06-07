using Bogus;
using WebAPI.Models;
using Person = WebAPI.Models.Person;

namespace WebAPI.Data;

public class DataSeeder
{
    private static readonly string[] _categories =
    {
        "Books",
        "Movies",
        "Music",
        "Games",
        "Electronics",
        "Computers",
        "Home",
        "Garden",
        "Tools",
        "Grocery",
        "Health",
        "Beauty",
        "Toys",
        "Kids",
        "Baby",
        "Clothing",
        "Shoes",
        "Jewelery",
        "Sports",
        "Outdoors",
        "Automotive",
        "Industrial"
    };
    
    public readonly IReadOnlyCollection<Person> Persons;
    public readonly IReadOnlyCollection<Interest> Interests;
    public readonly IReadOnlyCollection<Link> Links;

    public DataSeeder()
    {
        Persons = GeneratePersons();
        Interests = GenerateInterests(Persons);
        Links = GenerateLinks(Interests);
    }

    private static IReadOnlyCollection<Person> GeneratePersons(int amount = 10)
    {
        var personFaker = new Faker<Person>().Rules(
            (faker, person) =>
            {
                person.Id = faker.IndexFaker;
                person.Name = faker.Person.FullName;
                person.PhoneNumber = faker.Person.Phone;
            }
        );

        return Enumerable.Range(1, amount).Select(i => SeedRow(personFaker, i)).ToList();
    }

    private static IReadOnlyCollection<Interest> GenerateInterests(
        IEnumerable<Person> persons,
        int amountPerPerson = 5
    )
    {
        var totalInterests = 0;
        List<Interest> generatedInterests = new();

        var interestFaker = new Faker<Interest>().Rules(
            (
                (faker, interest) =>
                {
                    interest.Id = faker.IndexFaker;
                    interest.Title = faker.PickRandom(_categories);
                    interest.Description = faker.WaffleTitle();
                }
            )
        );

        foreach (var person in persons)
        {
            for (var i = 1; i <= new Randomizer(totalInterests++).Number(amountPerPerson); i++)
            {
                var interest = interestFaker.UseSeed(totalInterests).Generate();
                interest.PersonId = person.Id;
                generatedInterests.Add(interest);
            }
        }

        return generatedInterests;
    }

    private static IReadOnlyCollection<Link> GenerateLinks(
        IEnumerable<Interest> interests,
        int amountPerInterest = 3
    )
    {
        var totalLinks = 0;
        List<Link> generatedLinks = new();

        var linkFaker = new Faker<Link>().Rules(
            (faker, link) =>
            {
                link.Id = faker.IndexFaker;
                link.Title = faker.Hacker.Noun();
                link.LinkURL = new Uri(faker.Internet.Url());
            }
        );

        foreach (var interest in interests)
        {
            for (var i = 1; i <= new Randomizer(totalLinks++).Number(amountPerInterest); i++)
            {
                var link = linkFaker.UseSeed(totalLinks).Generate();
                link.InterestId = interest.Id;
                link.PersonId = interest.PersonId;
                generatedLinks.Add(link);
            }
        }

        return generatedLinks;
    }

    private static T SeedRow<T>(Faker<T> faker, int rowId)
        where T : class
    {
        var recordRow = faker.UseSeed(rowId).Generate();
        return recordRow;
    }
}
