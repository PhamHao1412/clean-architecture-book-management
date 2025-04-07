namespace My_Movie.Presentation.Authencation;

public class RequireApiKeyAttributes: Attribute
{
}
public static class ApiKeyExtension
{
    public static TBuilder RequireApiKey<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(new RequireApiKeyAttributes());
        });
        return builder;
    }
}
