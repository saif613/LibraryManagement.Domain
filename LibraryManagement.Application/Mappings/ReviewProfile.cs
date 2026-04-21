using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Mappings
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponse>();

            CreateMap<ReviewRequest, Review>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

           CreateMap<UpdateReviewRequest, Review>()    
    .ForMember(dest => dest.UserId, opt => opt.Ignore())   
    .ForMember(dest => dest.BookId, opt => opt.Ignore());   
        }
    }
}
