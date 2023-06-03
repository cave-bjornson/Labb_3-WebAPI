﻿using Riok.Mapperly.Abstractions;

namespace WebAPI.Models;

[Mapper]
public partial class PersonMapper
{
    public partial PersonDto PersonToPersonDto(Person person);
}

[Mapper]
public partial class InterestMapper
{
    public partial InterestDto InterestToInterestDto(Interest interest);
}

[Mapper]
public partial class LinkMapper
{
    public partial LinkDto LinkToLinkDto(Link link);
}
