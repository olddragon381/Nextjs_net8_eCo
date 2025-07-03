using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Setting;
using BookstoreApp.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Runtime;

public class RecommendationService : IRecommendationSystem
{
    private readonly HttpClient _httpClient;
    private readonly FastApiSettings _fastApiSettings;
    private readonly IUnitOfWork _unitOfWork;

    public RecommendationService(IUnitOfWork unitOfWork, HttpClient httpClient, IOptions<FastApiSettings> options)
    {
        _unitOfWork = unitOfWork;
        _httpClient = httpClient;
        _fastApiSettings = options.Value;
    }


    public async Task<List<BookDTO>> GetRecommendationsAsync(string bookId, int topN = 5)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                throw new ArgumentException("Book ID cannot be null or empty.", nameof(bookId));
            }

            var url = $"{_fastApiSettings.BaseUrl}/recommend/combined/{bookId}?top_n={topN}";
            var response = await _httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RecommendationResponse>();
                if (result?.Recommendations?.Any() == true)
                {
                    var bookIds = result.Recommendations
    .Select(r => r.BookId)
    .Where(id => !string.IsNullOrWhiteSpace(id))
    .ToList();

                    var books = new List<Book>();
                    foreach (var id in bookIds)
                    {
                        if (id == null) continue; // Ensure id is not null
                        var book = await _unitOfWork.Book.GetByIdAsync(id);
                        if (book != null)
                        {
                            books.Add(book);
                        }
                    }
                    return books.Select(b => new BookDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Image = b.Image,
                        Authors = b.Authors,
                        Description = b.Description,
                        Rating = b.Rating,
                        Status = b.Status,
                        RatingCount = b.RatingCount,
                        ReviewCount = b.ReviewCount,
                        NumPages = b.NumPages,
                        Price = b.Price,
                        Genres = b.Genres,
                        CreatedAt = b.CreatedAt
                    }).ToList();
                }
            }
            return await GetSameGenreBooks(bookId, topN);
        }
        catch
        {

 
        }
        return await GetSameGenreBooks(bookId, topN);


    }



    private async Task<List<BookDTO>> GetSameGenreBooks(string bookId, int topN)
    {
        var data = await _unitOfWork.Book.SameBookByGenreAsync(bookId, topN); // Await the Task to get the result
        return data.Select(b => new BookDTO
        {
            Id = b.Id,
            Title = b.Title,
            Image = b.Image,
            Authors = b.Authors,
            Description = b.Description,
            Rating = b.Rating,
            Status = b.Status,
            RatingCount = b.RatingCount,
            ReviewCount = b.ReviewCount,
            NumPages = b.NumPages,
            Price = b.Price,
            Genres = b.Genres,
            CreatedAt = b.CreatedAt
        }).ToList();

    }
}
