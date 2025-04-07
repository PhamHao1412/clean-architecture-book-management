using Carter;
using MediatR;
using My_Movie.Application.BookFeatures.Commands;
using My_Movie.Application.BookFeatures.Queries;
using My_Movie.Application.Features.Book.Commands.POST.ImportBookFormApiThirdParty;
using My_Movie.Application.Features.Book.Commands.POST.ImportBookFormSoftwium;
using My_Movie.Application.Features.Book.Queries.GetAllBooks;
using My_Movie.Application.Features.Book.Queries.GetBookByID;
using My_Movie.Application.Features.Book.Queries.GetBooksFromApiThirdParty;
using My_Movie.Presentation.Authencation;
using Polly;
namespace My_Movie.Presentation.Routes
{
    public class BookRoutes : ICarterModule
    { 
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet(
                "/books/GetBooksApiThirdParty", 
                async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetBooksFormApiThirdPartyQuery());
                return Results.Ok(response);
            });
            app.MapPost(
                "/books/ImportBookFormApiThirdParty", 
                async (ImportBookFormApiThirdPartyCommand command,  IMediator mediator) =>
                {
                    var response = await mediator.Send(command);
                    return Results.Ok(response);
                });
            app.MapGet(
                "/books/GetAll",
                async (IMediator mediator) =>
                {
                    var response = await mediator.Send(new GetAllBooksQuery());
                    return Results.Ok(response);
                }).AddEndpointFilter<ApiKeyEndpointFilter>();
            app.MapGet(
                "/books/GetAllCache",
                async (IMediator mediator) =>
                {
                    var response = await mediator.Send(new GetAllBooksQueryCache());
                    return Results.Ok(response);
                }).AddEndpointFilter<ApiKeyEndpointFilter>();
            app.MapGet(
                "/books/GetById", 
                async (int id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetBookByIdQuery(id));
                return Results.Ok(response);
            }).RequireRateLimiting("fixed");
            app.MapGet(
                "/books/SearchByTitle",
                async (string? searchTerm, string? sortColumn,
                    string? sortOrder, int page, int pageSize, IMediator mediator) =>
                {
                    var response =
                        await mediator.Send(new GetBooksQuery(searchTerm, sortColumn, sortOrder, page, pageSize));
                    return Results.Ok(response);
                }).RequireRateLimiting("fixed");

            app.MapGet(
                "/books/GetFavouriteBooks", 
                async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetFavouriteBooksQuery());
                return Results.Ok(response);
            });
            
            app.MapPost(
                "/books/AddFavouriteBook", 
                async (CreateFavouriteBookCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            });
            
            app.MapDelete(
                "/books/DelFavouriteBook",
                async (int id, IMediator mediator) =>
                {
                    var response = await mediator.Send(new DeleteFavouriteBookCommand(id));
                    return Results.Ok(response);
                });
        }
    }
}