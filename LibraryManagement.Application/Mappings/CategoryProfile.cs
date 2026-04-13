using AutoMapper;
using LibraryManagement.Application.DTOs.Requests;
using LibraryManagement.Application.DTOs.Responses;
using LibraryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.NumberOfBooks, opt => opt.MapFrom(src => src.Books.Count));
        }
    }
}
