using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Domain.Entities;


namespace LibraryManagement.Application.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookResponse>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "No Category"))

                .AfterMap((src, dest) =>
                {
                    if (dest.Borrows == null || !dest.Borrows.Any())
                    {
                        dest.Borrows = new List<BorrowResponse> { new BorrowResponse { Status = "Not borrowed" } };
                    }
                    if (dest.Reviews == null || !dest.Reviews.Any())
                    {
                        dest.Reviews!.Add(new ReviewResponse
                        {
                            Comment = "Not reviewed yet",
                        });
                    }
                });

            CreateMap<CreateBookRequest, Book>();

            CreateMap<UpdateBookMetadataRequest, Book>()
                 .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}