using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

namespace MovieShop.UnitTests
{
    [TestClass]
    public class MovieServiceUnitTest
    {
//tripe a (three steps):
//Arrange: Initializes objects(movie service), creates mocks with arguments that are passed to the method under test and adds expectations.
//Act: Invokes the method or property under test with the arranged parameters.
//Assert: Verifies that the action of the method under test behaves as expected.
        private MovieService _sut;//system under test,testing movie service
        private Mock<IMovieRepository> _mockMovieRepository;
        private Mock<ICastRepository> _mockCastRepository;
        private Mock<IAsyncRepository<MovieGenre>> _mockGenreRepository;
        private Mock<IAsyncRepository<Favorite>> _mockFavoriteRepository;
        private Mock<IPurchaseRepository> _mockPurchaseRepository;
        private Mock<IMapper> _mockMapper;
        
       



        private List<Movie> _fakemovies;
        [TestInitialize]
        public void Initialize()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockCastRepository = new Mock<ICastRepository>();
            _mockGenreRepository = new Mock<IAsyncRepository<MovieGenre>>();
            _mockFavoriteRepository = new Mock<IAsyncRepository<Favorite>>();
            _mockPurchaseRepository = new Mock<IPurchaseRepository>();
            _mockMapper = new Mock<IMapper>();
            
            
           
            _fakemovies = new List<Movie>
                      {
                          new Movie {Id = 1, Title = "Avengers: Infinity War",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 2, Title = "Avatar",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 3, Title = "Star Wars: The Force Awakens",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now,  Budget = 1200000},
                          new Movie {Id = 4, Title = "Titanic",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 5, Title = "Inception",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 6, Title = "Avengers: Age of Ultron",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 7, Title = "Interstellar", PosterUrl ="asdfghj", ReleaseDate = DateTime.Now,  Budget = 1200000},
                          new Movie {Id = 8, Title = "Fight Club", PosterUrl ="asdfghj", ReleaseDate = DateTime.Now,  Budget = 1200000},
                          new Movie
                          {
                              Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000
                          },
                          new Movie {Id = 10, Title = "The Dark Knight",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 11, Title = "The Hunger Games",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 12, Title = "Django Unchained",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie
                          {
                              Id = 13, Title = "The Lord of the Rings: The Return of the King",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000
                          },
                          new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", PosterUrl ="asdfghj", ReleaseDate = DateTime.Now,  Budget = 1200000},
                          new Movie {Id = 15, Title = "Iron Man",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000},
                          new Movie {Id = 16, Title = "Furious 7",  PosterUrl ="asdfghj", ReleaseDate = DateTime.Now, Budget = 1200000}
                      };
            _mockMovieRepository.Setup(m => m.GetHighestGrossingMovies()).ReturnsAsync(_fakemovies);
            //go to ImovieReposittor to get GetHighestGrossingMovie, every time call it, return fake movies
            _mockMovieRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => _fakemovies.First(m => m.Id == id)); 
            //expect any integer int& return single movie in fakemovie list by id
        }
        [TestMethod]
        public async Task Checking_HighestGrossing_Movies_from_FakeDate() //name should be and english sentence ez undersdanding
        {
            //arranging
            _sut = new MovieService(_mockMovieRepository.Object,
                                _mockMapper.Object,
                                _mockGenreRepository.Object,
                                _mockPurchaseRepository.Object,
                                 _mockFavoriteRepository.Object);//create instance
            //act
            var fakemovies = await _sut.GetHighestGrossingMovies();

            //asserting
            Assert.IsNotNull(fakemovies);
            //Assert.AreEqual(16, fakemovies.Result.Count());
        }
    }
    //public class FakeMovieRepository : IMovieRepository //replac the real movierepository
    //{
    //    public Task<Movie> AddAsync(Movie entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task DeleteAsync(Movie entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<Movie> GetByIdAsync(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> GetCountAsync(Expression<Func<Movie, bool>> filter = null)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> GetExistsAsync(Expression<Func<Movie, bool>> filter = null)
    //    {
    //        throw new NotImplementedException();
    //    }

//        public async Task<IEnumerable<Movie>> GetHighestGrossingMovies()
//        {
//            //return a list of movies(fake)
//            //create some fake movies
//            //avoid touch DB
//            var movies = new List<Movie>
//                      {
//                          new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
//                          new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
//                          new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
//                          new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
//                          new Movie {Id = 5, Title = "Inception", Budget = 1200000},
//                          new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
//                          new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
//                          new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
//                          new Movie
//                          {
//                              Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring", Budget = 1200000
//                          },
//                          new Movie {Id = 10, Title = "The Dark Knight", Budget = 1200000},
//                          new Movie {Id = 11, Title = "The Hunger Games", Budget = 1200000},
//                          new Movie {Id = 12, Title = "Django Unchained", Budget = 1200000},
//                          new Movie
//                          {
//                              Id = 13, Title = "The Lord of the Rings: The Return of the King", Budget = 1200000
//                          },
//                          new Movie {Id = 14, Title = "Harry Potter and the Philosopher's Stone", Budget = 1200000},
//                          new Movie {Id = 15, Title = "Iron Man", Budget = 1200000},
//                          new Movie {Id = 16, Title = "Furious 7", Budget = 1200000}
//                      };
//            return movies;


//        }

//        public Task<IEnumerable<Review>> GetMovieReviews(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<PaginatedList<Movie>> GetPagedData(int pageIndex, int pageSize, Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderedQuery = null, Expression<Func<Movie, bool>> filter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<Movie>> GetTopRatedMovies()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<Movie>> ListAllAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<Movie>> ListAllWithIncludesAsync(Expression<Func<Movie, bool>> where, params Expression<Func<Movie, object>>[] includes)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<Movie>> ListAsync(Expression<Func<Movie, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<Movie> UpdateAsync(Movie entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
}

