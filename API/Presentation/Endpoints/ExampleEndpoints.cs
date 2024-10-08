using Carter;

namespace Presentation.Endpoints;

public class ExampleEndpoints : ICarterModule {
    public void AddRoutes(IEndpointRouteBuilder builder) {
        builder.MapGet("/example", () => "Hello world!");
    }
}