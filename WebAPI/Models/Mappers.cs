using Riok.Mapperly.Abstractions;

namespace WebAPI.Models;

[Mapper]
public partial class PersonMapper
{
    public partial PersonDto PersonToPersonDto(Person person);

    public partial PersonWithNavigationDto PersonToPersonWithNavigationDto(Person person);

    public partial PersonWithInterestsDto PersonToPersonWithInterestsDto(Person person);

    public partial Person CreatePersonDtoToPerson(CreatePersonDto dto);
}

[Mapper]
public partial class InterestMapper
{
    public partial InterestDto InterestToInterestDto(Interest interest);

    public partial InterestWithNavigationDto InterestToInterestWithNavigationDto(Interest interest);

    public partial InterestWithLinksDto InterestToInterestWithLinksDto(Interest interest);

    public partial Interest CreateInterestDtoToInterest(CreateInterestDto dto);
}

[Mapper]
public partial class LinkMapper
{
    public partial LinkDto LinkToLinkDto(Link link);

    public partial LinkWithNavigationDto LinkToLinkWithNavigationDto(Link link);

    public partial Link CreateLinkDtoToLink(CreateLinkDto dto);
}
