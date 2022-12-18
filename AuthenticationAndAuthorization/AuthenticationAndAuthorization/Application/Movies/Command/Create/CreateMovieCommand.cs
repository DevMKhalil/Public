using AuthenticationAndAuthorization.Application.User.Query.GetCurrentUser;
using AuthenticationAndAuthorization.Logic;
using CSharpFunctionalExtensions;
using MediatR;

namespace AuthenticationAndAuthorization.Application.Movies.Command.Create
{
    public class CreateMovieCommand : IRequest<Result<Movie>>
    {
        public CreateMovieDTO MovieDTO { get; set; }
    }

    public class CreateMovieCommandHandeler : IRequestHandler<CreateMovieCommand, Result<Movie>>
    {
        readonly IAuthenticationAndAuthorizationContext _context;
        public CreateMovieCommandHandeler(IAuthenticationAndAuthorizationContext context)
        {
            _context = context;
        }
        public async Task<Result<Movie>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            Movie movie = new Movie 
            { 
                Title = request.MovieDTO.Title,
                Description = request.MovieDTO.Description,
                Rating = request.MovieDTO.Rating,
            };

            _context.Movies.Add(movie);

            var res = await _context.SaveChangesWithValidation();

            if (res.IsFailure)
                return Result.Failure<Movie>(res.Error);

            return Result.Success(movie);
        }
    }
}
