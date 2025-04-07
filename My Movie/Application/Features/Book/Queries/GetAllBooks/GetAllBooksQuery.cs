﻿using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.Features.Book.Queries.GetAllBooks
{
    public record GetAllBooksQuery : IRequest<ApiResponse<IEnumerable<BookResponse>>>;

}
